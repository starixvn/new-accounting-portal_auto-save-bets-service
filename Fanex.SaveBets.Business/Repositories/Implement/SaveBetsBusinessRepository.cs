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

        public DataTable GetTurnover(int MatchId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                GetTurnoverCriteria criteria = new GetTurnoverCriteria();
                criteria.MatchId = MatchId;

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

        public DataTable GetWinLoss(int MatchId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                GetWinLossCriteria criteria = new GetWinLossCriteria();
                criteria.MatchId = MatchId;

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

        public DataTable GetMatchInfo(int MatchId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                GetMatchInfoCriteria criteria = new GetMatchInfoCriteria();
                criteria.MatchId = MatchId;

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

        public IDataReader GetBetTikets(GetBetTicketsCriteria criteria)
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