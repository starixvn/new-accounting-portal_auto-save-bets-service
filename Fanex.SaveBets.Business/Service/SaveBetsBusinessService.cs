// -----------------------------------------------------------------------
// <copyright file="SaveBetsBusinessService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Fanex.Common;
    using Fanex.Logging;
    using Fanex.ServiceManagement.Utility;
    using NPOI.XSSF.UserModel;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Service.Implement;
    using SaveBetsBusiness.Service.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SaveBetsBusinessService
    {
        private IProcessSaveBets _processSaveBets = null;
        private List<DataRow> _jobs = null;
        private DataRow _matchInfo = null;
        private Impersonate _impersonate;
        private NetworkCredential _networkCredential;
        private string _userName;
        private string _password;

        public SaveBetsBusinessService(string username, string password)
        {
            _processSaveBets = new ProcessSaveBets();
            _jobs = new List<DataRow>();
            this._userName = username;
            this._password = password;
            this._networkCredential = new NetworkCredential(username, password);
            this._impersonate = new Impersonate(string.Empty, this._userName, this._password);
        }

        /// <summary>
        /// Processes the save bets.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public void ProcessSaveBets()
        {
            XSSFWorkbook excelFile = null;
            int matchId = 0;
            var startTime = DateTime.Now;
            try
            {
                _jobs = _processSaveBets.Jobs;
                matchId = _processSaveBets.MatchId;

                ExecuteEndMatchJobCriteria endMatchCriteria = new ExecuteEndMatchJobCriteria();
                endMatchCriteria.MatchId = matchId;

                if (_jobs.Count == 0)
                {
                    ////Jobs are empty, so the service does nothing.
                    if (matchId != AppSettings.MatchId && matchId != 0)
                    {
                        Mailing.MatchEmptyDataEmailNotification(matchId);
                        _processSaveBets.ExecuteEndMatchJob(endMatchCriteria);
                    }

                    return;
                }

                _matchInfo = _processSaveBets.MatchInfo;
                if (_matchInfo == null)
                {
                    if (matchId != AppSettings.MatchId && matchId != 0)
                    {
                        Mailing.MatchEmptyDataEmailNotification(matchId);
                        _processSaveBets.ExecuteEndMatchJob(endMatchCriteria);
                    }

                    return;
                }

                DateTime eventDate = Converter.ToDateTime(_matchInfo["eventdate"]);

                excelFile = new XSSFWorkbook();

                ////1. SUM - Render data excel file at Summary: Top Left, Top Right and Bottom
                RenderSUMSheet(ref excelFile);

                ////2. TB - Render data excel file at sheet Dead-TB/Live-TB
                RenderDeadTB_LiveTBSheet(ref excelFile);

                ////3.Bets - Render each bettype a Sheet
                RenderEachBettypeLiveIndicatorSheet(ref excelFile);

                ////.Write Excel File

                if (AppSettings.IsAuthenticate)
                {
                    using (new NetworkConnection(AppSettings.ExportDataDirectory, this._networkCredential))
                    {
                        WriteFile(eventDate, excelFile);
                    }
                }
                else
                {
                    WriteFile(eventDate, excelFile);
                }

                _processSaveBets.ExecuteEndMatchJob(endMatchCriteria);
                
                var endTime = DateTime.Now;
                var duration = endTime.Subtract(startTime).TotalSeconds;
                Logger.Log.Info($"Save Bets Service - MatchId: {matchId}, StartTime: {startTime}, EndTime: {endTime}, Duration: {duration:##,###} seconds");
            }
            catch (Exception ex)
            {
                Logger.Log.Info($"{ex.Message}, MatchId:{matchId}");
                LogExtensions.ExceptionHandler(ex);
            }
            finally
            {
                AppSettings.MatchId = matchId;
            }
        }

        /// <summary>
        /// Renders the summary sheet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        private void RenderSUMSheet(ref XSSFWorkbook excelFile)
        {
            ExportSummaryDataService _exportSummaryService = null;
            try
            {
                var turnovers = _processSaveBets.Turnovers;
                var winlosses = _processSaveBets.WinLosses;
                //// your code should be put here
                _exportSummaryService = new ExportSummaryDataService();
                _exportSummaryService.ExportSummarySheet(excelFile, _matchInfo, turnovers, winlosses);
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
            finally
            {
                _exportSummaryService.Dispose();
            }
        }

        /// <summary>
        /// Renders the dead total bet and live total bet sheet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        private void RenderDeadTB_LiveTBSheet(ref XSSFWorkbook excelFile)
        {
            ExportDeadAndLiveTotalBetService _exportDeadAndLiveTotalBetService = null;            
            int currentRow = 0;
            GetTotalBetCriteria criteria = new GetTotalBetCriteria();
            var bettypes = _processSaveBets.Bettypes_TB;
            var liveIndicators = bettypes.Select(b => b.LiveIndicator).Distinct().ToList();

            try
            {
                _exportDeadAndLiveTotalBetService = new ExportDeadAndLiveTotalBetService();

                foreach (var liveIndicator in liveIndicators)
                {
                    string sheetName = liveIndicator ? "Live-TB" : "Dead-TB";
                    XSSFSheet worksheet = excelFile.GetSheet(sheetName) as XSSFSheet ?? excelFile.CreateSheet(sheetName) as XSSFSheet;
                    _exportDeadAndLiveTotalBetService.InitColumnWidth(worksheet);
                    currentRow = 0;
                    var bettypesByLiveIndicator = bettypes.Where(s => s.LiveIndicator == liveIndicator).OrderBy(b => b.Bettype);

                    foreach (var bettype in bettypesByLiveIndicator)
                    {
                        criteria.MatchId = _processSaveBets.MatchId;
                        criteria.LiveIndicator = bettype.LiveIndicator;
                        criteria.Bettype = bettype.Bettype;

                        var totalBets = _processSaveBets.GetTotalBets(criteria);
                        ////Render Dead-TB/Live-TB Sheet depend on the value of liveIndicator = true or false
                        _exportDeadAndLiveTotalBetService.ExportDeadAndLiveTBSheet(excelFile, ref worksheet, ref currentRow, totalBets, criteria.Bettype);
                    }
                }
             
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
            finally
            {
                _exportDeadAndLiveTotalBetService.Dispose();
            }
        }

        /// <summary>
        /// Renders the each bet type live indicator sheet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        private void RenderEachBettypeLiveIndicatorSheet(ref XSSFWorkbook excelFile)
        {
            ExportDataByBetTypeService _exportDataByBetTypeService = null;            
            GetBetTicketsCriteria criteria = new GetBetTicketsCriteria();

            try
            {
                _exportDataByBetTypeService = new ExportDataByBetTypeService();
                var bettypes = _processSaveBets.Bettypes_Bets;
                var liveIndicators = bettypes.Select(b => b.LiveIndicator).Distinct().ToList();

                foreach (var liveIndicator in liveIndicators)
                {
                    var bettypesByLiveIndicator = bettypes.Where(s => s.LiveIndicator == liveIndicator)
                        .Select(bettype => new { bettype.Bettype, bettype.LiveIndicator }).Distinct().OrderBy(b => b.Bettype);

                    foreach (var bettype in bettypesByLiveIndicator)
                    {
                        criteria.MatchId = _processSaveBets.MatchId;
                        criteria.LiveIndicator = bettype.LiveIndicator;
                        criteria.Bettype = bettype.Bettype;

                        var betTickets = _processSaveBets.GetBetTickets(criteria);
                        ////Render each bettype a Sheet depend on the value of liveIndicator = true or false
                        _exportDataByBetTypeService.ExportDataByBetTypeAndLiveIndicator(excelFile, betTickets, criteria.Bettype, criteria.LiveIndicator);
                    }
                }               
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
            finally
            {
                _exportDataByBetTypeService.Dispose();
            }
        }

        private void WriteFile(DateTime eventDate, XSSFWorkbook excelFile)
        {
            string reportDirectory = DirectoryExtensions.CreateSaveBetReportDataDirectory(eventDate);
            string reportName = string.Format(
                "{0}_{1}_{2}.{3}",
                _matchInfo["matchid"],
                eventDate.ToShortFormatedDateWithSlash().Replace("/", "_"),
                _matchInfo["SportType"],
                "xlsx");

            string reportPath = Path.Combine(reportDirectory, reportName);

            var fileStream = new FileStream(
                reportPath,
                FileMode.Create,
                FileAccess.Write);

            excelFile.Write(fileStream);

            if (fileStream != null)
            {
                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}