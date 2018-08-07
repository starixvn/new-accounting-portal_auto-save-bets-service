// -----------------------------------------------------------------------
// <copyright file="SaveBetsBusinessRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Repositories.Implement
{
    using System.Collections;
    using System.Data;
    using Fanex.Data;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Repositories.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SaveBetsBusinessRepository : ISaveBetsBusinessRepository
    {
        /// <summary>
        /// Gets the save get jobs.
        /// </summary>
        /// <returns>Save bet jobs.</returns>
        public DataTable GetSaveGetJobs()
        {
            DataTable dataTable = new DataTable();

            using (ObjectDb db = new ObjectDb("NAP_SaveBet_GetJob"))
            {
                Hashtable param = new Hashtable();
                param.Add("groupsports", AppSettings.GroupSports);

                if (AppSettings.ForceRunning && AppSettings.ForceRunningOnProcessing)
                {
                    param.Add("matchid", AppSettings.ForceRunningWithMatchId);
                }

                db.AssignParamValues(param);
                var dataSet = db.ExecuteDataSet();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the turnover.
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <returns>Turn over information.</returns>
        public DataTable GetTurnover(int matchId)
        {
            DataTable dataTable = new DataTable();

            GetTurnoverCriteria criteria = new GetTurnoverCriteria();
            criteria.MatchId = matchId;

            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var parameters = criteria.GetSpParams();
                db.AssignParamValues(parameters);

                var dataSet = db.ExecuteDataSet();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the win loss.
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <returns>Get win loss of match information.</returns>
        public DataTable GetWinLoss(int matchId)
        {
            DataTable dataTable = new DataTable();

            GetWinLossCriteria criteria = new GetWinLossCriteria();
            criteria.MatchId = matchId;

            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var parameters = criteria.GetSpParams();
                db.AssignParamValues(parameters);

                var dataSet = db.ExecuteDataSet();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the match information.
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        /// <returns>Match information.</returns>
        public DataTable GetMatchInfo(int matchId)
        {
            DataTable dataTable = new DataTable();
            if (matchId == 0)
            {
                return dataTable;
            }

            GetMatchInfoCriteria criteria = new GetMatchInfoCriteria();
            criteria.MatchId = matchId;

            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var parameters = criteria.GetSpParams();
                db.AssignParamValues(parameters);

                var dataSet = db.ExecuteDataSet();
                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    dataTable = dataSet.Tables[0];
                }
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the total bet.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Total bet of match.</returns>
        public DataTable GetTotalBet(GetTotalBetCriteria criteria)
        {
            DataTable totalBets = new DataTable();

            ObjectDb db = new ObjectDb(criteria.GetSettingKey());
            var parameters = criteria.GetSpParams();
            var dataSet = db.ExecuteDataSet(parameters);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                totalBets = dataSet.Tables[0];
            }

            return totalBets;
        }

        /// <summary>
        /// Gets the bet ticket.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Bet tickets of match.</returns>
        public DataTable GetBetTickets(GetBetTicketsCriteria criteria)
        {
            DataTable betTickets = new DataTable();

            ObjectDb db = new ObjectDb(criteria.GetSettingKey());
            var parameters = criteria.GetSpParams();
            var dataSet = db.ExecuteDataSet(parameters);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                betTickets = dataSet.Tables[0];
            }

            return betTickets;
        }

        /// <summary>
        /// Executes the end match job.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public void ExecuteEndMatchJob(ExecuteEndMatchJobCriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var parameters = criteria.GetSpParams();
                db.ExecuteNonQuery(parameters);
            }
        }
    }
}