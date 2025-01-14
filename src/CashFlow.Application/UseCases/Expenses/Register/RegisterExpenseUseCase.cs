using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        
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