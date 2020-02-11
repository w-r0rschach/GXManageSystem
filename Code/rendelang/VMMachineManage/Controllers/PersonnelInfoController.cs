using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VMMachineManage.Data;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class PersonnelInfoController : Controller
    {
        private readonly VMMachineManageContext _context;

        public PersonnelInfoController(VMMachineManageContext context)
        {
            _context = context;
        }

        // GET: PersonnelInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Common_PersonnelInfo.ToListAsync());
        }

        // GET: PersonnelInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var common_PersonnelInfoModel = await _context.Common_PersonnelInfo
                .FirstOrDefaultAsync(m => m.PersonnelId == id);
            if (common_PersonnelInfoModel == null)
            {
                return NotFound();
            }

            return View(common_PersonnelInfoModel);
        }

        // GET: PersonnelInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonnelInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonnelId,PassWord,PersonnelNo,PersonnelName,DepId,Avatar,PersonnelSex,BirthDate,IdentityCard,IsWork,Nation,MaritalStatus,LiveAddress,Phone,WeChatAccount,Mailbox,Degree,Address,OnBoarDingTime,DepartureTime,TrialTime,IsStruggle,IsSecrecy")] Common_PersonnelInfoModel common_PersonnelInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(common_PersonnelInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(common_PersonnelInfoModel);
        }

        // GET: PersonnelInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var common_PersonnelInfoModel = await _context.Common_PersonnelInfo.FindAsync(id);
            if (common_PersonnelInfoModel == null)
            {
                return NotFound();
            }
            return View(common_PersonnelInfoModel);
        }

        // POST: PersonnelInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnelId,PassWord,PersonnelNo,PersonnelName,DepId,Avatar,PersonnelSex,BirthDate,IdentityCard,IsWork,Nation,MaritalStatus,LiveAddress,Phone,WeChatAccount,Mailbox,Degree,Address,OnBoarDingTime,DepartureTime,TrialTime,IsStruggle,IsSecrecy")] Common_PersonnelInfoModel common_PersonnelInfoModel)
        {
            if (id != common_PersonnelInfoModel.PersonnelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(common_PersonnelInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Common_PersonnelInfoModelExists(common_PersonnelInfoModel.PersonnelId))
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
            return View(common_PersonnelInfoModel);
        }

        // GET: PersonnelInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var common_PersonnelInfoModel = await _context.Common_PersonnelInfo
                .FirstOrDefaultAsync(m => m.PersonnelId == id);
            if (common_PersonnelInfoModel == null)
            {
                return NotFound();
            }

            return View(common_PersonnelInfoModel);
        }

        // POST: PersonnelInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var common_PersonnelInfoModel = await _context.Common_PersonnelInfo.FindAsync(id);
            _context.Common_PersonnelInfo.Remove(common_PersonnelInfoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Common_PersonnelInfoModelExists(int id)
        {
            return _context.Common_PersonnelInfo.Any(e => e.PersonnelId == id);
        }
    }
}
