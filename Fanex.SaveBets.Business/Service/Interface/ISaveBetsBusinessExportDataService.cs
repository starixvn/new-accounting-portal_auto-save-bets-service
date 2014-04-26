// -----------------------------------------------------------------------
// <copyright file="ISaveBetsBusinessExportDataService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Service.Interface
{
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Model;

    public interface ISaveBetsBusinessExportDataService
    {
        string ExportReportToExcel(SaveBetsBusinessInfo report, GetSaveBetsBusinessCriteria criteria);
    }
}