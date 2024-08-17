namespace DreamStuffApp.View;

public partial class AboutAsForm : ContentPage
{
    public AboutAsForm()
    {
        InitializeComponent();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        Location location = new(51.3050567, 37.8893144);
        MapLaunchOptions options = new() { Name = "ул. Олимпийский микрорайон, 52" };

        try
        {
            await Map.Default.OpenAsync(location, options);
        }
        catch (Exception ex)
        {

        }
    }

    private async void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        Location location = new(51.2861529, 37.8031041);
        MapLaunchOptions options = new() { Name = "просп. Губкина, 5" };

        try
        {
            await Map.Default.OpenAsync(location, options);
        }
        catch (Exception ex)
        {

        }
    }

    private async void ImageButton_Clicked_2(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new("https://vk.com/dream_stuff");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
        }
        catch
        {

        }
    }

    private async void ImageButton_Clicked_3(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new("www.instagram.com/dreamstuff31");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
        }
        catch
        {

        }
    }

    private async void ImageButton_Clicked_4(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new("t.me/dreamstuff31\r\n");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
        }
        catch
        {

        }
    }
}