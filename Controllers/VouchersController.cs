using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PFSSITE.Models;
using PFSSITE.ViewModel;

namespace PFSSITE.Controllers
{
    public class VouchersController : Controller
    {
        private readonly PFSDBContext _context;

        public VouchersController(PFSDBContext context)
        {
            _context = context;
        }

        // GET: Vouchers
        public async Task<IActionResult> Index()
        {
            return _context.Voucher != null ?
                        View(await _context.Voucher.Include(c => c.Class).Include(s => s.Student).ToListAsync()) :
                        Problem("Entity set 'PFSDBContext.Voucher'  is null.");
        }

        // GET: Vouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var voucher = await _context.Voucher
                .Include(c => c.Class)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.VoucherId == id);

            return PartialView("Details",voucher);
        }

        // GET: Vouchers/Create
        public IActionResult Create()
        {
            ViewBag.StudentId = new SelectList(_context.Student, "StudentId", "StudentName").ToList();
            ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName").ToList();
            return PartialView("Create");
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoucherVM voucher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.StudentId = new SelectList(_context.Student, "StudentId", "StudentName").ToList();
            ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName").ToList();
            return View(voucher);
        }

        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var voucher = await _context.Voucher.FindAsync(id);
            var voucherModel = new VoucherVM();
            voucherModel.VoucherId = voucher.VoucherId;
            voucherModel.Title = voucher.Title;
            voucherModel.DueDate = voucher.DueDate;
            voucherModel.Description = voucher.Description;
            voucherModel.Amount = voucher.Amount;
            voucherModel.RemainingAmount = voucher.RemainingAmount;
            voucherModel.Month = voucher.Month;
            voucherModel.StudentId = voucher.StudentId;
            voucherModel.ClassId = voucher.ClassId;
            voucherModel.Discount = voucher.Discount;
            voucherModel.ReceivingDate = voucher.ReceivingDate;
            ViewBag.StudentId = new SelectList(_context.Student, "StudentId", "StudentName", voucher.StudentId).ToList();
            ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName", voucher.ClassId).ToList();
            return PartialView("Edit", voucherModel);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,VoucherVM voucher)
        {
            var voucherModel = _context.Voucher.Find(id);
            if (voucherModel == null)
            {
                ViewBag.StudentId = new SelectList(_context.Student, "StudentId", "StudentName", voucher.StudentId).ToList();
                ViewBag.ClassId = new SelectList(_context.Class, "ClassId", "ClassName", voucher.ClassId).ToList();
                return View(voucher);
            }

            try
            {
                voucherModel.Title = voucher.Title;
                voucherModel.DueDate = voucher.DueDate;
                voucherModel.Description = voucher.Description;
                voucherModel.Amount = voucher.Amount;
                voucherModel.RemainingAmount = voucher.RemainingAmount;
                voucherModel.Month = voucher.Month;
                voucherModel.StudentId = voucher.StudentId;
                voucherModel.ClassId = voucher.ClassId;
                voucherModel.Discount = voucher.Discount;
                voucherModel.ReceivingDate = voucher.ReceivingDate;

                _context.Update(voucherModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(voucher.VoucherId))
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


        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Voucher == null)
            {
                return Problem("Entity set 'PFSDBContext.Voucher'  is null.");
            }
            var voucher = await _context.Voucher.FindAsync(id);
            if (voucher != null)
            {
                _context.Voucher.Remove(voucher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
            return (_context.Voucher?.Any(e => e.VoucherId == id)).GetValueOrDefault();
        }
    }
}
