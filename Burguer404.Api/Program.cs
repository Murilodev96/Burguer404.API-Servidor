using Burguer404.Configurations.DependecyInjections;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJsonSerializerConfiguration();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddRepositoriesConfiguration(builder.Configuration);
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddServicesConfiguration();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddOutputCacheConfiguration();
builder.Services.AddHealthChecksConfiguration();


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("PermitirTudo");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecksConfiguration(builder.Configuration);
app.UseSwaggerConfiguration();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();
app.MapControllers();

app.Run();