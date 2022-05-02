using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;

namespace SoloDevApp.Api.Filters
{
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
            string accessToken = context.HttpContext.Request.Headers[HeaderNames.Authorization];
            accessToken = accessToken.Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(accessToken);
            List<Claim> claims = new List<Claim>(jsonToken.Claims);

            try
            {
                if (DateTime.Parse(claims[0].Value) < DateTime.Now)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }


            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }


            await next();
        }
    }
}