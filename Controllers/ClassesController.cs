using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFSSITE.Models;
using PFSSITE.ViewModel;

namespace PFSSITE.Controllers
{
    public class ClassesController : Controller
    {
        private readonly PFSDBContext _context;

        public ClassesController(PFSDBContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return _context.Class != null ?
                        View(await _context.Class.Include(s => s.Session).ToListAsync()) :
                        Problem("Entity set 'PFSDBContext.Class'  is null.");
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        public async Task<PartialViewResult> _Details(int? id)
        {
            var @class = await _context.Class
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.ClassId == id);       
            return PartialView("_Details",@class);
        }
        // GET: Classes/Create
        public IActionResult Create()
        {
            ViewBag.SessionId = new SelectList(_context.Session, "SessionId", "SessionName").ToList();
            return View();
        }
        public PartialViewResult _Create()
        {
            ViewBag.SessionId = new SelectList(_context.Session, "SessionId", "SessionName").ToList();         
            return PartialView("_Create");
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassVM @class)
        {
            if (ModelState.IsValid)
            {
                var classModel = new ClassViewModel();
                classModel.ClassName = @class.ClassName;
                classModel.SessionId = @class.SessionId;
                classModel.Description = @class.Description;

                _context.Add(classModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = new SelectList(_context.Session, "SessionId", "SessionName").ToList();
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }
            var classModel = new ClassVM();
            classModel.SessionId = @class.SessionId;
            classModel.ClassName = @class.ClassName;
            classModel.Description = @class.Description;
            ViewBag.SessionId = new SelectList(_context.Session, "SessionId", "SessionName", @class.SessionId).ToList();
            return View(classModel);
        }
        public async Task<PartialViewResult> _Edit(int? id)
        {
            var @class = await _context.Class.FindAsync(id);
            var classModel = new ClassVM();
            classModel.ClassId=@class.ClassId;
            classModel.SessionId = @class.SessionId;
            classModel.ClassName = @class.ClassName;
            classModel.Description = @class.Description;
            ViewBag.SessionId = new SelectList(_context.Session, "SessionId", "SessionName", @class.SessionId).ToList();
            return PartialView("_Edit", classModel);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassVM @class)
        {
            var classModel = _context.Class.Find(@class.ClassId);
            if (classModel == null)
            {
                ViewBag.SessionId = new SelectList(_context.Session, "SessionId", "SessionName", @class.SessionId).ToList();
                return View(@class);
            }

            try
            {
                classModel.SessionId = @class.SessionId;
                classModel.ClassName = @class.ClassName;
                classModel.Description = @class.Description;

                _context.Update(classModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(@class.ClassId))
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

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.ClassId == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Class == null)
            {
                return Problem("Entity set 'PFSDBContext.Class'  is null.");
            }
            var @class = await _context.Class.FindAsync(id);
            if (@class != null)
            {
                _context.Class.Remove(@class);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return (_context.Class?.Any(e => e.ClassId == id)).GetValueOrDefault();
        }
    }
}
