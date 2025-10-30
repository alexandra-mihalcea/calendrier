using System;
using System.Text.Json;

namespace Calendrier.Services
{
    public class AppSettingsService
    {
        private readonly string _defaultSettingsPath = Path.Combine(AppContext.BaseDirectory, ".settings");
        private readonly string _defaultOrderDataPath = Path.Combine(AppContext.BaseDirectory, ".orderData");
        public AppSettings Settings { get; private set; } = new();

        // Add the SettingsChanged event to fix CS1061
        public event Action? SettingsChanged;

        public async Task LoadOrCreateAsync()
        {
            if (File.Exists(_defaultSettingsPath))
            {
                var json = await File.ReadAllTextAsync(_defaultSettingsPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json);
                if (loaded is not null)
                {
                    Settings = loaded;
                }
            }
            else
            {
                Settings = new AppSettings
                {
                    SettingsFilePath = _defaultSettingsPath,
                    OrderDataFilePath = _defaultOrderDataPath,
                    GoogleAPIKey = string.Empty,
                    DarkMode = false
                };

                await SaveAsync();
            }

            // When settings are loaded or changed, raise the event:
            SettingsChanged?.Invoke();
        }

        public async Task SaveAsync()
        {
            var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_defaultSettingsPath, json);

            // When settings are saved or changed, raise the event:
            SettingsChanged?.Invoke();
        }

        // Add a method to raise the event if needed elsewhere
        protected void OnSettingsChanged()
        {
            SettingsChanged?.Invoke();
        }
    }
}