// -----------------------------------------------------------------------
// <copyright file="SaveBetsBusinessRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Repositories.Implement
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.AccountingReports.SqLiteDatabase;
    using Fanex.Common;
    using Fanex.Data;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Enums;
    using SaveBetsBusiness.Model;
    using SaveBetsBusiness.Repositories.Interface;
    using SaveBetsBusiness.Resources;
    using SaveBetsBusiness.Common;

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


        public DataTable GetTotalBet(GetTotalBetCriteria criteria)
        {
            DataTable dataTable = new DataTable();
            try
            {
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


        public DataTable GetBetTikets(GetBetTicketsCriteria criteria)
        {
            DataTable dataTable = new DataTable();
            try
            {
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
    }
}