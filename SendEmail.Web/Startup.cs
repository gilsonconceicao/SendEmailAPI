using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SendEmail.Application.Emails.Commands;
using SendEmail.Application.Emails.Dtos;
using SendEmail.Application.Emails.Queries;
using SendEmail.Application.MappersProfile;
using SendEmail.Application.Validations;
using SendEmail.Domain.Contracts;
using SendEmail.Domain.Models;
using SendEmail.Domain.Services;
using SendEmail.Infrastructure.Contexts;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString = _configuration.GetConnectionString("DbContextConnectionString")!;
        services.AddEndpointsApiExplorer();

        // mediatR to CQRS of application
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // dependency injection setting to fluent validations 
        services.AddValidatorsFromAssemblyContaining<SendEmailCommandValidator>();

        services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }
        );

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // settings to handler, command and queries
        services.AddTransient<IRequestHandler<GetEmailListQuery, List<GetEmailListDto>>, GetEmailListQueryHandler>();
        services.AddTransient<IRequestHandler<SendEmailCommand, bool>, SendEmailCommandHandler>();

        //services by SMTP
        services.AddTransient<ISmtpService, SmtpServices>();

        services.AddDbContext<Context>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Send Email",
                Description = "System with resources for books",
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseSwagger();

        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Send Email");
        });

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}