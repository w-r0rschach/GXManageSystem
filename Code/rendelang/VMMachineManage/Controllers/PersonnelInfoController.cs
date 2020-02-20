using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.International.Converters.PinYinConverter;
using VMMachineManage.Data;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class PersonnelInfoController : RightsController
    {
        private readonly VMMachineManageContext _context;

        public PersonnelInfoController(VMMachineManageContext context)
        {
            Role = 1;
            _context = context;
        }

        // GET: PersonnelInfo
       
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
        public async Task<IActionResult> Create(string UserName,string PassWord)
        {

            if (ModelState.IsValid)
            {
                PersonnelInfoModel personnelInfoModel = new PersonnelInfoModel();
                personnelInfoModel.UserName = UserName;
                personnelInfoModel.PassWord = PassWord;
                personnelInfoModel.DepId = 2;

                _context.Add(personnelInfoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// 将汉字转换为拼音
        /// </summary>
        /// <param name="str">汉字</param>
        /// <returns></returns>
        private string PinYinConverter(string str)
        {
            string result = string.Empty;
            foreach (char item in str)
            {
                try
                {
                    ChineseChar cc = new ChineseChar(item);
                    if (cc.Pinyins.Count > 0 && cc.Pinyins[0].Length > 0)
                    {
                        string temp = cc.Pinyins[0].ToString();
                        result += temp.Substring(0, temp.Length - 1);
                    }
                }
                catch (Exception)
                {
                    result += item.ToString();
                }
            }
            return result.ToLower();
            // Console.WriteLine(result);//"WOAINIZHONGGUO!123"

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
