using ApiConsorcio.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ApiConsorcio.Services;

public class ExcelService
{
    public byte[] ExportLeadsToExcel(IEnumerable<Lead> leads)
    {
        string spreadsheetName = "LeadsExportados";

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add(spreadsheetName);

            var properties = typeof(Lead).GetProperties();
            for (int i = 0; i < 7; i++)
            {
                worksheet.Cells[1, i + 1].Value = properties[i + 1].Name;
                worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                
            }

            int row = 2;
            foreach (var lead in leads)
            {
                for (int i = 0; i <  7; i++)
                {
                    worksheet.Cells[row, i + 1].Value = properties[i + 1].GetValue(lead);
                    worksheet.Cells[row, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, i + 1].Style.Border.Equals("True");
                    
                    if (row % 2 == 1)
                    {
                        worksheet.Cells[row, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[row, i + 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }
                }
                row++;
            }

            worksheet.Cells["G:G"].Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
            worksheet.Cells["C:C"].Style.Numberformat.Format = "(00) 00000-0000";
            worksheet.Cells["A:G"].AutoFitColumns();
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            worksheet.Cells[worksheet.Dimension.Address].AutoFilter = true;

            return package.GetAsByteArray();
        }
    }
}
