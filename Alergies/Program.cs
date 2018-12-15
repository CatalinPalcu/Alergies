using System;
using System.Text;

namespace Allergies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AllergiesTest tom = new AllergiesTest("Tom", 173);
            Console.WriteLine(tom);
            Console.WriteLine("\n\tModify list\n");
            tom.AddAllergy(AllergiesTest.AllergiesList.Pollen);
            tom.AddAllergy(AllergiesTest.AllergiesList.Eggs);
            tom.RemoveAllergy(AllergiesTest.AllergiesList.Chocolate);
            Console.WriteLine(tom);
        }
    }
    class AllergiesTest
    {
        public enum AllergiesList
        {
            Eggs,
            Peanuts,
            Shellfish,
            Strawberries,
            Tomatoes,
            Chocolate,
            Pollen,
            Cats
        }

        private static readonly int NR_ALLERGIES = 8;

        private bool[] allergies;
        
        public string Patient { get; set; }

        public int Score
        {
            get
            {
                int score = 0;
                int nr = 1;
                for (int i = 0; i < NR_ALLERGIES; i++)
                {
                    if (allergies[i])
                        score += nr;
                    nr *= 2;
                }

                return score;
            }
        }

        public AllergiesTest()
        {
            Patient = "[no name]";
            allergies = new bool[NR_ALLERGIES];
        }

        public AllergiesTest(string patient, int score)
        {
            Patient = patient;
            allergies = new bool[NR_ALLERGIES];

            if (score > 0)
            {
                int index = 0;
                score = score % ((int)Math.Pow(2, NR_ALLERGIES));
                while (score > 0)
                {
                    if (score % 2 == 1)
                        allergies[index] = true;
                    index++;
                    score = score / 2;
                }
            }
        }

        public void AddAllergy(AllergiesList allergy)
        {
            int index = (int)allergy;
            if (allergies[index])
                Console.WriteLine($"{Patient} was already allergic to {allergy.ToString()}");
            else
                allergies[index] = true;
        }

        public void RemoveAllergy (AllergiesList allergy)
        {
            int index = (int)allergy;
            if (!allergies[index])
                Console.WriteLine($"{Patient} was not allergic to {allergy.ToString()}");
            else
                allergies[index] = false;
        }

        public bool IsAllergic()
        {
            int i = 0;
            while (i < NR_ALLERGIES)
            {
                if (allergies[i])
                    return true;
                i++;
            }
            return false;
        }

        public string FullListOfAllergies()
        {
            StringBuilder fullList = new StringBuilder();
            int i = 0;
            while (i < NR_ALLERGIES)
            {
                if (allergies[i])
                    fullList.Append(String.Format($"\n\t- {((AllergiesList)i).ToString()}"));
                i++;
            }

            return fullList.ToString();
        }

        public override string ToString()
        {
            if (!IsAllergic())
                return String.Format($"{Patient}, with the score 0, is not allergic to any ellement of the list ");
            return String.Format($"{Patient}, with the score {Score}, is allergic to:{FullListOfAllergies()}");
        }
    }
}
