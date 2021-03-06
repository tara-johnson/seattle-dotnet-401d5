﻿using Microsoft.AspNetCore.Mvc;
using SchoolEnrollment.Models.Interfaces;
using SchoolEnrollment.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolEnrollment.Controllers
{
	public class StudentsController : Controller
	{
		private readonly IStudent _student;

		public StudentsController(IStudent student)
		{
			_student = student;
		}

		// GET: Students
		public async Task<IActionResult> Index()
		{
			return View(await _student.GetStudents());
		}

		[HttpPost]
		public async Task<IActionResult> Index(string searchterm)
		{
			var myStudents = await _student.GetStudents();

			myStudents.Where(x => x.FirstName.Contains(searchterm));

			return View(myStudents);
		}

		// GET: Students/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var student = await _student.GetStudent(id);

			//var student = await _context.Student
			//	.FirstOrDefaultAsync(m => m.ID == id);

			if (student == null)
			{
				return NotFound();
			}

			return View(student);
		}

		public async Task<IActionResult> GetGPA(int id)
		{
			
			var student = _student.GetTranscripts(id);

			var studentModel = await _student.GetStudent(id);

			int averageGrade = 0;
			int count = 0;
			foreach (var item in student)
			{
				switch (item.Grade.ToLower())
				{
					case "a":
						averageGrade += 4;

						break;
					case "b":
						averageGrade += 3;

						break;
					case "c":
						averageGrade += 2;
						break;
					default:
						averageGrade += 0;
						break;
				}
				count++;
			}

			int average = averageGrade / count;

			AverageVM averageVM = new AverageVM
			{
				Student = studentModel,
				GPA = average

			};



			return View(averageVM);
		}

		//    // GET: Students/Create
		//    public IActionResult Create()
		//    {
		//        return View();
		//    }

		//    // POST: Students/Create
		//    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//    [HttpPost]
		//    [ValidateAntiForgeryToken]
		//    public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Age")] Student student)
		//    {
		//        if (ModelState.IsValid)
		//        {
		//            _context.Student.Add(student);
		//            var complete = await _context.SaveChangesAsync();


		//string cat = "Amanda";
		//int x = 10 + 10;
		//            return RedirectToAction(nameof(Index));
		//        }
		//        return View(student);
		//    }

		//    // GET: Students/Edit/5
		//    public async Task<IActionResult> Edit(int? id)
		//    {
		//        if (id == null)
		//        {
		//            return NotFound();
		//        }

		//        var student = await _context.Student.FindAsync(id);
		//        if (student == null)
		//        {
		//            return NotFound();
		//        }
		//        return View(student);
		//    }

		//    // POST: Students/Edit/5
		//    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		//    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		//    [HttpPost]
		//    [ValidateAntiForgeryToken]
		//    public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Age")] Student student)
		//    {
		//        if (id != student.ID)
		//        {
		//            return NotFound();
		//        }

		//        if (ModelState.IsValid)
		//        {
		//            try
		//            {
		//                _context.Update(student);
		//                await _context.SaveChangesAsync();
		//            }
		//            catch (DbUpdateConcurrencyException)
		//            {
		//                if (!StudentExists(student.ID))
		//                {
		//                    return NotFound();
		//                }
		//                else
		//                {
		//                    throw;
		//                }
		//            }
		//            return RedirectToAction(nameof(Index));
		//        }
		//        return View(student);
		//    }

		//    // GET: Students/Delete/5
		//    public async Task<IActionResult> Delete(int? id)
		//    {
		//        if (id == null)
		//        {
		//            return NotFound();
		//        }

		//        var student = await _context.Student
		//            .FirstOrDefaultAsync(m => m.ID == id);
		//        if (student == null)
		//        {
		//            return NotFound();
		//        }

		//        return View(student);
		//    }

		//    // POST: Students/Delete/5
		//    [HttpPost, ActionName("Delete")]
		//    [ValidateAntiForgeryToken]
		//    public async Task<IActionResult> DeleteConfirmed(int id)
		//    {
		//        var student = await _context.Student.FindAsync(id);
		//        _context.Student.Remove(student);
		//        await _context.SaveChangesAsync();
		//        return RedirectToAction(nameof(Index));
		//    }

		//    private bool StudentExists(int id)
		//    {
		//        return _context.Student.Any(e => e.ID == id);
		//    }
	}
}
