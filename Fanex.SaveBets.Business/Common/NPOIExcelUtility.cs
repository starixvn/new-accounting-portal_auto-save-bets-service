// -----------------------------------------------------------------------
// <copyright file="NPOIExcelUtility.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Common
{
    using System;
    using System.Drawing;
    using System.Web;
    using NPOI.SS.UserModel;
    using NPOI.SS.Util;
    using NPOI.XSSF.UserModel;
    using NPOI.XSSF.UserModel.Extensions;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class NPOIExcelUtility
    {
        public const int FontWidth = 30;
        public const int FontHeight = 20;
        public const int FontSize = 8;
        public const int MaxRowPerSheet = 20000;

        public static readonly Color BackGroundColor = Color.White;
        public static readonly Color HeaderTitleColor = Color.FromArgb(60, 94, 165);
        public static readonly Color EvenRowColor = Color.FromArgb(228, 242, 253);
        public static readonly Color OddRowColor = Color.White;
        public static readonly Color SubTotalRowColor = Color.FromArgb(194, 217, 236);
        public static readonly Color TotalRowColor = Color.FromArgb(244, 244, 190);
        public static readonly Color GrandTotalRowColor = Color.FromArgb(219, 225, 187);
        public static readonly Color GrandTotalSeparatorColor = Color.FromArgb(255, 102, 0);
        public static readonly Color RevenueLicenseeColor = Color.FromArgb(213, 132, 52);
        public static readonly Color ParentColor = Color.FromArgb(194, 222, 236);
        public static readonly Color ChildColor = Color.FromArgb(194, 226, 236);
        public static readonly Color NoteInformationCellColor = Color.FromArgb(181, 196, 208);
        public static readonly Color BorderColor = Color.FromArgb(192, 192, 192);

        private const string FontName = "Tahoma";
        private const string IntNumberFormat = "#,##0";
        private const string TwoDecimalDigitNumber = "##,##0.00";

        /// <summary>
        /// Style for header.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <returns>The cell style.</returns>
        public static XSSFCellStyle HeaderStyle(XSSFWorkbook excelFile)
        {
            XSSFCellStyle cellStyle = excelFile.CreateCellStyle() as XSSFCellStyle;
            XSSFFont font = excelFile.CreateFont() as XSSFFont;
            font.Boldweight = (short)FontBoldWeight.Bold;
            font.SetColor(new XSSFColor(Color.White));
            font.FontHeightInPoints = FontSize;
            font.FontName = FontName;

            cellStyle.SetFillForegroundColor(new XSSFColor(new byte[] { 60, 94, 165 }));
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.SetFont(font);

            return cellStyle;
        }

        /// <summary>
        /// Texts the style.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="cellColor">Color of the cell.</param>
        /// <param name="bold">Set font is bold or not.</param>
        /// <param name="align">The align.</param>
        /// <param name="fontColor">Color of the font.</param>
        /// <returns>The text style.</returns>
        public static XSSFCellStyle TextStyle(
           XSSFWorkbook excelFile,
           Color cellColor,
           bool bold,
           HorizontalAlignment align,
           Color fontColor)
        {
            XSSFCellStyle cellStyle = excelFile.CreateCellStyle() as XSSFCellStyle;
            XSSFFont font = excelFile.CreateFont() as XSSFFont;

            font.FontHeightInPoints = FontSize;
            font.FontName = FontName;
            font.SetColor(new XSSFColor(fontColor));

            if (bold)
            {
                font.Boldweight = (short)FontBoldWeight.Bold;
            }

            cellStyle.SetFillForegroundColor(new XSSFColor(cellColor));
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = align;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.SetFont(font);

            return cellStyle;
        }

        /// <summary>
        /// Style for decimal value.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="cellColor">The cell color.</param>
        /// <param name="bold">Check bold.</param>
        /// <param name="fontColor">The font color.</param>
        /// <returns>The cell style.</returns>
        public static XSSFCellStyle DecimalStyle(
            XSSFWorkbook excelFile,
            Color cellColor,
            bool bold,
            Color fontColor)
        {
            XSSFCellStyle cellStyle = excelFile.CreateCellStyle() as XSSFCellStyle;
            XSSFFont font = excelFile.CreateFont() as XSSFFont;
            IDataFormat dataFormat = excelFile.CreateDataFormat();

            font.FontHeightInPoints = FontSize;
            font.FontName = FontName;
            font.SetColor(new XSSFColor(fontColor));

            if (bold)
            {
                font.Boldweight = (short)FontBoldWeight.Bold;
            }

            cellStyle.SetFillForegroundColor(new XSSFColor(cellColor));
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Right;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.DataFormat = dataFormat.GetFormat(TwoDecimalDigitNumber);
            cellStyle.SetFont(font);

            return cellStyle;
        }

        /// <summary>
        /// Style for integer value.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="cellColor">The cell color.</param>
        /// <param name="bold">Check bold.</param>
        /// <param name="fontColor">The font color.</param>
        /// <returns>The cell style.</returns>
        public static XSSFCellStyle IntStyle(
            XSSFWorkbook excelFile,
            Color cellColor,
            bool bold,
            Color fontColor)
        {
            XSSFCellStyle cellStyle = excelFile.CreateCellStyle() as XSSFCellStyle;
            XSSFFont font = excelFile.CreateFont() as XSSFFont;
            IDataFormat dataFormat = excelFile.CreateDataFormat();

            font.FontHeightInPoints = FontSize;
            font.FontName = FontName;
            font.SetColor(new XSSFColor(fontColor));

            if (bold)
            {
                font.Boldweight = (short)FontBoldWeight.Bold;
            }

            cellStyle.SetFillForegroundColor(new XSSFColor(cellColor));
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.Alignment = HorizontalAlignment.Right;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.DataFormat = dataFormat.GetFormat(IntNumberFormat);
            cellStyle.SetFont(font);

            return cellStyle;
        }

        /// <summary>
        /// Style for integer value.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="cellColor">The cell color.</param>
        /// <param name="bold">Check bold.</param>
        /// <param name="align">Align text(value is right, left or center).</param>
        /// <param name="fontColor">The font color.</param>
        /// <returns>The cell style.</returns>
        public static ICellStyle IntStyle(
            XSSFWorkbook excelFile,
            Color cellColor,
            bool bold,
            HorizontalAlignment align,
            Color fontColor)
        {
            XSSFCellStyle cellStyle = excelFile.CreateCellStyle() as XSSFCellStyle;
            XSSFFont font = excelFile.CreateFont() as XSSFFont;
            IDataFormat dataFormat = excelFile.CreateDataFormat();

            font.FontHeightInPoints = FontSize;
            font.FontName = FontName;
            font.SetColor(new XSSFColor(fontColor));

            if (bold)
            {
                font.Boldweight = (short)FontBoldWeight.Bold;
            }

            cellStyle.Alignment = align;
            cellStyle.SetFillForegroundColor(new XSSFColor(cellColor));
            cellStyle.FillPattern = FillPattern.SolidForeground;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;
            cellStyle.DataFormat = dataFormat.GetFormat(IntNumberFormat);
            cellStyle.SetFont(font);

            return cellStyle;
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
        public static void RenderCell(
            ref XSSFSheet worksheet,
            ref XSSFRow row,
            int currentRow,
            ref int currentColumn,
            object cellValue,
            XSSFCellStyle cellStyle)
        {
            XSSFCell cell = row.GetCell(currentColumn) as XSSFCell ?? row.CreateCell(currentColumn) as XSSFCell;
            SetCellValue(cell, cellValue);
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
        public static void RenderMergeCell(
            ref XSSFSheet worksheet,
            ref XSSFRow row,
            int currentRow,
            ref int currentColumn,
            int endRow,
            int endColumn,
            object cellValue,
            XSSFCellStyle cellStyle)
        {
            XSSFCell cell = row.GetCell(currentColumn) as XSSFCell ?? row.CreateCell(currentColumn) as XSSFCell;

            SetCellValue(cell, cellValue);

            cell.CellStyle = cellStyle;
            worksheet.AddMergedRegion(new CellRangeAddress(currentRow, endRow, currentColumn, endColumn));
            currentColumn = endColumn + 1;
        }

        public static void ResponseStreamExcel(this IWorkbook workbook, string fileName)
        {
            var response = HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", fileName));
            workbook.Write(response.OutputStream);
        }

        /// <summary>
        /// Set border to cell.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <returns>The style of cell.</returns>
        public static XSSFCellStyle SetBorderToCell(XSSFCellStyle cellStyle)
        {
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;

            cellStyle.SetBorderColor(BorderSide.BOTTOM, new XSSFColor(BorderColor));
            cellStyle.SetBorderColor(BorderSide.LEFT, new XSSFColor(BorderColor));
            cellStyle.SetBorderColor(BorderSide.RIGHT, new XSSFColor(BorderColor));
            cellStyle.SetBorderColor(BorderSide.TOP, new XSSFColor(BorderColor));

            return cellStyle;
        }

        /// <summary>
        /// Sets the border to cell.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="topBorderColor">Color of the top border.</param>
        /// <param name="leftBorderColor">Color of the left border.</param>
        /// <param name="rightBorderColor">Color of the right border.</param>
        /// <param name="bottomBorderColor">Color of the bottom border.</param>
        /// <returns>Cell style.</returns>
        public static XSSFCellStyle SetBorderToCell(
            XSSFCellStyle cellStyle,
            Color topBorderColor,
            Color leftBorderColor,
            Color rightBorderColor,
            Color bottomBorderColor)
        {
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;

            cellStyle.SetBorderColor(BorderSide.BOTTOM, new XSSFColor(bottomBorderColor));
            cellStyle.SetBorderColor(BorderSide.LEFT, new XSSFColor(leftBorderColor));
            cellStyle.SetBorderColor(BorderSide.RIGHT, new XSSFColor(rightBorderColor));
            cellStyle.SetBorderColor(BorderSide.TOP, new XSSFColor(topBorderColor));

            return cellStyle;
        }

        /// <summary>
        /// Sets the double border bottom to cell.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="topBorderColor">Color of the top border.</param>
        /// <param name="leftBorderColor">Color of the left border.</param>
        /// <param name="rightBorderColor">Color of the right border.</param>
        /// <param name="bottomBorderColor">Color of the bottom border.</param>
        /// <returns>Cell style.</returns>
        public static XSSFCellStyle SetDoubleBorderBottomToCell(
            XSSFCellStyle cellStyle,
            Color topBorderColor,
            Color leftBorderColor,
            Color rightBorderColor,
            Color bottomBorderColor)
        {
            cellStyle.BorderBottom = BorderStyle.Double;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;

            cellStyle.SetBorderColor(BorderSide.BOTTOM, new XSSFColor(bottomBorderColor));
            cellStyle.SetBorderColor(BorderSide.LEFT, new XSSFColor(leftBorderColor));
            cellStyle.SetBorderColor(BorderSide.RIGHT, new XSSFColor(rightBorderColor));
            cellStyle.SetBorderColor(BorderSide.TOP, new XSSFColor(topBorderColor));

            return cellStyle;
        }

        /// <summary>
        /// Sets the bold border bottom to cell.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="topBorderColor">Color of the top border.</param>
        /// <param name="leftBorderColor">Color of the left border.</param>
        /// <param name="rightBorderColor">Color of the right border.</param>
        /// <param name="bottomBorderColor">Color of the bottom border.</param>
        /// <returns>Cell style.</returns>
        public static XSSFCellStyle SetBoldBorderBottomToCell(
            XSSFCellStyle cellStyle,
            Color topBorderColor,
            Color leftBorderColor,
            Color rightBorderColor,
            Color bottomBorderColor)
        {
            cellStyle.BorderBottom = BorderStyle.Medium;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;

            cellStyle.SetBorderColor(BorderSide.BOTTOM, new XSSFColor(bottomBorderColor));
            cellStyle.SetBorderColor(BorderSide.LEFT, new XSSFColor(leftBorderColor));
            cellStyle.SetBorderColor(BorderSide.RIGHT, new XSSFColor(rightBorderColor));
            cellStyle.SetBorderColor(BorderSide.TOP, new XSSFColor(topBorderColor));

            return cellStyle;
        }

        /// <summary>
        /// Sets the cell value.
        /// </summary>
        /// <param name="cell">The cell of row.</param>
        /// <param name="cellValue">The cell value.</param>
        public static void SetCellValue(XSSFCell cell, object cellValue)
        {
            if (cellValue is string)
            {
                cell.SetCellValue((string)cellValue);
            }
            else if (cellValue is int)
            {
                cell.SetCellValue((int)cellValue);
            }
            else if (cellValue is long)
            {
                cell.SetCellValue((long)cellValue);
            }
            else if (cellValue is double)
            {
                cell.SetCellValue((double)cellValue);
            }
            else if (cellValue is decimal)
            {
                cell.SetCellValue(Convert.ToDouble(cellValue));
            }
            else if (cellValue is bool)
            {
                cell.SetCellValue((bool)cellValue);
            }
            else if (cellValue is System.DateTime)
            {
                cell.SetCellValue((System.DateTime)cellValue);
            }
        }

        /// <summary>
        /// Creates the font.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="fontColor">Color of the font.</param>
        /// <param name="bold">Set font is bold or not.</param>
        /// <param name="underlineType">Type of the underline.</param>
        /// <returns>The font data.</returns>
        public static XSSFFont CreateFont(XSSFWorkbook excelFile, Color fontColor, bool bold, FontUnderlineType underlineType)
        {
            XSSFFont font = excelFile.CreateFont() as XSSFFont;

            font.FontHeightInPoints = FontSize;
            font.FontName = FontName;
            font.SetColor(new XSSFColor(fontColor));

            if (bold)
            {
                font.Boldweight = (short)FontBoldWeight.Bold;
            }

            font.Underline = underlineType;

            return font;
        }
    }
}