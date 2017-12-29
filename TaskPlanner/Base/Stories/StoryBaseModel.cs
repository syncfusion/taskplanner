using System;
using System.Collections.Generic;
using System.Linq;
using TaskPlanner.Objects;
using TaskPlanner.Entity;

namespace TaskPlanner.Base.Stories
{
    public interface IStoryBaseModel
    {
        /// <summary>
        /// Method declaration to get stories list
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>returns stories lists</returns>
        List<StoryObjects> GetStoriesList(int projectId);

        /// <summary>
        /// Method declaration to add/update story
        /// </summary>
        /// <param name="newStoryDetails">Story Object</param>
        /// <returns>transaction results</returns>
        TransactionResult UpdateStoryDetails(StoryObjects newStoryDetails);

        /// <summary>
        /// Method declaration to delete story
        /// </summary>
        /// <param name="storyId">Story Id</param>
        /// <returns>transaction results</returns>
        TransactionResult DeleteStory(int storyId);
    }

    public class StoryBaseModel : IStoryBaseModel
    {
        /// <summary>
        /// Method definition to get stories list
        /// </summary>
        /// <param name="projectId">Project Id</param>
        /// <returns>returns stories lists</returns>
        public List<StoryObjects> GetStoriesList(int projectId)
        {
            var result = new List<StoryObjects>();
            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    result = (from story in context.Stories.Where(s => s.IsActive == true && s.ProjectId == projectId)
                              join project in context.Projects on story.ProjectId equals project.ProjectId
                              join tme in context.Themes on story.ThemeId equals tme.ThemeId into ps
                              from theme in ps.DefaultIfEmpty()
                              join epi in context.Epics on story.EpicId equals epi.EpicId into ps1
                              from epic in ps1.DefaultIfEmpty()
                              join pri in context.Priorities on story.PriorityId equals pri.PriorityId into ps2
                              from priority in ps2.DefaultIfEmpty()
                              where project.IsActive == true
                              select new StoryObjects()
                              {
                                  StoryId = story.StoryId,
                                  TaskId = story.TaskId,
                                  Title = story.Title,
                                  ThemeName = theme.ThemeName,
                                  EpicName = epic.EpicName,
                                  Priority = priority.PriorityName,
                                  Benifit = story.Benefit,
                                  Penalty = story.Penality,
                                  StoryPoints = story.StoryPoints,
                                  Tag = story.Tag,
                                  SprintName = story.SprintName,
                                  AssigneeName = story.AssigneeName
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// Method definition to add/update story
        /// </summary>
        /// <param name="newStoryDetails">Story Object</param>
        /// <returns>transaction results</returns>
        public TransactionResult UpdateStoryDetails(StoryObjects newStoryDetails)
        {
            var result = new TransactionResult();

            try
            {
                newStoryDetails.ThemeId = GetThemeId(newStoryDetails.ThemeName,newStoryDetails.ProjectId,newStoryDetails.CreatedBy).Id;
                newStoryDetails.PriorityId = GetPriorityId(newStoryDetails.Priority,newStoryDetails.CreatedBy).Id;
                newStoryDetails.EpicId = GetEpicId(newStoryDetails.EpicName, newStoryDetails.ProjectId, newStoryDetails.CreatedBy).Id;

                using (var context = new TaskPlannerEntities())
                {
                    if (newStoryDetails != null && newStoryDetails.StoryId > 0)
                    {
                        var storyDetailsObj = (from story in context.Stories.Where(i => i.StoryId == newStoryDetails.StoryId && i.IsActive)
                                               select story).FirstOrDefault();
                        if (storyDetailsObj != null)
                        {
                            storyDetailsObj.TaskId = newStoryDetails.TaskId;
                            storyDetailsObj.Title = newStoryDetails.Title;
                            storyDetailsObj.Description = newStoryDetails.Description;
                            storyDetailsObj.ProjectId = newStoryDetails.ProjectId;
                            storyDetailsObj.ThemeId = newStoryDetails.ThemeId;
                            storyDetailsObj.EpicId = newStoryDetails.EpicId;
                            storyDetailsObj.PriorityId = newStoryDetails.PriorityId;
                            storyDetailsObj.StoryPoints = newStoryDetails.StoryPoints;
                            storyDetailsObj.Benefit = newStoryDetails.Benifit;
                            storyDetailsObj.Penality = newStoryDetails.Penalty;
                            storyDetailsObj.SortOrder = newStoryDetails.SortOrder;
                            storyDetailsObj.AssigneeName = newStoryDetails.AssigneeName;
                            storyDetailsObj.SprintName = newStoryDetails.SprintName;
                            storyDetailsObj.CreatedOn = newStoryDetails.CreatedOn;
                            storyDetailsObj.UpdatedOn = DateTime.Now;
                            storyDetailsObj.Tag = newStoryDetails.Tag;

                            result.IsNewRecord = false;
                        }
                    }
                    else
                    {
                        var newStory = new Story()
                        {
                            TaskId = newStoryDetails.TaskId,
                            Title = newStoryDetails.Title,
                            Description = newStoryDetails.Description,
                            ProjectId = newStoryDetails.ProjectId,
                            ThemeId = newStoryDetails.ThemeId,
                            EpicId = newStoryDetails.EpicId,
                            PriorityId = newStoryDetails.PriorityId,
                            StoryPoints = newStoryDetails.StoryPoints,
                            Benefit = newStoryDetails.Benifit,
                            Penality = newStoryDetails.Penalty,
                            SortOrder = newStoryDetails.SortOrder,
                            CreatedBy = newStoryDetails.CreatedBy,
                            AssigneeName = newStoryDetails.AssigneeName,
                            SprintName = newStoryDetails.SprintName,
                            CreatedOn = DateTime.Now,
                            UpdatedOn = DateTime.Now,
                            Tag = newStoryDetails.Tag,
                            IsActive = true
                        };

                        context.Stories.Add(newStory);

                        result.IsNewRecord = true;
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

        /// <summary>
        /// Method definition to delete story
        /// </summary>
        /// <param name="storyId">Story Id</param>
        /// <returns>transaction results</returns>
        public TransactionResult DeleteStory(int storyId)
        {
            var result = new TransactionResult();
            try
            {
                using (var context = new TaskPlannerEntities())
                {
                    var storyObj = (from storyDetails in context.Stories.Where(i => i.IsActive && i.StoryId == storyId)
                                    select storyDetails).ToList();

                    storyObj.ForEach(i => i.UpdatedOn = DateTime.Now);
                    storyObj.ForEach(i => i.IsActive = false);

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

        public TransactionResult GetThemeId(string themeName, int projectId, string userId)
        {
            var result = new TransactionResult();
            try
            {
                themeName = GetFormattedString(themeName);
                if (!string.IsNullOrEmpty(themeName))
                {
                    using (var context = new TaskPlannerEntities())
                    {
                        result.Id = (from resultDetails in context.Themes.Where(i => i.IsActive && i.ThemeName == themeName)
                                     select resultDetails.ThemeId).FirstOrDefault();

                        if (result.Id == null || result.Id == 0)
                        {
                            var obj = new Theme()
                            {
                                ThemeName = themeName,
                                ProjectId = projectId,
                                CreatedBy = userId,
                                CreatedOn = DateTime.Now,
                                IsActive = true
                            };
                            context.Themes.Add(obj);
                            context.SaveChanges();

                            result.Id = obj.ThemeId;
                        }
                    }
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Id = 0;
            }

            return result;
        }

        public TransactionResult GetEpicId(string epicName, int projectId, string userId)
        {
            var result = new TransactionResult();
            try
            {
                epicName = GetFormattedString(epicName);
                if (!string.IsNullOrEmpty(epicName))
                {
                    using (var context = new TaskPlannerEntities())
                    {
                        result.Id = (from resultDetails in context.Epics.Where(i => i.IsActive && i.EpicName == epicName)
                                     select resultDetails.EpicId).FirstOrDefault();

                        if (result.Id == null || result.Id == 0)
                        {
                            var obj = new Epic()
                            {
                                EpicName = epicName,
                                ProjectId = projectId,
                                CreatedBy = userId,
                                CreatedOn = DateTime.Now,
                                IsActive = true
                            };
                            context.Epics.Add(obj);
                            context.SaveChanges();

                            result.Id = obj.EpicId;
                        }
                    }
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Id = 0;
            }

            return result;
        }

        public TransactionResult GetPriorityId(string priorityName, string userId)
        {
            var result = new TransactionResult();
            try
            {
                priorityName = GetFormattedString(priorityName);
                if (!string.IsNullOrEmpty(priorityName))
                {
                    using (var context = new TaskPlannerEntities())
                    {
                        result.Id = (from resultDetails in context.Priorities.Where(i => i.IsActive && i.PriorityName == priorityName)
                                     select resultDetails.PriorityId).FirstOrDefault();

                        if (result.Id == null || result.Id == 0)
                        {
                            var obj = new Priority()
                            {
                                PriorityName = priorityName,
                                CreatedBy = userId,
                                SortOrder = 0,
                                IsActive = true
                            };
                            context.Priorities.Add(obj);
                            context.SaveChanges();

                            result.Id = obj.PriorityId;
                        }
                    }
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Exception = ex;
                result.Id = 0;
            }

            return result;
        }

        public string GetFormattedString(string data)
        {
            return !string.IsNullOrEmpty(data) ? data.Trim().ToLower() : string.Empty;
        }
    }
}
