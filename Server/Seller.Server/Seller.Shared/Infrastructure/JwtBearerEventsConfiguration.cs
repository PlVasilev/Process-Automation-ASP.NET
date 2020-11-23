﻿namespace Seller.Shared.Infrastructure
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    public class JwtBearerEventsConfiguration
    {
        public static JwtBearerEvents GetBearerEvent =>
            new JwtBearerEvents
             {
                 OnMessageReceived = context =>
                 {
                     var accessToken = context.Request.Query["access_token"];

                     var path = context.HttpContext.Request.Path;
                     if (!string.IsNullOrEmpty(accessToken) &&
                         path.StartsWithSegments("/notifications"))
                     {
                         context.Token = accessToken;
                     }
                     return Task.CompletedTask;
                 }
             };
    }
}
