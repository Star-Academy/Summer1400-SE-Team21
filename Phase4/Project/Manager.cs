using System;
using System.Linq;

namespace Project
{
    public class Manager
    {
        private const string StudentsJsonPath = "Students.json";
        private const string ScoresJsonPath = "Scores.json";
        private const int TopListSize = 3;
        public void run()
        {
            var students = JsonFileReader.Read<Student[]>(StudentsJsonPath);
            var scores = JsonFileReader.Read<StudentScore[]>(ScoresJsonPath);
            Ranker ranker = new Ranker(students, scores);

            var topStudents = ranker.GetNTopStudents(TopListSize);
            var topStudentsScore = ranker.GetNTopStudentsScores(TopListSize);
            for (int i = 0; i < topStudents.Count; i++)
            {
                var student = topStudents[i];
                var score = topStudentsScore[i];
                Console.WriteLine(student.FirstName + " " + student.LastName + " " + score);
            }
        }
    }
}