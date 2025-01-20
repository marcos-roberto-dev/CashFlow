using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesRepository repository) : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository = repository;
    
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        var entity = new Expense
        {
            Amount = request.Amount,
            Description = request.Description,
            PaymentType = (PaymentType)request.PaymentType,
            Date = request.Date,
            Title = request.Title,
        };
        _repository.Add(entity);
        return new ResponseRegisterExpenseJson();
    }

    private void Validate(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();

        var resultValidate = validator.Validate(request);

        if (resultValidate.IsValid == false)
        {
            var errorMessages = resultValidate.Errors.Select(parameter => parameter.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}