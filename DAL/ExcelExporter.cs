using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LTTQ_G2_2025.DAL
{
    public static class ExcelExporter
    {
        /// <summary>
        /// Xuất 1 sheet duy nhất với header đẹp.
        /// </summary>
        public static void ExportDataTable(string sheetTitle, DataTable dt, string savePath)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                throw new ArgumentException("DataTable không có dữ liệu để xuất!");
            }

            XLWorkbook wb = new XLWorkbook();
            try
            {
                AddSheet(wb, sheetTitle, dt);

                string dir = Path.GetDirectoryName(savePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                wb.SaveAs(savePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xuất Excel: {ex.Message}", ex);
            }
            finally
            {
                wb.Dispose();
            }
        }

        /// <summary>
        /// Xuất nhiều sheet (title -> DataTable) vào 1 file.
        /// </summary>
        public static void ExportMultiSheets(Dictionary<string, DataTable> sheets, string savePath)
        {
            if (sheets == null || sheets.Count == 0)
            {
                throw new ArgumentException("Không có sheet nào để xuất!");
            }

            XLWorkbook wb = new XLWorkbook();
            try
            {
                foreach (KeyValuePair<string, DataTable> kv in sheets)
                {
                    if (kv.Value != null && kv.Value.Rows.Count > 0)
                    {
                        AddSheet(wb, kv.Key, kv.Value);
                    }
                }

                if (wb.Worksheets.Count == 0)
                {
                    throw new Exception("Tất cả các sheet đều rỗng!");
                }

                string dir = Path.GetDirectoryName(savePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                wb.SaveAs(savePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xuất Excel nhiều sheet: {ex.Message}", ex);
            }
            finally
            {
                wb.Dispose();
            }
        }

        private static void AddSheet(XLWorkbook wb, string sheetTitle, DataTable dt)
        {
            if (dt == null)
                dt = new DataTable();

            string safeTitle = SafeSheetName(string.IsNullOrWhiteSpace(sheetTitle) ? "Sheet" : sheetTitle);

            IXLWorksheet ws = wb.Worksheets.Add(safeTitle);
            int totalCols = dt.Columns.Count > 0 ? dt.Columns.Count : 1;

            // Big header
            ws.Cell(1, 1).Value = sheetTitle.ToUpperInvariant();
            ws.Range(1, 1, 1, totalCols).Merge();
            IXLRange title = ws.Range(1, 1, 1, totalCols);

            title.Style.Font.Bold = true;
            title.Style.Font.FontSize = 18;
            title.Style.Font.FontColor = XLColor.White;
            title.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            title.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            title.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
            title.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            ws.Row(1).Height = 32;

            // Table (bắt đầu từ dòng 3)
            if (dt.Rows.Count > 0)
            {
                ws.Cell(3, 1).InsertTable(dt, "DATA", true);

                IXLRange header = ws.Range(3, 1, 3, totalCols);
                header.Style.Font.Bold = true;
                header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                header.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                header.Style.Fill.BackgroundColor = XLColor.FromArgb(235, 241, 255);
                header.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

                // Freeze header
                ws.SheetView.FreezeRows(3);

                // Viền bảng
                IXLRange tableRange = ws.Range(3, 1, 3 + dt.Rows.Count, totalCols);
                tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Hair;

                // Định dạng cột
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    IXLColumn col = ws.Column(i + 1);

                    if (dt.Columns[i].ColumnName.IndexOf("ID", StringComparison.OrdinalIgnoreCase) >= 0)
                        col.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    if (dt.Columns[i].DataType == typeof(DateTime))
                        col.Style.DateFormat.Format = "yyyy-MM-dd";
                }
            }
            else
            {
                // Nếu không có dữ liệu, vẫn tạo header trống
                ws.Cell(3, 1).Value = "Không có dữ liệu";
            }

            ws.Columns().AdjustToContents();

            // Footer
            int lastRow = Math.Max(5, 3 + dt.Rows.Count + 2);
            ws.Cell(lastRow, 1).Value = "Exported on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ws.Range(lastRow, 1, lastRow, totalCols).Merge();
            IXLRange footer = ws.Range(lastRow, 1, lastRow, totalCols);
            footer.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            footer.Style.Font.Italic = true;
            footer.Style.Font.FontColor = XLColor.Gray;
        }

        private static string SafeSheetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = "Sheet";

            // Loại bỏ ký tự không hợp lệ
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');

            foreach (char c in new[] { '[', ']', ':', '*', '?', '/', '\\' })
                name = name.Replace(c, '_');

            // Excel sheet name tối đa 31 ký tự
            if (name.Length > 31)
                name = name.Substring(0, 31);

            return name;
        }
    }
}