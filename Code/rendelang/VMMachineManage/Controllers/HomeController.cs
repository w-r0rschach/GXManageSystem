using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VMMachineManage.Data;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class HomeController : Controller
    {
        private readonly VMMachineManageContext _context;


        public HomeController(VMMachineManageContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// 登录方法
        /// 实现思路：根据界面填入的值如果和默认的用户名和密码不同则无法登录
        /// 目前因无员工数据，未使用数据库数据，用户名或密码暂定为admin，123456
        /// 真实情况为，根据填写的用户名称通过Linq去查询数据库是否有当前用户名
        /// 再根据用户名称去查询对应密码，用户名和密码都匹配是才算登录成功。
        /// </summary>
        /// <param name="user">用户表名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string user, string pwd)
        {
            if (string.IsNullOrWhiteSpace(user))
            {
                return BadRequest("请输入用户名或密码!");
            }
            //var loginuser = await _context.Common_PersonnelInfo.
            //    FirstOrDefaultAsync(s => s.PersonnelName == user);
            if (user !="admin")
            {
               // System.Net.Http.HttpRequestMessage
                return BadRequest("没有该用户!");
            }
            if (pwd!="123456")
            {
                return BadRequest("密码错误!");
            }
            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,user)
            //};
            //ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            //ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            //await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "PersonnelInfo");
        }
    }
}
