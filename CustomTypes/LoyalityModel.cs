using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamStuffApp.CustomTypes
{
    public class LoyalityModel
    {
        private const float L0_Treshold = 50.0F;
        private const float L1_Treshold = 150.0F;
        private const float L2_Treshold = 400.0F;
        private const float L3_Treshold = 1000.0F;

        private const int MaxSpentValue = 50000;


        public short GetLevelOfLoyality(double PersonalPoints)
        {
            if (PersonalPoints < L0_Treshold)
            {
                return 0;
            }
            else
            {
                if (PersonalPoints >= L0_Treshold && PersonalPoints < L1_Treshold)
                {
                    return 1;
                }
                else
                {
                    if (PersonalPoints >= L1_Treshold && PersonalPoints < L2_Treshold)
                    {
                        return 2;
                    }
                    else
                    {
                        if (PersonalPoints >= L2_Treshold && PersonalPoints < L3_Treshold)
                        {
                            return 3;
                        }
                        else
                        {
                            if (PersonalPoints >= L3_Treshold)
                            {
                                return 4;
                            }
                            else
                            { 
                                return -1;
                            }
                        }
                    }
                }
            }


        }

        public double RubToPTS(double Rub)
        {
            float ptsPerRub = (float)(L3_Treshold / MaxSpentValue);
            return Math.Round((double)(ptsPerRub * Rub),2);
        }

        public double NextLvlPoints(short Lvl)
        {
            switch (Lvl)
            {
                case 0:
                    return L0_Treshold;
                case 1:
                    return L1_Treshold;
                case 2:
                    return L2_Treshold;
                case 3:
                    return L3_Treshold;
                case 4:
                    return L3_Treshold;
            }
            return 0;
        }

        public double DiscountValue(short lvl)
        {
            switch (lvl) {
                case 0:
                    return 0.0;
                case 1:
                    return 0.02;
                case 2:
                    return 0.03;
                case 3:
                    return 0.05;
                case 4:
                    return 0.08;
            }
            return 0.0;
        }
    }
}
