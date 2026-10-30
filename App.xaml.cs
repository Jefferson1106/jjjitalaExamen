using Microsoft.Extensions.DependencyInjection;

namespace jjjitalaExamen
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Viesw.vLogin()));
        }
    }
}


