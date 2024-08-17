using DreamStuffApp.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamStuffApp.Models
{
    [Table("Goods")]
    public class GoodsModel
    {
        public int Good_ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string MadeIn { get; set; }

        public int Weigth { get; set; }

        public int Amount { get; set; }

        public int TypeOf { get; set; }

        public double Price { get; set; }

        public string ImageURL { get; set; }   

        public TypesModel Types { get; set; }

        public List<LocalStachModel> LocalStachs { get; set; }

    }
}
