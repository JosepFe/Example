﻿using JosepApp.Common.Options;
using JosepApp.Common.Options.JWT;
using JosepApp.Configuration.JWT.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JosepApp.Configuration.JWT
{
    public static class JwtConfiguration
    {
        public static void SetUpJwt(this IServiceCollection services, ref IConfiguration configuration)
        {
            var jwtOptions = services.GetTypedOptions<JwtOptions>(configuration, "JWT");
            var jwtHandler = new JwtHandler(jwtOptions);
            services.AddSingleton<IJwtHandler>(jwtHandler);

            SetUp(services, jwtOptions);
        }

        private static void SetUp(IServiceCollection services, JwtOptions jwtOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = jwtOptions.ValidateLifetime,
                    ValidAudience = jwtOptions.Audience,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });
        }
    }
}
