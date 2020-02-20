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
    public class MachApplyAndReturnController : RightsController
    {
        private readonly VMMachineManageContext _context;

       

        public MachApplyAndReturnController(VMMachineManageContext context)
        {
            Role = 2;
            _context = context;
        }

        // GET: MachApplyAndReturn
        public IActionResult Index(string searchString)
        {
            var machines = from m in _context.MachineInfo
                           where m.MachineState == 0    // 空闲状态
                           orderby m.MachineSystem ascending, m.MachineDiskCount ascending, m.MachineMemory ascending
                           group m by new { m.MachineSystem, m.MachineDiskCount, m.MachineMemory } into b
                           select new MachineInfoModel
                           {
                               MachineSystem = b.Key.MachineSystem,
                               MachineDiskCount = b.Key.MachineDiskCount,
                               MachineMemory = b.Key.MachineMemory,
                               MachineState = b.Count() // 临时当做剩余数量显示

                           };
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                machines = machines.Where(option => option.MachineState == 0 && (option.MachineIP.Contains(searchString) ||
                  option.MachineUser.Contains(searchString)));
            }

           
           
            return View(machines.ToList());
        }



        // GET: MachApplyAndReturn/Create
        // <summary>
        /// GET
        /// Vmware/Apply
        /// 确认申请
        /// </summary>/
        /// <param name="MachineSystem">操作系统 0：Windows 1：Linux</param>
        /// <param name="MachineDiskCount">硬盘大小/G</param>
        /// <param name="MachineMemory">内存大小/G</param>
        /// <param name="FreeNumber">空闲数量</param>
        /// <returns>IActionResult</returns>
        public IActionResult Create(int machineSystem, double machineDiskCount, double machineMemory, int freeNumber)
        {
            if (machineSystem >= 2 || machineDiskCount == 0 || machineMemory == 0 || freeNumber == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewData["MachineSystem"] = machineSystem;
            ApplyDataModel applyData = new ApplyDataModel();
            applyData.MachineDiskCount = machineDiskCount;
            applyData.MachineMemory = machineMemory;
            applyData.MachineSystem = machineSystem;
            applyData.Number = freeNumber;
            return View(applyData);
        }

        // POST: MachApplyAndReturn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="ApplyDataModel">返回申请信息</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplyDataModel ApplyDataModel)
        {
            PersonnelInfoModel userInfo = JsonConvert.DeserializeObject<PersonnelInfoModel>(HttpContext.Session.GetString("User"));
            if (userInfo.PersonnelId<1)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                if (ModelState.IsValid)
            {
                MachApplyAndReturnModel machApplyAndReturnModel = new MachApplyAndReturnModel();

                // 最大数量
                int appMaxCount = 3;

                IEnumerable<MachineInfoModel> list = from m in _context.MachineInfo
                                                     where m.MachineState == 0
                                                        && m.MachineSystem == ApplyDataModel.MachineSystem
                                                        && m.MachineDiskCount == ApplyDataModel.MachineDiskCount
                                                        && m.MachineMemory == ApplyDataModel.MachineMemory
                                                     select m;

                // 空闲数量小于申请数量 申请失败
                if (list.Count() < ApplyDataModel.Number)
                {
                    return View("Views/MachApplyAndReturn/Error.cshtml");
                }
                //1、查询当前用户申请的所有虚拟机数量
                //2、最新申请数量=已申请数量+本次申请数量>默认申请数量，使用审批流程
                var UserInfo = from m in _context.Common_PersonnelInfo.Where(m => m.PersonnelId == userInfo.PersonnelId) select m;
                PersonnelInfoModel personnelInfo = UserInfo.FirstOrDefault();
                personnelInfo.AppMaxCount = personnelInfo.AppMaxCount + ApplyDataModel.Number;

                // 未超过数量系统自动审批
                // 超过数量由管理员审批
                // 修改虚拟机状态：申请中
                for (int i = 0; i < ApplyDataModel.Number; i++)
                {
                    var model = list.ElementAt(i);

                    MachApplyAndReturnModel machApplyAndReturn = new MachApplyAndReturnModel();
                    machApplyAndReturn.OprationType = 0;
                    machApplyAndReturn.ApplyUserID = userInfo.PersonnelId;
                    machApplyAndReturn.ExamineUserID = Convert.ToInt32(ApplyDataModel.ExamineUserName);
                    machApplyAndReturn.MachineInfoID = model.MachineID;
                    machApplyAndReturn.ExamineResult = (personnelInfo.AppMaxCount <= appMaxCount ? 2 : 0);
                    machApplyAndReturn.ApplyTime = ApplyDataModel.ApplyTime;
                    machApplyAndReturn.ResultTime = ApplyDataModel.ApplyTime.AddDays(ApplyDataModel.DaysNumber); //归还时间=申请时间+默认天数
                    machApplyAndReturn.Remark = ApplyDataModel.Remark;
                    // 添加到申请记录表
                    _context.MachApplyAndReturn.Add(machApplyAndReturn);
                    if (machApplyAndReturn.ExamineResult == 2)
                    {
                        model.MachineState = 2;
                    }
                    if (machApplyAndReturn.ExamineResult == 0)
                    {
                        model.MachineState = 1;
                    }
                        _context.MachineInfo.Update(model);
                   
                }


                _context.Common_PersonnelInfo.Update(personnelInfo);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index", "MyMachine");
        }

        private bool MachApplyAndReturnModelExists(int id)
        {
            return _context.MachApplyAndReturn.Any(e => e.ApplyAndReturnId == id);
        }
    }
}
