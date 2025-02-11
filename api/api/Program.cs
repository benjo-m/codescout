using api.Controllers;
using api.Data;
using api.Services;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ProjectService, ProjectService>();
builder.Services.AddScoped<UserService, UserService>();
builder.Services.AddScoped<SeedData, SeedData>();
builder.Services.AddScoped<ResourceListService, ResourceListService>();
builder.Services.AddScoped<AuthService, AuthService>();
builder.Services.AddScoped<CompanyService, CompanyService>();
builder.Services.AddScoped<MessageService, MessageService>();
builder.Services.AddScoped<FriendService, FriendService>();
builder.Services.AddScoped<AwardService, AwardService>();
builder.Services.AddTransient<EmailService, EmailService>();
builder.Services.AddHostedService<TokenCleanupWorker>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                      });
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();