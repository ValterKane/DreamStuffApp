using DreamStuffApp.DataControllers;
using DreamStuffApp.View;
using Microsoft.Extensions.DependencyModel;

namespace DreamStuffApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IDataRuller, DataControlller>();
            MainPage = new AutorizationPage();
        }
    }
}