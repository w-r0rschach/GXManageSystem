using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class MachApplyAndReturnController : Controller
    {
        private readonly VMMachineManageContext _context;

        public MachApplyAndReturnController(VMMachineManageContext context)
        {
            _context = context;
        }

        // GET: MachApplyAndReturn
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Index(string searchString)
        {
            var machines = from m in _context.MachineInfo select m;
         
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                machines = machines.Where(option => option.MachineState == 0 && (option.MachineIP.Contains(searchString) ||
                  option.MachineUser.Contains(searchString)));
            }
           
            MachineInfoViewModel viewMode = new MachineInfoViewModel()
            { 
                MachineInfo = await machines.ToListAsync()
            };
            return View(viewMode);
        }


        // GET: MachApplyAndReturn/Details/5
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

        // GET: MachApplyAndReturn/Create
        public IActionResult Create(int id)
        {

            var machines = from m in _context.MachineInfo select m;

            machines = machines.Where(option => option.MachineID == id);

            MachineInfoModel machineinfo = machines.FirstOrDefault();
            MachApplyAndReturnModel viewMode = new MachApplyAndReturnModel()
            {
                MachineSystem = machineinfo.MachineSystem,
                MachineMemory = machineinfo.MachineMemory,
                MachineDiskCount = machineinfo.MachineDiskCount,
                MachineID = machineinfo.MachineID

            };
            return View(viewMode);
        }

        // POST: MachApplyAndReturn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,DaysNumber,ExamineUserName,ApplyAndReturnId,OprationType,ApplyUserID,ExamineUserID,MachineInfoID,ExamineResult,ApplyTime,ResultTime,Remark")] MachApplyAndReturnModel machApplyAndReturnModel)
        {
            if (string.IsNullOrWhiteSpace(User.FindFirstValue(ClaimTypes.Sid)))
            {
                NotFound();
            }
            else
                if (ModelState.IsValid)
            {

                //获取审批人Id
                machApplyAndReturnModel.ExamineUserID = Convert.ToInt32(machApplyAndReturnModel.ExamineUserName);
                machApplyAndReturnModel.ApplyUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.Sid));//获取当前申请人Id
                                                                                                           //归还时间=申请时间+归还天数
                machApplyAndReturnModel.ResultTime = machApplyAndReturnModel.ApplyTime.AddDays(machApplyAndReturnModel.DaysNumber);
                if (machApplyAndReturnModel.Number <= 3)
                {
                    machApplyAndReturnModel.ExamineResult = 0;
                }
                for (int i = 0; i < machApplyAndReturnModel.Number; i++)
                {
                    _context.Add(machApplyAndReturnModel);
                }
                await _context.SaveChangesAsync();

                //修改虚拟机状态
                var machines = from m in _context.MachineInfo select m;
                machines = machines.Where(option => option.MachineID == machApplyAndReturnModel.MachineID);
                MachineInfoModel machineInfo = new MachineInfoModel()
                {
                    MachineState = 1
                };
                try
                {
                    _context.Update(machineInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachApplyAndReturnModelExists(machineInfo.MachineID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //修改用户数量
                var personInfo = from m in _context.Common_PersonnelInfo select m;
                personInfo = personInfo.Where(option => option.PersonnelId == machApplyAndReturnModel.ApplyUserID);
                PersonnelInfoModel personnelInfoModel = new PersonnelInfoModel()
                {
                    AppMaxCount = machApplyAndReturnModel.Number
                };
                try
                {
                    _context.Update(personnelInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachApplyAndReturnModelExists(personnelInfoModel.PersonnelId))
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

        // GET: MachApplyAndReturn/Edit/5
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

        // POST: MachApplyAndReturn/Edit/5
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

        // GET: MachApplyAndReturn/Delete/5
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

        // POST: MachApplyAndReturn/Delete/5
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
