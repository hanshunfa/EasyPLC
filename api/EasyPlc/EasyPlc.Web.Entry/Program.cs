using System.Diagnostics;

Serve.Run(RunOptions.Default.ConfigureBuilder(builder =>
{
    builder.WebHost.UseUrls(builder.Configuration["AppSettings:Urls"]);
}).ConfigureOptions(new WebApplicationOptions
{
    EnvironmentName = Debugger.IsAttached ? "Development" : default
}));