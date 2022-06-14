﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RecipeApp.Filters
{
    public class EnsureRecipeExistsAttribute : TypeFilterAttribute
    {
        public EnsureRecipeExistsAttribute() : base(typeof(EnsureRecipeExistsAttribute)) {}
        public class EnsureRecipeExistsFilter : IAsyncActionFilter
        {
            private readonly RecipeService _service;
            public EnsureRecipeExistsFilter(RecipeService service) => _service = service;
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var recipeId = (int)context.ActionArguments["RecipeId"];
                if (!await _service.DoesRecipeExistAsync(recipeId))
                    context.Result = new NotFoundResult();
                else
                    await next();
            }
        }
    }
}
