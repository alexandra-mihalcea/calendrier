using System.Text.Json;

namespace Calendrier.Services
{
    public class AppSettingsService
    {
        private readonly string _defaultSettingsPath = Path.Combine(AppContext.BaseDirectory, ".settings");
        public AppSettings Settings { get; private set; } = new();

        public async Task LoadAsync()
        {
            if (File.Exists(_defaultSettingsPath))
            {
                var json = await File.ReadAllTextAsync(_defaultSettingsPath);
                var loaded = JsonSerializer.Deserialize<AppSettings>(json);
                if (loaded is not null)
                    Settings = loaded;
            }
        }

        public async Task SaveAsync()
        {
            var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });

            var oldSettingsPath = Path.Combine(AppContext.BaseDirectory, ".settings");
            if (_defaultSettingsPath != oldSettingsPath && File.Exists(oldSettingsPath))
            {
                File.Move(oldSettingsPath, _defaultSettingsPath, true);
            }
            await File.WriteAllTextAsync(_defaultSettingsPath, json);

            if (!File.Exists(Settings.OrderDataFilePath))
            {
                await File.WriteAllTextAsync(Settings.OrderDataFilePath, "{}");
            }
            else
            {
                var oldorderDataPath = Path.Combine(AppContext.BaseDirectory, ".orderData");
                if (Settings.OrderDataFilePath != oldorderDataPath && File.Exists(oldorderDataPath))
                {
                    File.Move(oldorderDataPath, Settings.OrderDataFilePath, true);
                }
            }
        }
    }
}