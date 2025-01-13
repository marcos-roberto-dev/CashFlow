using System;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

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
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (titleIsEmpty)
            throw new ArgumentException("Title is required");
        
        var amountIsEmpty = request.Amount <= 0;
        if (amountIsEmpty)
            throw new ArgumentException("Amount is required");
        
        var dateCompare = DateTime.Compare(request.Date, DateTime.Now);
        var dateIsGreaterThanNow = dateCompare > 0;
        
        if(dateIsGreaterThanNow)
            throw new ArgumentException("Expenses can't be in the future");
        
        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if (paymentTypeIsValid == false)
            throw new ArgumentException("Payment type is invalid");
    }
}