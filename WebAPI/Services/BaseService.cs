using AutoMapper;
using CORE;
using CORE.Backend;
using CORE.Entities;
using CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public abstract class BaseService
    {
        public readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        protected readonly DbOperator dbOperator;
        private HttpContext _httpContext { get { return _contextAccessor.HttpContext; } }
        private User _user;
        public User CurrentUser => GetCurrentUser();

        public BaseService(UserManager<User> userManager,
                           IHttpContextAccessor contextAccessor,
                           DbOperator dbOperator)
        {
            this.userManager = userManager;
            this._contextAccessor = contextAccessor;
            this.dbOperator = dbOperator;
        }

        private User GetCurrentUser()
        {
            if (!_httpContext.User.Identity.IsAuthenticated)
                return new User();

            if (_user != null)
                return _user;

            var userId = _httpContext.User?.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            _user = userManager.FindByIdAsync(userId).Result;
            return _user;
        }


        protected (bool isCreated, TResult result) GetOrCreateEntity<TResult>(
            DbSet<TResult> sourceCollection,
            Expression<Func<TResult, bool>> whereConditions = null)
            where TResult : class, new()
        {
            var isCreated = false;
            TResult result = null;

            if (whereConditions != null)
                result = sourceCollection.FirstOrDefault(whereConditions);

            if (result == null)
            {
                isCreated = true;
                result = new TResult();
                sourceCollection.Add(result);
            }

            var createdBy = typeof(TResult).GetProperty("CreatedBy");
            if (createdBy != null && isCreated)
                createdBy.SetValue(result, CurrentUser.Id);

            var modifiedBy = typeof(TResult).GetProperty("ModifiedBy");
            if (modifiedBy != null)
                modifiedBy.SetValue(result, CurrentUser.Id);

            var createDate = typeof(TResult).GetProperty("CreateDate");
            if (createDate != null && isCreated)
                createDate.SetValue(result, DateTime.UtcNow);

            var modifyDate = typeof(TResult).GetProperty("ModifyDate");
            if (modifyDate != null)
                modifyDate.SetValue(result, DateTime.UtcNow);

            return (isCreated, result);
        }

        protected StatusBase PrepareStatusBase(StatusBase status, bool isCreated)
        {
            if (isCreated)
            {
                status.CreateDate = DateTime.UtcNow;
                status.CreatedBy = CurrentUser.Id;
            }

            status.ModifyDate = DateTime.UtcNow;
            status.ModifiedBy = CurrentUser.Id;

            return status;
        }
    }
}