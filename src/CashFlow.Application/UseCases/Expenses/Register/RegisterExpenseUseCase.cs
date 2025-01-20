﻿using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesRepository repository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
{
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
        repository.Add(entity);
        unitOfWork.Commit();
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