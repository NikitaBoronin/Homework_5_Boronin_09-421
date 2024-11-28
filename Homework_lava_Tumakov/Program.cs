namespace Homework_lava_Tumakov
{
    internal class Program
    {

        static void Task1(string fileName)
        {
            
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден.");
                return;
            }

            try
            {
                
                string content = File.ReadAllText(fileName);

                
                char[] charArray = content.ToCharArray();

                
                (int vowelsCount, int consonantsCount) = CountVowelsAndConsonants(charArray);

                
                Console.WriteLine($"Гласных букв: {vowelsCount}");
                Console.WriteLine($"Согласных букв: {consonantsCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
            }
        }

        static (int vowels, int consonants) CountVowelsAndConsonants(char[] characters)
        {
            
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y', 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            int vowelsCount = 0;
            int consonantsCount = 0;

            foreach (char c in characters)
            {
                
                char lowerChar = char.ToLower(c);

                if (char.IsLetter(lowerChar)) 
                {
                    if (vowels.Contains(lowerChar))
                    {
                        vowelsCount++; // Гласная
                    }
                    else
                    {
                        consonantsCount++; // Согласная
                    }
                }
            }

            return (vowelsCount, consonantsCount);
        }


        // Упражнение 6.2
        static void Task2WithArray()
        {
            int[,] matrixA = {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };

            int[,] matrixB = {
            { 7, 8 },
            { 9, 10 },
            { 11, 12 }
        };

            try
            {
                
                Console.WriteLine("Matrix A:");
                PrintMatrixArray(matrixA);

                Console.WriteLine("\nMatrix B:");
                PrintMatrixArray(matrixB);

                
                int[,] resultMatrix = MultiplyMatricesArray(matrixA, matrixB);

               
                Console.WriteLine("\nResulting Matrix:");
                PrintMatrixArray(resultMatrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void PrintMatrixArray(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        static int[,] MultiplyMatricesArray(int[,] matrixA, int[,] matrixB)
        {
            if (matrixA.GetLength(1) != matrixB.GetLength(0))
            {
                throw new InvalidOperationException("Матрицы нельзя умножить: число столбцов первой матрицы не равно числу строк второй.");
            }

            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int colsB = matrixB.GetLength(1);

            int[,] resultMatrix = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < colsA; k++)
                    {
                        resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return resultMatrix;
        }


        // Упражнение 6.3
        static void Task3()
        {
            
            int[,] temperature = new int[12, 30];

           
            Random random = new Random();
            for (int month = 0; month < 12; month++)
            {
                for (int day = 0; day < 30; day++)
                {
                    temperature[month, day] = random.Next(-30, 31); 
                }
            }

           
            Console.WriteLine("Температуры по месяцам:");
            for (int month = 0; month < 12; month++)
            {
                Console.Write($"Месяц {month + 1}: ");
                for (int day = 0; day < 30; day++)
                {
                    Console.Write(temperature[month, day] + " ");
                }
                Console.WriteLine();
            }

            
            double[] averageTemperatures = CalculateMonthlyAverages(temperature);

           
            Console.WriteLine("\nСредние температуры по месяцам (до сортировки):");
            for (int month = 0; month < averageTemperatures.Length; month++)
            {
                Console.WriteLine($"Месяц {month + 1}: {averageTemperatures[month]:F2} °C");
            }

            
            SortArray(averageTemperatures);

            
            Console.WriteLine("\nСредние температуры по месяцам (после сортировки):");
            foreach (var temp in averageTemperatures)
            {
                Console.WriteLine($"{temp:F2} °C");
            }
        }

        /// <summary>
        /// Метод вычисления средних температур по месяцам.
        /// </summary>
        /// <param name="temperature">Двумерный массив температур (12 месяцев по 30 дней).</param>
        /// <returns>Массив средних температур по месяцам.</returns>
        static double[] CalculateMonthlyAverages(int[,] temperature)
        {
            double[] averages = new double[12];

            for (int month = 0; month < 12; month++)
            {
                int sum = 0;
                for (int day = 0; day < 30; day++)
                {
                    sum += temperature[month, day];
                }

                averages[month] = sum / 30.0; 
            }

            return averages;
        }

        /// <summary>
        /// Метод сортировки массива.
        /// </summary>
        /// <param name="array">Массив для сортировки.</param>
        static void SortArray(double[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        // Обмен элементов
                        double temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        // Домашнее задание 6.1
        static void Task4(string[] args)
        {
            
            if (args.Length == 0)
            {
                Console.WriteLine("Укажите имя файла как аргумент программы.");
                return;
            }

            string fileName = args[0];

            
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл {fileName} не найден.");
                return;
            }

            try
            {
                
                string content = File.ReadAllText(fileName);

                
                List<char> charList = new List<char>(content);

                
                (int vowelsCount, int consonantsCount) = CountVowelsAndConsonants(charList);

                
                Console.WriteLine($"Гласных букв: {vowelsCount}");
                Console.WriteLine($"Согласных букв: {consonantsCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
            }
        }

        static (int vowels, int consonants) CountVowelsAndConsonants(List<char> characters)
        {
            
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y', 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
            int vowelsCount = 0;
            int consonantsCount = 0;

            foreach (char c in characters)
            {
                
                char lowerChar = char.ToLower(c);

                if (char.IsLetter(lowerChar)) 
                {
                    if (vowels.Contains(lowerChar))
                    {
                        vowelsCount++; 
                    }
                    else
                    {
                        consonantsCount++;
                    }
                }
            }

            return (vowelsCount, consonantsCount);
        }


        static void Task5WithLinkedList()
        {
            LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>(new[]
            {
            new LinkedList<int>(new[] { 1, 2, 3 }),
            new LinkedList<int>(new[] { 4, 5, 6 })
        });

            LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>(new[]
            {
            new LinkedList<int>(new[] { 7, 8 }),
            new LinkedList<int>(new[] { 9, 10 }),
            new LinkedList<int>(new[] { 11, 12 })
        });

            try
            {
                
                Console.WriteLine("Matrix A:");
                PrintMatrixLinkedList(matrixA);

                Console.WriteLine("\nMatrix B:");
                PrintMatrixLinkedList(matrixB);

                
                LinkedList<LinkedList<int>> resultMatrix = MultiplyMatricesLinkedList(matrixA, matrixB);

                
                Console.WriteLine("\nResulting Matrix:");
                PrintMatrixLinkedList(resultMatrix);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void PrintMatrixLinkedList(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (var value in row)
                {
                    Console.Write(value + "\t");
                }
                Console.WriteLine();
            }
        }

        static LinkedList<LinkedList<int>> MultiplyMatricesLinkedList(LinkedList<LinkedList<int>> matrixA, LinkedList<LinkedList<int>> matrixB)
        {
            int colsA = matrixA.First.Value.Count;
            int rowsB = matrixB.Count;

            if (colsA != rowsB)
            {
                throw new InvalidOperationException("Матрицы нельзя умножить: число столбцов первой матрицы не равно числу строк второй.");
            }

            int colsB = matrixB.First.Value.Count;

            LinkedList<LinkedList<int>> resultMatrix = new LinkedList<LinkedList<int>>();
            int[,] matrixBArray = ConvertTo2DArray(matrixB);

            foreach (var rowA in matrixA)
            {
                LinkedList<int> resultRow = new LinkedList<int>();
                for (int j = 0; j < colsB; j++)
                {
                    int value = 0;
                    int index = 0;
                    foreach (var valueA in rowA)
                    {
                        value += valueA * matrixBArray[index, j];
                        index++;
                    }
                    resultRow.AddLast(value);
                }
                resultMatrix.AddLast(resultRow);
            }

            return resultMatrix;
        }

        static int[,] ConvertTo2DArray(LinkedList<LinkedList<int>> matrix)
        {
            int rows = matrix.Count;
            int cols = matrix.First.Value.Count;
            int[,] array = new int[rows, cols];

            int i = 0;
            foreach (var row in matrix)
            {
                int j = 0;
                foreach (var value in row)
                {
                    array[i, j] = value;
                    j++;
                }
                i++;
            }

            return array;
        }


        static void Task6()
        {
            
            string[] monthNames = {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

            
            Dictionary<string, double[]> temperature = new Dictionary<string, double[]>();

            
            Random random = new Random();
            for (int i = 0; i < 12; i++)
            {
                double[] dailyTemperatures = new double[30];
                for (int day = 0; day < 30; day++)
                {
                    dailyTemperatures[day] = random.Next(-30, 31); 
                }
                temperature.Add(monthNames[i], dailyTemperatures);
            }

            
            Console.WriteLine("Температуры по дням (по месяцам):");
            foreach (var entry in temperature)
            {
                Console.Write($"{entry.Key}: ");
                foreach (var temp in entry.Value)
                {
                    Console.Write($"{temp:F1} ");
                }
                Console.WriteLine();
            }

            
            Dictionary<string, double> averageTemperatures = CalculateMonthlyAverages(temperature);

            
            Console.WriteLine("\nСредние температуры по месяцам (до сортировки):");
            foreach (var entry in averageTemperatures)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:F2} °C");
            }

           
            var sortedAverages = SortDictionary(averageTemperatures);

            
            Console.WriteLine("\nСредние температуры по месяцам (после сортировки):");
            foreach (var entry in sortedAverages)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:F2} °C");
            }
        }

        /// <summary>
        /// Вычисление средних температур по месяцам.
        /// </summary>
        /// <param name="temperature">Словарь с температурами по дням для каждого месяца.</param>
        /// <returns>Словарь со средними температурами по месяцам.</returns>
        static Dictionary<string, double> CalculateMonthlyAverages(Dictionary<string, double[]> temperature)
        {
            Dictionary<string, double> averages = new Dictionary<string, double>();

            foreach (var entry in temperature)
            {
                double sum = 0;
                foreach (var temp in entry.Value)
                {
                    sum += temp;
                }
                averages.Add(entry.Key, sum / entry.Value.Length); 
            }

            return averages;
        }

        /// <summary>
        /// Сортировка словаря по значениям (средним температурам) в порядке возрастания.
        /// </summary>
        /// <param name="averageTemperatures">Словарь со средними температурами.</param>
        /// <returns>Отсортированный список пар ключ-значение.</returns>
        static List<KeyValuePair<string, double>> SortDictionary(Dictionary<string, double> averageTemperatures)
        {
            List<KeyValuePair<string, double>> sortedList = new List<KeyValuePair<string, double>>(averageTemperatures);

            
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                for (int j = 0; j < sortedList.Count - i - 1; j++)
                {
                    if (sortedList[j].Value > sortedList[j + 1].Value)
                    {
                        var temp = sortedList[j];
                        sortedList[j] = sortedList[j + 1];
                        sortedList[j + 1] = temp;
                    }
                }
            }

            return sortedList;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Упражнение 6.1\n");
            if (args.Length == 0)
            {
                Console.WriteLine("Укажите имя файла как аргумент программы.");
                return;
            }

            string fileName = args[0];
            Task1(fileName);
            Console.WriteLine("\nУпражнение 6.2\n");
            Task2WithArray();
            Console.WriteLine("\nУпражнение 6.3\n");
            Task3();
            Console.WriteLine("\nДомашнее задание 6.1\n");
            Task4(args);
            Console.WriteLine("\nДомашнее задание 6.2\n");
            Task5WithLinkedList();
            Console.WriteLine("\nДомашнее задание 6.3\n");
            Task6();

        }
    }
}
