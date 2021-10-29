using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Application;
using Application.Common.Mapping;
using Application.Interfaces;
using Persistent;
using Microsoft.Extensions.Configuration;
using WebApi.Middleware;

namespace WebApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration) => Configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(config =>
			{
				config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
				config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
			});

			services.AddApplication();
			services.AddPersistence(Configuration);
			services.AddControllers();
			services.AddCors(options => 
			{
				options.AddPolicy("AllowAll", policty => 
				{
					policty.AllowAnyHeader();
					policty.AllowAnyMethod();
					policty.AllowAnyOrigin();
				});
			});

			services.AddAuthentication(config =>
			{
				config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer("Bearer", options =>
				{
					options.Authority = "https://localhost:44329/";
					options.Audience = "NotesWebAPI";
					options.RequireHttpsMetadata = false; 
				});
		}

		// Настраивается pipeline приложения; указываем, что будет использовать приложение
		// Применяются все необходимые промежуточные ПО, англ. middleware
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCustomExceptionHandler();
			app.UseRouting();  // Это, например, middleware
			app.UseHttpsRedirection();
			app.UseCors("AllowAll");
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				// Мапимся на контроллеры и их методы
				endpoints.MapControllers();
			});
		}
	}
}
