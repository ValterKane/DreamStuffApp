using CommunityToolkit.Maui;
using DreamStuffApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DreamStuffApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-Black.ttf", "Black");
                    fonts.AddFont("Roboto-BlackItalic.ttf", "BlackItalic");
                    fonts.AddFont("Roboto-Bold.ttf", "Bold");
                    fonts.AddFont("Roboto-BoldItalic.ttf", "BoldItalic");
                    fonts.AddFont("Roboto-Italic.ttf", "Italic");
                    fonts.AddFont("Roboto-Light.ttf", "Light");
                    fonts.AddFont("Roboto-Regular.ttf", "Regular");
                    fonts.AddFont("Roboto-Medium.ttf", "Medium");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            RepositoryEditor.CopyIfNew();


            return builder.Build();
        }
    }
}