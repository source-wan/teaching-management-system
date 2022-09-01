using EMS.Api.Service;
using EMS.Infrastructure;
using EMS.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});

// Register http context
builder.Services.AddHttpContextAccessor();

// Register user information service
builder.Services.AddTransient(typeof(IUserSession), typeof(UserSession));
builder.Services.AddTransient(typeof(IQueryHelper<>), typeof(QueryHelper<>));
// Register CRUD service
builder.Services.AddTransient(typeof(ICreateDataServer<,>), typeof(CreateDataServer<,>));
builder.Services.AddTransient(typeof(ICreateDataServer<>), typeof(CreateDataServer<>));
builder.Services.AddTransient(typeof(IGetDataServer<>), typeof(GetDataServer<>));
builder.Services.AddTransient(typeof(IUpdateDataServer<,>), typeof(UpdateDataServer<,>));
builder.Services.AddTransient(typeof(IUpdateDataServer<>), typeof(UpdateDataServer<>));
builder.Services.AddTransient(typeof(IDeleteDataServer<>), typeof(DeleteDataServer<>));
builder.Services.AddTransient(typeof(ICRUDServer<,,>), typeof(CRUDServer<,,>));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.WebHost.UseUrls(new []{"http://localhost:5038"});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
await app.MigrateDatabaseAsync();

app.Run();
