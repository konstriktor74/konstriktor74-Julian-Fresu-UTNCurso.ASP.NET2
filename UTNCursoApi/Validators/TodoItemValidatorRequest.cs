using FluentValidation;
using UTNCurso.Core.DTOs;

namespace UTNCursoApi.Validators
{
    public class TodoItemValidatorRequest : AbstractValidator<TodoItemDto>
    {
        public TodoItemValidatorRequest()
        {
            RuleFor(x => x.Task).NotEmpty().MaximumLength(10);
        }
    }
}
