using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace AIS_Abiturient.Helpers
{
    public class ExportHelper
    {
        public static void ExportToExcel(DataGridView grid, string defaultFilename = "export.xlsx")
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel файлы (*.xlsx)|*.xlsx";
                sfd.FileName = defaultFilename;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Абитуриенты");

                        for (int i = 0; i < grid.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = grid.Columns[i].HeaderText;
                        }

                        for (int i = 0; i < grid.Rows.Count; i++)
                        {
                            for (int j = 0; j < grid.Columns.Count; j++)
                            {
                                if (grid.Rows[i].Cells[j].Value != null)
                                    worksheet.Cells[i + 2, j + 1].Value = grid.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                        FileInfo fi = new FileInfo(sfd.FileName);
                        package.SaveAs(fi);

                        MessageBox.Show("Экспорт завершён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}