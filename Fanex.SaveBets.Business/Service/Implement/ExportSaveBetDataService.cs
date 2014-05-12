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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Fanex.Common;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Extensions;

    /// <summary>
    /// Export save bet report data.
    /// </summary>
    public partial class ExportSaveBetDataService
    {
        private const string PositiveNumberFormat = "[Red]#,##0.00;-#,##0.00;\"-\"";

        /// <summary>
        /// Exports the summary sheet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="matchInfo">The match information.</param>
        /// <param name="turnOverInfo">The turn over information.</param>
        /// <param name="winLossInfo">The win loss information.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public void ExportSummarySheet(
            HSSFWorkbook excelFile,
            DataRow matchInfo,
            DataRow turnOverInfo,
            List<DataRow> winLossInfo)
        {
            string sheetName = "Summary";
            ISheet worksheet = excelFile.CreateSheet(sheetName);
            this.InitColumnWidth_SummarySheet(worksheet);

            ICellStyle textStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Left);
            ICellStyle centerTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Center);
            ICellStyle boldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Left);
            ICellStyle centerBoldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);
            ICellStyle decimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, false);

            int currentRow = 0, currentColumn = 0, startMergeColumn = 0, startMergeRow = 0;
            IRow row = worksheet.CreateRow(currentRow);
            currentRow++;
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["sportname"]), textStyle);
            currentColumn = 13;
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            startMergeColumn = currentColumn - 2;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Actual Rate", centerBoldTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            string eventDate = Converter.ToDateTime(matchInfo["eventdate"]).ToShortFormatedDateWithSlash().Replace("/", "-");
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, eventDate, textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["matchid"]), centerBoldTextStyle);
            currentColumn = 13;
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Total", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["TotalA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["matchcode"]), textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            startMergeColumn = currentColumn - 2;
            this.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                Converter.ToString2(matchInfo["leaguename"]),
                textStyle);
            currentColumn = 13;
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Super", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["SuperA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            string match = string.Format(
                "{0}-vs-{1}",
                matchInfo["Home"],
                matchInfo["Away"]);
            startMergeColumn = currentColumn - 3;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, match, boldTextStyle);
            currentColumn = 13;
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Master", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["MasterA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, matchInfo["FT"], textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, matchInfo["FG"], textStyle);
            currentColumn = 13;
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Agent", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["AgentA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["HT"]), textStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["LG"]), textStyle);
            currentColumn = 13;
            var ibcTurnOver =
               Converter.ToDecimal(turnOverInfo["TotalA"]) -
               Converter.ToDecimal(turnOverInfo["SuperA"]) -
               Converter.ToDecimal(turnOverInfo["MasterA"]) -
               Converter.ToDecimal(turnOverInfo["AgentA"]);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "IBC", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, ibcTurnOver, decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["Goal"]), textStyle);
            currentColumn = 13;
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            startMergeColumn = currentColumn - 2;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Virtual Rate", centerBoldTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Total", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["TotalV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Super", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["SuperV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Master", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["MasterV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Agent", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["AgentV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            var virtualIbcTurnOver =
                Converter.ToDecimal(turnOverInfo["TotalV"]) -
                Converter.ToDecimal(turnOverInfo["SuperV"]) -
                Converter.ToDecimal(turnOverInfo["MasterV"]) -
                Converter.ToDecimal(turnOverInfo["AgentV"]);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "TurnOver", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "IBC", centerTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualIbcTurnOver, decimalNumberStyle);
            currentColumn = 13;
            startMergeRow = currentRow - 12;
            this.RenderMergeCell(ref worksheet, ref row, startMergeRow, ref currentColumn, currentRow, currentColumn, "TurnOver", centerBoldTextStyle);
            currentRow += 2;

            this.ExportWinLossInfo(excelFile, worksheet, winLossInfo, currentRow);

            worksheet.CreateFreezePane(3, 0, 3, 0);
        }

        /// <summary>
        /// Creates the save bet report data directory.
        /// </summary>
        /// <param name="eventDate">The event date.</param>
        /// <returns>Create save bet report data directory.</returns>
        public string CreateSaveBetReportDataDirectory(DateTime eventDate)
        {
            string reportDataDirectory = AppSettings.ExportDataDirectory;

            DirectoryExtensions.CheckAndCreateDirectory(reportDataDirectory);

            string directoryByYearAndMonth = string.Format(
                "{0}{1}",
                eventDate.ToString("yyyy"),
                eventDate.ToString("MM"));
            string directoryByYearAndMonthPath = Path.Combine(reportDataDirectory, directoryByYearAndMonth);
            DirectoryExtensions.CheckAndCreateDirectory(directoryByYearAndMonthPath);

            return directoryByYearAndMonthPath;
        }

        /// <summary>
        /// Exports the win loss information.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="winLossInfo">The win loss information.</param>
        /// <param name="currentRow">The current row.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportWinLossInfo(HSSFWorkbook excelFile, ISheet worksheet, List<DataRow> winLossInfo, int currentRow)
        {
            IDataFormat dataFormat = excelFile.CreateDataFormat();
            IRow row = null;
            int currentColumn = 0;
            decimal actualRateWinLossTotal = 0, actualRateAgentTotal = 0, actutalRateCommTotal = 0;
            decimal virtualRateWinLossTotal = 0, virtualRateAgentTotal = 0, virtualRateCommTotal = 0;
            int totalBetCount = 0, totalRejectTicket = 0, totalVoidTicket = 0;
            decimal homeActualRateTotal = 0, awayActualRateTotal = 0;
            decimal homeVirtualRateTotal = 0, awayVirtualRateTotal = 0;

            ICellStyle leftTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Left);
            ICellStyle centerTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Center);
            ICellStyle rightTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Right);
            ICellStyle decimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, false);
            decimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);
            ICellStyle boldDecimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.Yellow.Index, true);
            boldDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);
            ICellStyle intNumberStyle = NpoiExcelExtensions.IntStyle(excelFile, HSSFColor.White.Index, false);

            this.ExportWinLossInfoHeader(excelFile, worksheet, ref currentRow);
            currentRow++;

            foreach (DataRow dataRow in winLossInfo)
            {
                //// calculate total.
                actualRateWinLossTotal += Converter.ToDecimal(dataRow["winlosta"]);
                actualRateAgentTotal += Converter.ToDecimal(dataRow["agenta"]);
                actutalRateCommTotal += Converter.ToDecimal(dataRow["comma"]);

                virtualRateWinLossTotal += Converter.ToDecimal(dataRow["winlostv"]);
                virtualRateAgentTotal += Converter.ToDecimal(dataRow["agentv"]);
                virtualRateCommTotal += Converter.ToDecimal(dataRow["commv"]);

                totalBetCount += Converter.ToInt(dataRow["BetCount"]);
                totalRejectTicket += Converter.ToInt(dataRow["Reject"]);
                totalVoidTicket += Converter.ToInt(dataRow["Void"]);

                homeActualRateTotal += Converter.ToDecimal(dataRow["HomeA"]);
                awayActualRateTotal += Converter.ToDecimal(dataRow["AwayA"]);

                homeVirtualRateTotal += Converter.ToDecimal(dataRow["HomeV"]);
                awayVirtualRateTotal += Converter.ToDecimal(dataRow["AwayV"]);

                //// export data.
                row = worksheet.CreateRow(currentRow);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(dataRow["typename"]), leftTextStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["bettype"]), intNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(dataRow["Live"]), centerTextStyle);

                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["winlosta"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["agenta"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["comma"]), decimalNumberStyle);

                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["winlostv"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["agentv"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["commv"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["FC"]), decimalNumberStyle);

                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["BetCount"]), intNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["Reject"]), intNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["Void"]), intNumberStyle);

                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["HomeA"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["AwayA"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["DiffA"]), decimalNumberStyle);

                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["HomeV"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["AwayV"]), decimalNumberStyle);
                this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["DiffV"]), decimalNumberStyle);
                currentRow++;
                currentColumn = 0;
            }

            currentColumn = 3;
            row = worksheet.CreateRow(currentRow);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actualRateWinLossTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actualRateAgentTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actutalRateCommTotal, decimalNumberStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateWinLossTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateAgentTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateCommTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalBetCount, intNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalRejectTicket, intNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalVoidTicket, intNumberStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, homeActualRateTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, awayActualRateTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, homeVirtualRateTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, awayVirtualRateTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);

            currentRow++;
            currentColumn = 4;
            row = worksheet.CreateRow(currentRow);
            decimal actualRateWinLossAndAgentTotal = Convert.ToDecimal(actualRateWinLossTotal + actualRateAgentTotal);
            decimal virtualRateWinLossAndAgentTotal = Convert.ToDecimal(virtualRateWinLossTotal + virtualRateAgentTotal);
            this.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                actualRateWinLossAndAgentTotal,
                decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actutalRateCommTotal, decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);
            this.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                virtualRateWinLossAndAgentTotal,
                decimalNumberStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateCommTotal, decimalNumberStyle);

            currentRow++;
            row = worksheet.CreateRow(currentRow);
            currentColumn = 4;
            this.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(actualRateWinLossAndAgentTotal + actutalRateCommTotal),
                boldDecimalNumberStyle);
            currentColumn = 7;
            this.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(virtualRateWinLossAndAgentTotal + virtualRateCommTotal),
                boldDecimalNumberStyle);
            currentColumn = 15;
            this.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(homeActualRateTotal + awayActualRateTotal),
                boldDecimalNumberStyle);
            currentColumn = 18;
            this.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(homeVirtualRateTotal + awayVirtualRateTotal),
                boldDecimalNumberStyle);
        }

        /// <summary>
        /// Exports the win loss information header.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="currentRow">The current row.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportWinLossInfoHeader(HSSFWorkbook excelFile, ISheet worksheet, ref int currentRow)
        {
            IRow row = null;
            ICellStyle centerBoldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);

            row = worksheet.CreateRow(currentRow);
            int currentColumn = 0, startMergeColumn = 0;

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Actual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Actual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Actual Rate", centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Actual Rate", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            startMergeColumn = currentColumn - 4;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Virtual Rate", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Bet Count", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Bet Count", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Bet Count", centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Bet Count", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Actual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Actual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Actual Rate", centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Actual Rate", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Virtual Rate", centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "Virtual Rate", centerBoldTextStyle);

            currentRow++;
            currentColumn = 0;
            row = worksheet.CreateRow(currentRow);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "BetType", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "BetType", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "BetType", centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            this.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, "BetType", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "WinLost", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Agent", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Comm", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "WinLost", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Agent", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Comm", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "FC", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Total", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Reject", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Void", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Home", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Away", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Total", centerBoldTextStyle);

            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Home", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Away", centerBoldTextStyle);
            this.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, "Total", centerBoldTextStyle);
        }

        private void InitColumnWidth_SummarySheet(ISheet worksheet)
        {
            for (int i = 0; i < 19; i++)
            {
                worksheet.SetColumnWidth(i, 170 * NpoiExcelExtensions.FontWidth);
            }
        }

        /// <summary>
        /// Renders the cell.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="row">The row of report.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="currentColumn">The current column.</param>
        /// <param name="cellValue">The cell value.</param>
        /// <param name="cellStyle">The cell style.</param>
        private void RenderCell(
            ref ISheet worksheet,
            ref IRow row,
            int currentRow,
            ref int currentColumn,
            object cellValue,
            ICellStyle cellStyle)
        {
            ICell cell = row.CreateCell(currentColumn);
            NpoiExcelExtensions.SetCellValue(cell, cellValue);
            cell.CellStyle = cellStyle;
            currentColumn += 1;
        }

        /// <summary>
        /// Renders the merge cell.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="row">The row of sheet.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="currentColumn">The current column.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endColumn">The end column.</param>
        /// <param name="cellValue">The cell value.</param>
        /// <param name="cellStyle">The cell style.</param>
        private void RenderMergeCell(
            ref ISheet worksheet,
            ref IRow row,
            int currentRow,
            ref int currentColumn,
            int endRow,
            int endColumn,
            object cellValue,
            ICellStyle cellStyle)
        {
            ICell cell = row.CreateCell(currentColumn);

            NpoiExcelExtensions.SetCellValue(cell, cellValue);

            cell.CellStyle = cellStyle;
            worksheet.AddMergedRegion(new CellRangeAddress(currentRow, endRow, currentColumn, endColumn));
            currentColumn = endColumn + 1;
        }

        private void IncreaseRowAndResetColumnIndex(
            ref int currentRow,
            ref int currentColumn,
            int newCurrentColumn)
        {
            currentRow++;
            currentColumn = newCurrentColumn;
        }

        private void RenderSpecialCell(
            ref ISheet worksheet,
            ref IRow row,
            int currentRow,
            ref int currentColumn,
            int endRow,
            int endColumn,
            object cellValue,
            ICellStyle cellStyle)
        {
            if (Converter.ToDecimal(cellValue) == 0)
            {
            }
        }
    }
}