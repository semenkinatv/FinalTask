using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace FinalTask
{
    [Serializable]
class Student 
{
    public string Name { get; set; }
    public string Group { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Student(string name, string group, DateTime dateOfBirth)
    {
        Name = name;
        Group = group;
        DateOfBirth = dateOfBirth;
    }
}
    class Program
    {
        
        static void Main(string[] args)
        {
            //Укажем путь к файлу
            string filePath = @"/Semenkina/Students.dat";  
            
            BinaryFormatter formatter = new BinaryFormatter();
            //Подготовим пустой массив 
            Student[] Students;
            using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                //Считаем массив из файла студентов в массив
                Students = (Student[])formatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
            }

            //Создание на рабочем столе директории Students
            //Укажем путь к папке
            string dirPath = @"\Users\SemenkinaTV\Desktop\Students"; //"C:

            //Если папка не существует - создадим
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            //Раскладываем студентов по файлам
            foreach (Student st in Students)
            {
                Console.Write($"Имя: {st.Name} -- д.р. {st.DateOfBirth.ToString("dd.MM.yyyy")} -- Группа: {st.Group} ");

                //Сформируем путь к файлу с учетом группы
                filePath = $@"{dirPath}\{st.Group}.txt";

                ////добавляем в файл нового студента, если файла нет - создаем
               using (var fs = new StreamWriter(filePath, true ))
                {
                    fs.WriteLine($"{st.Name}, {st.DateOfBirth.ToString("dd.MM.yyyy")}");
                    Console.WriteLine($" поместить в  {filePath}");
                }

            }
        }
    }
}
