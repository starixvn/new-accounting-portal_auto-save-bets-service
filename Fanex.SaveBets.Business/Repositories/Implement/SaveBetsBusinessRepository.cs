// -----------------------------------------------------------------------
// <copyright file="SaveBetsBusinessRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Repositories.Implement
{
    using System;
    using System.Data;
    using Fanex.Data;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Repositories.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SaveBetsBusinessRepository : ISaveBetsBusinessRepository
    {
        public SaveBetsBusinessRepository()
        {
        }

        /// <summary>
        /// Gets the save get jobs.
        /// </summary>
        /// <returns>Save bet jobs.</returns>
        public DataTable GetSaveGetJobs()
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (ObjectDb db = new ObjectDb("Save_GetJobs"))
                {
                    var dataSet = db.ExecuteDataSet();
                    if (dataSet != null && dataSet.Tables.Count > 0)
                    {
                        dataTable = dataSet.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
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
            try
            {
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataTable;
        }

        /// <summary>
        /// Gets the total bet.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Total bet of match.</returns>
        public IDataReader GetTotalBet(GetTotalBetCriteria criteria)
        {
            IDataReader reader = null;
            try
            {
                using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var parameters = criteria.GetSpParams();
                    db.AssignParamValues(parameters);

                    reader = db.ExecuteReader(parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reader;
        }

        /// <summary>
        /// Gets the bet ticket.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Bet tickets of match.</returns>
        public IDataReader GetBetTickets(GetBetTicketsCriteria criteria)
        {
            IDataReader reader = null;
            try
            {
                using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var parameters = criteria.GetSpParams();
                    db.AssignParamValues(parameters);

                    reader = db.ExecuteReader(parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reader;
        }
    }
}