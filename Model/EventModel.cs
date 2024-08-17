using System.ComponentModel.DataAnnotations.Schema;

namespace DreamStuffApp.Model
{
    [Table("Events")]
    public class EventModel
    {
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public string ImageURL { get; set; }
        public List<EventClientListModel> ClientList { get; set; } = new List<EventClientListModel>();
    }
}
