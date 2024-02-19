using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace StudentManagementApp.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentsApiController(StudentManagementContext context, ILogger<StudentsController> logger) : ControllerBase
    {
        private readonly StudentManagementContext _context = context;
        private readonly ILogger<StudentsController> _logger = logger;

        [HttpGet("details")]
        public async Task<IActionResult> GetStudentDetails([FromQuery(Name = "studentid")] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var student = await _context.Students.Include(ss => ss.StudentSubjects).ThenInclude(s => s.Subject).FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(student, options));
        }
        
        [HttpGet("list")]
        public IActionResult GetStudentList()
        {
           
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var students =  _context.Students.Include(ss => ss.StudentSubjects).ThenInclude(s => s.Subject).ToList();

            if (students == null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(students, options));
        }

    }
}
