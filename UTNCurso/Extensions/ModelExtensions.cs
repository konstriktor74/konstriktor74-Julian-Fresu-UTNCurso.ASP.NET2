using Microsoft.AspNetCore.Mvc.ModelBinding;
using UTNCurso.Core.Domain;

namespace UTNCurso.Extensions
{
    public static class ModelExtensions
    {
        public static ModelStateDictionary AddModelError(this ModelStateDictionary modelState, IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error?.ComponentName, error?.Message);
            }

            return modelState;
        }
    }
}
