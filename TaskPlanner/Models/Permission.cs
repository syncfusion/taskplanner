using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Entity;

namespace TaskPlanner.Models
{
    public static class Permission
    {
        public static bool IsUserOwnerOfProject(string emailId, int projectId)
        {
            var _result = false;

            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    var result = (from userDetails in context.AspNetUsers.Where(i => i.Email == emailId)
                                  from resultDetails in context.Projects.Where(i => i.IsActive && i.ProjectId == projectId && i.Owner == userDetails.Id)
                                  select resultDetails).Count();

                    if (result > 0)
                    {
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

        public static bool IsUserHasAccessToProject(string emailId, int projectId)
        {
            var _result = false;

            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    var result = (
                        from userDetails in context.AspNetUsers.Where(i => i.Email == emailId)
                        from projectDetails in context.ProjectPermissions.Where(i => i.IsActive && i.ProjectId == projectId && i.EmailId == emailId).DefaultIfEmpty()
                        from resultDetails in context.Projects.Where(i => i.IsActive && (i.ProjectId == projectDetails.ProjectId || (i.ProjectId == projectId && i.Owner == userDetails.Id)))
                        select resultDetails).Count();

                    if (result > 0)
                    {
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }

            return _result;
        }

    }
}
