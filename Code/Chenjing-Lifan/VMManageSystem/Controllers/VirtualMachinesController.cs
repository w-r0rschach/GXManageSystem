using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VMManageSystem.Data;
using VMManageSystem.Models;

namespace VMManageSystem.Controllers
{
    /// <summary>
    /// 虚拟机控制器
    /// </summary>
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
        public async Task<IActionResult> Apply([Bind("Approval,UsedDays,UsedCount,OperatingSystem")] ApprovalViewModel approvalView)
        {
            //TODO得到申请者Id（Session、Cookie）
            int id = 1;
            //得到申请者对虚拟机最大申请数量
            var personnels = from m in _context.Common_PersonnelInfo
                             where m.PersonnelId == id
                             select m;
            //判断当前用户是否存在
            if (personnels.Any())
            {
                //申请虚拟机
                bool result = await ApplyVM(id, personnels.ToArray()[0].AppMaxCount, approvalView);
                if (result) //如果申请成功
                {
                    //跳转到已申请信息界面
                    return RedirectToAction(nameof(ApprovalDetails));
                }
                else//申请失败，告知用户申请失败原因 
                {
                    return NotFound();
                }
            }
            else //未授权操作
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 申请虚拟机
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ApplyVM(int userId, int maxUsableCount, ApprovalViewModel model)
        {
            bool flag = false;
            //获取可用虚拟机列表
            List<VirtualMachineModel> usableVMs = GetUsableVMs(model.OperatingSystem, MachineStatueEnum.None);
            if (model.UsedCount > maxUsableCount)//超过使用虚拟机最大数
            {
                //向管理员申请，等待管理员审批完成
                _context.MachApplyAndReturn.Add((ApprovalModel)model.Approval.Clone());
                flag = true;
            }
            else//在可申请数量范围内
            {
                //如果申请数量超过空闲数量就使用空闲数量，反之则使用申请数量
                int count = model.UsedCount > usableVMs.Count ? usableVMs.Count : model.UsedCount;
                //申请多个
                for (int i = 0; i < count; i++)
                {
                    //系统自动审批
                    model.Approval.ExamineResult = ApprovalResultEnum.Pass;
                    //系统自动分配虚拟机
                    model.Approval.MachineInfoID = usableVMs[i].MachineId;
                    model.Approval.ApplyUserID = userId;
                    model.Approval.ExamineUserID = 1;//TODO应该为系统Id
                    //修改已分配虚拟机状态
                    _context.MachineInfo.ToList().Find(o =>
                        o.MachineId == model.Approval.MachineInfoID).MachineState = MachineStatueEnum.BeingUsed;
                    //添加审批记录
                    _context.MachApplyAndReturn.Add((ApprovalModel)model.Approval.Clone());
                }
                flag = true;
            }
            await _context.SaveChangesAsync();
            return flag;
        }

        /// <summary>
        /// 获取可用的虚拟机Id集合
        /// </summary>
        /// <param name="os">操作系统</param>
        /// <param name="statue">虚拟机状态</param>
        /// <returns></returns>
        private List<VirtualMachineModel> GetUsableVMs(OperatingSystemEnum os, MachineStatueEnum statue)
        {
            //查询所有符合条件的虚拟机Id
            var vmIds = from m in _context.MachineInfo
                        where m.MachineSystem == os &&
                        m.MachineState == statue
                        select m;
            return vmIds.ToList();
        }

        /// <summary>
        /// 已申请信息
        /// </summary>
        /// <returns></returns>
        public IActionResult ApprovalDetails()
        {
            //查询所有审批单
            var approvals = from m in _context.MachApplyAndReturn
                            orderby m.ExamineResult
                            select m;
            //查询所有员工
            var personnels = from m in _context.Common_PersonnelInfo
                             select m;
            //查询所有虚拟机
            var vms = from m in _context.MachineInfo
                      select m;
            ApprovalViewModel model = new ApprovalViewModel()
            {
                Approvals = approvals.ToList(),
                Personnels = personnels.ToList(),
                VirtualMachines = vms.ToList()
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
        public async Task<IActionResult> Agree(int? id, int? os)
        {
            //查询所有审批单
            var approvals = from m in _context.MachApplyAndReturn
                            where m.ApplyAndReturnId == id
                            select m;
            //查询所有符合条件的虚拟机Id
            var vmIds = from m in _context.MachineInfo
                        where m.MachineSystem == (OperatingSystemEnum)os &&
                        m.MachineState != MachineStatueEnum.BeingUsed
                        select m.MachineId;
            //查询所有审批单中虚拟机Id
            var appVMIds = from m in _context.MachApplyAndReturn
                           where m.MachineInfoID != 1
                           select m.MachineInfoID;
            //取差集
            var exIds = vmIds.Except(appVMIds);

            if (approvals.Any())
            {
                approvals.ToArray()[0].ExamineResult = ApprovalResultEnum.Pass;
                approvals.ToArray()[0].MachineInfoID = exIds.ToArray()[0];
                _context.MachineInfo.ToList().Find(o => o.MachineId == exIds.ToArray()[0]).MachineState = MachineStatueEnum.BeingUsed;
                await _context.SaveChangesAsync();
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
        public IActionResult Login(string user, string pwd)
        {
            if (string.IsNullOrWhiteSpace(user) && string.IsNullOrWhiteSpace(pwd))
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                if (user.Equals("JWT") && pwd.Equals("JWT"))
                {
                    ClaimsIdentity identity = new ClaimsIdentity("Forms");
                    identity.AddClaim(new Claim(ClaimTypes.Name, user));
                    identity.AddClaim(new Claim(ClaimTypes.Role, "用户"));//TODO换成具体的权限
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    return RedirectToAction(nameof(Index));
                }
                else
                    return RedirectToAction("Login", "Home");
            }
        }

        #endregion

        #region  默认创建的内容

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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


            var personnels = from m in _context.Common_PersonnelInfo
                             select m;

            vmModels.Personnels = personnels.ToList();

            //var a = personnels.ToList();

            var vmLst = from m in _context.MachApplyAndReturn
                        select m;
            //var p = n.ToList();

            foreach (VirtualMachineModel model in vmModels.VirtualMachines)
            {
                model.Approvals = vmLst.ToList();
            }
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
