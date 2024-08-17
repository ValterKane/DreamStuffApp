using System.ComponentModel.DataAnnotations.Schema;
namespace DreamStuffApp.Models
{
    [Table("Types")]
    public class TypesModel
    {
        public int Key { get; set; }
        public string Name { get; set; }
        public List<GoodsModel> Goods { get; set; } = new List<GoodsModel>();
    }
}
