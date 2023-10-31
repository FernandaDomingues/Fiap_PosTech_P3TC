using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TechChallenge2.Application.Services;
using TechChallenge2.Data.Context;
using TechChallenge2.Data.Repositories;
using TechChallenge2.Domain.Entities;
using TechChallenge2.Domain.Entities.Request;
using TechChallenge2.Domain.Interfaces.Repositories;
using TechChallenge2.Domain.Interfaces.Services;
using TechChallenge2.Identity.Data;
using TechChallenge2.Identity.Data.Dtos;
using TechChallenge2.Identity.Interfaces;
using TechChallenge2.Identity.Interfaces.Services;
using TechChallenge2.Identity.Models;
using TechChallenge2.Identity.Services;

namespace TechChallenge.Api.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public static class NativeInjectorConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection strings
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TechChallengeConnection")));

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TechChallengeConnection")));


            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9ASHDA98H9ah9ha9H9A89n0f")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });


            //Auto Mapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NoticiaRequest, Noticia>().ReverseMap();
                cfg.CreateMap<SignUpDto, Usuario>();
                cfg.CreateMap<LoginDto, Usuario>();
            });
            services.AddSingleton(autoMapperConfig.CreateMapper());

            // Repositórios
            services.AddScoped<INoticiaRepository, NoticiaRepository>();

            // Services
            services.AddScoped<INoticiaService, NoticiaService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();


            return services;
        }
    }
}