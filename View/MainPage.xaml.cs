using CommunityToolkit.Maui.Views;
using DreamStuffApp.CustomTypes;
using DreamStuffApp.DataControllers;
using DreamStuffApp.Model;
using DreamStuffApp.Models;
using DreamStuffApp.View.Popups;
using Microsoft.EntityFrameworkCore;

namespace DreamStuffApp.View;

public partial class MainPage : ContentPage
{
    private readonly IDataRuller DataRuller = DependencyService.Get<IDataRuller>(DependencyFetchTarget.GlobalInstance);
    private List<GoodsModel> Goods = new();

    public MainPage()
    {

        InitializeComponent();
        Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        LoadButtonOfTypes();
        DataLoadAsync(7);
        CallPersonalAnalyzer();
        LoadEventsList();
    }

    private void LoadEventsList()
    {
        DateTime ToDay = DateTime.Now.Date;
        DataRuller.DataBaseContext.Events.Load();
        IEnumerable<EventModel> EventListSource = DataRuller.DataBaseContext.Events.Local.Where(x => x.Data > ToDay);
        BindableLayout.SetItemsSource(EventsList, EventListSource);
    }

    private void LoadButtonOfTypes()
    {
        DataRuller.DataBaseContext.Types.Load();
        ButtonNaviZone.ItemsSource = DataRuller.DataBaseContext.Types.Local.ToList();
    }


    private void DataLoadAsync(int TypeOf)
    {
        Goods = DataRuller.DataBaseContext.Goods.Where(p => p.Amount > 0 && p.TypeOf == TypeOf).ToList();
        BindableLayout.SetItemsSource(GoodsFlexGrid, Goods);
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Image SelectedImage = (Image)sender;
        await SelectedImage.FadeTo(0.0, 200, Easing.Linear);
        await SelectedImage.FadeTo(1.0, 200, Easing.Linear);

        GoodsModel CurrentGood = (GoodsModel)e.Parameter;

        if (CurrentGood != null)
        {
            DataRuller.AddStachData(CurrentGood, 1);
        }
    }

    private void CallPersonalAnalyzer()
    {
        PersonalAnalyzer pa = new(DataRuller);
        BindableLayout.SetItemsSource(PersonalGoods, pa.Forecast(5));

    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        await ((Button)sender).FadeTo(0.0, 100, Easing.Linear);
        await ((Button)sender).FadeTo(1.0, 100, Easing.Linear);
        TypesModel param = e.Parameter as TypesModel;
        DataLoadAsync(param.Key);
    }

    private async void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
    {
        await ((Button)sender).FadeTo(0.0, 100, Easing.Linear);
        await ((Button)sender).FadeTo(1.0, 100, Easing.Linear);
        EventModel _CurrentEvent = (EventModel)e.Parameter;
        bool answer = await DisplayAlert("������������� �������", $"�� ������ ���������� �� �������\n{_CurrentEvent.Name},\n������� �������:\n{_CurrentEvent.Data}", "��", "���");
        if (answer)
        {
            EventClientListModel _temp = new()
            {
                ClientID = DataRuller.LoginID,
                Event_ID = _CurrentEvent.ID,
                Status = "��������"
            };
            try
            {
                DataRuller.DataBaseContext.EventClients.Add(_temp);
            }
            catch (Exception ex)
            {
                await DisplayAlert("������ �� �������", $"�� ��� �������� �� ��� �������!", "��");
            }

            DataRuller.DataBaseContext.SaveChanges();
        }

    }

    private void TapGestureRecognizer_Tapped_3(object sender, TappedEventArgs e)
    {
        EventModel ss = (EventModel)e.Parameter;

        EventDetail DetailPopup = new(ss.Name, ss.Data.Date.ToShortDateString(), ss.Data.Date.ToShortTimeString(), ss.ID);
        this.ShowPopup(DetailPopup);
    }
}