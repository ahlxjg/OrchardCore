using System.Text.Json;
using Acornima;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Html.Models;
using OrchardCore.Mvc.ModelBinding;
using OrchardCore.TinyMCE.ViewModels;

namespace OrchardCore.TinyMCE.Settings;

public sealed class TinyMCEHtmlBodyPartSettingsDriver : ContentTypePartDefinitionDisplayDriver<HtmlBodyPart>
{
    internal readonly IStringLocalizer S;

    public TinyMCEHtmlBodyPartSettingsDriver(IStringLocalizer<TinyMCEHtmlBodyPartSettingsDriver> stringLocalizer)
    {
        S = stringLocalizer;
    }

    public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, BuildEditorContext context)
    {
        return Initialize<TinyMCESettingsViewModel>("HtmlBodyPartTinyMCESettings_Edit", model =>
        {
            var settings = contentTypePartDefinition.GetSettings<TinyMCEHtmlBodyPartSettings>();
            model.Options = settings.Options;
        }).Location("Editor");
    }

    public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
    {
        if (contentTypePartDefinition.Editor() == "TinyMCE")
        {
            var model = new TinyMCESettingsViewModel();

            await context.Updater.TryUpdateModelAsync(model, Prefix);

            if (!string.IsNullOrWhiteSpace(model.Options))
            {
                var options = model.Options?.Trim();

                if (!IsValidJson(options))
                {
                    context.Updater.ModelState.AddModelError(Prefix, nameof(model.Options), S["The options are written in an incorrect format."]);
                }
                else
                {
                    try
                    {
                        var settings = new TinyMCEHtmlBodyPartSettings
                        {
                            Options = options,
                        };

                        context.Builder.WithSettings(settings);
                    }
                    catch (ParseErrorException)
                    {
                        context.Updater.ModelState.AddModelError(Prefix, nameof(model.Options), S["The options are written in an incorrect format."]);
                    }
                }
            }
            else
            {
                context.Builder.WithSettings(new TinyMCEHtmlBodyPartSettings());
            }
        }

        return Edit(contentTypePartDefinition, context);
    }

    public static bool IsValidJson(string jsonString)
    {
        try
        {
            using (JsonDocument.Parse(jsonString))
            {
                return true;
            }
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
