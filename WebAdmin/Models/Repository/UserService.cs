using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAdmin.Models.ModelView;
using WebAdmin.Models.MD5;

namespace WebAdmin.Models.Repository
{
    public class UserService
    {
        private WebBanHangDB _context =new WebBanHangDB();

        public UserService()
        {
           
        }

        public async Task<ApiResult> Create(User use)
        {
            if (use.id > 0)
            {
                var UserUpdate = await _context.Users.Where(x => x.Email != null && x.Email.Trim().Equals(use.Email.Trim()) && x.id != use.id && x.delete != true).FirstOrDefaultAsync();
                if (UserUpdate != null)
                {
                    return new ApiResult() { Message = "Tài khoản này không tồn tại", Success = false };
                }
                UserUpdate = await _context.Users.Where(x => x.id == use.id).FirstOrDefaultAsync();

                if (UserUpdate == null)
                {
                    return new ApiResult() { Message = "Không Tìm Thấy", Success = false };
                }
                try
                {
                    UserUpdate.idRole = use.idRole;
                    UserUpdate.UserName = use.UserName;
                    UserUpdate.Email = use.Email;

                    await _context.SaveChangesAsync();
                    return new ApiResult() { Message = "Cập nhật thành công", Success = true, Data = use.id };
                }
                catch
                {
                    return new ApiResult() { Message = "Lỗi Server", Success = false };
                }


            }
            var Users = await _context.Users.Where(x => x.Email != null && x.Email.Trim().Equals(use.Email.Trim())).FirstOrDefaultAsync();
            if (Users != null)
            {
                return new ApiResult() { Message = "Tên đăng nhập này đã tồn tại", Success = false };
            }
            try
            {
                Users = new User()
                {
                    UserName = use.UserName,
                    Email = use.Email,
                    CreateDate = DateTime.Now,
                    idRole = use.idRole,
                    Pass = new  WebAdmin.Models.MD5.MD5().GetMD5("123456"),
                };
                _context.Users.Add(Users);
                var id = await _context.SaveChangesAsync();
                return new ApiResult() { Message = "Thêm mới thành công", Success = true, Data = id };
            }
            catch
            {
                return new ApiResult() { Message = "Lỗi Server", Success = false };
            }



        }

        public async Task<ApiResult> DeleteRole(long id)
        {
            try
            {

                var Role = await _context.Roles.Where(x => x.id == id).FirstOrDefaultAsync();
                if (Role == null)
                {
                    return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };
                }
                var RolePermisstion = await _context.Role_Permisstion.Where(x => x.idRole == id).ToListAsync();
                _context.Role_Permisstion.RemoveRange(RolePermisstion);
                Role.delete = true;
                await _context.SaveChangesAsync();
                return new ApiResult() { Data = null, Message = "Xóa Thành Công", Success = true };
            }
            catch { return new ApiResult() { Data = null, Message = "Lỗi Hệ Thống", Success = false }; }

        }

        public async Task<ApiResult> DeleteUse(long id)
        {
            try
            {

                var USE = await _context.Users.Where(x => x.id == id).FirstOrDefaultAsync();
                if (USE == null)
                {
                    return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };
                }
                USE.Email = "";
                USE.delete = true;
                await _context.SaveChangesAsync();
                return new ApiResult() { Data = null, Message = "Xóa Thành Công", Success = true };
            }
            catch { return new ApiResult() { Data = null, Message = "Lỗi Hệ Thống", Success = false }; }
        }

        public async Task<UserView> GetAll(int? status)
        {
            UserView view = new UserView();
            var Users = await _context.Users.ToListAsync();
            var Roles = await _context.Roles.Where(x => x.delete != true).ToListAsync();
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
            return view;
        }

        public async Task<RoleView> GetAllRole()
        {
            RoleView view = new RoleView();
            view.Permisstion = await _context.Permisstions.ToListAsync();
            view.Role = await _context.Roles.Where(x => x.delete != true).ToListAsync();
            return view;
        }

        public async Task<ApiResult> GetRoleByid(long id)
        {
            _context.Configuration.ProxyCreationEnabled = false;

            var Role = await _context.Roles.FirstOrDefaultAsync(x => x.id == id && x.delete != true);
            if (Role == null)
            {
                return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };
            }
     
        var  Role_Permisstion= await _context.Role_Permisstion.Where(x => x.idRole == id).Select(x => x).Select(x => new {  x.id, idPermisstion = x.idPermisstion, idRole = x.idRole }).ToListAsync();
            var data = new
            {
                Role,
                Role_Permisstion
            };
            _context.Configuration.ProxyCreationEnabled = false;
     
            return new ApiResult() { Data = data, Message = "Success", Success = true };
        }

        public async Task<ApiResult> ResetPassUse(long id)
        {
            var UserUpdate = await _context.Users.Where(x => x.id == id).FirstOrDefaultAsync();
            if (UserUpdate == null)
            {
                return new ApiResult() { Message = "Tài khoản này không tồn tại", Success = false };
            }



            try
            {
                UserUpdate.Pass = new WebAdmin.Models.MD5.MD5().GetMD5("123456");
                await _context.SaveChangesAsync();
                return new ApiResult() { Message = "Reset thành công", Success = true, Data = UserUpdate };
            }
            catch
            {
                return new ApiResult() { Message = "Lỗi Server", Success = false };
            }

        }

        public async Task<ApiResult> RoleCreate(Role Role)
        {
            try
            {

                if (Role.id > 0)
                {
                    var RoleUpdate = await _context.Roles.Where(x => x.id == Role.id).FirstOrDefaultAsync();
                    if (RoleUpdate == null)
                    {
                        return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };
                    }
                    RoleUpdate.Name = Role.Name;
                    RoleUpdate.Description = Role.Description;
                    await _context.SaveChangesAsync();

                    var Role_Permisstion = await _context.Role_Permisstion.Where(x => x.idRole == Role.id).ToListAsync();
                    _context.Role_Permisstion.RemoveRange(Role_Permisstion);
                    await _context.SaveChangesAsync();
                    foreach (var item in Role.Role_Permisstion)
                    {
                        Role_Permisstion role_Permisstion = new Role_Permisstion()
                        {
                            idPermisstion = item.idPermisstion,
                            idRole = (long)Role.id
                        };
                        _context.Role_Permisstion.Add(role_Permisstion);
                        await _context.SaveChangesAsync();
                    }
                    return new ApiResult()
                    {
                        Data = Role.id,
                        Success = true,
                        Message = "Cập nhật thành công"
                    };
                }

                Role role = new Role()
                {
                    Name = Role.Name,
                    Description = Role.Description
                };
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                foreach (var item in Role.Role_Permisstion)
                {
                    Role_Permisstion role_Permisstion = new Role_Permisstion()
                    {
                        idPermisstion = item.idPermisstion,
                        idRole = (long)role.id
                    };
                    _context.Role_Permisstion.Add(role_Permisstion);
                    await _context.SaveChangesAsync();
                }
                return new ApiResult()
                {
                    Data = role.id,
                    Success = true,
                    Message = "Thêm thành công"
                };
            }
            catch (Exception e)
            {
                return new ApiResult()
                {
                    Data = null,
                    Success = false,
                    Message = "Lỗi Server"
                };
            }

        }
    }
}