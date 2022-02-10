using WordHunter.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Words>();
builder.Services.AddSingleton<Hunter>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    //app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseRouting();
app.UseStaticFiles();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
