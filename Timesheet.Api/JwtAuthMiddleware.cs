using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        public JwtAuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"]
                .FirstOrDefault();

            if (authHeader != null)
            {
                var secret = _configuration.GetSection("jwtSecret").Value;

                if(string.IsNullOrWhiteSpace(secret))
                {
                    throw new Exception("Set up your secret");
                }

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

                context.Items["LastName"] = jwtToken.Claims
                    .First(x => x.Type == "LastName")
                    .Value;
            }

            await _next(context);
        }
    }
}
