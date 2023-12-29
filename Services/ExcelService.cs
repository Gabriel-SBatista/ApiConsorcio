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

            string[] titles = { "Nome", "Email", "Telefone", "Cidade", "Origem", "Campanha", "Data" };

            for (int i = 0; i < 7; i++)
            {
                worksheet.Cells[1, i + 1].Value = titles[i];
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
                    switch (i)
                    {
                        case 0:
                            worksheet.Cells[row, i + 1].Value = lead.Name;
                            break;
                        case 1:
                            worksheet.Cells[row, i + 1].Value = lead.Email;
                            break;
                        case 2:
                            worksheet.Cells[row, i + 1].Value = lead.Telephone;
                            break;
                        case 3:
                            worksheet.Cells[row, i + 1].Value = lead.City;
                            break;
                        case 4:
                            worksheet.Cells[row, i + 1].Value = lead.Source;
                            break;
                        case 5:
                            worksheet.Cells[row, i + 1].Value = lead.Campaign;
                            break;
                        case 6:
                            worksheet.Cells[row, i + 1].Value = lead.DateLead;
                            break;
                    }                    
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
