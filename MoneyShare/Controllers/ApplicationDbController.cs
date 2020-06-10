using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoneyShare.Models;

namespace MoneyShare.Controllers
{
    public class ApplicationDbController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationDb
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberModels.ToListAsync());
        }

        // GET: ApplicationDb/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.MemberModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberModel == null)
            {
                return NotFound();
            }

            return View(memberModel);
        }

        // GET: ApplicationDb/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationDb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,TwoFactorCode,TwoFactorCodeDateTime,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] MemberModel memberModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberModel);
        }

        // GET: ApplicationDb/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.MemberModels.FindAsync(id);
            if (memberModel == null)
            {
                return NotFound();
            }
            return View(memberModel);
        }

        // POST: ApplicationDb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FirstName,LastName,TwoFactorCode,TwoFactorCodeDateTime,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] MemberModel memberModel)
        {
            if (id != memberModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberModelExists(memberModel.Id))
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
            return View(memberModel);
        }

        // GET: ApplicationDb/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberModel = await _context.MemberModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memberModel == null)
            {
                return NotFound();
            }

            return View(memberModel);
        }

        // POST: ApplicationDb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var memberModel = await _context.MemberModels.FindAsync(id);
            _context.MemberModels.Remove(memberModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberModelExists(string id)
        {
            return _context.MemberModels.Any(e => e.Id == id);
        }
    }
}
