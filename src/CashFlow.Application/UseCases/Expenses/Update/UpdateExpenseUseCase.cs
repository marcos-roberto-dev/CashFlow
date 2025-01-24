using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(IExpenseUpdateOnlyRepository repository,IMapper mapper, IUnitOfWork unitOfWork) : IUpdateExpenseUseCase
{
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);
        
        var expense = await repository.GetById(id);
        
        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        mapper.Map(request, expense);
        
        repository.Update(expense);
        
        await unitOfWork.Commit(); 
    }
    
    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var resultValidate = validator.Validate(request);

        if (resultValidate.IsValid == false)
        {
            var errorMessages = resultValidate.Errors.Select(parameter => parameter.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}