// -----------------------------------------------------------------------
// <copyright file="ExportSaveBetDataService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Service.Implement
{
    using System.Collections.Generic;
    using System.Data;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Export save bet report data.
    /// </summary>
    public partial class ExportSaveBetDataService
    {
        public void ExportSummarySheet(
            HSSFWorkbook excelFile,
            List<DataRow> matchInfo,
            List<DataRow> turnOverInfo,
            List<DataRow> winLossInfo)
        {
            string sheetName = "Summary";
            ISheet worksheet = excelFile.CreateSheet(sheetName);
            int currentRow = 0, currentColumn = 0;

            IRow row = worksheet.CreateRow(currentRow);
        }
    }
}