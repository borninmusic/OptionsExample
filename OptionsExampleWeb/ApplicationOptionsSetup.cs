using Microsoft.Extensions.Options;

namespace OptionsExample;

public class ApplicationOptionsSetup : IConfigureOptions<ApplicationOptions>
{
    private readonly IConfiguration _configuration;

    public ApplicationOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(ApplicationOptions options)
    {
        //This will render change tracking capabilities of IOptionsMonitor to not work properly
        _configuration.GetSection(nameof(ApplicationOptions)).Bind(options);
    }
}