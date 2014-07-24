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
    using SaveBetsBusiness.Constants;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Model;
    using SaveBetsBusiness.Repositories.Implement;
    using SaveBetsBusiness.Repositories.Interface;
    using SaveBetsBusiness.Service.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProcessSaveBets : IProcessSaveBets
    {
        private ISaveBetsBusinessRepository _repository = null;
        private int _matchId = 0;
        private List<BettypeInfo> _bettypes_tb = null;
        private List<BettypeInfo> _bettypes_bets = null;
        private bool _endMatch = false;

        public ProcessSaveBets()
        {
            _repository = new SaveBetsBusinessRepository();
            _bettypes_tb = new List<BettypeInfo>();
            _bettypes_bets = new List<BettypeInfo>();
        }

        public List<DataRow> Jobs
        {
            get
            {
                List<DataRow> jobsByMatch = new List<DataRow>();

                var jobs = _repository.GetSaveGetJobs();
                if (jobs == null || jobs.Rows.Count == 0)
                {
                    _matchId = 0;
                    return jobsByMatch;
                }

                _matchId = jobs.AsEnumerable().Select(m => Converter.To<int>(m["MatchId"])).Distinct().FirstOrDefault();
                jobsByMatch = jobs.AsEnumerable().Where(m => Converter.To<int>(m["MatchId"]) == _matchId).ToList();

                _bettypes_tb = jobsByMatch.AsEnumerable()
                    .Where(m => m["JobType"].ToString().ToLower() == KeyConstants.TB.ToLower())
                    .Select(s => new BettypeInfo()
                    {
                        LiveIndicator = Converter.To<bool>(s["LiveIndicator"]),
                        Bettype = Converter.To<int>(s["Bettype"])
                    })
                    .ToList();

                _bettypes_bets = jobsByMatch.AsEnumerable()
                    .Where(m => m["JobType"].ToString().ToLower() == KeyConstants.Bets.ToLower())
                    .Select(s => new BettypeInfo()
                    {
                        LiveIndicator = Converter.To<bool>(s["LiveIndicator"]),
                        Bettype = Converter.To<int>(s["Bettype"])
                    })
                    .ToList();

                _endMatch = jobsByMatch.AsEnumerable()
                    .Where(m => m["JobType"].ToString().ToLower() == KeyConstants.End.ToLower()).ToList().Count > 0;

                return jobsByMatch;
            }
        }

        public int MatchId
        {
            get
            {
                return _matchId;
            }
        }

        public DataRow Turnovers
        {
            get
            {
                return _repository.GetTurnover(this._matchId).AsEnumerable().FirstOrDefault();
            }
        }

        public List<DataRow> WinLosses
        {
            get
            {
                return _repository.GetWinLoss(this._matchId).AsEnumerable().ToList();
            }
        }

        public DataRow MatchInfo
        {
            get
            {
                return _repository.GetMatchInfo(this._matchId).AsEnumerable().FirstOrDefault();
            }
        }

        public bool IsEndMatch
        {
            get
            {
                return _endMatch;
            }
        }

        public List<BettypeInfo> Bettypes_TB
        {
            get { return _bettypes_tb; }
        }

        public List<BettypeInfo> Bettypes_Bets
        {
            get { return _bettypes_bets; }
        }

        public IDataReader GetTotalBets(GetTotalBetCriteria criteria)
        {
            return _repository.GetTotalBet(criteria);
        }

        public IDataReader GetBetTickets(GetBetTicketsCriteria criteria)
        {
            return _repository.GetBetTickets(criteria);
        }

        public void ExecuteEndMatchJob(ExecuteEndMatchJobCriteria criteria)
        {
            _repository.ExecuteEndMatchJob(criteria);
        }
    }
}