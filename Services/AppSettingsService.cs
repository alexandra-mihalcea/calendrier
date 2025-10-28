using System.Text.Json;

namespace Calendrier.Services
{
    public class AppSettingsService
    {
        private readonly string _defaultSettingsPath = Path.Combine(AppContext.BaseDirectory, ".settings");
        private readonly string _defaultOrderDataPath = Path.Combine(AppContext.BaseDirectory, ".orderData");
        public AppSettings Settings { get; private set; } = new();

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
        }

        public async Task SaveAsync()
        {
            var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_defaultSettingsPath, json);
        }
    }
}