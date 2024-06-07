// Services/StudentService.cs
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models; 

namespace dotnetapp.Services 
{
    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService()
        {
            _students = new List<Student>
            {
                new Student { StudentId = 1, Name = "Alice", Age = 18, Grade = "A" },
                new Student { StudentId = 2, Name = "Bob", Age = 17, Grade = "B" },
                new Student { StudentId = 3, Name = "Charlie", Age = 16, Grade = "C" }
            };
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudentById(int studentId)
        {
            return _students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public void AddStudent(Student newStudent)
        {
            newStudent.StudentId = _students.Count > 0 ? _students.Max(s => s.StudentId) + 1 : 1;
            _students.Add(newStudent);
        }

        public void UpdateStudent(int studentId, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudentId == studentId);
            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.Age = updatedStudent.Age;
                existingStudent.Grade = updatedStudent.Grade;
            }
        }

        public void DeleteStudent(int studentId)
        {
            var existingStudent = _students.FirstOrDefault(s => s.StudentId == studentId);
            if (existingStudent != null)
            {
                _students.Remove(existingStudent);
            }
        }
    }
}
