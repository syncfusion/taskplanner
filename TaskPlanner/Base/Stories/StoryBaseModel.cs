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
                    result = (from story in context.Stories.Where(s=>s.IsActive==true && s.ProjectId==projectId)
                              join project in context.Projects on story.ProjectId equals project.ProjectId
                              join theme in context.Themes on story.ThemeId equals theme.ThemeId
                              join epic in context.Epics on story.EpicId equals epic.EpicId
                              join priority in context.Priorities on story.PriorityId equals priority.PriorityId
                              where project.IsActive == true && theme.IsActive == true && epic.IsActive == true && priority.IsActive == true
                              select new StoryObjects()
                              {
                                  StoryId = story.StoryId,
                                  Title = story.Title,
                                  ThemeName= theme.ThemeName,
                                  EpicName = epic.EpicName,
                                  Priority = priority.PriorityName,
                                  Benifit = story.Benefit,
                                  Penalty = story.Penality,
                                  StoryPoints = story.StoryPoints,
                                  Tag = story.Tag
                              }).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return result;
        }
    }
}
