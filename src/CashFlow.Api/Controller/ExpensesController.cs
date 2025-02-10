using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase registerExpenseUseCase, [FromBody] RequestExpenseJson request)
    {
        var response = await registerExpenseUseCase.Execute(request);
        return Created(string.Empty, response);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromServices] IGetAllExpensesUseCase getExpensesUseCase)
    {
        var response = await getExpensesUseCase.Execute();

        if (response.Expenses.Count != 0)
        {
            return Ok(response);
        }

        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:long}")]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdExpenseUseCase getByIdExpenseUseCase, [FromRoute] long id)
    {
        var response = await getByIdExpenseUseCase.Execute(id);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteExpenseUseCase deleteExpenseUseCase, [FromRoute] long id)
    {
        await deleteExpenseUseCase.Execute(id);
        return NoContent();
    }

    [HttpPut]
    [Route("{id:long}")]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateExpenseUseCase updateExpenseUseCase, [FromRoute] long id, [FromBody] RequestExpenseJson request)
    {
        await updateExpenseUseCase.Execute(id, request);
        
        return NoContent();
    }
}