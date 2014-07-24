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
    using System.Drawing;
    using System.IO;
    using Fanex.Common;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Constants;
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Resources;

    /// <summary>
    /// Export summary sheet.
    /// </summary>
    public class ExportSummaryDataService : IDisposable
    {
        private const string PositiveNumberFormat = "[Red]#,##0.00;-#,##0.00;\"-\"";
        private const int SoccerSportType = 1;
        private const string BetHomeTeam = "1";
        private const string BetAwayTeam = "2";
        private Color _borderColor = NPOIExcelUtility.BorderColor;

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
            XSSFWorkbook excelFile,
            DataRow matchInfo,
            DataRow turnOverInfo,
            List<DataRow> winLossInfo)
        {
            string sheetName = SaveBetsBusinessResource.Summary_SheetName;
            XSSFSheet worksheet = excelFile.GetSheet(sheetName) as XSSFSheet ?? excelFile.CreateSheet(sheetName) as XSSFSheet;
            this.InitColumnWidth_SummarySheet(worksheet);

            int currentRow = 0;
            XSSFRow row = worksheet.CreateRow(currentRow) as XSSFRow;
            this.ExportMatchInfo(excelFile, worksheet, matchInfo);
            this.ExportTurnOverInfo(excelFile, worksheet, turnOverInfo);
            currentRow = 15;
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

        /// <summary>
        /// Initializes the column width for summary sheet.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        public void InitColumnWidth_SummarySheet(ISheet worksheet)
        {
            for (int i = 0; i < 19; i++)
            {
                worksheet.SetColumnWidth(i, 90 * NpoiExcelExtensions.FontWidth);
            }

            worksheet.SetColumnWidth(0, 100 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(1, 100 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(2, 100 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(10, 80 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(11, 80 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(12, 80 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(15, 120 * NpoiExcelExtensions.FontWidth);
        }

        /// <summary>
        /// Exports the match information.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="matchInfo">The match information.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportMatchInfo(
            XSSFWorkbook excelFile,
            XSSFSheet worksheet,
            DataRow matchInfo)
        {
            int currentRow = 1, currentColumn = 0, startMergeColumn = 0;
            XSSFRow row = null;
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Left, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            XSSFCellStyle boldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Left, Color.Black);
            NPOIExcelUtility.SetBorderToCell(boldTextStyle);
            XSSFCellStyle centerBoldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(centerBoldTextStyle);

            string firstGoalLastGoal = string.Empty, _1HFirstGoalLastGoal = string.Empty, _1HFirstGoal = string.Empty, _1HLastGoal = string.Empty;

            if (Converter.ToInt(matchInfo["SportType"]) == SoccerSportType)
            {
                string status = Converter.ToString2(matchInfo["betstatus"]);
                this.GetFirstLastGoalInformation(out _1HFirstGoal, out _1HLastGoal, out firstGoalLastGoal, out _1HFirstGoalLastGoal, status);
            }

            row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["sportname"]), textStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow) as XSSFRow;

            string eventDate = Converter.ToDateTime(matchInfo["eventdate"]).ToShortFormatedDateWithSlash().Replace("/", "-");
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, eventDate, textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["matchid"]), centerBoldTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["matchcode"]), textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            startMergeColumn = currentColumn - 2;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                Converter.ToString2(matchInfo["leaguename"]),
                textStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
            string match = string.Format(
                "{0}-vs-{1}",
                matchInfo["Home"],
                matchInfo["Away"]);
            startMergeColumn = currentColumn - 3;
            NPOIExcelUtility.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, match, boldTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["FT"]), textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["FG"]), textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, _1HFirstGoal, textStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["HT"]), textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["LG"]), textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, _1HLastGoal, textStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 0);
            row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(matchInfo["Goal"]), textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, firstGoalLastGoal, textStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, _1HFirstGoalLastGoal, textStyle);
        }

        /// <summary>
        /// Exports the turn over information.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="turnOverInfo">The turn over information.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportTurnOverInfo(
            XSSFWorkbook excelFile,
            XSSFSheet worksheet,
            DataRow turnOverInfo)
        {
            XSSFCellStyle headerTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(headerTextStyle, Color.Black, Color.Black, Color.Black, Color.Black);
            XSSFCellStyle centerTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(centerTextStyle);
            XSSFCellStyle decimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(decimalNumberStyle, _borderColor, _borderColor, Color.Black, _borderColor);
            XSSFCellStyle ibcTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetDoubleBorderBottomToCell(ibcTextStyle, Color.Black, Color.Black, _borderColor, Color.Black);
            XSSFCellStyle ibcDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetDoubleBorderBottomToCell(ibcDecimalNumberStyle, Color.Black, _borderColor, Color.Black, Color.Black);
            XSSFCellStyle totalDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(totalDecimalNumberStyle, _borderColor, _borderColor, Color.Black, Color.Black);

            XSSFRow row = null;
            int currentRow = 1, currentColumn = 13, startMergeColumn = 0, startMergeRow = 0;

            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerTextStyle);
            startMergeColumn = currentColumn - 2;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.ActualRate,
                headerTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["TotalA"]), totalDecimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Super, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["SuperA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Master, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["MasterA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["AgentA"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;
            var ibcTurnOver =
               Converter.ToDecimal(turnOverInfo["TotalA"]) -
               Converter.ToDecimal(turnOverInfo["SuperA"]) -
               Converter.ToDecimal(turnOverInfo["MasterA"]) -
               Converter.ToDecimal(turnOverInfo["AgentA"]);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.IBC, ibcTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, ibcTurnOver, ibcDecimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerTextStyle);
            startMergeColumn = currentColumn - 2;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.VirtualRate,
                headerTextStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["TotalV"]), totalDecimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Super, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["SuperV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Master, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["MasterV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(turnOverInfo["AgentV"]), decimalNumberStyle);
            this.IncreaseRowAndResetColumnIndex(ref currentRow, ref currentColumn, 13);
            row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            var virtualIbcTurnOver =
                Converter.ToDecimal(turnOverInfo["TotalV"]) -
                Converter.ToDecimal(turnOverInfo["SuperV"]) -
                Converter.ToDecimal(turnOverInfo["MasterV"]) -
                Converter.ToDecimal(turnOverInfo["AgentV"]);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.TurnOver, headerTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.IBC, ibcTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualIbcTurnOver, ibcDecimalNumberStyle);
            currentColumn = 13;
            startMergeRow = currentRow - 12;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                startMergeRow,
                ref currentColumn,
                currentRow,
                currentColumn,
                SaveBetsBusinessLabel.TurnOver,
                headerTextStyle);
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
        private void ExportWinLossInfo(XSSFWorkbook excelFile, XSSFSheet worksheet, List<DataRow> winLossInfo, int currentRow)
        {
            IDataFormat dataFormat = excelFile.CreateDataFormat();
            XSSFRow row = null;
            int currentColumn = 0;
            decimal actualRateWinLossTotal = 0, actualRateAgentTotal = 0, actutalRateCommTotal = 0;
            decimal virtualRateWinLossTotal = 0, virtualRateAgentTotal = 0, virtualRateCommTotal = 0;
            int totalBetCount = 0, totalRejectTicket = 0, totalVoidTicket = 0;
            decimal homeActualRateTotal = 0, awayActualRateTotal = 0;
            decimal homeVirtualRateTotal = 0, awayVirtualRateTotal = 0;

            XSSFCellStyle leftTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Left, Color.Black);
            NPOIExcelUtility.SetBorderToCell(leftTextStyle);

            XSSFCellStyle centerTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(centerTextStyle);

            XSSFCellStyle rightTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Right, Color.Black);
            NPOIExcelUtility.SetBorderToCell(rightTextStyle);

            XSSFCellStyle decimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(decimalNumberStyle);
            decimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);

            XSSFCellStyle boldDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.Yellow, true, Color.Black);
            NPOIExcelUtility.SetBoldBorderBottomToCell(boldDecimalNumberStyle, _borderColor, _borderColor, _borderColor, Color.Black);
            boldDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);

            XSSFCellStyle intNumberStyle = NPOIExcelUtility.IntStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(intNumberStyle);

            XSSFCellStyle rightBorderDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(rightBorderDecimalNumberStyle, _borderColor, _borderColor, Color.Black, _borderColor);
            rightBorderDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);

            XSSFCellStyle rightBorderIntNumberStyle = NPOIExcelUtility.IntStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(rightBorderIntNumberStyle, _borderColor, _borderColor, Color.Black, _borderColor);

            XSSFCellStyle doubleBorderBottomDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, true, Color.Black);
            doubleBorderBottomDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);
            NPOIExcelUtility.SetDoubleBorderBottomToCell(doubleBorderBottomDecimalNumberStyle, _borderColor, _borderColor, _borderColor, Color.Black);

            XSSFCellStyle topBorderTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Left, Color.Black);
            NPOIExcelUtility.SetBorderToCell(topBorderTextStyle, Color.Black, _borderColor, _borderColor, _borderColor);

            XSSFCellStyle totalDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(totalDecimalNumberStyle, Color.Black, _borderColor, _borderColor, Color.Black);
            totalDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);

            XSSFCellStyle totalIntNumberStyle = NPOIExcelUtility.IntStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(totalIntNumberStyle, Color.Black, _borderColor, _borderColor, Color.Black);

            XSSFCellStyle totalRightBorderIntNumberStyle = NPOIExcelUtility.IntStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(totalRightBorderIntNumberStyle, Color.Black, _borderColor, Color.Black, Color.Black);

            XSSFCellStyle totalRightBorderDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(totalRightBorderDecimalNumberStyle, Color.Black, _borderColor, Color.Black, Color.Black);
            totalRightBorderDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);

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
                row = worksheet.CreateRow(currentRow) as XSSFRow;
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(dataRow["typename"]), leftTextStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["bettype"]), intNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(dataRow["Live"]), centerTextStyle);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["winlosta"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["agenta"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["comma"]), rightBorderDecimalNumberStyle);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["winlostv"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["agentv"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["commv"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["FC"]), rightBorderDecimalNumberStyle);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["BetCount"]), intNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["Reject"]), intNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(dataRow["Void"]), rightBorderIntNumberStyle);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["HomeA"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["AwayA"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["DiffA"]), rightBorderDecimalNumberStyle);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["HomeV"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["AwayV"]), decimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(dataRow["DiffV"]), rightBorderDecimalNumberStyle);
                currentRow++;
                currentColumn = 0;
            }

            row = worksheet.CreateRow(currentRow) as XSSFRow;
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, topBorderTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, topBorderTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, topBorderTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actualRateWinLossTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actualRateAgentTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actutalRateCommTotal, totalRightBorderDecimalNumberStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateWinLossTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateAgentTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateCommTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, totalRightBorderDecimalNumberStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalBetCount, totalIntNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalRejectTicket, totalIntNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalVoidTicket, totalRightBorderIntNumberStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, homeActualRateTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, awayActualRateTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, totalRightBorderDecimalNumberStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, homeVirtualRateTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, awayVirtualRateTotal, totalDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, totalRightBorderDecimalNumberStyle);

            currentRow++;
            currentColumn = 4;
            row = worksheet.CreateRow(currentRow) as XSSFRow;
            decimal actualRateWinLossAndAgentTotal = Convert.ToDecimal(actualRateWinLossTotal + actualRateAgentTotal);
            decimal virtualRateWinLossAndAgentTotal = Convert.ToDecimal(virtualRateWinLossTotal + virtualRateAgentTotal);
            NPOIExcelUtility.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                actualRateWinLossAndAgentTotal,
                doubleBorderBottomDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, actutalRateCommTotal, doubleBorderBottomDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, rightTextStyle);
            NPOIExcelUtility.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                virtualRateWinLossAndAgentTotal,
                doubleBorderBottomDecimalNumberStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, virtualRateCommTotal, doubleBorderBottomDecimalNumberStyle);

            currentRow++;
            row = worksheet.CreateRow(currentRow) as XSSFRow;
            currentColumn = 4;
            NPOIExcelUtility.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(actualRateWinLossAndAgentTotal + actutalRateCommTotal),
                boldDecimalNumberStyle);
            currentColumn = 7;
            NPOIExcelUtility.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(virtualRateWinLossAndAgentTotal + virtualRateCommTotal),
                boldDecimalNumberStyle);
            currentColumn = 15;
            NPOIExcelUtility.RenderCell(
                ref worksheet,
                ref row,
                currentRow,
                ref currentColumn,
                Convert.ToDecimal(homeActualRateTotal + awayActualRateTotal),
                boldDecimalNumberStyle);
            currentColumn = 18;
            NPOIExcelUtility.RenderCell(
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
        private void ExportWinLossInfoHeader(XSSFWorkbook excelFile, XSSFSheet worksheet, ref int currentRow)
        {
            XSSFRow row = null;
            XSSFCellStyle centerBoldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(centerBoldTextStyle, Color.Black, Color.Black, Color.Black, Color.Black);

            row = worksheet.CreateRow(currentRow) as XSSFRow;
            int currentColumn = 0, startMergeColumn = 0;
            currentColumn = 3;
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.ActualRate,
                centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 4;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.VirtualRate,
                centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetCount, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetCount, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetCount, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.BetCount,
                centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.ActualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.ActualRate,
                centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.VirtualRate, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NPOIExcelUtility.RenderMergeCell(
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
            row = worksheet.CreateRow(currentRow) as XSSFRow;
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetType, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetType, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetType, centerBoldTextStyle);
            startMergeColumn = currentColumn - 3;
            NPOIExcelUtility.RenderMergeCell(
                ref worksheet,
                ref row,
                currentRow,
                ref startMergeColumn,
                currentRow,
                currentColumn - 1,
                SaveBetsBusinessLabel.BetType,
                centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.WinLost, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Comm, centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.WinLost, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Agent, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Comm, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.FC, centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Reject, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Void, centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Home, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Away, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerBoldTextStyle);

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Home, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Away, centerBoldTextStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, centerBoldTextStyle);
        }

        private void IncreaseRowAndResetColumnIndex(
            ref int currentRow,
            ref int currentColumn,
            int newCurrentColumn)
        {
            currentRow++;
            currentColumn = newCurrentColumn;
        }

        /// <summary>
        /// Gets the first last goal information.
        /// </summary>
        /// <param name="firstHalfFirstGoal">The first half first goal.</param>
        /// <param name="firstHalfLastGoal">The first half last goal.</param>
        /// <param name="firstGoalLastGoal">The first goal last goal.</param>
        /// <param name="firstHalfFirstGoalLastGoal">The first half first goal last goal.</param>
        /// <param name="status">The status.</param>
        private void GetFirstLastGoalInformation(
            out string firstHalfFirstGoal,
            out string firstHalfLastGoal,
            out string firstGoalLastGoal,
            out string firstHalfFirstGoalLastGoal,
            string status)
        {
            firstHalfFirstGoal = string.Empty;
            firstHalfLastGoal = string.Empty;
            firstGoalLastGoal = string.Empty;
            firstHalfFirstGoalLastGoal = string.Empty;

            bool statusIsNullOrEmpty = string.IsNullOrEmpty(status);

            if (statusIsNullOrEmpty)
            {
                return;
            }
            else
            {
                string[] bettypes = status.Split(new char[] { ';' });
                foreach (string bettype in bettypes)
                {
                    if (bettype.StartsWith(BetTypeConstants.FirstHalfFirstGoalLastGoal))
                    {
                        firstHalfFirstGoalLastGoal = bettype;
                        string[] bettypeNameAndValue = bettype.Split(new char[] { ':' });
                        string[] betValues = bettypeNameAndValue[1].Split(new char[] { '-' });
                        string firstHalfFirstGoalTeam = this.GetTeamFromBetValue(betValues[0]);
                        string firstHalfLastGoalTeam = this.GetTeamFromBetValue(betValues[1]);

                        firstHalfFirstGoal = string.Format("{0}:{1}", SaveBetsBusinessLabel._1HFG, firstHalfFirstGoalTeam);
                        firstHalfLastGoal = string.Format("{0}:{1}", SaveBetsBusinessLabel._1HLG, firstHalfLastGoalTeam);
                    }
                    else if (bettype.StartsWith(BetTypeConstants.FirstGoalLastGoal))
                    {
                        firstGoalLastGoal = bettype;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the team from bet value.
        /// </summary>
        /// <param name="betValue">The bet value.</param>
        /// <returns>The team name.</returns>
        private string GetTeamFromBetValue(string betValue)
        {
            string teamName = string.Empty;

            if (betValue == BetHomeTeam)
            {
                teamName = SaveBetsBusinessLabel.Home;
            }
            else if (betValue == BetAwayTeam)
            {
                teamName = SaveBetsBusinessLabel.Away;
            }
            else
            {
                teamName = string.Empty;
            }

            return teamName;
        }
    }
}