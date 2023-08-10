using Microsoft.Extensions.Options;
using OptionsExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationOptions>(builder.Configuration.GetSection(nameof(ApplicationOptions)));

//More elegant approach to options registration 
//builder.Services.ConfigureOptions<ApplicationOptionsSetup>();

var app = builder.Build();

app.MapGet("options",
    (IOptions<ApplicationOptions> opt, // Singleton - reads configuration options once per app lifetime
     IOptionsSnapshot<ApplicationOptions> optSnapshot, // Scoped - resolved once per project scope, which here means once per http request
     IOptionsMonitor<ApplicationOptions> optMonitor) => // Singleton - has CurrentValue prop which re-reads current value of the option every time we're asking for it
    {
        var result = new
        {
            OptionsValue = opt.Value.ProjectVersion,
            SnapshotValue = optSnapshot.Value.ProjectVersion,
            MonitorValue = optMonitor.CurrentValue.ProjectVersion
        };

        return result;
    });

app.Run();