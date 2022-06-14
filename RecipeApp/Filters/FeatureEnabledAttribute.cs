using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace RecipeApp.Filters
{
    public class FeatureEnabledAttribute:Attribute, IResourceFilter
    {
        public bool IsEnabled { get; set; }
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!IsEnabled)
                context.Result = new BadRequestResult();
        }
        public void OnResourceExecuted(ResourceExecutedContext context) { }
    }
}
