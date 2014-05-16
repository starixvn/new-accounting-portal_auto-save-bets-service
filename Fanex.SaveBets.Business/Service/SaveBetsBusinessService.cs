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
    using Fanex.Common;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
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

        public SaveBetsBusinessService()
        {
            _processSaveBets = new ProcessSaveBets();
            _jobs = new List<DataRow>();
        }

        /// <summary>
        /// Processes the save bets.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public void ProcessSaveBets()
        {
            FileStream fileStream = null;
            HSSFWorkbook excelFile = null;
            bool isEndMatch = false;
            int matchId = 0;
            try
            {
                _jobs = _processSaveBets.Jobs;
                matchId = _processSaveBets.MatchId;
                var matchInfo = _processSaveBets.MatchInfo;
                DateTime eventDate = Converter.ToDateTime(matchInfo["eventdate"]);

                if (_jobs.Count == 0 || matchId == 0)
                {
                    ////Jobs are empty, so the service does nothing.
                    return;
                }

                excelFile = new HSSFWorkbook();

                ////1. SUM - Render data excel file at Summary: Top Left, Top Right and Bottom
                RenderSUMSheet(ref excelFile);

                ////2. TB - Render data excel file at sheet Dead-TB/Live-TB
                RenderDeadTB_LiveTBSheet(ref excelFile);

                ////3.Bets - Render each bettype a Sheet
                RenderEachBettypeLiveIndicatorSheet(ref excelFile);

                string reportDirectory = DirectoryExtensions.CreateSaveBetReportDataDirectory(eventDate);
                string reportName = string.Format(
                    "{0}_{1}_{2}.{3}",
                    matchInfo.ItemArray[3],
                    Converter.ToDateTime(matchInfo.ItemArray[2]).ToShortFormatedDateWithSlash().Replace("/", "_"),
                    matchInfo.ItemArray[1],
                    "xls");

                string reportPath = Path.Combine(reportDirectory, reportName);

                fileStream = new FileStream(
                    reportPath,
                    FileMode.Create,
                    FileAccess.Write);

                excelFile.Write(fileStream);
                isEndMatch = _processSaveBets.IsEndMatch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (isEndMatch)
                {
                    ExecuteEndMatchJobCriteria criteria = new ExecuteEndMatchJobCriteria();
                    criteria.MatchId = matchId;
                    _processSaveBets.ExecuteEndMatchJob(criteria);
                }

                fileStream.Close();
                fileStream.Dispose();
            }
        }

        /// <summary>
        /// Renders the summary sheet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        private void RenderSUMSheet(ref HSSFWorkbook excelFile)
        {
            ExportSummaryDataService _exportSummaryService = null;
            try
            {
                var matchInfo = _processSaveBets.MatchInfo;
                var turnovers = _processSaveBets.Turnovers;
                var winlosses = _processSaveBets.WinLosses;
                //// your code should be put here
                _exportSummaryService = new ExportSummaryDataService();
                _exportSummaryService.ExportSummarySheet(excelFile, matchInfo, turnovers, winlosses);
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void RenderDeadTB_LiveTBSheet(ref HSSFWorkbook excelFile)
        {
            ExportDeadAndLiveTotalBetService _exportDeadAndLiveTotalBetService = null;
            IDataReader reader = null;
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
                    ISheet worksheet = excelFile.GetSheet(sheetName) ?? excelFile.CreateSheet(sheetName);
                    _exportDeadAndLiveTotalBetService.InitColumnWidth(worksheet);
                    currentRow = 0;
                    var bettypesByLiveIndicator = bettypes.Where(s => s.LiveIndicator == liveIndicator);

                    foreach (var bettype in bettypesByLiveIndicator)
                    {
                        criteria.MatchId = _processSaveBets.MatchId;
                        criteria.LiveIndicator = bettype.LiveIndicator;
                        criteria.Bettype = bettype.Bettype;

                        reader = _processSaveBets.GetTotalBets(criteria);
                        ////Render Dead-TB/Live-TB Sheet depend on the value of liveIndicator = true or false
                        _exportDeadAndLiveTotalBetService.ExportDeadAndLiveTBSheet(excelFile, ref worksheet, ref currentRow, reader, criteria.Bettype);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void RenderEachBettypeLiveIndicatorSheet(ref HSSFWorkbook excelFile)
        {
            ExportDataByBetTypeService _exportDataByBetTypeService = null;
            IDataReader reader = null;
            GetBetTicketsCriteria criteria = new GetBetTicketsCriteria();

            try
            {
                _exportDataByBetTypeService = new ExportDataByBetTypeService();
                var bettypes = _processSaveBets.Bettypes_Bets;
                var liveIndicators = bettypes.Select(b => b.LiveIndicator).Distinct().ToList();

                foreach (var liveIndicator in liveIndicators)
                {
                    var bettypesByLiveIndicator = bettypes.Where(s => s.LiveIndicator == liveIndicator);

                    foreach (var bettype in bettypesByLiveIndicator)
                    {
                        criteria.MatchId = _processSaveBets.MatchId;
                        criteria.LiveIndicator = bettype.LiveIndicator;
                        criteria.Bettype = bettype.Bettype;

                        reader = _processSaveBets.GetBetTickets(criteria);
                        ////Render each bettype a Sheet depend on the value of liveIndicator = true or false
                        _exportDataByBetTypeService.ExportDataByBetTypeAndLiveIndicator(excelFile, reader, criteria.Bettype, criteria.LiveIndicator);
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _exportDataByBetTypeService.Dispose();
            }
        }
    }
}