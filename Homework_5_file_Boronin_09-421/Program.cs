namespace Homework_5_file_Boronin_09_421
{
    internal class Program
    {
        /// <summary>
        /// Метод перемешивает фотографии
        /// </summary>
        /// <param name="list"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        static List<string> ShuffleList(List<string> list, Random random)
        {
            List<string> shuffledList = new List<string>(list);
            for (int i = shuffledList.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // Обмен значениями
                string temp = shuffledList[i];
                shuffledList[i] = shuffledList[j];
                shuffledList[j] = temp;
            }
            return shuffledList;
        }

        static void Task1()
        {
            List<string> images = new List<string>();
            for (int i = 1; i <= 32; i++)
            {
                string imageName = $"Image_{i}";
                images.Add(imageName);
                images.Add(imageName);
            }

            Console.WriteLine("Изначальный список:");
            for (int i = 0; i < images.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {images[i]}");
            }

            Random random = new Random();
            List<string> shuffledImages = ShuffleList(images, random);

            Console.WriteLine("\nПеремешанный список:");
            for (int i = 0; i < shuffledImages.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {shuffledImages[i]}");
            }
        }

        static void Task2()
        {
            string filePath = "students.txt";
            List<Student> students = LoadStudentsFromFile(filePath);

            
            bool isRunning = true;
            while (isRunning) 
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Новый студент");
                Console.WriteLine("2. Удалить");
                Console.WriteLine("3. Сортировать");
                Console.WriteLine("4. Показать всех студентов");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                if (choice == "5") 
                {
                    isRunning = false;
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                            AddNewStudent(students);
                            SaveStudentsToFile(students, filePath);
                            break;

                        case "2":
                            RemoveStudent(students);
                            SaveStudentsToFile(students, filePath);
                            break;

                        case "3":
                            SortStudentsByScore(students);
                            break;

                        case "4":
                            DisplayStudents(students);
                            break;

                        default:
                            Console.WriteLine("Неверный ввод. Попробуйте снова.");
                            break;
                    }
                }
            }
        }

        static void AddNewStudent(List<Student> students)
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Ошибка: фамилия не может быть пустой.");
                return;
            }

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Ошибка: имя не может быть пустым.");
                return;
            }

            Console.Write("Введите год рождения: ");
            if (!int.TryParse(Console.ReadLine(), out int birthYear))
            {
                Console.WriteLine("Ошибка: введён некорректный год рождения.");
                return;
            }

            Console.Write("Введите экзамен: ");
            string exam = Console.ReadLine();

            Console.Write("Введите баллы: ");
            if (!int.TryParse(Console.ReadLine(), out int score))
            {
                Console.WriteLine("Ошибка: введено некорректное количество баллов.");
                return;
            }

            students.Add(new Student
            {
                LastName = lastName,
                FirstName = firstName,
                BirthYear = birthYear,
                Exam = exam,
                Score = score
            });

            Console.WriteLine("Студент добавлен.");
        }

        static void RemoveStudent(List<Student> students)
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Ошибка: фамилия не может быть пустой.");
                return;
            }

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Ошибка: имя не может быть пустым.");
                return;
            }

            
            Student studentToRemove = null;
            foreach (var student in students)
            {
                if (student.LastName == lastName && student.FirstName == firstName)
                {
                    studentToRemove = student;
                    break;
                }
            }

            if (studentToRemove != null)
            {
                students.Remove(studentToRemove);
                Console.WriteLine("Студент удалён.");
            }
            else
            {
                Console.WriteLine("Студент не найден.");
            }
        }

        static void SortStudentsByScore(List<Student> students)
        {
            students.Sort(CompareStudentsByScore);  
            Console.WriteLine("Список отсортирован по баллам.");
        }

        static int CompareStudentsByScore(Student s1, Student s2)
        {
            return s1.Score.CompareTo(s2.Score); // Сравнение по баллам
        }

        static void DisplayStudents(List<Student> students)
        {
            Console.WriteLine("\nСписок студентов:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        static void SaveStudentsToFile(List<Student> students, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.LastName};{student.FirstName};{student.BirthYear};{student.Exam};{student.Score}");
                }
            }
        }

        static List<Student> LoadStudentsFromFile(string filePath)
        {
            var students = new List<Student>();
            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(';');
                    if (parts.Length == 5
                        && int.TryParse(parts[2], out int birthYear)
                        && int.TryParse(parts[4], out int score))
                    {
                        students.Add(new Student
                        {
                            LastName = parts[0],
                            FirstName = parts[1],
                            BirthYear = birthYear,
                            Exam = parts[3],
                            Score = score
                        });
                    }
                }
            }
            return students;
        }

        class Student
        {
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public int BirthYear { get; set; }
            public string Exam { get; set; }
            public int Score { get; set; }

            public override string ToString()
            {
                return $"{LastName} {FirstName}, {BirthYear} год, Экзамен: {Exam}, Баллы: {Score}";
            }
        }

        static void Task3()
        {
            Queue<Grandma> grandmas = new Queue<Grandma>();
            Stack<Hospital> hospitals = new Stack<Hospital>();

            hospitals.Push(new Hospital("Больница №1", new List<string> { "Грипп", "Мигрень" }, 2));
            hospitals.Push(new Hospital("Больница №2", new List<string> { "Кашель", "Грипп", "Гипертония" }, 3));
            hospitals.Push(new Hospital("Больница №3", new List<string> { "Гипертония", "Сахарный диабет" }, 1));

            Console.WriteLine("Введите количество бабуль:");
            if (!int.TryParse(Console.ReadLine(), out int grandmaCount) || grandmaCount <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное целое число.");
                return;
            }

            for (int i = 0; i < grandmaCount; i++)
            {
                Console.WriteLine($"\nВведите имя бабули #{i + 1}:");
                string name = Console.ReadLine();

                Console.WriteLine("Введите возраст бабули:");
                if (!int.TryParse(Console.ReadLine(), out int age) || age <= 0)
                {
                    Console.WriteLine("Ошибка: введите корректный возраст.");
                    i--;
                    continue;
                }

                Console.WriteLine("Введите болезни бабули через запятую (оставьте пустым, если нет):");
                string illnessesInput = Console.ReadLine();
                List<string> illnesses = string.IsNullOrWhiteSpace(illnessesInput) ? new List<string>() : SplitAndTrimIllnesses(illnessesInput);

                grandmas.Enqueue(new Grandma(name, age, illnesses));
            }

            List<Grandma> homelessGrandmas = new List<Grandma>();

            while (grandmas.Count > 0)
            {
                Grandma currentGrandma = grandmas.Dequeue();
                Hospital assignedHospital = null;

               
                foreach (var hospital in hospitals)
                {
                    if (hospital.CanTreat(currentGrandma) && hospital.AddGrandma(currentGrandma))
                    {
                        assignedHospital = hospital;
                        break;
                    }
                }

                if (assignedHospital == null)
                {
                    homelessGrandmas.Add(currentGrandma);
                }
            }

            Console.WriteLine("\nИнформация о больницах:");
            
            foreach (var hospital in hospitals.Reverse())
            {
                Console.WriteLine(hospital);
            }

            Console.WriteLine("\nБабули, оставшиеся на улице:");
            if (homelessGrandmas.Count > 0)
            {
                foreach (var grandma in homelessGrandmas)
                {
                    Console.WriteLine(grandma);
                }
            }
            else
            {
                Console.WriteLine("Нет бабуль на улице.");
            }
        }

        static List<string> SplitAndTrimIllnesses(string input)
        {
            var illnesses = input.Split(",");
            List<string> trimmedIllnesses = new List<string>();
            foreach (var illness in illnesses)
            {
                trimmedIllnesses.Add(illness.Trim());
            }
            return trimmedIllnesses;
        }

        static int CountTreatableIllnesses(Grandma grandma, Hospital hospital)
        {
            int count = 0;
            foreach (var illness in grandma.Illnesses)
            {
                if (hospital.TreatedIllnesses.Contains(illness))
                {
                    count++;
                }
            }
            return count;
        }

        class Grandma
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<string> Illnesses { get; set; }

            public Grandma(string name, int age, List<string> illnesses)
            {
                Name = name;
                Age = age;
                Illnesses = illnesses;
            }

            public override string ToString()
            {
                return $"{Name} (Возраст: {Age}, Болезни: {(Illnesses.Count > 0 ? string.Join(", ", Illnesses) : "Нет")})";
            }
        }

        class Hospital
        {
            public string Name { get; set; }
            public List<string> TreatedIllnesses { get; set; }
            public int Capacity { get; set; }
            private List<Grandma> CurrentGrandmas { get; set; }

            public Hospital(string name, List<string> treatedIllnesses, int capacity)
            {
                Name = name;
                TreatedIllnesses = treatedIllnesses;
                Capacity = capacity;
                CurrentGrandmas = new List<Grandma>();
            }

            public bool AddGrandma(Grandma grandma)
            {
                if (CurrentGrandmas.Count >= Capacity) return false;
                CurrentGrandmas.Add(grandma);
                return true;
            }

            public double GetFillPercentage()
            {
                return (double)CurrentGrandmas.Count / Capacity * 100;
            }

            public override string ToString()
            {
                return $"{Name} (Вместимость: {Capacity}, Заполненность: {GetFillPercentage():F2}%, Лечим: {string.Join(", ", TreatedIllnesses)}, Пациенты: {CurrentGrandmas.Count})";
            }

            public bool CanTreat(Grandma grandma)
            {
                if (grandma.Illnesses.Count == 0) return true; 
                int treatableIllnesses = CountTreatableIllnesses(grandma, this);
                return treatableIllnesses >= grandma.Illnesses.Count / 2.0; 
            }
        }

        static void Task4()
        {
            Graph graph = new Graph();

            
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 6);

            
            Console.WriteLine("Введите начальную вершину:");
            if (!int.TryParse(Console.ReadLine(), out int start))
            {
                Console.WriteLine("Ошибка: введите корректное число.");
                return;
            }

            
            Console.WriteLine("Введите конечную вершину:");
            if (!int.TryParse(Console.ReadLine(), out int target))
            {
                Console.WriteLine("Ошибка: введите корректное число.");
                return;
            }

            
            graph.PrintShortestPath(start, target);
        }
        class Graph
        {
            private Dictionary<int, List<int>> adjacencyList;

            public Graph()
            {
                adjacencyList = new Dictionary<int, List<int>>();
            }

            
            public void AddEdge(int start, int end)
            {
                if (!adjacencyList.ContainsKey(start))
                    adjacencyList[start] = new List<int>();

                if (!adjacencyList.ContainsKey(end))
                    adjacencyList[end] = new List<int>();

                adjacencyList[start].Add(end);
                adjacencyList[end].Add(start);
            }

            
            public void PrintShortestPath(int start, int target)
            {
               
                if (!adjacencyList.ContainsKey(start) || !adjacencyList.ContainsKey(target))
                {
                    Console.WriteLine("Ошибка: одна или обе введённые вершины отсутствуют в графе.");
                    return;
                }

                
                if (start == target)
                {
                    Console.WriteLine($"Кратчайший путь от {start} до {target}: {start}");
                    Console.WriteLine("Длина пути: 0");
                    return;
                }

                
                Dictionary<int, int?> predecessors = new Dictionary<int, int?>();  
                Dictionary<int, int> distances = new Dictionary<int, int>();

                
                Queue<int> queue = new Queue<int>();

                
                foreach (var node in adjacencyList.Keys)
                {
                    predecessors[node] = null;
                    distances[node] = int.MaxValue;
                }

                distances[start] = 0;
                queue.Enqueue(start);

                
                while (queue.Count > 0)
                {
                    int current = queue.Dequeue();

                    foreach (var neighbor in adjacencyList[current])
                    {
                        
                        if (distances[neighbor] == int.MaxValue)
                        {
                            distances[neighbor] = distances[current] + 1;
                            predecessors[neighbor] = current;
                            queue.Enqueue(neighbor);

                            
                            if (neighbor == target)
                                break;
                        }
                    }
                }

                
                if (distances[target] == int.MaxValue)
                {
                    Console.WriteLine($"Путь от {start} до {target} не существует.");
                    return;
                }

                
                List<int> path = new List<int>();
                int? step = target;
                while (step != null)
                {
                    path.Add(step.Value);
                    step = predecessors[step.Value];
                }

                path.Reverse();  

               
                Console.WriteLine($"Кратчайший путь от {start} до {target}: {string.Join(" -> ", path)}");
                Console.WriteLine($"Длина пути: {distances[target]}");
            }
        }
        


        static void Main(string[] args)
        {
            Console.WriteLine("1 Задание\n");
            Task1();
            Console.WriteLine("\n2 Задание\n");
            Task2();
            Console.WriteLine("\n3 Задание\n");
            Task3();
            Console.WriteLine("\n4 Задание\n");
            Task4();
        }
    }
}
