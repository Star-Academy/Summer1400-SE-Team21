using System.Collections.Generic;
using System.Linq;

namespace Project
{
    public class Ranker
    {
        private readonly Student[] _students;
        private readonly StudentScore[] _scores;

        public Ranker(Student[] students, StudentScore[] scores)
        {
            _students = students;
            _scores = scores;
            CalculateAverages();
        }

        private void CalculateAverages()
        {
            foreach (var student in _students)
            {
                student.Average = GetStudentAverage(student);
            }
        }

        private List<Student> GetRankedStudents()
        {
            return _students.OrderByDescending(student => student.Average).ToList();
        }

        private float GetStudentAverage(Student student)
        {
            return _scores.Where(score => score.StudentNumber == student.StudentNumber).Select(score => score.Score).Average();
        }

        public List<Student> GetNTopStudents(int n)
        {
            return GetRankedStudents().Take(n).ToList();
        }
    }
}