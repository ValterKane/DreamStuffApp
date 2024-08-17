using System.ComponentModel.DataAnnotations.Schema;

namespace DreamStuffApp.Model
{
    [Table("Clients")]
    public class ClientModel
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }  
        public string Email { get; set; }   
        public string SureName {  get; set; }
        public double Points { get; set; }
        public string RegDate { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }

        public List<StachModel> Stachs { get; set; } = new List<StachModel>();
        public List<EventClientListModel> Events { get; set; } = new List<EventClientListModel> { };

    }
}
