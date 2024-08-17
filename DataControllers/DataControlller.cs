using DreamStuffApp.CustomTypes;
using DreamStuffApp.Model;
using DreamStuffApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStuffApp.DataControllers
{
    public class DataControlller : IDataRuller
    {
        public Context DataBaseContext { get; set; } = new Context();
        public string LoginID { get; set; }

        public Dictionary<int, StashesInfo> LocalStach { get; set; } = new Dictionary<int, StashesInfo>();
        public double Discount { get; set; }

        public void AddStachData(GoodsModel GS, int count)
        {
            StashesInfo StInfo = new StashesInfo()
            {
                Good_ID = GS.Good_ID,
                Count = count,
                Good_Name = GS.Name,
                ImageURL = GS.ImageURL,
                Price = GS.Price,
            };

            if (!LocalStach.ContainsKey(StInfo.Good_ID))
            {
                LocalStach.Add(StInfo.Good_ID, StInfo);
            }
            else 
            {
                LocalStach[StInfo.Good_ID].Count += 1;
            }
        }
    }
}
