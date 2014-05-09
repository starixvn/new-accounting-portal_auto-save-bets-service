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
    using System.Data;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Enums;
    using SaveBetsBusiness.Model;
    using SaveBetsBusiness.Repositories.Implement;
    using SaveBetsBusiness.Repositories.Interface;
    using SaveBetsBusiness.Service.Implement;
    using SaveBetsBusiness.Service.Interface;
    using System.Collections.Generic;
    using System.Linq;
    using NPOI.HSSF.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SaveBetsBusinessService
    {
        private IProcessSaveBets processSaveBets = null;
        private ISaveBetsBusinessExportDataService _saveBetsBusinessExportDataService = null;
        private List<DataRow> jobs = null;

        public SaveBetsBusinessService()
        {
            processSaveBets = new ProcessSaveBets();
            _saveBetsBusinessExportDataService = new SaveBetsBusinessExportDataService();
            jobs = new List<DataRow>();
        }

        private void RenderSUMSheet(ref HSSFWorkbook excelFile)
        {
            var matchInfo = processSaveBets.MatchInfo;
            var turnovers = processSaveBets.Turnovers;
            var winlosses = processSaveBets.WinLosses;
            //// your code should be put here
        }

        private void RenderDeadTB_LiveTBSheet(ref HSSFWorkbook excelFile)
        {
            GetTotalBetCriteria criteria = new GetTotalBetCriteria();
            var bettypes = processSaveBets.Bettypes_TB;
            var liveIndicators = bettypes.Select(b => b.LiveIndicator).Distinct().ToList();

            foreach (var liveIndicator in liveIndicators)
            {
                var bettypesByLiveIndicator = bettypes.Where(s => s.LiveIndicator == liveIndicator);

                foreach (var bettype in bettypesByLiveIndicator)
                {
                    criteria.MatchId = processSaveBets.MatchId;
                    criteria.LiveIndicator = bettype.LiveIndicator;
                    criteria.Bettype = bettype.Bettype;

                    var rows = processSaveBets.GetTotalBets(criteria);
                    //Render Dead-TB/Live-TB Sheet depend on the value of liveIndicator = true or false
                }
            }
        }

        private void RenderEachBettypeLiveIndicatorSheet(ref HSSFWorkbook excelFile)
        {
            GetBetTicketsCriteria criteria = new GetBetTicketsCriteria();
            var bettypes = processSaveBets.Bettypes_Bets;
            var liveIndicators = bettypes.Select(b => b.LiveIndicator).Distinct().ToList();

            foreach (var liveIndicator in liveIndicators)
            {
                var bettypesByLiveIndicator = bettypes.Where(s => s.LiveIndicator == liveIndicator);

                foreach (var bettype in bettypesByLiveIndicator)
                {
                    criteria.MatchId = processSaveBets.MatchId;
                    criteria.LiveIndicator = bettype.LiveIndicator;
                    criteria.Bettype = bettype.Bettype;

                    var rows = processSaveBets.GetBetTickets(criteria);
                    //Render each bettype a Sheet depend on the value of liveIndicator = true or false
                }
            }
        }

        public void ProcessSaveBets()
        {
            try
            {
                jobs = processSaveBets.Jobs;
                var matchId = processSaveBets.MatchId;

                if (jobs.Count == 0 || matchId == 0)
                {
                    ////Jobs are empty, so the service does nothing.
                    return;
                }

                HSSFWorkbook excelFile = new HSSFWorkbook(); 

                ////1. SUM - Render data excel file at Summary: Top Left, Top Right and Bottom                
                RenderSUMSheet(ref excelFile);               

                ////2. TB - Render data excel file at sheet Dead-TB/Live-TB 
                RenderDeadTB_LiveTBSheet(ref excelFile);

                ////3.Bets - Render each bettype a Sheet
                RenderEachBettypeLiveIndicatorSheet(ref excelFile);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}