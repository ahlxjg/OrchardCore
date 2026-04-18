using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace OrchardCore.TinyMCE;

public sealed class ResourceManagementOptionsConfiguration : IConfigureOptions<ResourceManagementOptions>
{
    private static readonly ResourceManifest _manifest;

    static ResourceManagementOptionsConfiguration()
    {
        _manifest = new ResourceManifest();

        _manifest
            .DefineScript("tinymce")
            .SetUrl("~/OrchardCore.TinyMCE/Scripts/lib/tinymce/tinymce.min.js");

        _manifest
            .DefineScript("tinymce-default-settings")
            .SetUrl("~/OrchardCore.TinyMCE/Scripts/tinymce.default.settings.min.js")
            .SetDependencies("tinymce");

        _manifest
            .DefineScript("tinymce-extended-features")
            .SetUrl("~/OrchardCore.TinyMCE/Scripts/tinymce.extended.features.min.js")
            .SetDependencies("tinymce");
    }

    public void Configure(ResourceManagementOptions options)
    {
        options.ResourceManifests.Add(_manifest);
    }
}
