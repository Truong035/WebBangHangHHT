using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebAdmin.Contant;
using WebAdmin.Models;
using WebAdmin.Models.ModelView;
using WebAdmin.Models.Repository;

namespace WebAdmin.Controllers
{
    public class UseController : Controller
    {
        private WebBanHangDB db = new WebBanHangDB();
        // GET: User
        public async Task<ActionResult> Index()
        {
            UserView view = new UserView();
            var Users = await db.Users.ToListAsync();
            var Roles = await db.Roles.Where(x => x.delete != true).ToListAsync();
            view.Role = Roles;
            view.User = (from u in Users
                         join R in Roles on u.idRole equals R.id
                         where u.delete != true && R.delete != true
                         select new User()
                         {
                             UserName = u.UserName,
                             Email = u.Email,
                             id = u.id,
                             idRole = u.idRole,
                             Role = R,
                         }
                      ).ToList();
     
            return View(view);
        }
        public async Task<ActionResult> Role()
        {
            //string cookie = HttpContext.Request.Cookies[UserContant.UseToken].Value;
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return RedirectToAction("Index", "Account");
            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
            RoleView view = new RoleView();
            view.Permisstion = await db.Permisstions.ToListAsync();
            view.Role = await db.Roles.Where(x => x.delete != true).ToListAsync();
            return View(view);
            //}
            //return View("UseNotFound");
        }


        public async Task<ActionResult> RoleCreate(Role Role)
        {
            //string cookie = Request.Cookies[UserContant.UseToken];
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
          
                return Json(await new UserService().RoleCreate(Role), JsonRequestBehavior.AllowGet);
    
            

            //}
            //return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });
        }
        public async Task<ActionResult> Create(User use)
        {

            //   string cookie = Request.Cookies[UserContant.UseToken];
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
            return Json(await new UserService().Create(use), JsonRequestBehavior.AllowGet);
          //  return Ok(await service.Create(use));

            //}
            //return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });
        }
        public async Task<ActionResult> GetRole(long id)
        {
            //string cookie = Request.Cookies[UserContant.UseToken];
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
            return Json(await new UserService().GetRoleByid(id), JsonRequestBehavior.AllowGet);
            //return Ok(await service.GetRoleByid(id));

            //}
            //return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });

        }

        public async Task<ActionResult> DeleteRole(long id)
        {
            //string cookie = Request.Cookies[UserContant.UseToken];
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
            return Json( await new UserService().DeleteRole(id), JsonRequestBehavior.AllowGet);
            //}
            //return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });

        }

        public async Task<ActionResult> ResetPass(long id)
        {
            //string cookie = Request.Cookies[UserContant.UseToken];
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
            return Json( await new UserService().ResetPassUse(id), JsonRequestBehavior.AllowGet);
           // return Ok(await service.ResetPassUse(id));


            //}
            //return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });

        }

        public async Task<ActionResult> DeleteUse(long id)
        {
            //string cookie = Request.Cookies[UserContant.UseToken];
            //if (cookie == null || cookie.Length == 0)
            //{
            //    return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            //}
            //var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            //if (check)
            //{
            return Json(await new UserService().DeleteUse(id), JsonRequestBehavior.AllowGet);
            //return Ok(await service.DeleteUse(id));

            //}
            //return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });

        }

      
        

    }
}