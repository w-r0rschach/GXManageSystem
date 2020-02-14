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
    public class PersonnelInfoController : Controller
    {
        private readonly VMMachineManageContext _context;

        public PersonnelInfoController(VMMachineManageContext context)
        {
            _context = context;
        }

        // GET: PersonnelInfo
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index()
        {
            var personnelInfo = from m in _context.Common_PersonnelInfo select m;

            return View(await personnelInfo.ToListAsync());
        }

        // GET: PersonnelInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PersonnelInfoModel = await _context.Common_PersonnelInfo
                .FirstOrDefaultAsync(m => m.PersonnelId == id);
            if (PersonnelInfoModel == null)
            {
                return NotFound();
            }

            return View(PersonnelInfoModel);
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
        public async Task<IActionResult> Create([Bind("PersonnelId,PassWord,PersonnelNo,PersonnelName,DepId,Avatar,PersonnelSex,BirthDate,IdentityCard,IsWork,Nation,MaritalStatus,LiveAddress,Phone,WeChatAccount,Mailbox,Degree,Address,OnBoarDingTime,DepartureTime,TrialTime,IsStruggle,IsSecrecy")] PersonnelInfoModel PersonnelInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(PersonnelInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PersonnelInfoModel);
        }

        // GET: PersonnelInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PersonnelInfoModel = await _context.Common_PersonnelInfo.FindAsync(id);
            if (PersonnelInfoModel == null)
            {
                return NotFound();
            }
            return View(PersonnelInfoModel);
        }

        // POST: PersonnelInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonnelId,PassWord,PersonnelNo,PersonnelName,DepId,Avatar,PersonnelSex,BirthDate,IdentityCard,IsWork,Nation,MaritalStatus,LiveAddress,Phone,WeChatAccount,Mailbox,Degree,Address,OnBoarDingTime,DepartureTime,TrialTime,IsStruggle,IsSecrecy")] PersonnelInfoModel PersonnelInfoModel)
        {
            if (id != PersonnelInfoModel.PersonnelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(PersonnelInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelInfoModelExists(PersonnelInfoModel.PersonnelId))
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
            return View(PersonnelInfoModel);
        }

        // GET: PersonnelInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PersonnelInfoModel = await _context.Common_PersonnelInfo
                .FirstOrDefaultAsync(m => m.PersonnelId == id);
            if (PersonnelInfoModel == null)
            {
                return NotFound();
            }

            return View(PersonnelInfoModel);
        }

        // POST: PersonnelInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var PersonnelInfoModel = await _context.Common_PersonnelInfo.FindAsync(id);
            _context.Common_PersonnelInfo.Remove(PersonnelInfoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelInfoModelExists(int id)
        {
            return _context.Common_PersonnelInfo.Any(e => e.PersonnelId == id);
        }
    }
}
