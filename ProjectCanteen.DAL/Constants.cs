using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCanteen.DAL
{
    public class Constants
    {
        public const int MaxClassNameLength = 20;

        public const int MinTitleLength = 1;
        public const int MaxTitleLength = 100;

        public const int MinDescriptionLength = 1;
        public const int MaxDescriptionLength = 500;

        public const int MinDishNameLength = 1;
        public const int MaxDishNameLength = 50;

        public const decimal MinPriceUAH = 0;
        public const decimal MaxPriceUAH = 10000;
        public const int PriceUAHPrecision = 6;
        public const int PriceUAHScale = 2;

        public const double MinMacronutrients = 0;
        public const double MaxMacronutrients = 100;

        public const int MinCalories = 0;
        public const int MaxCalories= 10000;

        public const int NutritionalValuePerXGrams = 100;

        public const double MinIngredientInDishAmount = 1000;
        public const double MaxIngredientInDishAmount = 1000;

        public const int MinTagLength = 1;
        public const int MaxTagLength = 50;

        public const int MaxNameLength = 50;

        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 20;

        public const int MinEmailLength = 3;
        public const int MaxEmailLength = 50;

        public const int MinNumber = 0;
        public const int MaxNumber = 200;

        public const int MinPortionsCount = 1;
        public const int MaxPortionsCount = 5;


        public static readonly DateTime MinDate = new DateTime(2010, 1, 1);
    }
}
