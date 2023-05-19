using Words.Api;
using Words.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddDatabase(builder.Configuration)
        .AddServices();
}

WebApplication app = builder.Build();
{
    app.UseHttpsRedirection();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger().UseSwaggerUI();
    }
    
    app.MapApiEndpoints();
    app.Run();
}
