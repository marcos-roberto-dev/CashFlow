﻿using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), 201)]
    public IActionResult Register([FromServices] IRegisterExpenseUseCase registerExpenseUseCase, [FromBody] RequestRegisterExpenseJson request)
    {
        var response = registerExpenseUseCase.Execute(request);
        return Created(string.Empty, response);
    }
}