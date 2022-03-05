using csi5112lec4b.models;
using csi5112lec4b.services;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});

// Add services and controllers to the container.

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<TodoDatabaseSettings>(
                builder.Configuration.GetSection(nameof(TodoDatabaseSettings)));

// Add our services for DI
var options = builder.Configuration.GetSection(nameof(TodoDatabaseSettings)).Get<TodoDatabaseSettings>();
builder.Services.AddSingleton<TodoDatabaseSettings>(options);

builder.Services.AddSingleton<TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
// useCors must be after use routing and before use authorization
app.UseCors(MyAllowSpecificOrigins);

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
