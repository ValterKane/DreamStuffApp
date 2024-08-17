using System.ComponentModel.DataAnnotations.Schema;

namespace DreamStuffApp.Model
{
    [Table("Stach")]
    public class StachModel
    {
        public long StachID { get; set; }
        public string ClientID { get; set; }
        public double TotalAmount { get; set; }
        public string Data { get; set; }
        public double TotalPriceWithSell { get; set; }
        public string Status { get; set; }
        public ClientModel Clients { get; set; }
        public List<LocalStachModel> LocalStachs { get; set; }
    }
}
