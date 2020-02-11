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

            return View(vmModels);
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

        // GET: VirtualMachines/Create
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
        }

        // POST: VirtualMachines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MachineId,Guid,PersonneId,MachineIP,MachineSystem,MachineDiskCount,MachineMemory,MachineState,MachineUser,MachinePassword")] VirtualMachine virtualMachine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(virtualMachine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(virtualMachine);
        }

        // GET: VirtualMachines/Edit/5
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
        public async Task<IActionResult> Edit(int id, [Bind("MachineId,Guid,PersonneId,MachineIP,MachineSystem,MachineDiskCount,MachineMemory,MachineState,MachineUser,MachinePassword")] VirtualMachine virtualMachine)
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
    }
}
