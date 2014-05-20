// -----------------------------------------------------------------------
// <copyright file="ExportDeadAndLiveTotalBetService.cs" company="Nexcel Solutions Vietnam">
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
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Resources;

    /// <summary>
    /// Export dead and live TB sheet.
    /// </summary>
    public class ExportDeadAndLiveTotalBetService : IDisposable
    {
        private const string PositiveNumberFormat = "[Red]#,##0.00;-#,##0.00;\"-\"";

        /// <summary>
        /// Exports the dead and live tb sheet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="bettype">The bet type.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public void ExportDeadAndLiveTBSheet(
            XSSFWorkbook excelFile,
            ref XSSFSheet worksheet,
            ref int currentRow,
            IDataReader reader,
            int bettype)
        {
            IDataFormat dataFormat = excelFile.CreateDataFormat();
            XSSFRow row = null;
            int currentColumn = 0, numberBetTypeElements = 0, virtualScore = 0;
            decimal loseHomeTotal = 0, loseAwayTotal = 0, wonHomeTotal = 0, wonAwayTotal = 0;
            string hdpScoreString = string.Empty;
            bool isHaveLoseTicket = false;
            decimal oddTotalByBetType = 0, fc_p3Total = 0, fc_p2Total = 0, fc_p1Total = 0, fc_p0Total = 0, fc_m1Total = 0, fc_m2Total = 0, fc_m3Total = 0;
            decimal homeTotalByBetType = 0, awayTotalByBetType = 0;

            XSSFCellStyle headerStyle = NPOIExcelUtility.HeaderStyle(excelFile);
            NPOIExcelUtility.SetBorderToCell(headerStyle);
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            XSSFCellStyle boldTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(boldTextStyle);
            XSSFCellStyle boldDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, true, Color.Black);
            NPOIExcelUtility.SetBorderToCell(boldDecimalNumberStyle);
            boldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            boldDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);
            XSSFCellStyle decimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, false, Color.Black);
            NPOIExcelUtility.SetBorderToCell(decimalNumberStyle);
            decimalNumberStyle.Alignment = HorizontalAlignment.Center;
            XSSFCellStyle yellowBoldDecimalNumberStyle = NPOIExcelUtility.DecimalStyle(excelFile, Color.White, true, Color.Black);
            NPOIExcelUtility.SetBorderToCell(yellowBoldDecimalNumberStyle);
            yellowBoldDecimalNumberStyle.Alignment = HorizontalAlignment.Center;
            yellowBoldDecimalNumberStyle.DataFormat = dataFormat.GetFormat(PositiveNumberFormat);

            string bettypeName = CacheRef.GetBettype(bettype);
            row = worksheet.CreateRow(currentRow) as XSSFRow;
            currentColumn = 0;
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, bettypeName, headerStyle);
            currentRow += 2;

            while (reader.Read())
            {
                if (numberBetTypeElements == 0)
                {
                    hdpScoreString = string.Format(
                        "{0}{1}{2}",
                        reader["hdp"],
                        reader["livehomescore"],
                        reader["liveawayscore"]);
                }

                string tempHdpScoreString = string.Format(
                    "{0}{1}{2}",
                    reader["hdp"],
                    reader["livehomescore"],
                    reader["liveawayscore"]);

                row = worksheet.CreateRow(currentRow) as XSSFRow;
                currentColumn = 0;
                virtualScore = Converter.ToInt(reader["vScore"]);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, reader["hdp"], decimalNumberStyle);
                string score = string.Format("{0}-{1}", reader["livehomescore"], reader["liveawayscore"]);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, score, textStyle);
                decimal odds = Converter.ToDecimal(reader["FC_m3"]) == 0 ?
                    0 :
                    Converter.ToDecimal(
                    Converter.ToDecimal(reader["FC_p3"]) /
                    Converter.ToDecimal(reader["FC_m3"]));
                fc_p3Total += Converter.ToDecimal(reader["FC_p3"]);
                fc_p2Total += Converter.ToDecimal(reader["FC_p2"]);
                fc_p1Total += Converter.ToDecimal(reader["FC_p1"]);
                fc_p0Total += Converter.ToDecimal(reader["FC_p0"]);
                fc_m1Total += Converter.ToDecimal(reader["FC_m1"]);
                fc_m2Total += Converter.ToDecimal(reader["FC_m2"]);
                fc_m3Total += Converter.ToDecimal(reader["FC_m3"]);

                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, odds, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_p3"]), boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_p2"]), boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_p1"]), boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_p0"]), boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_m1"]), boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_m2"]), boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["FC_m3"]), boldDecimalNumberStyle);

                var isSameMatch = numberBetTypeElements != 0 ? string.Equals(hdpScoreString, tempHdpScoreString) : true;
                decimal homeTotal = Converter.ToDecimal(reader["HomeTotal"]);
                decimal awayTotal = Converter.ToDecimal(reader["AwayTotal"]);
                homeTotalByBetType += homeTotal;
                awayTotalByBetType += awayTotalByBetType;
                string balance = string.Format(
                        "{0} - {1}",
                        Math.Round(homeTotal, 2) == 0 ? 0 : Math.Round(homeTotal, 2),
                        Math.Round(awayTotal, 2) == 0 ? 0 : Math.Round(awayTotal, 2));
                decimal total = homeTotal - awayTotal;

                if (!isSameMatch)
                {
                    hdpScoreString = tempHdpScoreString;
                }

                if (Converter.ToString2(reader["status"]).ToLower() == "lose")
                {
                    loseHomeTotal = Converter.ToDecimal(reader["HomeTotal"]);
                    loseAwayTotal = Converter.ToDecimal(reader["AwayTotal"]);
                    isHaveLoseTicket = true;
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, balance, textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, total, boldDecimalNumberStyle);
                }
                else if (Converter.ToString2(reader["status"]).ToLower() == "won")
                {
                    wonHomeTotal = Converter.ToDecimal(reader["HomeTotal"]);
                    wonAwayTotal = Converter.ToDecimal(reader["AwayTotal"]);

                    if (isSameMatch && isHaveLoseTicket)
                    {
                        string wonBalance = string.Format(
                            "{0} - {1}",
                            Math.Round(wonHomeTotal, 2) == 0 ? 0 : Math.Round(wonHomeTotal, 2),
                            Math.Round(loseHomeTotal, 2) == 0 ? 0 : Math.Round(loseHomeTotal, 2));
                        string loseBalance = string.Format(
                            "{0} - {1}",
                            Math.Round(wonAwayTotal, 2) == 0 ? 0 : Math.Round(wonAwayTotal, 2),
                            Math.Round(loseAwayTotal, 2) == 0 ? 0 : Math.Round(loseAwayTotal, 2));
                        decimal wonTotal = wonHomeTotal - loseHomeTotal;
                        decimal loseTotal = wonAwayTotal - loseAwayTotal;
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, wonBalance, textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, wonTotal, boldDecimalNumberStyle);
                        int previousRow = currentRow - 1;
                        worksheet.GetRow(previousRow).GetCell(10).SetCellValue(loseBalance);
                        worksheet.GetRow(previousRow).GetCell(11).SetCellValue((double)loseTotal);
                    }
                    else
                    {
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, balance, textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, total, boldDecimalNumberStyle);
                    }
                }
                else
                {
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, balance, textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, total, boldDecimalNumberStyle);
                }

                currentRow++;
                numberBetTypeElements += 1;
            }

            if (numberBetTypeElements > 0)
            {
                oddTotalByBetType = fc_m3Total == 0 ? 0 : fc_p3Total / fc_m3Total;
                row = worksheet.CreateRow(currentRow) as XSSFRow;
                currentColumn = 0;
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, boldTextStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, boldTextStyle);
                int startMergeColumn = currentColumn - 2;
                NPOIExcelUtility.RenderMergeCell(
                    ref worksheet,
                    ref row,
                    currentRow,
                    ref startMergeColumn,
                    currentRow,
                    currentColumn - 1,
                    SaveBetsBusinessLabel.Total,
                    boldTextStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, oddTotalByBetType, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_p3Total, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_p2Total, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_p1Total, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_p0Total, yellowBoldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_m1Total, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_m2Total, boldDecimalNumberStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, fc_m3Total, boldDecimalNumberStyle);
                string totalBalanceByBetType = string.Format("{0} - {1}", homeTotalByBetType, awayTotalByBetType);
                decimal totalByBetType = homeTotalByBetType - awayTotalByBetType;
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalBalanceByBetType, textStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, totalByBetType, boldDecimalNumberStyle);
                currentRow++;

                int bettypeHeaderRowIndex = currentRow - numberBetTypeElements - 2;
                this.ExportHeaderByBetType(excelFile, worksheet, ref bettypeHeaderRowIndex, virtualScore);
                worksheet.GroupRow(bettypeHeaderRowIndex + 1, bettypeHeaderRowIndex + numberBetTypeElements);
                worksheet.SetRowGroupCollapsed(bettypeHeaderRowIndex + 1, true);
            }
        }

        public void InitColumnWidth(ISheet worksheet)
        {
            for (int i = 0; i < 12; i++)
            {
                worksheet.SetColumnWidth(i, 170 * NpoiExcelExtensions.FontWidth);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        /// <summary>
        /// Exports the type of the header by bet.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="virtualScore">The virtual score.</param>
        private void ExportHeaderByBetType(
            XSSFWorkbook excelFile,
            XSSFSheet worksheet,
            ref int currentRow,
            int virtualScore)
        {
            XSSFCellStyle headerStyle = NPOIExcelUtility.HeaderStyle(excelFile);
            NPOIExcelUtility.SetBorderToCell(headerStyle);
            int currentColumn = 0;
            XSSFRow row = worksheet.CreateRow(currentRow) as XSSFRow;

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Hdp, headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score, headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Odds, headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore + 3), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore + 2), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore + 1), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore - 1), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore - 2), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(virtualScore - 3), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Balance, headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Total, headerStyle);
        }
    }
}