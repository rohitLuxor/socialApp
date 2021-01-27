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
using SocialApp.Model.Admin;

namespace SocialApp.Controllers
{
    [Route("Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SocialContext _socialContext;
        private readonly IConfiguration _config;
        public AdminController(SocialContext SocialContext, IConfiguration config)
        {
            _socialContext = SocialContext;
            _config = config;

        }

        public EntityTable GetEntityModel { get; private set; }

        [Route("AddEntity")]
        [HttpPost]
        public async Task<ReturnModel> AddEntity([FromBody] AddEntityModel entityModel)
        {
            ReturnModel returnModel = new ReturnModel();
            EntityTable entityTable = new EntityTable
            {
                EntityName = entityModel.EntityName.ToUpper(),
                EntityType = entityModel.EntityType.ToUpper(),
                IsShown = true,
                CreatedDateTime = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false,
                IsUpdated = false,
            };
            _socialContext.EntityTable.Add(entityTable);
            var isSaved = await _socialContext.SaveChangesAsync();
            if (isSaved > 0)
            {
                returnModel.StatusCode = HttpStatusCode.OK;
                returnModel.Message = "Successfully Added New Entity";
            }
            else
            {
                returnModel.StatusCode = HttpStatusCode.InternalServerError;
                returnModel.Message = "Entity Can't Be Added, Please Try Again Later";
            }
            return returnModel;
        }

        [Route("GetAllEntity")]
        [HttpGet]
        public async Task<ReturnModel> GetAllEntity()
        {
            ReturnModel returnModel = new ReturnModel();
            List<GetEntityModel> entityTables = (from et in _socialContext.EntityTable
                                                 where et.IsActive && !et.IsDeleted
                                                 select new GetEntityModel
                                                 {
                                                     EntityId = et.Id,
                                                     EntityName = et.EntityName,
                                                     EntityType = et.EntityType,
                                                     IsShown = et.IsShown
                                                 }).ToList();
            if (entityTables.Count > 0)
            {
                returnModel.ResponseData = entityTables;
                returnModel.StatusCode = HttpStatusCode.OK;
                returnModel.Message = "Successfully Loaded Entities";
            }
            else
            {
                returnModel.StatusCode = HttpStatusCode.NotFound;
                returnModel.Message = "No Entities Could Be Found";
            }
            return returnModel;
        }

        [Route("GetActiveEntity")]
        [HttpGet]
        public async Task<ReturnModel> GetActiveEntity()
        {
            ReturnModel returnModel = new ReturnModel();
            List<GetEntityModel> entityTables = (from et in _socialContext.EntityTable
                                                 where et.IsActive && !et.IsDeleted && et.IsShown
                                                 select new GetEntityModel
                                                 {
                                                     EntityId = et.Id,
                                                     EntityName = et.EntityName,
                                                     EntityType = et.EntityType,
                                                     IsShown = et.IsShown
                                                 }).ToList();
            if (entityTables.Count > 0)
            {
                returnModel.ResponseData = entityTables;
                returnModel.StatusCode = HttpStatusCode.OK;
                returnModel.Message = "Successfully Loaded Entities";
            }
            else
            {
                returnModel.StatusCode = HttpStatusCode.NotFound;
                returnModel.Message = "No Entities Could Be Found";
            }
            return returnModel;
        }


        [Route("UpdateEntity")]
        [HttpPost]
        public async Task<ReturnModel> UpdateEntity(List<UpdateEntityModel> updateEntities)
        {
            ReturnModel returnModel = new ReturnModel();
            foreach (var item in updateEntities)
            {
                var data = _socialContext.EntityTable.FirstOrDefault(x => x.Id == item.EntityId);
                data.IsShown = item.IsChecked;
                _socialContext.Update(data);
                _socialContext.SaveChanges();
            }
            List<GetEntityModel> entityTables = (from et in _socialContext.EntityTable
                                                 where et.IsActive && !et.IsDeleted
                                                 select new GetEntityModel
                                                 {
                                                     EntityId = et.Id,
                                                     EntityName = et.EntityName,
                                                     EntityType = et.EntityType,
                                                     IsShown = et.IsShown
                                                 }).ToList();
            if (entityTables.Count > 0)
            {
                returnModel.ResponseData = entityTables;
                returnModel.StatusCode = HttpStatusCode.OK;
                returnModel.Message = "Entities have been updated";
            }
            else
            {
                returnModel.StatusCode = HttpStatusCode.NotFound;
                returnModel.Message = "No Entities Could Be Found";
            }
            return returnModel;
        }
    }
}
