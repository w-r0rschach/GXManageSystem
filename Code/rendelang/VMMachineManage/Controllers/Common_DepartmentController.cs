using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VMMachineManage.Data;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    [Authorize]
    public class Common_DepartmentController : Controller
    {
        private readonly VMMachineManageContext _context;

        public Common_DepartmentController(VMMachineManageContext context)
        {
            _context = context;
        }

        // GET: Common_Department
        public async Task<IActionResult> Index()
        {
            return View(await _context.Common_Department.ToListAsync());
        }

        // GET: Common_Department/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var common_DepartmentModel = await _context.Common_Department
                .FirstOrDefaultAsync(m => m.DepId == id);
            if (common_DepartmentModel == null)
            {
                return NotFound();
            }

            return View(common_DepartmentModel);
        }

        // GET: Common_Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Common_Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepId,ParentNumber,DepName")] Common_DepartmentModel common_DepartmentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(common_DepartmentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(common_DepartmentModel);
        }

        // GET: Common_Department/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var common_DepartmentModel = await _context.Common_Department.FindAsync(id);
            if (common_DepartmentModel == null)
            {
                return NotFound();
            }
            return View(common_DepartmentModel);
        }

        // POST: Common_Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepId,ParentNumber,DepName")] Common_DepartmentModel common_DepartmentModel)
        {
            if (id != common_DepartmentModel.DepId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(common_DepartmentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Common_DepartmentModelExists(common_DepartmentModel.DepId))
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
            return View(common_DepartmentModel);
        }

        // GET: Common_Department/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var common_DepartmentModel = await _context.Common_Department
                .FirstOrDefaultAsync(m => m.DepId == id);
            if (common_DepartmentModel == null)
            {
                return NotFound();
            }

            return View(common_DepartmentModel);
        }

        // POST: Common_Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var common_DepartmentModel = await _context.Common_Department.FindAsync(id);
            _context.Common_Department.Remove(common_DepartmentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Common_DepartmentModelExists(int id)
        {
            return _context.Common_Department.Any(e => e.DepId == id);
        }
    }
}
