public class AppSettings
{
    public string SettingsFilePath { get; set; } = string.Empty;
    public string OrderDataFilePath { get; set; } = string.Empty;
    public string GoogleAPIKey { get; set; } = string.Empty;
    public bool DarkMode { get; set; }

    // Home clock / date / day settings
    public bool ClockVisible { get; set; } = true;
    public int ClockFormat { get; set; } = 0; // index 0..9

    public bool DayVisible { get; set; } = true;
    public int DayFormat { get; set; } = 0; // index 0..9

    public bool DateVisible { get; set; } = true;
    public int DateFormat { get; set; } = 0; // index 0..9
}