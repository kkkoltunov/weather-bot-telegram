using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeatherBot.BLL.Services;
using WeatherBot.Core.Repositories;
using WeatherBot.Core.Services;
using WeatherBot.DAL;
using WeatherBot.DAL.Profile;
using WeatherBot.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IUpdateHandler<Update>, UpdateHandler>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<IWeathersGetterService, WeathersGetterService>();



builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddHttpClient("tgwebhook").AddTypedClient<ITelegramBotClient>(httpClient
    => new TelegramBotClient(builder.Configuration["BotConfiguration:Token"], httpClient));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper((provider, expression) =>
{
    expression.AddProfile(new MapperProfile());
    expression.AddCollectionMappers();
    expression.UseEntityFrameworkCoreModel<ApplicationDbContext>(provider);
    expression.AddExpressionMapping();
}, typeof(ApplicationDbContext).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseRouting();

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute("tgwebhook",
        builder.Configuration["BotConfiguration:Token"],
        new {controller = "Bot", action = "Post"});
});

app.Run();