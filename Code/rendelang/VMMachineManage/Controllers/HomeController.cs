

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using VMMachineManage.Data;

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
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string userName, string userPwd)
        { 
            if (string.IsNullOrWhiteSpace(userName)|| string.IsNullOrWhiteSpace(userPwd))
            {
                return BadRequest("请输入用户名或密码!");
            }

            var loginuser = await _context.Common_PersonnelInfo.
                FirstOrDefaultAsync(s => s.UserName == userName && s.PassWord == userPwd);

            if (loginuser.UserName!= userName)
            {
                return BadRequest("用户名不存在!");
            }
            if (loginuser.PassWord != userPwd)
            {
                return BadRequest("密码错误");
            }
            string json = JsonConvert.SerializeObject(loginuser);
            // 存入Session
            HttpContext.Session.Set("User", System.Text.Encoding.UTF8.GetBytes(json));
            // 存入Session
            if (loginuser.DepId == 1)
            {
                return RedirectToAction("Index", "MachineInfo");
            }
            else
            {
                return RedirectToAction("Index", "MyMachine");
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public IActionResult LoginOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
