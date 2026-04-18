using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "TinyMCE",
    Author = ManifestConstants.OrchardCoreTeam,
    Website = ManifestConstants.OrchardCoreWebsite,
    Version = ManifestConstants.OrchardCoreVersion,
    Description = "TinyMCE editor integration for HtmlBodyPart.",
    Dependencies = new[] { "OrchardCore.Html", "OrchardCore.ResourceManagement" },
    Category = "Content Management"
)]
