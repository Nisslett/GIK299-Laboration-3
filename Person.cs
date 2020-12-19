using System;
namespace Lab3
{
    public class Person
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string EyeColor { get; set; }
        private HairData privHair;
        public HairData Hair
        {
            get
            {
                return privHair;
            }
            set
            {
                privHair = value;
            }
        }

        public Person() : this("Jane Doe", DateTime.Today, "Kvinna", "Blå", new HairData("Blond", 20.0f)) { }
        public Person(string name, DateTime birthday, string gender, string eyecolor, HairData hdata)
        {
            Name = name;
            Birthday = birthday;
            Gender = gender;
            EyeColor = eyecolor;
            privHair = hdata;
        }
        public HairData GetHair()
        {
            return privHair;
        }
        public void SetHair(HairData h)
        {
            privHair = h;
        }

        public string PrintString()
        {
            return $"Namn:{Name}\nKön:{Gender}\nÖgonfärg:{EyeColor}\nFödelsedag:{Birthday.ToString().Split(" ")[0]}\nHår\n\tHårlängd:{privHair.Lenght} cm\n\t {privHair.Color}";
        }

        public override string ToString()
        {
            return string.Format($"Name:{Name},BirthDay:{Birthday.ToString().Split(" ")[0]},Gender:{Gender},EyeColor:{EyeColor},Hair:({privHair})");
        }
        //Static Methods
        // public static void ConsolePrintPerson(Person p)
        // {
        //     Console.Write($"Namn:{p.Name}\nKön:{p.Gender}\nÖgonfärg:{p.EyeColor}\nFödelsedag:{p.Birthday.ToString().Split(" ")[0]}\nHår\n\tHårlängd:{p.GetHair().Lenght} cm\n\t {p.GetHair().Color}");
        // }
    }
}