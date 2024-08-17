using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using DreamStuffApp.CustomTypes;
using DreamStuffApp.DataControllers;
using DreamStuffApp.Model;
using DreamStuffApp.View.Popups;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DreamStuffApp.View;

public partial class ProfileForm : ContentPage
{
    private readonly IDataRuller DataRuller = DependencyService.Get<IDataRuller>(DependencyFetchTarget.GlobalInstance);
    private ObservableCollection<StachModel> StachData = new();

    public ICommand SelectedCommand { get; set; }

    public ProfileForm()
    {
        InitializeComponent();
        Loaded += ProfileForm_Loaded;
        NavigatedTo += ProfileForm_NavigatedTo;
    }

    private void ProfileForm_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        ProfileDataloader();
    }

    private void ProfileForm_Loaded(object sender, EventArgs e)
    {
        ProfileDataloader();
    }

    private async void loyalityLoader(double Points)
    {
        LoyalityModel lm = new();
        short lvl = lm.GetLevelOfLoyality(Points);

        switch (lvl)
        {
            case 0:
                Lvl_1.Opacity = 0.3;
                Lvl_1_1.Opacity = 0.3;
                Lvl_2.Opacity = 0.3;
                Lvl_2_2.Opacity = 0.3;
                Lvl_3.Opacity = 0.3;
                Lvl_3_3.Opacity = 0.3;
                Lvl_4.Opacity = 0.30;
                break;
            case 1:
                Lvl_1.Opacity = 1.0;
                Lvl_1_1.Opacity = 1.0;
                Lvl_2.Opacity = 0.3;
                Lvl_2_2.Opacity = 0.3;
                Lvl_3.Opacity = 0.3;
                Lvl_3_3.Opacity = 0.3;
                Lvl_4.Opacity = 0.3;
                break;
            case 2:
                Lvl_1.Opacity = 1.0;
                Lvl_1_1.Opacity = 1.0;
                Lvl_2.Opacity = 1.0;
                Lvl_2_2.Opacity = 1.0;
                Lvl_3.Opacity = 0.3;
                Lvl_3_3.Opacity = 0.3;
                Lvl_4.Opacity = 0.3;
                break;
            case 3:
                Lvl_1.Opacity = 1.0;
                Lvl_1_1.Opacity = 1.0;
                Lvl_2.Opacity = 1.0;
                Lvl_2_2.Opacity = 1.0;
                Lvl_3.Opacity = 1.0;
                Lvl_3_3.Opacity = 1.0;
                Lvl_4.Opacity = 0.3;
                break;
            case 4:
                Lvl_1.Opacity = 1.0;
                Lvl_1_1.Opacity = 1.0;
                Lvl_2.Opacity = 1.0;
                Lvl_2_2.Opacity = 1.0;
                Lvl_3.Opacity = 1.0;
                Lvl_3_3.Opacity = 1.0;
                Lvl_4.Opacity = 1.0;
                break;
            case -1:
                await DisplayAlert("Ошибка загрузки профиля!", "Ошибка загрузки. Профиль не может быть загружен!", "OK");
                break;

        }

        if (lvl != 4)
        {
            NextLvlPointsLbl.Text = $"{Math.Round((lm.NextLvlPoints(lvl) - Points), 2)} pts.";
        }
        else
        {
            NextLvlPointsLbl.Text = $"Макс. ур.";
        }

    }

    private void ProfileDataloader()
    {
        ClientModel Data = DataRuller.DataBaseContext.Clients.Where(k => k.PhoneNumber == DataRuller.LoginID).FirstOrDefault();

        PorfileNameLabel.Text = $"{Data.Name} {Data.SecondName} {Data.SureName}";

        StachData = DataRuller.DataBaseContext.Staches.Include(p => p.LocalStachs).ThenInclude(t => t.Goods).
            Where(k => k.ClientID == DataRuller.LoginID).ToObservableCollection();

        double _tempTotal = 0.0;
        foreach (StachModel stache in StachData)
        {
            _tempTotal += stache.TotalAmount;
        }

        loyalityLoader(Data.Points);

        TotalPointsLabel.Text = $"{Math.Round(Data.Points, 2)} pts.";
        TotalSellsAmount.Text = $"{Math.Round(_tempTotal, 2)} Р.";

        BindableLayout.SetItemsSource(StashFlexGrid, StachData);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        StachModel ss = (StachModel)e.Parameter;

        StachDetail DetailPopup = new(ss.LocalStachs, ss.Status, ss.Data);
        this.ShowPopup(DetailPopup);

    }
}