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
    public class MachineInfoController : RightsController
    {
        private readonly VMMachineManageContext _context;

        public MachineInfoController(VMMachineManageContext context)
        {
            Role = 1;
            _context = context;
        }

        // GET: MachineInfo
      
        public async Task<IActionResult> Index(string searchString="")
        {
            string sid = User.FindFirstValue(ClaimTypes.Sid);//获取ID

            var machines = from m in _context.MachineInfo select m;
                         
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                machines = machines.Where(option => option.MachineIP.Contains(searchString) ||
                  option.MachineUser.Contains(searchString));
            }
            MachineInfoViewModel machineInfoViewModel = new MachineInfoViewModel()
            {
                MachineInfo = machines.ToList()
            };
            return View(machineInfoViewModel);
        } 

        // GET: MachineInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineInfoModel = await _context.MachineInfo
                .FirstOrDefaultAsync(m => m.MachineID == id);
            if (machineInfoModel == null)
            {
                return NotFound();
            }

            return View(machineInfoModel);
        }
        /// <summary>
        /// 虚拟机回收
        /// </summary>
        /// <param name="id">回收的虚拟机Id</param>
        /// <returns></returns>
        public  IActionResult Recycling(int id)
        {
            //修改虚拟机状态
            var machineInfo = from m in _context.MachineInfo where (m.MachineID == id) select m;
            MachineInfoModel infoModel = machineInfo.FirstOrDefault();
            infoModel.MachineState = 0;
            _context.Update(infoModel);
             
            //修改虚拟机申请表单数量
            var machineReturn = from m in _context.MachApplyAndReturn.Where(m=>m.MachineInfoID == id && m.OprationType == 0 &&
                              m.ExamineResult == 2 ) select m;
            MachApplyAndReturnModel machApplyAndReturn  = machineReturn.FirstOrDefault();
            machApplyAndReturn.OprationType = 1;
            machApplyAndReturn.ResultTime = DateTime.Now;
            _context.Update(machApplyAndReturn);
            //修改员工持有虚拟机数量
            var UserInfo = from m in _context.Common_PersonnelInfo.Where(m => m.PersonnelId == Convert.ToInt32(machApplyAndReturn.ApplyUserID)) select m;
            PersonnelInfoModel personnelInfo = UserInfo.FirstOrDefault();
            personnelInfo.AppMaxCount = personnelInfo.AppMaxCount - 1;
            _context.Update(personnelInfo);
            _context.SaveChanges();
            return RedirectToAction("Index", "MachineInfo");
        }
        // GET: MachineInfo/Create
        public IActionResult Create()
        {
            var machine = from m in _context.MachineInfo select m;
           
            MachineInfoViewModel viewMode = new MachineInfoViewModel()
            { 
                MachineInfo =  machine.ToList()
            };

            return View();
        }

        // POST: MachineInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachineID,MachineIP,MachineSystem,MachineDiskCount,MachineMemory,MachineState,MachineUser,MachinePassword")] MachineInfoModel machineInfoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(machineInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(machineInfoModel);
        }

        // GET: MachineInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineInfoModel = await _context.MachineInfo.FindAsync(id);
            if (machineInfoModel == null)
            {
                return NotFound();
            }
            return View(machineInfoModel);
        }

        // POST: MachineInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MachineID,MachineIP,MachineSystem,MachineDiskCount,MachineMemory,MachineState,MachineUser,MachinePassword")] MachineInfoModel machineInfoModel)
        {
            if (id != machineInfoModel.MachineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineInfoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineInfoModelExists(machineInfoModel.MachineID))
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
            return View(machineInfoModel);
        }

        // GET: MachineInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineInfoModel = await _context.MachineInfo
                .FirstOrDefaultAsync(m => m.MachineID == id);
            if (machineInfoModel == null)
            {
                return NotFound();
            }

            return View(machineInfoModel);
        }

        // POST: MachineInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var machineInfoModel = await _context.MachineInfo.FindAsync(id);
            _context.MachineInfo.Remove(machineInfoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MachineInfoModelExists(int id)
        {
            return _context.MachineInfo.Any(e => e.MachineID == id);
        }
    }
}
