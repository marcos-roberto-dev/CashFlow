using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestRegisterExpenseJsonBuilder
{
  public static RequestExpenseJson Build()
  {
    var faker = new Faker();
    return new RequestExpenseJson
    {
      Amount = faker.Random.Decimal(min: 1, max: 1000),
      Date = faker.Date.Past(),
      Description = faker.Commerce.ProductDescription(),
      Title = faker.Commerce.ProductName(),
      PaymentType = faker.PickRandom<PaymentType>()
    };
  }  
}