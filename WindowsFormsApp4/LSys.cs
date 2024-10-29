

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace WindowsFormsApp4
{
    internal class LSys
    {
        public class ParamsFromFile
        {
            public string axiom { get; set; } //аксиома
            public float rotationAngle { get; set; } //угол поворота
            public float startDirect { get; set; } //начальное направление
            public int maxDepth { get; set; } //максимальная глубина
            public string name { get; set; } //название файла
            public Dictionary<char, string> ruleDict { get; set; }
            public ParamsFromFile(string filePath)
            {
                if (!ReadFromFile(filePath))
                {
                    throw new ArgumentException("Ошибка чтения параметров из файла.");
                }
            }
            private bool ReadFromFile(string filePath)
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length >= 2)
                {
                    axiom = lines[0].Split(' ')[0];
                    rotationAngle = float.Parse(lines[0].Split(' ')[1], CultureInfo.InvariantCulture) * (float)Math.PI / 180;
                    startDirect = GetRotationDirection(lines[0].Split(' ')[2]);
                    maxDepth = Int32.Parse(lines[0].Split(' ')[3]);
                    ruleDict = new Dictionary<char, string>();
                    name = filePath;
                    for (int i = 1; i < lines.Length; i++)
                    {
                        InitializeRules(lines[i].Trim());
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            private void InitializeRules(string ruleString)
            {
                string[] ruleParts = ruleString.Split(' ');
                char symbol = ruleParts[0][0];  // symbol = 'F'
                string replacement = ruleParts[1]; // replacement = "F-G+F+G-F"
                ruleDict[symbol] = replacement; // Добавление в словарь: {'F' -> "F-G+F+G-F"}
            }
            private float GetRotationDirection(string direction)
            {
                if (direction.ToLower() == "up")
                {
                    return (float)(3 * Math.PI / 2); // 3pi/2
                }
                else if (direction.ToLower() == "down")
                {
                    return (float)(Math.PI / 2); //pi/2
                }
                else if (direction.ToLower() == "left")
                {
                    return (float)(Math.PI); // pi
                }
                else if (direction.ToLower() == "right")
                {
                    return 0f; //0
                }
                else
                {
                    throw new ArgumentException("Неверное направление угла поворота.");
                }
            }

        }


    }
}
