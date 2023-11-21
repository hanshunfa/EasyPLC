using System.Diagnostics;

Serve.Run(RunOptions.Default.ConfigureBuilder(builder =>
{
    builder.WebHost.UseUrls(builder.Configuration["AppSettings:Urls"]);
}).ConfigureOptions(new WebApplicationOptions
{
    // Debugger.IsAttached 可判断释放为 Debug 模式
    EnvironmentName = Debugger.IsAttached ? "Development" : default
}));