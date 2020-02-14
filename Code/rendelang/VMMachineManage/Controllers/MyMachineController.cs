using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VMMachineManage.Data; 
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class MyMachineController : Controller
    {
        private readonly VMMachineManageContext _context;

        public MyMachineController(VMMachineManageContext context)
        {
            _context = context;
        }

        // GET: MyMachine
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index(string searchString)
        {

            // = from m in _context.MachineInfo.Include(q=>q.MachineID).Include(q=>q.MachApplyAndReturnInfo.MachineID) select m;

            var machines = from m in _context.MachineInfo
                           join m1 in _context.MachApplyAndReturn on m.MachineID equals m1.MachineInfoID
                           where m1.OprationType == 0
                           select m;


            if (!string.IsNullOrWhiteSpace(searchString))
            {
                //machines = machines.Where(option => option.MachineState == MachineStateEnum.None && (option.MachineIP.Contains(searchString) ||
                 // option.MachineUser.Contains(searchString)));
            }

             
            MachineInfoViewModel viewMode = new MachineInfoViewModel()
            { 
              MachineInfo = await machines.ToListAsync()
            };
            return View(viewMode); 
        }

        // GET: MyMachine/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machApplyAndReturnModel = await _context.MachApplyAndReturn
                .FirstOrDefaultAsync(m => m.ApplyAndReturnId == id);
            if (machApplyAndReturnModel == null)
            {
                return NotFound();
            }

            return View(machApplyAndReturnModel);
        }

        // GET: MyMachine/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MyMachine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplyAndReturnId,OprationType,ApplyUserID,ExamineUserID,MachineInfoID,ExamineResult,ApplyTime,ResultTime,Remark")] MachApplyAndReturnModel machApplyAndReturnModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machApplyAndReturnModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machApplyAndReturnModel);
        }

        // GET: MyMachine/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machApplyAndReturnModel = await _context.MachApplyAndReturn.FindAsync(id);
            if (machApplyAndReturnModel == null)
            {
                return NotFound();
            }
            return View(machApplyAndReturnModel);
        }

        // POST: MyMachine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplyAndReturnId,OprationType,ApplyUserID,ExamineUserID,MachineInfoID,ExamineResult,ApplyTime,ResultTime,Remark")] MachApplyAndReturnModel machApplyAndReturnModel)
        {
            if (id != machApplyAndReturnModel.ApplyAndReturnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machApplyAndReturnModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachApplyAndReturnModelExists(machApplyAndReturnModel.ApplyAndReturnId))
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
            return View(machApplyAndReturnModel);
        }

        // GET: MyMachine/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machApplyAndReturnModel = await _context.MachApplyAndReturn
                .FirstOrDefaultAsync(m => m.ApplyAndReturnId == id);
            if (machApplyAndReturnModel == null)
            {
                return NotFound();
            }

            return View(machApplyAndReturnModel);
        }

        // POST: MyMachine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machApplyAndReturnModel = await _context.MachApplyAndReturn.FindAsync(id);
            _context.MachApplyAndReturn.Remove(machApplyAndReturnModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachApplyAndReturnModelExists(int id)
        {
            return _context.MachApplyAndReturn.Any(e => e.ApplyAndReturnId == id);
        }
    }
}
