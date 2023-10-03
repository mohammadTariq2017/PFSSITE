using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFSSITE.Models;
using PFSSITE.ViewModel;

namespace PFSSITE.Controllers
{
    public class StudentsController : Controller
    {
        private readonly PFSDBContext _context;

        public StudentsController(PFSDBContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return _context.Student != null ?
                        View(await _context.Student.Include(c => c.Class).ToListAsync()) :
                        Problem("Entity set 'PFSDBContext.Student'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {   
            var student = await _context.Student
                .Include(c => c.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);
     
            return PartialView("Details",student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var lstStatus = new List<Status> { new Status { Value = "Active", Text = "Active" }, new Status { Value = "InActive", Text = "InActive" } };
            ViewBag.Status = new SelectList(lstStatus, "Value", "Text").ToList();
            ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName").ToList();
            return PartialView("Create",new StudentVM());
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVM studentVm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student();
                student.Address = studentVm.Address;
                student.StudentName = studentVm.StudentName;
                student.Status = studentVm.Status;
                student.Phone1 = studentVm.Phone1;
                student.Phone2 = studentVm.Phone2;
                student.GRNO = studentVm.GRNO;
                student.RollNo = studentVm.RollNo;
                student.StationaryCharges = studentVm.StationaryCharges;
                student.AdmissionDate = studentVm.AdmissionDate;
                student.AdmissionFee = studentVm.AdmissionFee;
                student.AnnualCharges = studentVm.AnnualCharges;
                student.Gender = studentVm.Gender;
                student.FatherName = studentVm.FatherName;
                student.FatherCNIC = studentVm.FatherCNIC;
                student.Description = studentVm.Description;
                student.DOB = studentVm.DOB;
                student.MonthlyFee = studentVm.MonthlyFee;
                student.ClassId = studentVm.ClassId;
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var lstStatus = new List<Status>
            {
                new Status { Value = "Active", Text = "Active" },
        new Status { Value ="InActive", Text = "InActive" },
        // Add more countries as needed
    };
            ViewBag.Status = new SelectList(lstStatus, "Value", "Text").ToList();
            ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName").ToList();
            return View(studentVm);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var student = await _context.Student.FindAsync(id);
            var studentVm = new StudentVM();
            studentVm.StudentId = student.StudentId;
            studentVm.Address = student.Address;
            studentVm.StudentName = student.StudentName;
            studentVm.Status = student.Status;
            studentVm.Phone1 = student.Phone1;
            studentVm.Phone2 = student.Phone2;
            studentVm.GRNO = student.GRNO;
            studentVm.RollNo = student.RollNo;
            studentVm.StationaryCharges = student.StationaryCharges;
            studentVm.AdmissionDate = student.AdmissionDate;
            studentVm.AdmissionFee = student.AdmissionFee;
            studentVm.AnnualCharges = student.AnnualCharges;
            studentVm.Gender = student.Gender;
            studentVm.FatherName = student.FatherName;
            studentVm.FatherCNIC = student.FatherCNIC;
            studentVm.Description = student.Description;
            studentVm.DOB = student.DOB;
            studentVm.MonthlyFee = student.MonthlyFee;
            studentVm.ClassId = student.ClassId;
            var lstStatus = new List<Status> { new Status { Value = "Active", Text = "Active" }, new Status { Value = "InActive", Text = "InActive" } };
            ViewBag.Status = new SelectList(lstStatus, "Value", "Text", student.Status).ToList();
            ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName", student.ClassId).ToList();
            return PartialView("Edit",studentVm);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentVM student)
        {
            var studentModel = _context.Student.Find(student.StudentId);
            if (studentModel==null)
            {
                var lstStatus = new List<Status> { new Status { Value = "Active", Text = "Active" }, new Status { Value = "InActive", Text = "InActive" } };
                ViewBag.Status = new SelectList(lstStatus, "Value", "Text", student.Status).ToList();
                ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName", student.ClassId).ToList();
                return PartialView("Edit", student);
            }
            try
            {
                studentModel.Address = student.Address;
                studentModel.StudentName = student.StudentName;
                studentModel.Status = student.Status;
                studentModel.Phone1 = student.Phone1;
                studentModel.Phone2 = student.Phone2;
                studentModel.GRNO = student.GRNO;
                studentModel.RollNo = student.RollNo;
                studentModel.StationaryCharges = student.StationaryCharges;
                studentModel.AdmissionDate = student.AdmissionDate;
                studentModel.AdmissionFee = student.AdmissionFee;
                studentModel.AnnualCharges = student.AnnualCharges;
                studentModel.Gender = student.Gender;
                studentModel.FatherName = student.FatherName;
                studentModel.FatherCNIC = student.FatherCNIC;
                studentModel.Description = student.Description;
                studentModel.DOB = student.DOB;
                studentModel.MonthlyFee = student.MonthlyFee;
                studentModel.ClassId = student.ClassId;

                _context.Update(studentModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
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

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'PFSDBContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
