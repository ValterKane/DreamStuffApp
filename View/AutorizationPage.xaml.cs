using DreamStuffApp.CustomTypes;
using DreamStuffApp.DataControllers;

namespace DreamStuffApp.View;

public partial class AutorizationPage : ContentPage
{
    private readonly IDataRuller DataRuller = DependencyService.Get<IDataRuller>(DependencyFetchTarget.GlobalInstance);
    public AutorizationPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        string login = LoginTB.Text;
        if (login != "")
        {
            await ((Button)sender).FadeTo(0, 1000, Easing.Linear);
            Model.ClientModel data = DataRuller.DataBaseContext.Clients.Where(p => p.PhoneNumber == login).FirstOrDefault();
            if (data != null)
            {
                if (data.Password == PassTB.Text)
                {

                    ((Button)sender).BackgroundColor = Color.FromArgb("008000");
                    ((Button)sender).TextColor = Color.FromArgb("FFFFFF");
                    await ((Button)sender).FadeTo(1, 1000, Easing.Linear);
                    DataRuller.LoginID = login;
                    LoyalityModel _temp = new();
                    DataRuller.Discount = _temp.DiscountValue(_temp.GetLevelOfLoyality(data.Points));
                    App.Current.MainPage = new AppShell();

                }
                else
                {
                    await ((Button)sender).FadeTo(1, 100, Easing.BounceOut);
                    ((Button)sender).BackgroundColor = Color.FromArgb("A52A2A");
                    ((Button)sender).TextColor = Color.FromArgb("FFFFFF");
                    await ((Button)sender).TranslateTo(-15, 0, 100, Easing.SpringIn);
                    await ((Button)sender).TranslateTo(+30, 0, 100, Easing.SpringOut);
                    await ((Button)sender).TranslateTo(0, 0, 100, Easing.SpringIn);
                    ((Button)sender).BackgroundColor = Color.FromArgb("FFFFFF");
                    ((Button)sender).TextColor = Color.FromArgb("E26CFF");

                }
            }
        }
        else
        {
            await ((Button)sender).FadeTo(1, 100, Easing.BounceOut);
            ((Button)sender).BackgroundColor = Color.FromArgb("A52A2A");
            ((Button)sender).TextColor = Color.FromArgb("FFFFFF");
            await ((Button)sender).TranslateTo(-15, 0, 100, Easing.SpringIn);
            await ((Button)sender).TranslateTo(+30, 0, 100, Easing.SpringOut);
            await ((Button)sender).TranslateTo(0, 0, 100, Easing.SpringIn);
            ((Button)sender).BackgroundColor = Color.FromArgb("FFFFFF");
            ((Button)sender).TextColor = Color.FromArgb("E26CFF");
        }

    }
}