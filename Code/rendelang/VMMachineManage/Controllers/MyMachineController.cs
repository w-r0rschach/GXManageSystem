using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VMMachineManage.Data;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class MyMachineController : RightsController
    {
        private readonly VMMachineManageContext _context;

        public MyMachineController(VMMachineManageContext context)
        {
            Role = 2;
            _context = context;
        }

        // GET: MyMachine
       
        public async Task<IActionResult> Index(string searchString)
        {
            PersonnelInfoModel userInfo = JsonConvert.DeserializeObject<PersonnelInfoModel>(HttpContext.Session.GetString("User"));
            IQueryable<VMMachineInfoModel> info; 
            info = from m in _context.MachineInfo
                   join m1 in _context.MachApplyAndReturn on m.MachineID equals m1.MachineInfoID
                   join m2 in _context.Common_PersonnelInfo on m1.ApplyUserID equals m2.PersonnelId
                   where(m1.OprationType==0&&m2.PersonnelId== userInfo.PersonnelId)
                   select new VMMachineInfoModel
                   {
                       machineInfo = m,
                       MachApplyAndReturn = m1
                   };
            return View(await info.ToListAsync());
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
        public IActionResult Create(int id)
        {
            if (id <1)
            {
                return NotFound();
            }

            var machineReturnInfo = from m in _context.MachApplyAndReturn
                                    join m1 in _context.MachineInfo on m.MachineInfoID equals m1.MachineID
                                    where (m.ApplyAndReturnId == id)
                                    select new VMMachineInfoModel
                                    {
                                        machineInfo = m1,
                                        MachApplyAndReturn = m
                                    };
            VMMachineInfoModel vMMachineInfo = machineReturnInfo.FirstOrDefault();

            MachApplyAndReturnModel machApplyAndReturn = vMMachineInfo.MachApplyAndReturn;
            machApplyAndReturn.ResultTime = DateTime.Now;
            machApplyAndReturn.MachineDiskCount = vMMachineInfo.machineInfo.MachineDiskCount;
            machApplyAndReturn.MachineMemory = vMMachineInfo.machineInfo.MachineMemory;
            machApplyAndReturn.MachineSystem = vMMachineInfo.machineInfo.MachineSystem;


            return View(machApplyAndReturn); 
        }

        // POST: MyMachine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string Remark, int DaysNumber, int ExamineUserName)
        {
            if (ModelState.IsValid)
            {
                PersonnelInfoModel userInfo = JsonConvert.DeserializeObject<PersonnelInfoModel>(HttpContext.Session.GetString("User"));
                var info = from m in _context.MachApplyAndReturn where (m.ApplyAndReturnId == id) select m;
                MachApplyAndReturnModel machApplyAndReturn = info.FirstOrDefault();
                machApplyAndReturn.Remark = Remark;
                machApplyAndReturn.ResultTime = machApplyAndReturn.ResultTime.AddDays(DaysNumber);
                machApplyAndReturn.ExamineUserID = ExamineUserName;
                machApplyAndReturn.OprationType = 0;
                machApplyAndReturn.ExamineResult = 0;
                _context.Update(machApplyAndReturn);

                var machineInfos = from m in _context.MachineInfo where (m.MachineID == machApplyAndReturn.MachineInfoID) select m;
                MachineInfoModel machineInfo = machineInfos.FirstOrDefault();
                machineInfo.MachineState = 1;
                _context.Update(machineInfo);

                var UserInfo = from m in _context.Common_PersonnelInfo.Where(m => m.PersonnelId == userInfo.PersonnelId) select m;
                PersonnelInfoModel personnelInfo = UserInfo.FirstOrDefault();
                personnelInfo.AppMaxCount = personnelInfo.AppMaxCount - 1;
                _context.Update(personnelInfo);

                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MyMachine/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineReturnInfo = from m in _context.MachApplyAndReturn
                                    join m1 in _context.MachineInfo on m.MachineInfoID equals m1.MachineID
                                    where(m.ApplyAndReturnId==id)
                                    select new VMMachineInfoModel
                                    {
                                        machineInfo = m1,
                                        MachApplyAndReturn = m
                                    };
            VMMachineInfoModel vMMachineInfo = machineReturnInfo.FirstOrDefault();

            MachApplyAndReturnModel machApplyAndReturn = vMMachineInfo.MachApplyAndReturn;
            machApplyAndReturn.ResultTime = DateTime.Now; 
            machApplyAndReturn.MachineDiskCount = vMMachineInfo.machineInfo.MachineDiskCount;
            machApplyAndReturn.MachineMemory = vMMachineInfo.machineInfo.MachineMemory;
            machApplyAndReturn.MachineSystem = vMMachineInfo.machineInfo.MachineSystem;
           

            return View(machApplyAndReturn);
        }

        // POST: MyMachine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,string Remark,DateTime ResultTime,int ExamineUserName)
        {
            if (id <=0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    PersonnelInfoModel userInfo = JsonConvert.DeserializeObject<PersonnelInfoModel>(HttpContext.Session.GetString("User"));

                    var info = from m in _context.MachApplyAndReturn where (m.ApplyAndReturnId == id) select m;
                    MachApplyAndReturnModel machApplyAndReturn = info.FirstOrDefault();
                    machApplyAndReturn.Remark = Remark;
                    machApplyAndReturn.ResultTime = ResultTime;
                    machApplyAndReturn.ExamineUserID = ExamineUserName;
                    machApplyAndReturn.OprationType = 1;
                    _context.Update(machApplyAndReturn);

                    var machineInfos = from m in _context.MachineInfo where (m.MachineID==machApplyAndReturn.MachineInfoID) select m;
                    MachineInfoModel machineInfo = machineInfos.FirstOrDefault();
                    machineInfo.MachineState = 0;
                    _context.Update(machineInfo);

                    var UserInfo = from m in _context.Common_PersonnelInfo.Where(m => m.PersonnelId == userInfo.PersonnelId) select m;
                    PersonnelInfoModel personnelInfo = UserInfo.FirstOrDefault();
                    personnelInfo.AppMaxCount = personnelInfo.AppMaxCount - 1;
                    _context.Update(personnelInfo);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachApplyAndReturnModelExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            }
            return RedirectToAction(nameof(Index));
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
