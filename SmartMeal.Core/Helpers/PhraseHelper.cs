using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Helpers
{
    public static class PhraseHelper
    {

        public static string GetGradePhrase(int grade)
        {
            string Phrase = String.Empty;

            if (grade >= 90)
            {
                Phrase = "A";
            }
            else if(grade >=80 && grade <= 89)
            {
                Phrase = "B";
            }
            else if (grade >= 70 && grade <= 79)
            {
                Phrase = "C";
            }
            else if (grade >= 60 && grade <= 69)
            {
                Phrase = "D";
            }
            else if (grade >= 0 && grade <= 59)
            {
                Phrase = "F";
            }
            else if (grade == 0)
            {
                Phrase = "Not Graded";
            }

            return Phrase;
        }
    }
}
