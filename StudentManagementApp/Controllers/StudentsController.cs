using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace StudentManagementApp.Controllers
{
    public class StudentsController(StudentManagementContext context, ILogger<StudentsController> logger) : Controller
    {
        private readonly StudentManagementContext _context = context;
        private readonly ILogger<StudentsController> _logger = logger;

        //get students list
        [HttpGet]
        public IActionResult Index()
        {
            //passing delete confirmation msg
            ViewBag.Message = TempData["Message"] as string;

            //get student list
            var students = _context.Students.ToList();
            return View(students);
        }

        //read detail
        [HttpGet]
        public async Task<IActionResult> Read(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.Include(ss => ss.StudentSubjects).ThenInclude(s => s.Subject).FirstOrDefaultAsync(s => s.Id == id);

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var yearOfStudyOptions = new List<SelectListItem>
            {
                 new() { Text = "1st Year", Value = "1st Year" },
                 new() { Text = "2nd Year", Value = "2nd Year" },
                 new() { Text = "3rd Year", Value = "3rd Year" }
            };

            ViewBag.YearOfStudyOptions = yearOfStudyOptions;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var yearOfStudyOptions = new List<SelectListItem>
            {
                 new() { Text = "1st Year", Value = "1st Year" },
                 new() { Text = "2nd Year", Value = "2nd Year" },
                 new() { Text = "3rd Year", Value = "3rd Year" }
            };

            ViewBag.YearOfStudyOptions = yearOfStudyOptions;

            var student = await _context.Students.FindAsync(Id);
            return View(student);
        }

        //update student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var updateStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);

            if (updateStudent == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Student>(updateStudent, "", s => s.FirstName, s => s.LastName, s => s.DateOfBirth))
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateException ex)
                    {
                        _logger.LogError("An error has occcured {exception}", ex);

                        return RedirectToAction("Error", "Home");
                    }

                }
            }

            return View(updateStudent);
        }

        //create student
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _context.Students.AddAsync(student);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");

                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("An error has occcured {exception}", ex);

                    return RedirectToAction("Error", "Home");
                }
            }

            return View();
        }

        //delete student
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Student deleted successfully.";
                return RedirectToAction("Index");
            }

            catch (DbUpdateException ex)
            {
                _logger.LogError("An error has occcured {exception}", ex);

                return RedirectToAction("Error", "Home");
            }


        }

    }
}
