using Infrastructure.Employee.Models.Request;
using FluentValidation;
namespace EmployeeApi.Validators
{
    public class EmployeeAddRequestValidator:AbstractValidator<EmployeeAddRequest>
    {
        public EmployeeAddRequestValidator()
        {
            RuleFor(req => req.FirstName).NotEmpty();
            RuleFor(req => req.FirstName).NotNull();
            RuleFor(req => req.LastName).NotEmpty();
            RuleFor(req => req.LastName).NotNull();
            RuleFor(req => req.Address1).NotEmpty();
            RuleFor(req => req.Address1).NotNull();
        }
    }
}
