using CommunityToolkit.Maui.Views;
using DreamStuffApp.Model;
using DreamStuffApp.Models;

namespace DreamStuffApp.View.Popups;

public partial class StachDetail : Popup
{
	List<LocalStachModel> LocalStaches;
	string Status;
	string Data;
	public StachDetail(List<LocalStachModel> Local, string status, string data)
	{
		InitializeComponent();
		LocalStaches = Local;
		Status = status;
		Data = data;	
		LoadDetailData();

    }

	private void LoadDetailData()
	{
		DataLbl.Text = Data;
		StatusLlb.Text = Status;
		List<GoodsModel> goods = new List<GoodsModel>();
        for(int i = 0; i < LocalStaches.Count; i++)
		{
			goods.Add(LocalStaches[i].Goods);
		}

		DetailCarusel.ItemsSource = goods;

    }
}