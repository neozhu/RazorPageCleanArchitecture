using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming

namespace SmartAdmin.WebUI.Models
{
    /// <summary>Provides easy-access to building the SmartAdmin Navigation using JSON text data.</summary>
    /// <remarks>These classes are solely created for Demo purposes, please do not use them in Production.</remarks>
    internal static class NavigationBuilder
    {
        private static JsonSerializerOptions DefaultSettings => SerializerSettings();

        private static JsonSerializerOptions SerializerSettings(bool indented = true)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = indented,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return options;
        }

        public static SmartNavigation FromJson(string json) => JsonSerializer.Deserialize<SmartNavigation>(json, DefaultSettings);
    }

    public sealed class SmartNavigation
    {
        public SmartNavigation()
        {
        }

        public SmartNavigation(IEnumerable<ListItem> items)
        {
            Lists = new List<ListItem>(items);
        }

        public string Version { get; set; }
        public List<ListItem> Lists { get; set; } = new List<ListItem>();
    }

    public class ListItem
    {
        public string Icon { get; set; }
        public bool ShowOnSeed { get; set; } = true;
        public string Parent { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Href { get; set; }
        public ItemType Type { get; set; } = ItemType.Single;
        public string Route { get; set; }
        public string Tags { get; set; }
        public string I18n { get; set; }
        public string Target { get; set; }
        public bool Disabled { get; set; }
        public List<ListItem> Items { set; get; } = new List<ListItem>();
        public Span Span { get; set; } = new Span();
        public string[] Roles { get; set; }
    }

    public sealed class Span
    {
        public string Position { get; set; }
        public string Class { get; set; }
        public string Text { get; set; }

        public bool HasValue() => (Position?.Length ?? 0) + (Class?.Length ?? 0) + (Text?.Length ?? 0) > 0;
    }

    public enum ItemType
    {
        Category = 0,
        Single,
        Parent,
        Sibling,
        Child
    }
}
