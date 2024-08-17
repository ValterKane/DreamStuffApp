using Android.App;
using Android.Content.PM;
using Android.OS;
using DreamStuffApp.View;

namespace DreamStuffApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, NoHistory =true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)] 
    public class MainActivity : MauiAppCompatActivity
    {
    
    }
}