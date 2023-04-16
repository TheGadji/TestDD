using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите путь к входному текстовому файлу:");  //C:\Война_и_мир.txt
        string inputFile = Console.ReadLine();
        Console.WriteLine("Введите путь к выходному текстовому файлу:"); //C:\Результат.txt
        string outputFile = Console.ReadLine();

        try
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in words)
                    {
                        if (wordCount.ContainsKey(word))
                        {
                            wordCount[word]++;
                        }
                        else
                        {
                            wordCount[word] = 1;
                        }
                    }
                }
            }

            // Сортировка слов по убыванию
            var sortedWords = wordCount.OrderByDescending(pair => pair.Value);

            // Запись отсортированных слов и их количества в выходной файл
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var word in sortedWords)
                {
                    writer.WriteLine($"{word.Key}\t\t{word.Value}");
                }
            }

            Console.WriteLine("Результат записан в выходной файл !");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.ReadLine();
    }
}
