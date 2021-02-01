using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timesheet.Api
{
    public class JwtAuthMiddleware
    {

        private readonly RequestDelegate _next;

        public JwtAuthMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }



        public async Task Invoke(HttpContext httpContext)
        {
            


            var authHeader = httpContext.Request.Headers["Authorization"]
                .FirstOrDefault();

            if (authHeader != null)
            {
                var secret = "security security security security security";
                var key = Encoding.UTF8.GetBytes(secret);


                var token = authHeader.Split(" ").Last();

                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
                tokenHandler.ValidateToken(token, 
                    validationParameters,  
                    out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                httpContext.Items["LastName"] = jwtToken.Claims.
                    First(x => x.Type == "LastName").
                    Value;
            }
            await _next(httpContext);
        }
    }
}
