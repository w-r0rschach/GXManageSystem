using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VMMachineManage.Data;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class AuitMachineController : RightsController
    {
        private readonly VMMachineManageContext _context;

        public AuitMachineController(VMMachineManageContext context)
        {
            Role = 1;
            _context = context;
        }
        // GET: AuitMachine.
        
        public ActionResult Index()
        {
            PersonnelInfoModel userInfo = JsonConvert.DeserializeObject<PersonnelInfoModel>(HttpContext.Session.GetString("User"));
            var info = from m in _context.MachApplyAndReturn
                       join m1 in _context.MachineInfo on m.MachineInfoID equals m1.MachineID
                       join m2 in _context.Common_PersonnelInfo on m.ApplyUserID equals m2.PersonnelId
                       where (m.ExamineResult == 0 && m.ExamineUserID == userInfo.PersonnelId&&
                       m1.MachineState==1&&m.OprationType==0)
                       select new VMMachineInfoModel
                       { 
                           machineInfo=m1,
                           PersonnelInfo=m2,
                           MachApplyAndReturn=m
                       };
            return View(info);
        }

        // GET: AuitMachine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuitMachine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuitMachine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuitMachine/Edit/5
        public ActionResult Edit(int id,int ApplyAndReturnId,int ExamineResult)
        {
            var info = from m in _context.MachApplyAndReturn where (m.ApplyAndReturnId == ApplyAndReturnId) select m;
            MachApplyAndReturnModel machApplyAndReturn = info.FirstOrDefault();
            machApplyAndReturn.ExamineResult = ExamineResult;

            var machinInfo= from m in _context.MachineInfo where (m.MachineID == machApplyAndReturn.MachineInfoID) select m;
            MachineInfoModel machineInfo = machinInfo.FirstOrDefault();
            if (ExamineResult==2)
            {
                machineInfo.MachineState = 2;
            }
            else
            {
                machineInfo.MachineState = 0;

                var UserInfo = from m in _context.Common_PersonnelInfo.Where(m => m.PersonnelId == Convert.ToInt32(machApplyAndReturn.ApplyUserID)) select m;
                PersonnelInfoModel personnelInfo = UserInfo.FirstOrDefault();
                personnelInfo.AppMaxCount = personnelInfo.AppMaxCount - 1;
                _context.Update(personnelInfo);
            }
            
            _context.Update(machApplyAndReturn);
            _context.Update(machineInfo);
            _context.SaveChanges();
            return RedirectToAction("Index", "AuitMachine"); 
        }

        // POST: AuitMachine/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuitMachine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuitMachine/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}