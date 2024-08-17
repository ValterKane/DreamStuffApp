using DreamStuffApp.DataControllers;
using DreamStuffApp.Model;
using DreamStuffApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStuffApp.CustomTypes
{
    public class PersonalAnalyzer
    {
        private IDataRuller _Ruller;

        private int mostPopularForPerson = 0;

        public PersonalAnalyzer(IDataRuller Ruller)
        {
            _Ruller = Ruller;
            Analyze();
        }

        private void Analyze()
        {
            List<int> PopularType = new List<int>();
            var TotalyData = _Ruller.DataBaseContext.Staches.Include(Nav => Nav.LocalStachs).ThenInclude(LS_Nav => LS_Nav.Goods).Where(x => x.ClientID == _Ruller.LoginID);
            
            foreach (var item in TotalyData)
            {
                foreach(var Local in  item.LocalStachs)
                {
                    PopularType.Add(Local.Goods.TypeOf);
                }
            }

            foreach (var item in PopularType)
            {
                mostPopularForPerson = (PopularType.Count(x=>x == item) > mostPopularForPerson) ? item : mostPopularForPerson;
            }



        
        }

        public List<GoodsModel> Forecast(int count)
        {
            if (mostPopularForPerson != 0)
            {
                Dictionary<int, GoodsModel> keyValuePairs = new Dictionary<int, GoodsModel>();
                Random rnd = new Random();

                var data = _Ruller.DataBaseContext.Goods.Where(x => x.TypeOf == mostPopularForPerson).ToList();


                int i = 0;
                int index = 0;
                while (i < count)
                {
                    do
                    {
                        index = rnd.Next(0, data.Count);
                    }
                    while (!keyValuePairs.TryAdd(index, data[index]));
                    i++;

                }
                return keyValuePairs.Values.ToList();
            }
            return null;
            
        }


    }
}
