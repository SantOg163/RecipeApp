using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace RecipeApp.Filters
{
    public class RequireIpAddressAttribute:Attribute, IAuthorizationFilter
    {
        private readonly IPAddress[] _allowedAddress = 
        {
            IPAddress.Parse("127.0.0.1"),
            IPAddress.Parse("::1")
        };
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var ipAdress = context.HttpContext.Connection.RemoteIpAddress;
            if (!_allowedAddress.Contains(ipAdress))
                context.Result = new ForbidResult();
        }
    }
}