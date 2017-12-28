namespace TaskPlanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TaskPlanner.Entity;
    using TaskPlanner.Objects;

    public interface IProjectViewModel
    {
        TransactionResult DeleteProject(int projectId);
        TransactionResult UpdateFavouriteList(int projectId, string userId, bool isFavouriteListAdded);
        TransactionResult UpdateProjectPermissionList(int projectId, string emailId, bool isProjectPermissionListAdded);
    }

    public class ProjectViewModel : IProjectViewModel
    {
        public TransactionResult DeleteProject(int projectId)
        {
            var result = new TransactionResult();
            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    var projectObj = (from projectDetails in context.Projects.Where(i => i.IsActive && i.ProjectId == projectId)
                                      select projectDetails).ToList();

                    projectObj.ForEach(i => i.IsActive = false);

                    context.SaveChanges();
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            return result;
        }

        public TransactionResult UpdateFavouriteList(int projectId, string userId, bool isFavouriteListAdded)
        {
            var result = new TransactionResult();
            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    var favouriteObj = (from favouritesDetails in context.Favourites.Where(i => i.ProjectId == projectId && i.UserId == userId)
                                        select favouritesDetails).ToList();

                    if (isFavouriteListAdded)
                    {
                        if (favouriteObj != null && favouriteObj.Count > 0 && favouriteObj.Any(i => i.IsActive == false))
                        {
                            favouriteObj.Where(i => i.IsActive == false).ToList().ForEach(i => i.IsActive = true);
                        }
                        else
                        {
                            var favouriteListObj = new Favourite()
                            {
                                ProjectId = projectId,
                                UserId = userId,
                                UpdatedOn = DateTime.Now,
                                IsActive = true
                            };
                            context.Favourites.Add(favouriteListObj);
                        }
                    }
                    else
                    {
                        if (favouriteObj != null && favouriteObj.Count > 0 && favouriteObj.Any(i => i.IsActive == true))
                        {
                            favouriteObj.Where(i => i.IsActive == true).ToList().ForEach(i => i.IsActive = false);
                        }
                    }

                    context.SaveChanges();
                }

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            return result;
        }

        public TransactionResult UpdateProjectPermissionList(int projectId, string emailId, bool isProjectPermissionListAdded)
        {
            var result = new TransactionResult();
            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    var projectPermissionObj = (from projectPermissionDetails in context.ProjectPermissions.Where(i => i.ProjectId == projectId && i.EmailId == emailId)
                                        select projectPermissionDetails).ToList();

                    if (isProjectPermissionListAdded)
                    {
                        if (projectPermissionObj != null && projectPermissionObj.Count > 0 && projectPermissionObj.Any(i => i.IsActive == false))
                        {
                            projectPermissionObj.Where(i => i.IsActive == false).ToList().ForEach(i => i.IsActive = true);
                        }
                        else
                        {
                            var projectPermissionListObj = new ProjectPermission()
                            {
                                ProjectId = projectId,
                                EmailId=emailId,
                                UpdatedOn = DateTime.Now,
                                IsActive = true
                            };
                            context.ProjectPermissions.Add(projectPermissionListObj);
                        }
                    }
                    else
                    {
                        if (projectPermissionObj != null && projectPermissionObj.Count > 0 && projectPermissionObj.Any(i => i.IsActive == true))
                        {
                            projectPermissionObj.Where(i => i.IsActive == true).ToList().ForEach(i => i.IsActive = false);
                        }
                    }

                    context.SaveChanges();
                }

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
            }

            return result;
        }


    }
}
