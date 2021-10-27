using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Application;
using Application.Common.Mapping;
using Application.Interfaces;
using Persistent;
using Microsoft.Extensions.Configuration;

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
		}

		// ������������� pipeline ����������; ���������, ��� ����� ������������ ����������
		// ����������� ��� ����������� ������������� ��, ����. middleware
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();  // ���, ��������, middleware
			app.UseHttpsRedirection();
			app.UseCors("AllowAll");

			app.UseEndpoints(endpoints =>
			{
				// ������� �� ����������� � �� ������
				endpoints.MapControllers();
			});
		}
	}
}
