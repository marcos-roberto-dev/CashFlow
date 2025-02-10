namespace CashFlow.Application.UseCases.Expenses.Register.Reports;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}