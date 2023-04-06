using ExceptionHandlingInTasks.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

System.Timers.Timer timer = new System.Timers.Timer(8000);
timer.Elapsed += async (sender, e) => await OnTimer();
timer.AutoReset = true;
timer.Enabled = true;

async Task OnTimer()
{
    await Task.Run(() =>
    {
        // throw new Exception($"Timer: exception from task. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
    });
    // throw new Exception($"Timer: exception. IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");
}

app.Run();
