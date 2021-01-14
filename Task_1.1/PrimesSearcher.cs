using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Library;
using System.Text.Json;
using System.Diagnostics;

namespace Task_1._1
{
    class PrimesSearcher
    {
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        static void Main(string[] args)
        {
            var start = DateTime.Now;
            bool success = true;
            string error = "null";
            TimeSpan duration;
            var primes = new List<int>();
   
            try
            {
                var settingsPath = "settings.json";
                var settings = Deserialize(settingsPath);

                 primes = FindPrimes(settings.PrimesFrom, settings.PrimesTo);

            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("File not found!");
                success = false;
                error = "FileNotFoundException";
            }
            catch (JsonException)
            {
                Debug.WriteLine("Settings file is corrupted!");
                success = false;
                error = "JsonException";
            }

            duration = DateTime.Now - start;

            Serialize("result.json", new Result(success, error, duration, primes));

        }

        private static List<int> FindPrimes(int left, int right)
        {
            if (left > right || left < 0) return new List<int>();

            bool is_Simple;
            var primes = new List<int>(right - left);
            
            for (int i = left; i <= right; i++)
            {
                is_Simple = true;
                if (i <= 1) continue;

                for (int j = 2; j <= Math.Round(Math.Sqrt(i)); j++)
                {
                    if (i % j == 0)
                    {
                        is_Simple = false;
                        break;
                    }
                }
                if (is_Simple) primes.Add(i);
            }
            primes.TrimExcess();

            return primes;
        }

        private static void Serialize(string jsonPath, Result result)
        {            
            var json = JsonSerializer.Serialize(result, options);
            FileWorker.Write("result.json", json);
        }

        private static Settings Deserialize(string jsonPath)
        {
            var json = FileWorker.Read(jsonPath);
            var settings = JsonSerializer.Deserialize<Settings>(json, options);
            return settings;
        }
    }
}
