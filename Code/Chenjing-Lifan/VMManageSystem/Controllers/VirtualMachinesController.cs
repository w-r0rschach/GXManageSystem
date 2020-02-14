using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VMManageSystem.Data;
using VMManageSystem.Models;

namespace VMManageSystem.Controllers
{
    public class VirtualMachinesController : Controller
    {
        private readonly VirtualMachineContext _context;

        public VirtualMachinesController(VirtualMachineContext context)
        {
            _context = context;
        }

        #region  后续增加业务

        /// <summary>
        /// 申请页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Apply()
        {
            return View();
        }

        /// <summary>
        /// 申请虚拟机
        /// </summary>
        /// <param name="approvalView"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply([Bind("Approval,UsedDays,UsedCount")] ApprovalViewModel approvalView)
        {

            _context.MachApplyAndReturn.Add(approvalView.Approval);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        /// <summary>
        /// 已申请信息
        /// </summary>
        /// <returns></returns>
        public IActionResult ApprovalDetails()
        {
            var n = from j in _context.MachApplyAndReturn
                    select j;
            ApprovalViewModel model = new ApprovalViewModel()
            {
                Approvals = n.ToList()
            };
            return View(model);
        }

        /// <summary>
        /// 同意申请
        /// 逻辑：同意后根据Id找到审批单中的申请信息
        /// 再查询所有符合条件虚拟机，将查询得到的第
        /// 一条虚拟机Id赋值给审批单中的虚拟机Id，然
        /// 后将审批人的Id复制审批单中的审批人Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Agree(int? id)
        {
            var approvals = from m in _context.MachApplyAndReturn
                    where m.ApplyAndReturnId == id
                    select m;
            if (approvals.Any()) 
            {
                approvals.ToArray()[0].ExamineResult = ApprovalResultEnum.Pass;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(ApprovalDetails));
        }

        /// <summary>
        /// 拒绝申请
        /// 逻辑：修改审批单中的状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Disagree(int? id)
        {
            var approvals = from m in _context.MachApplyAndReturn
                            where m.ApplyAndReturnId == id
                            select m;
            if (approvals.Any())
            {
                approvals.ToArray()[0].ExamineResult = ApprovalResultEnum.NotPass;
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(ApprovalDetails));
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<IActionResult> Login(string user, string pwd)
        {
            if (string.IsNullOrWhiteSpace(user) && string.IsNullOrWhiteSpace(pwd))
                return RedirectToAction("Login", "Home");
            else
            {
                if (user.Equals("JWT") && pwd.Equals("JWT"))
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction("Login", "Home");
            }
        }




        #endregion

        #region  默认创建的内容

        // GET: VirtualMachines
        public async Task<IActionResult> Index(string searchString)
        {
            var virtualMachines = from m in _context.MachineInfo
                                  select m;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                virtualMachines = virtualMachines.Where(options =>
                     options.MachineIP.Contains(searchString) ||
                     options.MachineUser.Contains(searchString));
            }

            OperatingSystemEnum[] operatingSystem = new OperatingSystemEnum[2];
            operatingSystem[0] = OperatingSystemEnum.Windows;
            operatingSystem[1] = OperatingSystemEnum.Linux;
            VirtualMachineViewModel vmModels = new VirtualMachineViewModel()
            {
                OperationSystem = new SelectList(operatingSystem),
                VirtualMachines = await virtualMachines.ToListAsync()
            };


            var s = from m in _context.Common_PersonnelInfo
                    select m;
            var a = s.ToList();

            var n = from j in _context.MachApplyAndReturn
                    select j;
            var p = n.ToList();

            //foreach (VirtualMachineModel model in vmModels.VirtualMachines)
            //{
            //    var d = p.Find(o => o.MachineInfoID.Equals(model.MachineId));
            //    var c = a.Find(o => o.PersonnelId.Equals(d.ApplyUserID));
            //    vmModels.Name = c.PersonnelName;
            //}
            return View(vmModels);
        }

        // GET: VirtualMachines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virtualMachine = await _context.MachineInfo
                .FirstOrDefaultAsync(m => m.MachineId == id);
            if (virtualMachine == null)
            {
                return NotFound();
            }

            return View(virtualMachine);
        }

        /// <summary>
        /// 添加虚拟机页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var virtualMachines = from m in _context.MachineInfo
                                  select m;
            OperatingSystemEnum[] operatingSystem = new OperatingSystemEnum[2];
            operatingSystem[0] = OperatingSystemEnum.Windows;
            operatingSystem[1] = OperatingSystemEnum.Linux;
            VirtualMachineViewModel vmModels = new VirtualMachineViewModel()
            {
                OperationSystem = new SelectList(operatingSystem),
                VirtualMachines = virtualMachines.ToList()
            };

            return View();
            //return View(virtualMachines.ToArray());
        }

        /// <summary>
        /// 添加虚拟机方法
        /// </summary>
        /// <param name="virtualMachine"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachineIP,MachineSystem,MachineDiskCount,MachineMemory,MachineState,MachineUser,MachinePassword")] VirtualMachineModel virtualMachine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(virtualMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(virtualMachine);
        }

        /// <summary>
        /// 批量添加虚拟机
        /// </summary>
        /// <param name="ipLst">IP地址列表</param>
        /// <param name="osLst">操作系统列表</param>
        /// <param name="diskLst">磁盘容量列表</param>
        /// <param name="memoryLst">内存容量列表</param>
        /// <param name="stateLst">状态列表</param>
        /// <param name="userLst">账号列表</param>
        /// <param name="pwdLst">密码列表</param>
        /// <returns></returns>
        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public string CreateVMs(List<string> ipLst, List<byte> osLst, List<double> diskLst, List<double> memoryLst,
            List<byte> stateLst, List<string> userLst, List<string> pwdLst)
        {
            string hreaf = string.Empty;
            if (ModelState.IsValid)
            {

                for (int i = 0; i < ipLst.Count; i++)
                {
                    VirtualMachineModel vm = new VirtualMachineModel()
                    {
                        MachineIP = ipLst[i],
                        //MachineSystem = (OperatingSystemEnum)osLst[i],
                        MachineDiskCount = diskLst[i],
                        MachineMemory = memoryLst[i],
                        //MachineState = (MachineStatueEnum)stateLst[i],
                        MachineUser = userLst[i],
                        MachinePassword = pwdLst[i]
                    };
                    _context.MachineInfo.Add(vm);
                }
                _context.SaveChangesAsync();
                hreaf = "Index";
            }
            return hreaf;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virtualMachine = await _context.MachineInfo.FindAsync(id);
            if (virtualMachine == null)
            {
                return NotFound();
            }
            return View(virtualMachine);
        }

        // POST: VirtualMachines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MachineId,Guid,PersonneId,MachineIP,MachineSystem,MachineDiskCount,MachineMemory,MachineState,MachineUser,MachinePassword")] VirtualMachineModel virtualMachine)
        {
            if (id != virtualMachine.MachineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(virtualMachine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VirtualMachineExists(virtualMachine.MachineId))
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
            return View(virtualMachine);
        }

        // GET: VirtualMachines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virtualMachine = await _context.MachineInfo
                .FirstOrDefaultAsync(m => m.MachineId == id);
            if (virtualMachine == null)
            {
                return NotFound();
            }

            return View(virtualMachine);
        }

        // POST: VirtualMachines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var virtualMachine = await _context.MachineInfo.FindAsync(id);
            _context.MachineInfo.Remove(virtualMachine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VirtualMachineExists(int id)
        {
            return _context.MachineInfo.Any(e => e.MachineId == id);
        }

        #endregion
    }
}
