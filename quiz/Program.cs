using System;
using System.Collections.Generic;
using System.IO;

namespace IrregularVerbsQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Dictionary<string, string[]> verbs = LoadVerbsFromFile(@"Irregular_verbs.txt");

            
            List<KeyValuePair<string, string[]>> quizVerbs = GetQuizVerbs(verbs);

            
            int score = RunQuiz(quizVerbs);

           
            Console.WriteLine("Twój wynik: " + score + " / " + quizVerbs.Count);
            Console.ReadLine();

        }

        static Dictionary<string, string[]> LoadVerbsFromFile(string fileName)
        {
            Dictionary<string, string[]> verbs = new Dictionary<string, string[]>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine().ToLower();
                    string[] parts = line.Split('\t');
                    if (parts.Length == 4)
                    {
                        verbs[parts[0]] = new string[] { parts[1], parts[2], parts[3] };
                    }
                }
            }

            return verbs;
        }

        static List<KeyValuePair<string, string[]>> GetQuizVerbs(Dictionary<string, string[]> verbs)
        {
            List<KeyValuePair<string, string[]>> quizVerbs = new List<KeyValuePair<string, string[]>>();
            List<string> keys = new List<string>(verbs.Keys);

            Random rnd = new Random();
            for (int i = 0; i < 15; i++)
            {
                int index = rnd.Next(keys.Count);
                string key = keys[index];
                quizVerbs.Add(new KeyValuePair<string, string[]>(key, verbs[key]));
                keys.RemoveAt(index);
            }

            return quizVerbs;
        }

        static int RunQuiz(List<KeyValuePair<string, string[]>> quizVerbs)
        {
            int score = 0;

            foreach (var verb in quizVerbs)
            {
                Console.WriteLine("Podaj formy dla czasownika " + verb.Key + ":");
                Console.WriteLine("infinitive: " + verb.Key);

                Console.Write("past simple: ");
                string pastSimple = Console.ReadLine();
                if (pastSimple == verb.Value[0])
                {
                    score++;
                }

                Console.Write("past participle: ");
                string pastParticiple = Console.ReadLine();
                if (pastParticiple == verb.Value[1])
                {
                    score++;
                }

                Console.Write("polish translation: ");
                string polishTranslation = Console.ReadLine();
                if (polishTranslation == verb.Value[2])
                {
                    score++;
                }

                if (pastSimple != verb.Value[0] || pastParticiple != verb.Value[1] || polishTranslation != verb.Value[2])
                {
                    Console.WriteLine("Poprawne odpowiedzi to: " + verb.Value[0] + ", " + verb.Value[1] + " i " + verb.Value[2]);
                }
                Console.WriteLine();
            }

            return score;
        }
    }
}

