using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using VMMachineManage.Models;

namespace VMMachineManage.Controllers
{
    public class RightsController : Controller
    {
        /// <summary>
        /// 角色
        /// 0：普通
        /// 1：管理
        /// </summary>
        public int Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userJson = context.HttpContext.Session.GetString("User");
            if (string.IsNullOrWhiteSpace(userJson))
            {
                context.Result = new RedirectResult("/Home/Index");
                return;
            }

            ViewBag.User = JsonConvert.DeserializeObject<PersonnelInfoModel>(userJson);

            string controllerName = ((object[])context.RouteData.Values.Values)[1].ToString();

            // 验证权限 0：普通 1:管理
            if (ViewBag.User.DepId != Role)
            {
                context.Result = new RedirectResult("/User/LoginOut");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}