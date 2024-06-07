// Controllers/StudentController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            if (students == null || !students.Any())
            {
                return NoContent(); // HTTP 204
            }
            return Ok(students); // HTTP 200
        }

        [HttpGet("{studentId}")]
        public ActionResult<Student> GetStudentById(int studentId)
        {
            var student = _studentService.GetStudentById(studentId);
            if (student == null)
            {
                return NotFound(); // HTTP 404
            }
            return Ok(student); // HTTP 200
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent(Student newStudent)
        {
            if (newStudent == null)
            {
                return BadRequest(); // HTTP 400
            }
            _studentService.AddStudent(newStudent);
            return CreatedAtAction(nameof(GetStudentById), new { studentId = newStudent.StudentId }, newStudent); // HTTP 201
        }

        [HttpPut("{studentId}")]
        public ActionResult UpdateStudent(int studentId, Student updatedStudent)
        {
            var existingStudent = _studentService.GetStudentById(studentId);
            if (existingStudent == null)
            {
                return NotFound(); // HTTP 404
            }
            _studentService.UpdateStudent(studentId, updatedStudent);
            return NoContent(); // HTTP 204
        }

        [HttpDelete("{studentId}")]
        public ActionResult DeleteStudent(int studentId)
        {
            var existingStudent = _studentService.GetStudentById(studentId);
            if (existingStudent == null)
            {
                return NotFound(); // HTTP 404
            }
            _studentService.DeleteStudent(studentId);
            return NoContent(); // HTTP 204
        }
    }
}
