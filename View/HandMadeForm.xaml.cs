using DreamStuffApp.DataControllers;
using DreamStuffApp.Models;

namespace DreamStuffApp.View;

public partial class HandMadeForm : ContentPage
{
    private readonly IDataRuller DataRuller = DependencyService.Get<IDataRuller>(DependencyFetchTarget.GlobalInstance);
    public HandMadeForm()
    {
        InitializeComponent();
        LoadMerch();
        MerchPicker.SelectedIndexChanged += MerchPicker_SelectedIndexChanged;
    }

    private void MerchPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        MerchImage.Source = (MerchPicker.SelectedItem as GoodsModel).ImageURL;
    }

    private void LoadMerch()
    {
        List<GoodsModel> Data = DataRuller.DataBaseContext.Goods.Where(p => p.TypeOf == 8).ToList();
        MerchPicker.ItemsSource = Data;
        MerchPicker.SelectedIndex = 0;

    }

    private async void TakeAPhoto_Clicked(object sender, EventArgs e)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                using Stream sourceStream = await photo.OpenReadAsync();
                SelectedImage.Source = ImageSource.FromFile(photo.FullPath);
            }
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (SelectedImage.Source != null)
        {
            DataRuller.AddStachData(MerchPicker.SelectedItem as GoodsModel, 1);
        }
    }
}