// -----------------------------------------------------------------------
// <copyright file="ProcessSaveBets.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Service.Implement
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Common;
    using SaveBetsBusiness.Repositories.Implement;
    using SaveBetsBusiness.Repositories.Interface;
    using SaveBetsBusiness.Service.Interface;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Constants;
    using SaveBetsBusiness.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProcessSaveBets : IProcessSaveBets
    {
        ISaveBetsBusinessRepository repository = null;
        int matchId = 0;
        List<BettypeInfo> bettypes_tb = null;
        List<BettypeInfo> bettypes_bets = null;

        public ProcessSaveBets()
        {
            repository = new SaveBetsBusinessRepository();
            bettypes_tb = new List<BettypeInfo>();
            bettypes_bets = new List<BettypeInfo>(); 
        }

        public List<DataRow> Jobs
        {
            get
            {
                List<DataRow> jobsByMatch = new List<DataRow>();

                var jobs = repository.GetSaveGetJobs();
                if (jobs == null) 
                {
                    return jobsByMatch;
                }

                matchId = jobs.AsEnumerable().Select(m => Converter.To<int>(m["MatchId"])).Distinct().FirstOrDefault();
                matchId = 8527049;//for testing
                jobsByMatch = jobs.AsEnumerable().Where(m => Converter.To<int>(m["MatchId"]) == matchId).ToList();

                bettypes_tb = jobsByMatch.AsEnumerable()
                    .Where(m => m["SaveType"].ToString().ToLower() == KeyConstants.TB.ToLower())
                    .Select(s => new BettypeInfo()
                    {
                        LiveIndicator = Converter.To<bool>(s["LiveIndicator"]),
                        Bettype = Converter.To<int>(s["Bettype"])
                    })
                    .ToList();

                bettypes_bets = jobsByMatch.AsEnumerable()
                    .Where(m => m["SaveType"].ToString().ToLower() == KeyConstants.Bets.ToLower())
                    .Select(s => new BettypeInfo()
                    {
                        LiveIndicator = Converter.To<bool>(s["LiveIndicator"]),
                        Bettype = Converter.To<int>(s["Bettype"])
                    })
                    .ToList();
                

                return jobsByMatch;
            }
        }

        public int MatchId
        {
            get
            {
                return matchId;
            }            
        }


        public List<DataRow> Turnovers
        {
            get 
            {
                return repository.GetTurnover(this.matchId).AsEnumerable().ToList();
            }
        }


        public List<DataRow> WinLosses
        {
            get
            {
                return repository.GetWinLoss(this.matchId).AsEnumerable().ToList();
            }
        }


        public DataRow MatchInfo
        {
            get 
            {
                return repository.GetMatchInfo(this.matchId).AsEnumerable().FirstOrDefault();
            }
        }


        public List<DataRow> GetTotalBets(GetTotalBetCriteria criteria)
        {
            return repository.GetTotalBet(criteria).AsEnumerable().ToList();
        }

        public List<DataRow> GetBetTickets(GetBetTicketsCriteria criteria)
        {
            return repository.GetBetTikets(criteria).AsEnumerable().ToList();
        }


        public List<BettypeInfo> Bettypes_TB
        {
            get { return bettypes_tb; }
        }


        public List<BettypeInfo> Bettypes_Bets
        {
            get { return bettypes_bets ; }
        }


        
    }
}
