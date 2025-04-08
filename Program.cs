using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using System.Text.Json;

var webAppBuilder = WebApplication.CreateBuilder(args);
webAppBuilder.Services.AddSwaggerGen();
webAppBuilder.Services.AddEndpointsApiExplorer();
webAppBuilder.Services.AddControllers();

var webApp = webAppBuilder.Build();

webApp.UseSwagger();
webApp.UseSwaggerUI();
webApp.UseMiddleware<RequestLoggingMiddleware>();
webApp.UseMiddleware<ExceptionHandlingMiddleware>();
webApp.UseMiddleware<UserAuthMiddleware>();
webApp.UseAuthorization();
webApp.MapControllers();

webApp.Run();