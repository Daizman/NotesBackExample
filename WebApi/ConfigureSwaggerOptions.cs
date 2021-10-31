using System;
using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi
{
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		private readonly IApiVersionDescriptionProvider _provider;

		public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
			_provider = provider;

		public void Configure(SwaggerGenOptions options)
		{
			foreach (var descr in _provider.ApiVersionDescriptions)
			{
				var apiVers = descr.ApiVersion.ToString();
				options.SwaggerDoc(descr.GroupName,
					new OpenApiInfo
					{
						Version = apiVers,
						Title = $"Notes API {apiVers}",
						Description = 
							"A simple example ASP NET Core Web API.",
						TermsOfService = new Uri("https://www.youtube.com/c/PlatinumTechTalks"),
						Contact = new OpenApiContact
						{ 
							Name = "Platinum Chat",
							Email = string.Empty,
							Url = new Uri("https://t.me/platinum_chat")
						},
						License = new OpenApiLicense
						{ 
							Name = "Platinum Telegram Channel",
							Url = new Uri("https://t.me/platinum_tech_talks")
						}
					});

				options.AddSecurityDefinition($"AuthToken {apiVers}",
					new OpenApiSecurityScheme
					{
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.Http,
						BearerFormat = "JWT",
						Scheme = "bearer",
						Name = "Autorizarion",
						Description = "Auth token"
					});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{ 
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = $"AuthToken {apiVers}"
							}
						},
						new string[] { }
					}
				});

				options.CustomOperationIds(apiDescription =>
					apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
						? methodInfo.Name
						: null);
			}
		}
	}
}
