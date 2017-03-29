namespace SaveBetsBusiness.Service.Implement
{
    using Fanex.Common;
    using Fanex.SaveBets.Builder.Content;
    using Fanex.SaveBets.Builder.Header;
    using Fanex.SaveBets.Builder.Mapper;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;

    public class ExportDataByBetTypeService : IDisposable
    {
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

        private void ExportHeaderByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, int bettype)
        {
            XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, true, HorizontalAlignment.Center, Color.Black);
            NPOIExcelUtility.SetBorderToCell(textStyle);
            int currentRow = 0, currentColumn = 0;
            XSSFRow row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

            var sectionObj = new SectionBettypeMapperImp();
            int numberOfSection = sectionObj.GetNumberSectionOf(bettype);

            if (numberOfSection == 2)
            {
                this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
            }
            else if (numberOfSection == 3)
            {
                this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, textStyle);
                this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
            }
            else
            {
                this.ExportHeaderGroup(excelFile, worksheet, ref currentRow, ref currentColumn, bettype);
            }
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed. Suppression is OK here.")]
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
            var header = new HeaderBuilderImp();
            var headerList = header.GetHeaderHandicapAndBetteamOf(bettype);
            var sectionObj = new SectionBettypeMapperImp();
            var numberOfSection = sectionObj.GetNumberSectionOf(bettype);

            foreach (string headerText in headerList)
            {
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, headerText, headerStyle);
            }

            if (numberOfSection != 3)
            {
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
                startMergeColumn = currentColumn - 2;
                NPOIExcelUtility.RenderMergeCell(ref worksheet, ref row, currentRow, ref startMergeColumn, currentRow, currentColumn - 1, SaveBetsBusinessLabel.Score.ToUpper(), headerStyle);
            }

            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, string.Empty, headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Trans_Time.ToUpper(), headerStyle);
            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, SaveBetsBusinessLabel.Creator.ToUpper(), headerStyle);
        }

        private void ExportBetTypeContent(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow, int bettype)
        {
            var sectionObj = new SectionBettypeMapperImp();
            int numberOfSection = sectionObj.GetNumberSectionOf(bettype);

            if (numberOfSection == 2)
            {
                this.ExportTwoSectionContentByBetType(excelFile, worksheet, reader, ref currentRow, bettype);
                InitTwoSectionColumnWidth(worksheet, bettype);
            }
            else if (sectionObj.IsMyanmarBettypeGroup(bettype))
            {
                this.ExportMyanmarOddsTwoSectionContentByBetType(excelFile, worksheet, reader, ref currentRow);
                InitOneSectionColumnWidth(worksheet);
            }
            else if (numberOfSection == 3)
            {
                this.ExportThreeSectionContentByBetType(excelFile, worksheet, reader, ref currentRow);
                InitThreeSectionColumnWidth(worksheet);
            }
            else
            {
                this.ExportOneSectionContentByBetType(excelFile, worksheet, reader, ref currentRow, bettype);
                InitOneSectionColumnWidth(worksheet);
            }
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportThreeSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow)
        {
            try
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
                var numberStyle = NPOIExcelUtility.IntStyleWithoutSeparateThousand(excelFile, Color.White, false, Color.Black);
                NPOIExcelUtility.SetBorderToCell(numberStyle);
                numberStyle.Alignment = HorizontalAlignment.Center;

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
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["transid"]), numberStyle);
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
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetTwoRowIndex, ref currentColumn, Converter.ToDecimal(reader["transid"]), numberStyle);
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
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetXRowIndex, ref currentColumn, Converter.ToDecimal(reader["transid"]), numberStyle);
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
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
        }

        /// <summary>
        /// Exports the two section content by bet type.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="currentRow">The current row.</param>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed. Suppression is OK here.")]
        private void ExportTwoSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow, int bettype)
        {
            try
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
                var numberStyle = NPOIExcelUtility.IntStyleWithoutSeparateThousand(excelFile, Color.White, false, Color.Black);
                NPOIExcelUtility.SetBorderToCell(numberStyle);
                numberStyle.Alignment = HorizontalAlignment.Center;

                XSSFRow row = null;

                var betChoiceToShowObj = new ContentBetChoiceBuilderImp();
                List<string> firstSection = new List<string> { "h", "o", "y" };
                List<string> secondSection = new List<string> { "a", "u", "n", "e" };

                while (reader.Read())
                {
                    string amount = string.Format(
                        "{0}/{1}/{2}",
                        Math.Round(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2),
                        Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                        Math.Round(Converter.ToDecimal(reader["stake"]), 2));
                    var betChoices = betChoiceToShowObj.GetContentBetchoiceToShowOf(bettype, reader);
                    string betteam = Converter.ToString2(reader["betteam"]).ToLower();
                    if (firstSection.Contains(betteam))
                    {
                        row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["transid"]), numberStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);

                        foreach (var choice in betChoices)
                        {
                            if (choice is string)
                            {
                                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, choice, textStyle);
                            }
                            else
                            {
                                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, choice, decimalNumberStyle);
                            }
                        }

                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["livehomescore"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToInt(reader["liveawayscore"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["betfrom"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transdate"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["creator"]), textStyle);
                        currentRow++;
                    }
                    else if (secondSection.Contains(betteam))
                    {
                        var secondSectionStartColumnIndexMapper = new SecondSectionStartColumnIndexMapperImp();
                        row = worksheet.GetRow(currentBetAwayTeamRowIndex) as XSSFRow ?? worksheet.CreateRow(currentBetAwayTeamRowIndex) as XSSFRow;
                        currentColumn = secondSectionStartColumnIndexMapper.GetStartColumnIndexOfSecondSection(bettype);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToDecimal(reader["transid"]), numberStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, amount, redBoldTextStyle);

                        foreach (var choice in betChoices)
                        {
                            if (choice is string)
                            {
                                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, choice, textStyle);
                            }
                            else
                            {
                                NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, choice, decimalNumberStyle);
                            }
                        }

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
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        private void ExportOneSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow, int bettype)
        {
            try
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
                int currentColumn = 0;
                var numberStyle = NPOIExcelUtility.IntStyleWithoutSeparateThousand(excelFile, Color.White, false, Color.Black);
                NPOIExcelUtility.SetBorderToCell(numberStyle);
                numberStyle.Alignment = HorizontalAlignment.Center;

                XSSFRow row = null;

                var betChoiceToShowObj = new ContentBetChoiceBuilderImp();
                while (reader.Read())
                {
                    string amount = string.Format(
                        "{0}/{1}/{2}",
                        Math.Round(Converter.ToDecimal(Converter.ToDecimal(reader["cpstake"]) * Converter.ToDecimal(reader["virtualrate"]), 2)),
                        Math.Round(Converter.ToDecimal(reader["cpstake"]), 2),
                        Math.Round(Converter.ToDecimal(reader["stake"]), 2));
                    var betChoices = betChoiceToShowObj.GetContentBetchoiceToShowOf(bettype, reader);

                    row = worksheet.CreateRow(currentRow) as XSSFRow;

                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["transid"]), numberStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                    NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);

                    foreach (var choice in betChoices)
                    {
                        if (choice is string)
                        {
                            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, choice, textStyle);
                        }
                        else
                        {
                            NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, choice, decimalNumberStyle);
                        }
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
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
        }

        private void InitOneSectionColumnWidth(XSSFSheet worksheet)
        {
            for (int i = 0; i < 13; i++)
            {
                worksheet.SetColumnWidth(i, 80 * NPOIExcelUtility.FontWidth);
            }

            worksheet.SetColumnWidth(0, 170 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(2, 150 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(4, 220 * NPOIExcelUtility.FontWidth);

            worksheet.SetColumnWidth(8, 80 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(9, 80 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(11, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(12, 150 * NPOIExcelUtility.FontWidth);
            worksheet.AutoSizeColumn(5);
            worksheet.AutoSizeColumn(10);
        }

        /// <summary>
        /// Initializes the width of the two section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void InitTwoSectionColumnWidth(XSSFSheet worksheet, int bettype)
        {
            for (int i = 0; i < 23; i++)
            {
                worksheet.SetColumnWidth(i, 90 * NpoiExcelExtensions.FontWidth);
            }

            var secondSectionStartColumnIndexMapper = new SecondSectionStartColumnIndexMapperImp();
            var secondSectionStartColumnIndex = secondSectionStartColumnIndexMapper.GetStartColumnIndexOfSecondSection(bettype);

            worksheet.SetColumnWidth(0, 170 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(2, 150 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(4, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(secondSectionStartColumnIndex - 3, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(secondSectionStartColumnIndex - 2, 150 * NPOIExcelUtility.FontWidth);

            worksheet.SetColumnWidth(secondSectionStartColumnIndex, 170 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(secondSectionStartColumnIndex + 2, 150 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(secondSectionStartColumnIndex + 4, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(2 * secondSectionStartColumnIndex - 3, 220 * NPOIExcelUtility.FontWidth);
            worksheet.SetColumnWidth(2 * secondSectionStartColumnIndex - 2, 150 * NPOIExcelUtility.FontWidth);
            worksheet.AutoSizeColumn(5);
            worksheet.AutoSizeColumn(secondSectionStartColumnIndex + 5);
            worksheet.AutoSizeColumn(secondSectionStartColumnIndex - 4);
            worksheet.AutoSizeColumn(2 * secondSectionStartColumnIndex - 4);

            if (secondSectionStartColumnIndex == 13)
            {
                //worksheet.SetColumnWidth(6, 220 * NPOIExcelUtility.FontWidth);
                //worksheet.SetColumnWidth(19, 220 * NPOIExcelUtility.FontWidth);
                worksheet.AutoSizeColumn(6);
                worksheet.AutoSizeColumn(19);
            }
        }

        /// <summary>
        /// Initializes the width of the three section column.
        /// </summary>
        /// <param name="worksheet">The worksheet.</param>
        private void InitThreeSectionColumnWidth(XSSFSheet worksheet)
        {
            for (int i = 0; i < 26; i++)
            {
                worksheet.SetColumnWidth(i, 90 * NpoiExcelExtensions.FontWidth);
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

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportMyanmarOddsTwoSectionContentByBetType(XSSFWorkbook excelFile, XSSFSheet worksheet, IDataReader reader, ref int currentRow)
        {
            try
            {
                XSSFCellStyle textStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Center, Color.Black);
                NPOIExcelUtility.SetBorderToCell(textStyle);
                XSSFCellStyle rightTextStyle = NPOIExcelUtility.TextStyle(excelFile, Color.White, false, HorizontalAlignment.Right, Color.Black);
                NPOIExcelUtility.SetBorderToCell(rightTextStyle);
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

                    decimal handicap_one = Math.Round(Converter.ToDecimal(reader["hdp1"]));
                    decimal oddsspread = Math.Round(Converter.ToDecimal(reader["oddsspread"]));
                    string handicap_Odds = string.Format(
                        "{0} ({1})",
                        handicap_one,
                        oddsspread);

                    if (Converter.ToString2(reader["betteam"]).ToLower() == "h")
                    {
                        row = worksheet.GetRow(currentRow) as XSSFRow ?? worksheet.CreateRow(currentRow) as XSSFRow;

                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["username"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToDecimal(reader["odds"]), blueBoldDecimalNumberStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["transid"]), textStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, Converter.ToString2(reader["currency"]), boldTextStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, amount, redBoldTextStyle);
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentRow, ref currentColumn, handicap_Odds, rightTextStyle);
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
                        NPOIExcelUtility.RenderCell(ref worksheet, ref row, currentBetAwayTeamRowIndex, ref currentColumn, handicap_Odds, rightTextStyle);
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
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
        }
    }
}