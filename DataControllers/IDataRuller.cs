using DreamStuffApp.CustomTypes;
using DreamStuffApp.Models;

namespace DreamStuffApp.DataControllers
{
    public interface IDataRuller
    {
        public Context DataBaseContext { get; set; }

        public string LoginID { get; set; }

        public double Discount { get; set; }

        public Dictionary<int, StashesInfo> LocalStach { get; set; }

        public void AddStachData(GoodsModel GS, int count);

    }
}
