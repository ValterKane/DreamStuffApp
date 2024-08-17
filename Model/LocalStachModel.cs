using DreamStuffApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamStuffApp.Model
{
    [Table("LocalStach")]
    public class LocalStachModel
    {
        public long StachID { get; set; }
        public int Goods_ID { get; set; }
        public int Count { get; set; }
        public GoodsModel Goods { get; set; }
        public StachModel Stachs { get; set; }
    }
}
