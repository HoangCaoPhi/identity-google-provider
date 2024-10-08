using google_login.Server;
using google_login.Server.Options;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.GetSection())
                .ValidateOnStart();

builder.Services.AddOptions<AuthenticationOptions>()
                .BindConfiguration(AuthenticationOptions.GetSection())
                .ValidateOnStart();

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://example.com")
            .AllowAnyHeader()
            .AllowAnyMethod());

    options.AddPolicy("AllowAllOrigins",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var corsPolicy = builder.Environment.IsDevelopment()
    ? "AllowAllOrigins"
    : "AllowSpecificOrigin";

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 
 
app.MapFallbackToFile("/index.html");

app.Run();