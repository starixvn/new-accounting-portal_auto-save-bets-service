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
    using Fanex.Common;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
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
            HSSFWorkbook excelFile,
            IDataReader reader,
            int bettype,
            bool liveIndicator)
        {
            string sheetName = string.Format(
            "{0}-{1}",
            liveIndicator ? SaveBetsBusinessLabel.Live : SaveBetsBusinessLabel.Dead,
            bettype);
            ISheet worksheet = excelFile.GetSheet(sheetName) ?? excelFile.CreateSheet(sheetName);

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
        private void ExportHeaderByBetType(HSSFWorkbook excelFile, ISheet worksheet, int bettype)
        {
            ICellStyle textStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);
            int currentRow = 0, currentColumn = 0;
            IRow row = worksheet.GetRow(currentRow) ?? worksheet.CreateRow(currentRow);
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
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    break;

                case BetTypeConstants._1x2:
                case BetTypeConstants._1st1x2:
                    this.InitThreeSectionColumnWidth(worksheet);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                    this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
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
        private void ExportHeaderGroup(HSSFWorkbook excelFile, ISheet worksheet, ref int currentRow, ref int currentColumn, int bettype)
        {
            ICellStyle headerStyle = NpoiExcelExtensions.HeaderStyle(excelFile);
            ICellStyle textStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);
            int startMergeColumn = 0;
            IRow row = worksheet.GetRow(currentRow) ?? worksheet.CreateRow(currentRow);

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Account.ToUpper(), headerStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Odds.ToUpper(), headerStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.BetId.ToUpper(), headerStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Cur.ToUpper(), headerStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Amount.ToUpper(), headerStyle);
            bool isNotShowTeamAndHdpColumn = this.IsNotShowTeamAndHdpColumnByBettype(bettype);
            bool isShowHdpColumn = this.IsShowHdpColumnByBettype(bettype);
            bool isNotShowScoreColumn = this.IsNotShowScoreColumnByBetType(bettype);

            if (isShowHdpColumn)
            {
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp.ToUpper(), headerStyle);
            }
            else if (!isNotShowTeamAndHdpColumn)
            {
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Team.ToUpper(), headerStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp1.ToUpper(), headerStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp2.ToUpper(), headerStyle);
            }

            if (!isNotShowScoreColumn)
            {
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
                startMergeColumn = currentColumn - 2;
                NpoiExcelExtensions.RenderMergeCell(
                    ref worksheet,
                    ref row,
                    currentRow,
                    ref startMergeColumn,
                    currentRow,
                    currentColumn - 1,
                    SaveBetsBusinessLabel.Score.ToUpper(),
                    headerStyle);
            }

            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Trans_Time.ToUpper(), headerStyle);
            NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Creator.ToUpper(), headerStyle);
        }

        /// <summary>
        /// Exports the content of the bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="bettype">The bet type.</param>
        private void ExportBetTypeContent(HSSFWorkbook excelFile, ISheet worksheet, IDataReader reader, ref int currentRow, int bettype)
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
        private void ExportThreeSectionContentByBetType(HSSFWorkbook excelFile, ISheet worksheet, IDataReader reader, ref int currentRow)
        {
            ICellStyle textStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Center);
            ICellStyle boldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);
            ICellStyle redBoldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center, HSSFColor.Red.Index);
            ICellStyle decimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, false);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            ICellStyle blueBoldDecimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, true, HSSFColor.Blue.Index);
            blueBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            int currentBetTwoRowIndex = 1, currentBetXRowIndex = 1, currentColumn = 0;
            IRow row = null;

            while (reader.Read())
            {
                string amount = string.Format(
                    "{0}/{1}/{2}",
                    Math.Round(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2),
                    Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                    Math.Round(Converter.ToDecimal(reader["stake"]), 2));

                if (Converter.ToString2(reader["betteam"]).ToLower() == "1")
                {
                    row = worksheet.CreateRow(currentRow);

                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentRow++;
                }
                else if (Converter.ToString2(reader["betteam"]).ToLower() == "2")
                {
                    row = worksheet.GetRow(currentBetTwoRowIndex) ?? worksheet.CreateRow(currentBetTwoRowIndex);
                    currentColumn = 9;
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NpoiExcelExtensions.RenderCell(
                        ref worksheet,
                        ref row,
                        currentBetTwoRowIndex,
                        ref currentColumn,
                        Converter.ToDecimal(reader["odds"]),
                        blueBoldDecimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, amount, redBoldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentBetTwoRowIndex++;
                }
                else if (Converter.ToString2(reader["betteam"]).ToLower() == "x")
                {
                    row = worksheet.GetRow(currentBetXRowIndex) ?? worksheet.CreateRow(currentBetXRowIndex);
                    currentColumn = 18;
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, amount, redBoldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
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
        private void ExportTwoSectionContentByBetType(HSSFWorkbook excelFile, ISheet worksheet, IDataReader reader, ref int currentRow)
        {
            ICellStyle textStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Center);
            ICellStyle boldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);
            ICellStyle redBoldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center, HSSFColor.Red.Index);
            ICellStyle decimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, false);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            ICellStyle blueBoldDecimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, true, HSSFColor.Blue.Index);
            blueBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            int currentBetAwayTeamRowIndex = 1, currentColumn = 0;
            IRow row = null;

            while (reader.Read())
            {
                string amount = string.Format(
                    "{0}/{1}/{2}",
                    Math.Round(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2),
                    Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                    Math.Round(Converter.ToDecimal(reader["stake"]), 2));

                if (Converter.ToString2(reader["betteam"]).ToLower() == "a")
                {
                    row = worksheet.CreateRow(currentRow);

                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["hdp1"]), decimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                    currentRow++;
                }
                else
                {
                    row = worksheet.GetRow(currentBetAwayTeamRowIndex) ?? worksheet.CreateRow(currentBetAwayTeamRowIndex);
                    currentColumn = 12;
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NpoiExcelExtensions.RenderCell(
                        ref worksheet,
                        ref row,
                        currentBetAwayTeamRowIndex,
                        ref currentColumn,
                        Converter.ToDecimal(reader["odds"]),
                        blueBoldDecimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, amount, redBoldTextStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToDecimal(reader["hdp1"]), decimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
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
        private void ExportOneSectionContentByBetType(HSSFWorkbook excelFile, ISheet worksheet, IDataReader reader, ref int currentRow, int bettype)
        {
            ICellStyle textStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, false, NpoiExcelExtensions.Alignment.Center);
            ICellStyle boldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center);
            ICellStyle redBoldTextStyle = NpoiExcelExtensions.TextStyle(excelFile, HSSFColor.White.Index, true, NpoiExcelExtensions.Alignment.Center, HSSFColor.Red.Index);
            ICellStyle decimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, false);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            ICellStyle blueBoldDecimalNumberStyle = NpoiExcelExtensions.DecimalStyle(excelFile, HSSFColor.White.Index, true, HSSFColor.Blue.Index);
            blueBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            int currentColumn = 0;
            IRow row = null;
            bool isNotShowTeamAndHdpColumn = this.IsNotShowTeamAndHdpColumnByBettype(bettype);

            while (reader.Read())
            {
                string amount = string.Format(
                    "{0}/{1}/{2}",
                    Math.Round(Converter.ToDecimal(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2)),
                    Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                    Math.Round(Converter.ToDecimal(reader["stake"]), 2));

                row = worksheet.CreateRow(currentRow);

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);

                if (!isNotShowTeamAndHdpColumn)
                {
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betteam"]), textStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["hdp1"]), decimalNumberStyle);
                    NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["hdp2"]), decimalNumberStyle);
                }

                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                NpoiExcelExtensions.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                currentRow++;
                currentColumn = 0;
            }
        }

        /// <summary>
        /// Initializes the width of the one section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="bettype">The bet type.</param>
        private void InitOneSectionColumnWidth(ISheet worksheet, int bettype)
        {
            for (int i = 0; i < 13; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NpoiExcelExtensions.FontWidth);
            }

            bool isNotShowTeamAndHdpColumn = this.IsNotShowTeamAndHdpColumnByBettype(bettype);

            worksheet.SetColumnWidth(0, 200 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(2, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(4, 250 * NpoiExcelExtensions.FontWidth);

            if (isNotShowTeamAndHdpColumn)
            {
                worksheet.SetColumnWidth(8, 250 * NpoiExcelExtensions.FontWidth);
                worksheet.SetColumnWidth(9, 170 * NpoiExcelExtensions.FontWidth);
            }
            else
            {
                worksheet.SetColumnWidth(11, 250 * NpoiExcelExtensions.FontWidth);
                worksheet.SetColumnWidth(12, 170 * NpoiExcelExtensions.FontWidth);
            }
        }

        /// <summary>
        /// Initializes the width of the two section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void InitTwoSectionColumnWidth(ISheet worksheet)
        {
            for (int i = 0; i < 23; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NpoiExcelExtensions.FontWidth);
            }

            worksheet.SetColumnWidth(0, 200 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(2, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(4, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(9, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(10, 170 * NpoiExcelExtensions.FontWidth);

            worksheet.SetColumnWidth(12, 200 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(14, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(16, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(21, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(22, 170 * NpoiExcelExtensions.FontWidth);
        }

        /// <summary>
        /// Initializes the width of the three section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void InitThreeSectionColumnWidth(ISheet worksheet)
        {
            for (int i = 0; i < 26; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NpoiExcelExtensions.FontWidth);
            }

            worksheet.SetColumnWidth(0, 200 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(2, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(4, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(6, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(7, 170 * NpoiExcelExtensions.FontWidth);

            worksheet.SetColumnWidth(9, 200 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(11, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(13, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(15, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(16, 170 * NpoiExcelExtensions.FontWidth);

            worksheet.SetColumnWidth(18, 200 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(20, 170 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(22, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(24, 250 * NpoiExcelExtensions.FontWidth);
            worksheet.SetColumnWidth(25, 170 * NpoiExcelExtensions.FontWidth);
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