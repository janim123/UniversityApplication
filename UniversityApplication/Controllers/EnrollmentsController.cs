#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityApplication.Data;
using UniversityApplication.Models;
using UniversityApplication.ViewModels;

namespace UniversityApplication.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly UniversityApplicationContext _context;
        

        public EnrollmentsController(UniversityApplicationContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var universityApplicationContext = _context.Enrollment.Include(e => e.course).Include(e => e.student);
            return View(await universityApplicationContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.course)
                .Include(e => e.student)
                .FirstOrDefaultAsync(m => m.enrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["courseId"] = new SelectList(_context.Course, "courseId", "title");
            ViewData["studentId"] = new SelectList(_context.Student, "Id", "fullName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("enrollmentId,courseId,studentId,semester,year,grade,seminalUrl,projectUrl,examPoints,seminalPoints,projectPoints,additionalPoints,finishDate,student,course")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["courseId"] = new SelectList(_context.Course, "courseId", "title", enrollment.courseId);
            ViewData["studentId"] = new SelectList(_context.Student, "Id", "fullName", enrollment.studentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["courseId"] = new SelectList(_context.Course, "courseId", "title", enrollment.courseId);
            ViewData["studentId"] = new SelectList(_context.Student, "Id", "firstName", enrollment.studentId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("enrollmentId,courseId,studentId,semester,year,grade,seminalUrl,projectUrl,examPoints,seminalPoints,projectPoints,additionalPoints,finishDate")] Enrollment enrollment)
        {
            if (id != enrollment.enrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.enrollmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["courseId"] = new SelectList(_context.Course, "courseId", "title", enrollment.courseId);
            ViewData["studentId"] = new SelectList(_context.Student, "Id", "firstName", enrollment.studentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.course)
                .Include(e => e.student)
                .FirstOrDefaultAsync(m => m.enrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var enrollment = await _context.Enrollment.FindAsync(id);
            _context.Enrollment.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(long id)
        {
            return _context.Enrollment.Any(e => e.enrollmentId == id);
        }
        
        public async Task<IActionResult> StudentsEnrolledAtCourse(int? id, string teacher, int year)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .FirstOrDefaultAsync(m => m.courseId == id);

            string[] names = teacher.Split(" ");
            var teacherModel = await _context.Teacher.FirstOrDefaultAsync(m => m.firstName == names[0] && m.lastName == names[1]);
            ViewBag.teacher = teacher;
            ViewBag.course = course.title;
            var enrollment = _context.Enrollment.Where(x => x.courseId == id && (x.course.firstTeacherId == teacherModel.teacherId || x.course.secondTeacherId == teacherModel.teacherId)) .Include(e => e.course) .Include(e => e.student);
            await _context.SaveChangesAsync();
            IQueryable<int?> yearsQuery = _context.Enrollment.OrderBy(m => m.year).Select(m => m.year).Distinct();
            IQueryable<Enrollment> enrollmentQuery = enrollment.AsQueryable();
            if (year != null && year != 0)
            {
                enrollmentQuery = enrollmentQuery.Where(x => x.year == year);
            }
           

            if (enrollment == null)
            {
                return NotFound();
            }

            FilterEnrollments viewmodel = new FilterEnrollments
            {
                Enrollments = await enrollmentQuery.ToListAsync(),
                yearsList = new SelectList(await yearsQuery.ToListAsync())
            };

            return View(viewmodel);
        }

        public async Task<IActionResult> Edit(int id, [Bind("enrollmentId,courseId,studentId,semester,year,grade,seminalUrl,projectUrl,examPoints,seminalPoints,projectPoints,additionalPoints,finishDate")] Enrollment enrollment)
        {
            if (id != enrollment.enrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.enrollmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(enrollment);
        }
      
        }

    }


