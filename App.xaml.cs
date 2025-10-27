using Calendrier.Services;

namespace Calendrier
{
    public partial class App : Application
    {
        public App(AppSettingsService appSettingsService)
        {
            InitializeComponent();

            // Load settings at startup (fire and forget)
            _ = appSettingsService.LoadAsync();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage()) { Title = "Calendrier" };
        }
    }
}
