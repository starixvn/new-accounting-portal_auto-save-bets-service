// -----------------------------------------------------------------------
// <copyright file="ExportSummaryDataService.cs" company="Nexcel Solutions Vietnam">
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
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Resources;

    /// <summary>
    /// Export summary sheet.
    /// </summary>
    public class ExportSummaryDataService : IDisposable
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
            ISheet worksheet = excelFile.GetSheet(sheetName) ?? excelFile.CreateSheet(sheetName);
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

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["sportname"]), textStyle);
            currentColumn = 13;
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            startMergeColumn = currentColumn - 2;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.ActualRate,
                centerBoldTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            string eventDate = Converter.ToDateTime(matchInfo["eventdate"]).ToShortFormatedDateWithSlash().Replace("/", "-");
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, eventDate, textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["matchid"]), centerBoldTextStyle);
            currentColumn = 13;
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["TotalA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["matchcode"]), textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            startMergeColumn = currentColumn - 2;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                Converter.ToString2(matchInfo["leaguename"]),
                textStyle);
            currentColumn = 13;
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Super, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["SuperA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            string match = string.Format(
                "{0}-vs-{1}",
                matchInfo["Home"],
                matchInfo["Away"]);
            startMergeColumn = currentColumn - 3;
            NpoiExcelExtensions.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, match, boldTextStyle);
            currentColumn = 13;
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Master, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["MasterA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, matchInfo["FT"], textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, matchInfo["FG"], textStyle);
            currentColumn = 13;
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["AgentA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["HT"]), textStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["LG"]), textStyle);
            currentColumn = 13;
            var ibcTurnOver =
               Converter.ToDecimal(turnOverInfo["TotalA"]) -
               Converter.ToDecimal(turnOverInfo["SuperA"]) -
               Converter.ToDecimal(turnOverInfo["MasterA"]) -
               Converter.ToDecimal(turnOverInfo["AgentA"]);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.IBC, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, ibcTurnOver, decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["Goal"]), textStyle);
            currentColumn = 13;
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            startMergeColumn = currentColumn - 2;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.VirtualRate,
                centerBoldTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["TotalV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Super, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["SuperV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Master, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["MasterV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["AgentV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.CreateRow(currentRow);

            var virtualIbcTurnOver =
                Converter.ToDecimal(turnOverInfo["TotalV"]) -
                Converter.ToDecimal(turnOverInfo["SuperV"]) -
                Converter.ToDecimal(turnOverInfo["MasterV"]) -
                Converter.ToDecimal(turnOverInfo["AgentV"]);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.IBC, centerTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualIbcTurnOver, decimalNumberStyle);
            currentColumn = 13;
            startMergeRow = currentRow - 12;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row, 
                startMergeRow,
                ref currentColumn, 
                currentRow,
                currentColumn, 
                SaveBetsBusinessLabel.TurnOver,
                centerBoldTextStyle);
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

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public void InitColumnWidth_SummarySheet(ISheet worksheet)
        {
            for (int i = 0; i < 19; i++)
            {
                worksheet.SetColumnWidth(i, 170 * NpoiExcelExtensions.FontWidth);
            }
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
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(dataRow["typename"]), leftTextStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["bettype"]), intNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(dataRow["Live"]), centerTextStyle);

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["winlosta"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["agenta"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["comma"]), decimalNumberStyle);

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["winlostv"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["agentv"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["commv"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["FC"]), decimalNumberStyle);

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["BetCount"]), intNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["Reject"]), intNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["Void"]), intNumberStyle);

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["HomeA"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["AwayA"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["DiffA"]), decimalNumberStyle);

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["HomeV"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["AwayV"]), decimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["DiffV"]), decimalNumberStyle);
                currentRow++;
                currentColumn = 0;
            }

            currentColumn = 3;
            row = worksheet.CreateRow(currentRow);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actualRateWinLossTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actualRateAgentTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actutalRateCommTotal, decimalNumberStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateWinLossTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateAgentTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateCommTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalBetCount, intNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalRejectTicket, intNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalVoidTicket, intNumberStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, homeActualRateTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, awayActualRateTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, homeVirtualRateTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, awayVirtualRateTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);

            currentRow++;
            currentColumn = 4;
            row = worksheet.CreateRow(currentRow);
            decimal actualRateWinLossAndAgentTotal = Convert.ToDecimal(actualRateWinLossTotal + actualRateAgentTotal);
            decimal virtualRateWinLossAndAgentTotal = Convert.ToDecimal(virtualRateWinLossTotal + virtualRateAgentTotal);
            NpoiExcelExtensions.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                actualRateWinLossAndAgentTotal,
                decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actutalRateCommTotal, decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);
            NpoiExcelExtensions.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                virtualRateWinLossAndAgentTotal,
                decimalNumberStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateCommTotal, decimalNumberStyle);

            currentRow++;
            row = worksheet.CreateRow(currentRow);
            currentColumn = 4;
            NpoiExcelExtensions.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(actualRateWinLossAndAgentTotal + actutalRateCommTotal),
                boldDecimalNumberStyle);
            currentColumn = 7;
            NpoiExcelExtensions.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(virtualRateWinLossAndAgentTotal + virtualRateCommTotal),
                boldDecimalNumberStyle);
            currentColumn = 15;
            NpoiExcelExtensions.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(homeActualRateTotal + awayActualRateTotal),
                boldDecimalNumberStyle);
            currentColumn = 18;
            NpoiExcelExtensions.RenderCell(
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

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.ActualRate,
                centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 4;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.VirtualRate,
                centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetCount, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetCount, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetCount, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.BetCount,
                centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.ActualRate,
                centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.VirtualRate,
                centerBoldTextStyle);

            currentRow++;
            currentColumn = 0;
            row = worksheet.CreateRow(currentRow);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetType, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetType, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetType, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NpoiExcelExtensions.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.BetType,
                centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.WinLost, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Comm, centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.WinLost, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Comm, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.FC, centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Reject, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Void, centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Home, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Away, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerBoldTextStyle);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Home, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Away, centerBoldTextStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerBoldTextStyle);
        }

        private void IncreaseRowAndResetColumnIndex(
            ref int currentRow,
            ref int currentColumn,
            int newCurrentColumn)
        {
            currentRow++;
            currentColumn = newCurrentColumn;
        }
    }
}