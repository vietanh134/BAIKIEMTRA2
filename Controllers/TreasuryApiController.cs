using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PCMSystem.Data;

namespace PCMSystem.Pages.Treasury
{
    [Route("api/treasury")]
    [ApiController]
    public class TreasuryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TreasuryApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("export-transactions")]
        public async Task<IActionResult> ExportTransactions()
        {
            var transactions = await _context.Transactions
                .Include(t => t.Category)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Giao dịch");

                // Header
                worksheet.Cell(1, 1).Value = "Ngày giao dịch";
                worksheet.Cell(1, 2).Value = "Danh mục";
                worksheet.Cell(1, 3).Value = "Loại";
                worksheet.Cell(1, 4).Value = "Số tiền";
                worksheet.Cell(1, 5).Value = "Mô tả";

                // Format header
                var headerRow = worksheet.Range(1, 1, 1, 5);
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                headerRow.Style.Font.FontColor = XLColor.White;

                // Data
                int row = 2;
                decimal totalIncome = 0;
                decimal totalExpense = 0;

                foreach (var transaction in transactions)
                {
                    worksheet.Cell(row, 1).Value = transaction.TransactionDate;
                    worksheet.Cell(row, 2).Value = transaction.Category.CategoryName;
                    worksheet.Cell(row, 3).Value = transaction.Category.Type;
                    worksheet.Cell(row, 4).Value = transaction.Amount;
                    worksheet.Cell(row, 5).Value = transaction.Description;

                    // Format số tiền
                    if (transaction.Category.Type == "Thu")
                    {
                        worksheet.Cell(row, 4).Style.Font.FontColor = XLColor.Green;
                        totalIncome += transaction.Amount;
                    }
                    else
                    {
                        worksheet.Cell(row, 4).Style.Font.FontColor = XLColor.Red;
                        totalExpense += transaction.Amount;
                    }

                    row++;
                }

                // Tổng cộng
                row += 1;
                worksheet.Cell(row, 2).Value = "TỔNG THU:";
                worksheet.Cell(row, 4).Value = totalIncome;
                worksheet.Cell(row, 4).Style.Font.Bold = true;
                worksheet.Cell(row, 4).Style.Font.FontColor = XLColor.Green;

                row += 1;
                worksheet.Cell(row, 2).Value = "TỔNG CHI:";
                worksheet.Cell(row, 4).Value = totalExpense;
                worksheet.Cell(row, 4).Style.Font.Bold = true;
                worksheet.Cell(row, 4).Style.Font.FontColor = XLColor.Red;

                row += 1;
                worksheet.Cell(row, 2).Value = "SỐ DƯ:";
                worksheet.Cell(row, 4).Value = totalIncome - totalExpense;
                worksheet.Cell(row, 4).Style.Font.Bold = true;
                worksheet.Cell(row, 4).Style.Font.FontColor = (totalIncome - totalExpense) >= 0 ? XLColor.Green : XLColor.Red;

                // Adjust column width
                worksheet.Columns(1, 5).AdjustToContents();

                // Save to stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;

                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        $"GiaoDich_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    );
                }
            }
        }
    }
}
