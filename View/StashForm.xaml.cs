using DreamStuffApp.CustomTypes;
using DreamStuffApp.DataControllers;

namespace DreamStuffApp.View;

public partial class StashForm : ContentPage
{
    private readonly IDataRuller DataRuller = DependencyService.Get<IDataRuller>(DependencyFetchTarget.GlobalInstance);
    public StashForm()
    {
        InitializeComponent();
        Loaded += StashForm_Loaded;
        NavigatedTo += StashForm_NavigatedTo;
        NavigatingFrom += StashForm_NavigatingFrom;
    }

    private void StashForm_NavigatingFrom(object sender, NavigatingFromEventArgs e)
    {
    }

    private void StashForm_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        DataUploader();
    }

    private void StashForm_Loaded(object sender, EventArgs e)
    {
        DataUploader();
    }

    private void DataUploader()
    {
        List<StashesInfo> data = DataRuller.LocalStach.Values.ToList();

        double TotalPrice = data.Sum(x => x.Price * x.Count);

        double TotalPriceWithDiscount = data.Sum(x => x.Price * x.Count) * (1 - DataRuller.Discount);

        TotalPriceLabel.Text = $"{TotalPrice} Р.";
        TotalPriceWithDiscountLabel.Text = $"{Math.Round(TotalPriceWithDiscount, 0)} Р.";


        BindableLayout.SetItemsSource(StashFlexGrid, data);

    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        StashesInfo SelectedGoods = (StashesInfo)e.Parameter;

        int amount = DataRuller.DataBaseContext.Goods.Where(x => x.Good_ID == SelectedGoods.Good_ID).FirstOrDefault().Amount;

        await ((Border)sender).FadeTo(0.0, 100, Easing.Linear);

        DataRuller.LocalStach[SelectedGoods.Good_ID].Count += 1;
        if (amount < DataRuller.LocalStach[SelectedGoods.Good_ID].Count)
        {
            SelectedGoods.Overwalming = "Столько товара нет в наличии";
        }
        else
        {
            SelectedGoods.Overwalming = string.Empty;
        }
        DataUploader();

        await ((Border)sender).FadeTo(1.0, 100, Easing.Linear);
    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        StashesInfo SelectedGoods = (StashesInfo)e.Parameter;
        int amount = DataRuller.DataBaseContext.Goods.Where(x => x.Good_ID == SelectedGoods.Good_ID).FirstOrDefault().Amount;

        await ((Border)sender).FadeTo(0.0, 100, Easing.Linear);
        if (SelectedGoods.Count - 1 == 0)
        {
            DataRuller.LocalStach.Remove(SelectedGoods.Good_ID);
        }
        else
        {
            DataRuller.LocalStach[SelectedGoods.Good_ID].Count -= 1;
            if (amount < DataRuller.LocalStach[SelectedGoods.Good_ID].Count)
            {
                SelectedGoods.Overwalming = "Столько товара нет в наличии";
            }
            else
            {
                SelectedGoods.Overwalming = string.Empty;
            }
        }
        DataUploader();
        await ((Border)sender).FadeTo(1.0, 100, Easing.Linear);

    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        long CheckID = DateTime.Now.Ticks / 2;



        if (DataRuller.LocalStach.Keys.Count > 0)
        {


            DataRuller.DataBaseContext.Staches.Add(new Model.StachModel
            {
                ClientID = DataRuller.LoginID,
                StachID = CheckID,
                TotalAmount = 0,
                Data = DateTime.Now.ToString("dd.MM.yyyy"),
                Status = "Оформляется"
            });

            List<StashesInfo> Data = DataRuller.LocalStach.Values.ToList();

            foreach (StashesInfo item in Data)
            {
                DataRuller.DataBaseContext.LocalStaches.Add(
                    new Model.LocalStachModel
                    {
                        Count = item.Count,
                        Goods_ID = item.Good_ID,
                        StachID = CheckID,
                    });
            }
            DataRuller.DataBaseContext.SaveChanges();
            Model.StachModel _temp = DataRuller.DataBaseContext.Staches.Where(x => x.StachID == CheckID).FirstOrDefault();
            _temp.Status = "Готов к выдаче!";

            double TA = Convert.ToDouble(TotalPriceLabel.Text.Trim('Р', '.'));
            double TAW = Convert.ToDouble(TotalPriceWithDiscountLabel.Text.Trim('Р', '.'));
            _temp.TotalAmount = TA;
            _temp.TotalPriceWithSell = TAW;
            DataRuller.DataBaseContext.Staches.Update(_temp);
            DataRuller.LocalStach.Clear();

            Model.ClientModel _tempClient = DataRuller.DataBaseContext.Clients.Where(x => x.PhoneNumber == DataRuller.LoginID).FirstOrDefault();

            _tempClient.Points += new LoyalityModel().RubToPTS(TAW);

            DataRuller.DataBaseContext.Clients.Update(_tempClient);

            DataRuller.DataBaseContext.SaveChanges();


            await DisplayAlert("Обработка заказа", "Заказ успешно обработан!", "Ок");

            DataUploader();

        }
    }
}