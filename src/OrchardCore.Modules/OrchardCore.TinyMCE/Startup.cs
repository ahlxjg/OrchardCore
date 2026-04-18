using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement;
using OrchardCore.Modules;
using OrchardCore.TinyMCE.Settings;
using OrchardCore.TinyMCE.Media;

namespace OrchardCore.TinyMCE;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IContentTypePartDefinitionDisplayDriver, TinyMCEHtmlBodyPartSettingsDriver>();
        services.AddResourceConfiguration<ResourceManagementOptionsConfiguration>();
    }
}
