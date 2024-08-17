using CommunityToolkit.Maui.Views;
using DreamStuffApp.CustomTypes;
using DreamStuffApp.DataControllers;
using DreamStuffApp.Model;
using Microsoft.EntityFrameworkCore;

namespace DreamStuffApp.View.Popups;

public partial class EventDetail : Popup
{
    private IDataRuller DataRuller = DependencyService.Get<IDataRuller>(DependencyFetchTarget.GlobalInstance);
    public EventDetail(string Name, string Date, string Time, int EventIndex)
	{
		InitializeComponent();
		EventName.Text = Name;
		EventDate.Text = Date;
		EventTime.Text = Time;
		DataLoad(EventIndex);
	}

	public void DataLoad(int EventID)
	{
		var ListOfClients = DataRuller.DataBaseContext.EventClients.Include(x => x.Clients_Navigation).Where(x => x.Event_ID == EventID).ToList();

		List<ListOfClientOnEvent> listOfClientOnEvents = new List<ListOfClientOnEvent>();

        foreach (var item in ListOfClients)
        {
			listOfClientOnEvents.Add(new ListOfClientOnEvent
			{
				Name = item.Clients_Navigation.Name,
				SecondName = item.Clients_Navigation.SecondName,
				LastName = item.Clients_Navigation.SureName,
				Status = item.Status,
			});
        }
        BindableLayout.SetItemsSource(ClientStatus, listOfClientOnEvents);
	}


}