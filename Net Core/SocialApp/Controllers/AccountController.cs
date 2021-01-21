using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SocialApp.DAL.Context;
using SocialApp.DAL.Tables;
using SocialApp.Model;
using SocialApp.Model.Account;

namespace SocialApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly SocialContext _socialContext;
        private readonly IConfiguration _config;
        public AccountController(SocialContext SocialContext, IConfiguration config)
        {
            _socialContext = SocialContext;
            _config = config;

        }
        [Route("AddUser")]
        [HttpPost]
        public async Task<ReturnModel> AddUser([FromBody] AddUserModel userModel)
        {
            ReturnModel returnModel = new ReturnModel();
            var userExists = _socialContext.UserTables.FirstOrDefault(x => x.Email.ToLower().Trim() == userModel.Email.ToLower().Trim());
            if (userExists != null)
            {
                returnModel.Message = "User Already Exists";
                returnModel.StatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                UserTable userTable = new UserTable
                {
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    UserName = userModel.UserName,
                    Password = userModel.Password,
                    Email = userModel.Email,
                    IsSocial = false,
                    CreatedDateTime = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false,
                    IsUpdated = false,
                };
                _socialContext.UserTables.Add(userTable);
                var isSaved = await _socialContext.SaveChangesAsync();
                if (isSaved > 0)
                {
                    returnModel.StatusCode = HttpStatusCode.OK;
                    returnModel.Message = "Successfully Added New User";
                }
                else
                {
                    returnModel.StatusCode = HttpStatusCode.InternalServerError;
                    returnModel.Message = "User Can't Be Added, Please Try Again Later";
                }
            }
            return returnModel;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ReturnModel> Login([FromBody] LoginModel userModel)
        {
            ReturnModel returnModel = new ReturnModel();
            var userExists = _socialContext.UserTables.FirstOrDefault(x => x.Email.ToLower().Trim() == userModel.Email.ToLower().Trim());
            if (userExists != null)
            {
                if (userExists.IsSocial)
                {
                    returnModel.Message = "Please use social Login";
                    returnModel.StatusCode = HttpStatusCode.BadRequest;
                }
                else
                {
                    returnModel.Message = "User can login";
                    returnModel.StatusCode = HttpStatusCode.OK;
                }
            }
            else
            {
                returnModel.Message = "User Doesn't Exists";
                returnModel.StatusCode = HttpStatusCode.NotFound;
            }
            return returnModel;
        }

        [Route("SocialLogin")]
        [HttpPost]
        public async Task<ReturnModel> SocialLogin([FromBody] SocialLoginModel userModel)
        {
            ReturnModel returnModel = new ReturnModel();
            var userExists = _socialContext.UserTables.FirstOrDefault(x => x.Email.ToLower().Trim() == userModel.Email.ToLower().Trim());
            if (userExists == null)
            {
                string FirstName = userModel.Name.Split(" ")[0];
                string LastName = userModel.Name.Split(" ")[1];
                UserTable userTable = new UserTable
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    UserName = FirstName + LastName,
                    SocialId = userModel.Id,
                    SocialProvider = userModel.Provider,
                    Password = "asbdaksbdansdbkahsbndkansbdjasdakjsdnmasjdmabsdjamd",
                    IsSocial = true,
                    Email = userModel.Email,
                    CreatedDateTime = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false,
                    IsUpdated = false,
                };
                _socialContext.UserTables.Add(userTable);
                var isSaved = await _socialContext.SaveChangesAsync();
                if (isSaved > 0)
                {
                    returnModel.StatusCode = HttpStatusCode.OK;
                    returnModel.Message = "Successfully Added New User";
                }
                else
                {
                    returnModel.StatusCode = HttpStatusCode.InternalServerError;
                    returnModel.Message = "User Can't Be Added, Please Try Again Later";
                }
                returnModel.Message = "User can login";
                returnModel.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                returnModel.Message = "User Already Exists";
                returnModel.StatusCode = HttpStatusCode.Redirect;
            }
            return returnModel;
        }
    }
}
