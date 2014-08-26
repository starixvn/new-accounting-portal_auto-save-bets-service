// -----------------------------------------------------------------------
// <copyright file="ExportDataByBetTypeService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Service.Implement
{
    using System;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using Fanex.Common;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Constants;
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Resources;

    /// <summary>
    /// Export data by bet type service.
    /// </summary>
    public class ExportDataByBetTypeService : IDisposable
    {
        /// <summary>
        /// Exports the data by bet type and live indicator.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="bettype">The bet type.</param>
        /// <param name="liveIndicator">If set to <c>true</c> [live indicator].</param>
        public void ExportDataByBetTypeAndLiveIndicator(
            XSSFWorkbook excelFile,
            IDataReader reader,
            int bettype,
            bool liveIndicator)
        {
            string sheetName = string.Format(
            "{0}-{1}",
            liveIndicator ? SaveBetsBusinessLabel.Live : SaveBetsBusinessLabel.Dead,
            bettype);
            XSSFSheet worksheet = excelFile.GetSheet(sheetName) as XSSFSheet ?? excelFile.CreateSheet(sheetName) as XSSFSheet;

            int currentRow = 0;
            this.ExportHeaderByBetType(excelFile, worksheet, bettype);
            currentRow++;
            this.ExportBetTypeContent(excelFile, worksheet, reader, ref currentRow, bettype);

            worksheet.CreateFreezePane(0, 1, 0, 1);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        /// <summary>
        /// Exports the header by bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="bettype">The bet type.</param>
        private void ExportHeaderByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, int bettype)
        {
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            int currentRow = 0, currentColumn = 0;
            XSSFRow row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;
            switch (bettype)
            {
                case BetTypeConstants.Handicap:
                case BetTypeConstants._1stHandicap:
                case BetTypeConstants.OddEven:
                case BetTypeConstants._1stOddEven:
                case BetTypeConstants.OverUnder:
                case BetTypeConstants._1stOverUnder:
                case BetTypeConstants.MoneyLine:
                    this.InitTwoSectionColumnWidth(worksheet);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    break;

                case BetTypeConstants._1x2:
                case BetTypeConstants._1st1x2:
                    this.InitThreeSectionColumnWidth(worksheet);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    break;

                default:
                    this.InitOneSectionColumnWidth(worksheet, bettype);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    break;
            }
        }

        /// <summary>
        /// Exports the header group.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="currentColumn">The current column.</param>
        /// <param name="bettype">The bet type.</param>
        private void ExportHeaderGroup(XSSFWorkbook excelFile, XSSFSheet worksheet, ref int currentRow, ref int currentColumn, int bettype)
        {
            XSSFCellStyle headerStyle = NPOIExcelUtility.HeaderStyle(excelFile);
            NPOIExcelUtility.SetBorderToCell(headerStyle);
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            int startMergeColumn = 0;
            XSSFRow row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Account.ToUpper(), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Odds.ToUpper(), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetId.ToUpper(), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Cur.ToUpper(), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Amount.ToUpper(), headerStyle);
            bool isNotShowTeamAndHdpColumn = this.IsNotShowTeamAndHdpColumnByBettype(bettype);
            bool isShowHdpColumn = this.IsShowHdpColumnByBettype(bettype);
            bool isNotShowScoreColumn = this.IsNotShowScoreColumnByBetType(bettype);

            if (isShowHdpColumn)
            {
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp.ToUpper(), headerStyle);
            }
            else if (!isNotShowTeamAndHdpColumn)
            {
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Team.ToUpper(), headerStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp1.ToUpper(), headerStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp2.ToUpper(), headerStyle);
            }

            if (!isNotShowScoreColumn)
            {
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
                startMergeColumn = currentColumn - 2;
                NPOIExcelUtility.RenderMergeCell(
                    ref worksheet,
                    ref row,
                    currentRow,
                    ref startMergeColumn,
                    currentRow,
                    currentColumn - 1,
                    SaveBetsBusinessLabel.Score.ToUpper(),
                    headerStyle);
            }

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Trans_Time.ToUpper(), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Creator.ToUpper(), headerStyle);
        }

        /// <summary>
        /// Exports the content of the bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="bettype">The bet type.</param>
        private void ExportBetTypeContent(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow, int bettype)
        {
            switch (bettype)
            {
                case BetTypeConstants.Handicap:
                case BetTypeConstants._1stHandicap:
                case BetTypeConstants.OddEven:
                case BetTypeConstants._1stOddEven:
                case BetTypeConstants.OverUnder:
                case BetTypeConstants._1stOverUnder:
                case BetTypeConstants.MoneyLine:
                    this.ExportTwoSectionContentByBetType(excelFile, worksheet, reader, ref currentRow);
                    break;

                case BetTypeConstants._1x2:
                case BetTypeConstants._1st1x2:
                    this.ExportThreeSectionContentByBetType(excelFile, worksheet, reader, ref currentRow);
                    break;

                default:
                    this.ExportOneSectionContentByBetType(excelFile, worksheet, reader, ref currentRow, bettype);
                    break;
            }
        }

        /// <summary>
        /// Exports the three section content by bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="currentRow">The current row.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportThreeSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow)
        {
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            XSSFCellStyle boldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(boldTextStyle);
            XSSFCellStyle redBoldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Red);
            NPOIExcelUtility.SetBorderToCell(redBoldTextStyle);
            XSSFCellStyle decimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(decimalNumberStyle);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            XSSFCellStyle blueBoldDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, true, Color.Blue);
            NPOIExcelUtility.SetBorderToCell(blueBoldDecimalNumberStyle);
            blueBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            int currentBetTwoRowIndex = 1, currentBetXRowIndex = 1, currentColumn = 0;
            XSSFRow row = null;

            while (reader.Read())
            {
                string amount = string.Format(
                    "{0}/{1}/{2}",
                    Math.Round(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2),
                    Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                    Math.Round(Converter.ToDecimal(reader["stake"]), 2));

                if (Converter.ToString2(reader["betteam"]).ToLower() == "1")
                {
                    row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentRow++;
                }
                else if (Converter.ToString2(reader["betteam"]).ToLower() == "x")
                {
                    row = worksheet.GetRow(currentBetTwoRowIndex) as XSSFRow ?? worksheet.CreateRow(currentBetTwoRowIndex) as XSSFRow;
                    currentColumn = 9;
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NPOIExcelUtility.RenderCell(
                        ref worksheet,
                        ref row,
                        currentBetTwoRowIndex,
                        ref currentColumn,
                        Converter.ToDecimal(reader["odds"]),
                        blueBoldDecimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, amount, redBoldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentBetTwoRowIndex++;
                }
                else if (Converter.ToString2(reader["betteam"]).ToLower() == "2")
                {
                    row = worksheet.GetRow(currentBetXRowIndex) as XSSFRow ?? worksheet.CreateRow(currentBetXRowIndex) as XSSFRow;
                    currentColumn = 18;
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, amount, redBoldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentBetXRowIndex++;
                }

                currentColumn = 0;
            }
        }

        /// <summary>
        /// Exports the two section content by bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="currentRow">The current row.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportTwoSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow)
        {
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            XSSFCellStyle boldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(boldTextStyle);
            XSSFCellStyle redBoldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Red);
            NPOIExcelUtility.SetBorderToCell(redBoldTextStyle);
            XSSFCellStyle decimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(decimalNumberStyle);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            XSSFCellStyle blueBoldDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, true, Color.Blue);
            NPOIExcelUtility.SetBorderToCell(blueBoldDecimalNumberStyle);
            blueBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            int currentBetAwayTeamRowIndex = 1, currentColumn = 0;
            XSSFRow row = null;

            while (reader.Read())
            {
                string amount = string.Format(
                    "{0}/{1}/{2}",
                    Math.Round(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2),
                    Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                    Math.Round(Converter.ToDecimal(reader["stake"]), 2));

                if (Converter.ToString2(reader["betteam"]).ToLower() == "h")
                {
                    row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["hdp1"]), decimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentRow++;
                }
                else
                {
                    row = worksheet.GetRow(currentBetAwayTeamRowIndex) as XSSFRow ?? worksheet.CreateRow(currentBetAwayTeamRowIndex) as XSSFRow;
                    currentColumn = 12;
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NPOIExcelUtility.RenderCell(
                        ref worksheet,
                        ref row,
                        currentBetAwayTeamRowIndex,
                        ref currentColumn,
                        Converter.ToDecimal(reader["odds"]),
                        blueBoldDecimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, amount, redBoldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToDecimal(reader["hdp1"]), decimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentBetAwayTeamRowIndex++;
                }

                currentColumn = 0;
            }
        }

        /// <summary>
        /// Exports the one section content by bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="bettype">The bet type.</param>
        private void ExportOneSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow, int bettype)
        {
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            XSSFCellStyle boldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.White);
            NPOIExcelUtility.SetBorderToCell(boldTextStyle);
            XSSFCellStyle redBoldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Red);
            NPOIExcelUtility.SetBorderToCell(redBoldTextStyle);
            XSSFCellStyle decimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(decimalNumberStyle);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            XSSFCellStyle blueBoldDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, true, Color.Blue);
            NPOIExcelUtility.SetBorderToCell(blueBoldDecimalNumberStyle);
            blueBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            int currentColumn = 0;
            XSSFRow row = null;
            bool isNotShowTeamAndHdpColumn = this.IsNotShowTeamAndHdpColumnByBettype(bettype);

            while (reader.Read())
            {
                string amount = string.Format(
                    "{0}/{1}/{2}",
                    Math.Round(Converter.ToDecimal(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2)),
                    Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                    Math.Round(Converter.ToDecimal(reader["stake"]), 2));

                row = worksheet.CreateRow(currentRow) as XSSFRow;

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);

                if (!isNotShowTeamAndHdpColumn)
                {
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betteam"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["hdp1"]), decimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["hdp2"]), decimalNumberStyle);
                }

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                currentRow++;
                currentColumn = 0;
            }
        }

        /// <summary>
        /// Initializes the width of the one section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="bettype">The bet type.</param>
        private void InitOneSectionColumnWidth(XSSFSheet worksheet, int bettype)
        {
            for (int i = 0; i < 13; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NPOIExcelUtility.FontWidth);
            }

            bool isNotShowTeamAndHdpColumn = this.IsNotShowTeamAndHdpColumnByBettype(bettype);

            worksheet.SetColumnWidth(0, 170 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(2, 150 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(4, 220 * NPOIExcelUtility.FontWidth);

            if (isNotShowTeamAndHdpColumn)
            {
                worksheet.SetColumnWidth(8, 220 * NPOIExcelUtility.FontWidth);
                worksheet.SetColumnWidth(9, 150 * NPOIExcelUtility.FontWidth);
            }
            else
            {
                worksheet.SetColumnWidth(11, 220 * NPOIExcelUtility.FontWidth);
                worksheet.SetColumnWidth(12, 150 * NPOIExcelUtility.FontWidth);
            }
        }

        /// <summary>
        /// Initializes the width of the two section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void InitTwoSectionColumnWidth(XSSFSheet worksheet)
        {
            for (int i = 0; i < 23; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NpoiExcelExtensions.FontWidth);
            }

            worksheet.SetColumnWidth(0, 170 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(2, 150 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(4, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(9, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(10, 150 * NPOIExcelUtility.FontWidth);

            worksheet.SetColumnWidth(12, 170 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(14, 150 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(16, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(21, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(22, 150 * NPOIExcelUtility.FontWidth);
        }

        /// <summary>
        /// Initializes the width of the three section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void InitThreeSectionColumnWidth(XSSFSheet worksheet)
        {
            for (int i = 0; i < 26; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NpoiExcelExtensions.FontWidth);
            }

            worksheet.SetColumnWidth(0, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(2, 150 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(4, 220 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(6, 220 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(7, 150 * NpoiExcelExtensions.FontWidth);

            worksheet.SetColumnWidth(9, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(11, 150 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(13, 220 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(15, 220 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(16, 150 * NpoiExcelExtensions.FontWidth);

            worksheet.SetColumnWidth(18, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(20, 150 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(22, 220 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(24, 220 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(25, 150 * NpoiExcelExtensions.FontWidth);
        }

        /// <summary>
        /// Determines whether [is show HDP column by bet type] [the specified bet type].
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>To be show HDP column or not.</returns>
        private bool IsShowHdpColumnByBettype(int bettype)
        {
            bool isShow = false;

            if (bettype == BetTypeConstants._1stHandicap ||
                bettype == BetTypeConstants.Handicap ||
                bettype == BetTypeConstants._1stOddEven ||
                bettype == BetTypeConstants.OddEven ||
                bettype == BetTypeConstants._1stOverUnder ||
                bettype == BetTypeConstants.OverUnder ||
                bettype == BetTypeConstants.MoneyLine)
            {
                isShow = true;
            }

            return isShow;
        }

        /// <summary>
        /// Determines whether [is not show team and HDP column by bet type] [the specified bet type].
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>To be show team and HDP column or not.</returns>
        private bool IsNotShowTeamAndHdpColumnByBettype(int bettype)
        {
            bool isNotShow = false;

            if (bettype == BetTypeConstants._1st1x2 ||
                bettype == BetTypeConstants._1x2 ||
                bettype == BetTypeConstants.CleanSheet ||
                bettype == BetTypeConstants.DoubleChange ||
                bettype == BetTypeConstants.BothOneNeitherTeamToScore)
            {
                isNotShow = true;
            }

            return isNotShow;
        }

        /// <summary>
        /// Determines whether [is not show score column by bet type] [the specified bet type].
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>To be show score column or not.</returns>
        private bool IsNotShowScoreColumnByBetType(int bettype)
        {
            bool isNotShow = false;

            if (bettype == BetTypeConstants._1st1x2 ||
                bettype == BetTypeConstants._1x2)
            {
                isNotShow = true;
            }

            return isNotShow;
        }
    }
}