using Amazon.S3;
using Avaliacao_loja_interativa_c.Repositorio;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projeto_Api.Controllers.Usuario;
using Projeto_Api.data.Interfaces;
using Projeto_Api.Repositorio;
using Projeto_Api.Services;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.


        builder.Services.AddDbContext<ApplicationDbContext>(
                    opts => opts.UseMySql(builder.Configuration["ConnectionString:apiDb"],
                     new MySqlServerVersion(new Version(8, 0))));

     
        var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]);

        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
              .RequireAuthenticatedUser(   )
              .Build();
           
        });

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        builder.Services.AddControllers();
        //builder.Services.AddTransient<TipoRepositorio>;
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<ITipoRepositorio, TipoRepositorio>();
        builder.Services.AddScoped<IConcessaoRepo, ConcessaoRepo>();
        builder.Services.AddScoped<IDocumentoRepo, DocumentoRespo>();
        builder.Services.AddScoped<IAws3Services, Aws3Services>();
        builder.Services.AddScoped<IUserRepo, UserRepo>();
        builder.Services.AddScoped<UsuarioAutenticado>();
        builder.Services.AddScoped<PaginarIQueryableService>();
        builder.Services.AddScoped<SendGridService>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy(name: "CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200","*")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        var app = builder.Build();
        app.UseAuthentication();
        app.UseAuthorization();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("corsapp");
        app.UseCors("CorsPolicy");

        app.MapControllers();
        app.Run();
    }
}