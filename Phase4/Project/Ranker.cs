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
        }

        private List<Student> GetRankedStudents()
        {
            return _students.OrderByDescending(student => GetStudentAverage(student)).ToList();
        }

        private float GetStudentAverage(Student student)
        {
            return _scores.Where(score => score.StudentNumber == student.StudentNumber).Select(score => score.Score).Average();
        }

        public List<Student> GetNTopStudents(int n)
        {
            return GetRankedStudents().Take(n).ToList();
        }

        private List<float> GetRankedStudentsScores()
        {
            return GetRankedStudents().Select(student => GetStudentAverage(student)).ToList();
        }

        public List<float> GetNTopStudentsScores(int n)
        {
            return GetRankedStudentsScores().Take(n).ToList();
        }
    }
}