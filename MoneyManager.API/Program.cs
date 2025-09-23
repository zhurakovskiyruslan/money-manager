    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MoneyManager.Application.Accounts.Commands.CreateAccount;
    using MoneyManager.Application.Common.Behaviors;
    using MoneyManager.Infrastructure;

    
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateAccountCommand).Assembly));
    builder.Services.AddValidatorsFromAssembly(typeof(CreateAccountCommandValidator).Assembly);
    
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    
    var cs = builder.Configuration.GetConnectionString("DefaultConnection")
             ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddInfrastructure(cs);

    var app = builder.Build();

   
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Money Manager V1");
            c.RoutePrefix = string.Empty;
        });
    

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
