using System;
using System.Linq;

namespace Project
{
    public class Manager
    {
        private const string StudentsJsonPath = "Students.json";
        private const string ScoresJsonPath = "Scores.json";
        private const int TopListSize = 3;
        public void Run()
        {
            var students = JsonFileReader.Read<Student[]>(StudentsJsonPath);
            var scores = JsonFileReader.Read<StudentScore[]>(ScoresJsonPath);
            var ranker = new Ranker(students, scores);

            var topStudents = ranker.GetNTopStudents(TopListSize);
            foreach (var student in topStudents)
            {
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.Average);
            }
        }
    }
}