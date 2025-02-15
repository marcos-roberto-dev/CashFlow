using System.Net.Mime;
using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controller;

public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase gerenerateExpensesReportExcelUseCase, 
        [FromHeader] DateOnly date
        )
    {
        byte[] file = await gerenerateExpensesReportExcelUseCase.Execute(date);
        
        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        return NoContent();
    }
    
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateExpensesReportPdfUseCase gerenerateExpensesReportPdfUseCase, 
        [FromHeader] DateOnly date
        )
    {
        byte[] file = await gerenerateExpensesReportPdfUseCase.Execute(date);
        
        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");
        return NoContent();
    }
}