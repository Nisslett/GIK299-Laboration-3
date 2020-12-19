using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace Lab3
{
    ///<summary>Klassen tillhör laboration 2 som var gjord för en labb i GIK299 2020-12-04
    /// Denna klass återanvänds nu för att användas för laboration 3</summary>
    class Program
    {
        private static string JsonFile = "PersonList.json";
        private static string BinaryFile = "PersonList.bin";
        private static List<Person> personList;
        static void Main(string[] args)
        {
            Menu();
        }

        ///<summary>Denna motod skapar en menyn för programmet visar de funktioner programmet kan köra</summary>
        private static void Menu()
        {
            if (File.Exists(BinaryFile))
            {
                personList = LoadFromBinaryFile();
            }
            else if (File.Exists(JsonFile))
            {
                personList = LoadFromJson();
            }
            else
            {
                personList = new List<Person>();
            }
            char choice = '\0';
            Console.WriteLine("Laboration 3 - Nils Broberg");
            while (choice != 'A')
            {
                Console.WriteLine("{0}\n\t\tMeny", new string('-', 60));
                Console.WriteLine("1. Skriv ut Multiplikationstabeller.");
                Console.WriteLine("2. Skapa en Heltalsarray med användar definerade värden.");
                Console.WriteLine("3. Skapa en Heltalsarray med slumpade värden.");
                Console.WriteLine("4. Laboration 1 - Skriv in ett antal personer och gör beräkningar på deras åldrar.");
                Console.WriteLine("5. Läggtill Person i PersonListan.");
                Console.WriteLine("6. Skriv ut Personer PersonListan.");
                Console.WriteLine("7. Ladda in PersonListan från fil.");
                Console.WriteLine("8. Spara PersonListan i fil.");
                Console.WriteLine("9. Skriv ut Fibonacci nummer up till 2500.");
                Console.WriteLine("A. Avsluta");
                Console.Write("Skriv in ditt menyval:");
                //Använde en char för att jag antog att det skulle gå snabbare att jämnföra chars än strings, prestand mässigt
                choice = char.ToUpper(ReadChar());
                switch (choice)
                {
                    case '1':
                        PrintMultiplicationTables(9);
                        break;
                    case '2':
                        UserInputArray();
                        break;
                    case '3':
                        RandomArray();
                        break;
                    case '4':
                        Laboration1();
                        break;
                    case '5':
                        AddPersonToList();
                        break;
                    case '6':
                        PrintPersonList();
                        break;
                    case '7':
                        LoadInFromFileMenu();
                        break;
                    case '8':
                        SaveToFileMenu();
                        break;
                    case '9':
                        Feb(2500);
                        break;
                    case 'A':
                        Console.WriteLine("\nMenyn Avsultas.");
                        break;
                    default:
                        Console.WriteLine("\n{0} är inte ett giltigt val!", choice);
                        break;
                }
            }
            if (File.Exists(BinaryFile))
            {
                SaveToBinaryFile(personList);
            }
            else if (File.Exists(JsonFile))
            {
                SaveToJson(personList);
            }
            else
            {
                SaveToBinaryFile(personList);
            }
        }
        ///<summary>Denna metod skriver ut multiplikations tabeller från 1 till det värdet som skickas in som input</summary>
        ///<param name="numTables"><c>numTables</c> värdet förväntas vara 1 eller större</param>
        ///<value><c>numTables</c> värdet förväntas vara 1 eller större</value>
        private static void PrintMultiplicationTables(int numTables)
        {
            Console.WriteLine("Skriver ut Multiplikationstabeller 1 till {0}", numTables);
            for (int i = 1; i <= numTables; i++)
            {
                Console.WriteLine("Tabell {0}", i);
                for (int j = 1; j <= 10; j++)
                {
                    Console.Write("{0}x{1}={2}  ", i, j, i * j);
                }
                Console.Write("\n");
            }
        }
        ///<summary>Denna metod skapar en användar definerad heltsarray med valfri storlek.
        ///Summan av talen i arrayen räknas sedan ut samt att min och max värde identifieras.
        ///</summary>
        private static void UserInputArray()
        {
            Console.WriteLine("Skriv in antalet tal som arrayen ska innehålla:");
            int arraySize = ReadPositiveInteger();
            int[] heltalsArray = new int[arraySize];
            int max = Int32.MinValue, min = Int32.MaxValue, sum = 0;
            for (int i = 0; i < heltalsArray.Length; i++)
            {
                Console.WriteLine("Skriv in tal {0}:", i + 1);
                heltalsArray[i] = ReadInteger();
                sum += heltalsArray[i];
                if (heltalsArray[i] > max)
                {
                    max = heltalsArray[i];
                }
                if (heltalsArray[i] < min)
                {
                    min = heltalsArray[i];
                }
            }
            //avrundade avrageValue till två decimaler för att göra värdet mer överskådligt
            double averageValue = Math.Round((double)sum / arraySize, 2);
            Console.WriteLine("Summan av alla tal är {0}, medelvärdet är {1}.", sum, averageValue);
            Console.WriteLine("Största värdet i arrayen är {0} samt minsta värdet är {1}", max, min);
        }

        ///<summary>Denna metod skapar en heltasarray med slumpade värden.
        ///Arrayen sorteras sedan och presenterar resulatet.</summary>
        private static void RandomArray()
        {
            Console.WriteLine("Skriv in antalet tal som arrayen med slumpade tal ska innehålla:");
            int arraySize = ReadPositiveInteger();
            int[] heltalsArray = new int[arraySize];
            Console.WriteLine("Skriv in maxvärde för de slumpade talen som inte får över skridas(värdet får inte vara större än Int32.MaxValue({0})):", Int32.MaxValue);
            int max = ReadPositiveInteger();
            Random rand = new Random();
            Console.WriteLine("Fyller arrayen med slumpade värden.");
            //Skriver ut arrayen osorterad.
            for (int i = 0; i < heltalsArray.Length; i++)
            {
                heltalsArray[i] = rand.Next(max);
                Console.WriteLine("Tal{0}:\t{1}", i + 1, heltalsArray[i]);
            }
            Console.WriteLine("Sorterar arrayen.\nSkriver ut den sorterade arrayen.");
            Array.Sort(heltalsArray);
            //Skriver ut arrayen sorterad.
            for (int i = 0; i < heltalsArray.Length; i++)
            {
                Console.WriteLine("Tal{0}:\t{1}", i + 1, heltalsArray[i]);
            }
        }

        ///<summary>Denna metod gör labboration 1 med arrayer</summary>
        private static void Laboration1()
        {
            Console.WriteLine("Skriv antalet Personer som du vill mata in.");
            int numberOfPeople = ReadPositiveInteger();
            string[] nameArray = new string[numberOfPeople];
            int[] ageArray = new int[numberOfPeople];
            for (int i = 0; i < nameArray.Length; i++)
            {
                Console.WriteLine("Skriv namnet på person {0}:", (i + 1));
                nameArray[i] = Console.ReadLine();
                Console.WriteLine("Skriv åldern på person {0}:", (i + 1));
                ageArray[i] = ReadPositiveInteger();
            }
            int sum = 0, youngest = 0, oldest = 0;
            for (int i = 0; i < nameArray.Length; i++)
            {
                Console.WriteLine("{0} är {1} år gammal, har levt i {2} dagar.", nameArray[i], ageArray[i], 365 * ageArray[i]);
                sum += ageArray[i];
                if (ageArray[i] > ageArray[oldest])
                {
                    oldest = i;
                }
                if (ageArray[i] < ageArray[youngest])
                {
                    youngest = i;
                }
            }
            Console.WriteLine("Sammanlagd ålder är: {0}", sum);
            double averageAge = Math.Round((double)sum / numberOfPeople, 2);
            Console.WriteLine("Medelåldern är: {0}", averageAge);
            Console.WriteLine("{0}({1} år) är äldst och {2}({3} år) är yngst.", nameArray[oldest], ageArray[oldest], nameArray[youngest], ageArray[youngest]);
        }

        ///<summary>Denna metod hanterar input av ett heltal och loop om användarens input inte är giltigt.</summary>
        ///<example><code>int a=ReadInteger();</code></example>
        ///<returns>En int(heltal) som matats in av användaren</returns>
        private static int ReadInteger()
        {
            int resultat = 0;
            while (!int.TryParse(Console.ReadLine(), out resultat))
            {
                Console.WriteLine("Felaktigt inmatning prova igen.");
            }
            return resultat;
        }

        ///<summary>Denna metod hanterar input av ett heltal och loop om användarens input inte är giltigt med avsenade på att heltalet ska vara positivt.
        ///<example><code>int a=ReadPositiveInteger();</code></example>
        ///</summary>
        ///<returns>En positiv int(heltal) som matats in av användaren som är större än 0</returns>
        private static int ReadPositiveInteger()
        {
            int resultat = 0;
            while (true)
            {
                resultat = ReadInteger();
                if (resultat > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Felaktigt inmatning. Inmatningen måste vara större än 0.");
                }
            }
            return resultat;
        }

        private static char ReadChar()
        {
            char resultat;
            while (!char.TryParse(Console.ReadLine(), out resultat))
            {
                Console.WriteLine("Felaktigt inmatning prova igen.");
            }
            return resultat;
        }

        private static DateTime ReadDateTime()
        {
            DateTime resultat;
            while (!DateTime.TryParse(Console.ReadLine(), out resultat))
            {
                Console.WriteLine("Felaktigt inmatning prova igen.");
            }
            return resultat;
        }
        private static float ReadFloat()
        {
            float resultat;
            while (!float.TryParse(Console.ReadLine(), out resultat))
            {
                Console.WriteLine("Felaktigt inmatning prova igen.");
            }
            return resultat;
        }

        private static void AddPersonToList()
        {
            Console.WriteLine("Skriv in personens namn:");
            string name = Console.ReadLine();
            Console.WriteLine("Skriv in personens födelsedag[YYYY-MM-DD , YYYY/MM/DD]:");
            DateTime bday = ReadDateTime();
            Console.WriteLine("Skriv in personens kön:");
            string gen = Console.ReadLine();
            //Tyvär så fanns det ingen brun färg i listan från ConsoleColor
            Console.WriteLine("Skriv in personens ögonfärg från uravelt i listan:");
            string eyecol = Console.ReadLine();
            Console.WriteLine("Skriv in personens hårfärg från uravelt i listan:");
            string col = Console.ReadLine();
            Console.WriteLine("Skriv in personens hårlängd(cm)float:");
            float len = ReadFloat();
            personList.Add(new Person(name, bday, gen, eyecol, new HairData(col, len)));
        }

        private static void PrintPersonList()
        {
            if (personList.Count == 0)
            {
                Console.WriteLine("PersonListan är tom! Så det finns inget att visa.");
            }
            for (int i = 0; i < personList.Count; i++)
            {
                Console.WriteLine($"Person {i + 1}:\n{personList[i].PrintString()}\n");
            }
        }
        //Bonus uppgiften
        private static void Feb(int limit)
        {
            Console.WriteLine($"Skriver ut Fibonacci nummer up till {limit}");
            List<int> feblist = new List<int>();
            int febnum = 1;
            while (febnum <= limit)
            {
                feblist.Add(febnum);
                if (febnum == 1)
                {
                    febnum++;
                }
                else
                {
                    febnum += feblist[feblist.Count - 2];
                }
            }
            foreach (int item in feblist)
            {
                Console.WriteLine($"{item}");
            }
            Console.WriteLine();
        }

        private static void LoadInFromFileMenu()
        {
            char choice = '\0';
            do
            {
                Console.WriteLine($"{new string('-', 60)}\nIn Laddning Meny");
                Console.WriteLine($"1.Ladda PersonLista från JsonFilen{JsonFile}");
                Console.WriteLine($"2.Ladda PersonLista från BinäraFilen{BinaryFile}");
                Console.WriteLine("3. Återgå till Huvudmenyn");
                Console.Write("Skriv in ditt val:");
                choice = char.ToUpper(ReadChar());
                switch (choice)
                {
                    case '1':
                        personList = LoadFromJson();
                        break;
                    case '2':
                        personList = LoadFromBinaryFile();
                        break;
                    case '3':
                        Console.WriteLine("Återgår till Huvudmenyn");
                        break;
                    default:
                        Console.WriteLine("Odefinerat Alternativ");
                        break;
                }
            } while (choice != '3');
        }
        private static void SaveToFileMenu()
        {
            char choice = '\0';
            do
            {
                Console.WriteLine($"{new string('-', 60)}\nIn Sparnings Meny");
                Console.WriteLine($"1.Spara PersonLista till JsonFilen \"{JsonFile}\"");
                Console.WriteLine($"2.Spara PersonLista till BinäraFilen \"{BinaryFile}\"");
                Console.WriteLine("3. Återgå till Huvudmenyn");
                Console.Write("Skriv in ditt val:");
                choice = char.ToUpper(ReadChar());
                switch (choice)
                {
                    case '1':
                        SaveToJson(personList);
                        break;
                    case '2':
                        SaveToBinaryFile(personList);
                        break;
                    case '3':
                        Console.WriteLine("Återgår till Huvudmenyn");
                        break;
                    default:
                        Console.WriteLine("Odefinerat Alternativ");
                        break;
                }
            } while (choice != '3');
        }
        private static void SaveToJson(List<Person> plist)
        {
            if (plist.Count == 0)
            {
                return;
            }
            string jsonStr = JsonSerializer.Serialize(plist);
            using (StreamWriter sw = File.CreateText(JsonFile))
            {
                sw.WriteLine(jsonStr);
                sw.Close();
            }
        }

        private static List<Person> LoadFromJson()
        {
            if (File.Exists(JsonFile))
            {
                string jsonstr;
                using (StreamReader sr = File.OpenText(JsonFile))
                {
                    jsonstr = sr.ReadToEnd();
                    sr.Close();
                }
                List<Person> dejsonfile = JsonSerializer.Deserialize<List<Person>>(jsonstr);
                Console.WriteLine(jsonstr);
                return dejsonfile;
            }
            else
            {
                return null;
            }
        }

        private static void SaveToBinaryFile(List<Person> plist)
        {
            if (plist.Count == 0)
            {
                return;
            }
            //BinaryFile
            BinaryWriter binarywriter = new BinaryWriter(new FileStream(BinaryFile, FileMode.Create));

            binarywriter.Write(plist.Count); //int
            foreach (Person pitem in plist)
            {
                binarywriter.Write(pitem.Name);//string
                //
                DateTime dt = pitem.Birthday;
                binarywriter.Write(dt.Year);//int
                binarywriter.Write(dt.Month);//int
                binarywriter.Write(dt.Day);//int

                binarywriter.Write(pitem.Gender);//string
                binarywriter.Write(pitem.EyeColor);//string

                HairData hd = pitem.Hair;
                binarywriter.Write(hd.Color);//string
                binarywriter.Write(hd.Lenght);//float
            }
            binarywriter.Flush();
            binarywriter.Close();
        }

        private static List<Person> LoadFromBinaryFile()
        {
            if (File.Exists(BinaryFile))
            {
                BinaryReader binaryreader = new BinaryReader(new FileStream(BinaryFile, FileMode.Open));
                int limit = binaryreader.ReadInt32();
                List<Person> newList = new List<Person>();

                for (int i = 0; i < limit; i++)
                {
                    string name = binaryreader.ReadString();

                    int year = binaryreader.ReadInt32();
                    int month = binaryreader.ReadInt32();
                    int day = binaryreader.ReadInt32();
                    DateTime bday = new DateTime(year, month, day);

                    string gender = binaryreader.ReadString();
                    string eyecolor = binaryreader.ReadString();

                    string haircolor = binaryreader.ReadString();
                    float hairlength = binaryreader.ReadSingle();//Reads in float
                    HairData hair = new HairData(haircolor, hairlength);

                    Person newp = new Person(name, bday, gender, eyecolor, hair);
                    newList.Add(newp);
                }
                binaryreader.Close();
                return newList;
            }
            return null;
        }
    }
}
