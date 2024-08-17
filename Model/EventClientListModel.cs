using System.ComponentModel.DataAnnotations.Schema;

namespace DreamStuffApp.Model
{
    [Table("EventClientList")]
    public class EventClientListModel
    {
        public int Event_ID { get; set; }
        public string ClientID { get; set; }
        public string Status { get; set; }

        public ClientModel Clients_Navigation { get; set; }
        public EventModel Events_Navigation { get; set; }

    }
}
