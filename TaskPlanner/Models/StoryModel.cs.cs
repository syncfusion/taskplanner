using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskPlanner.Base.Stories;
using TaskPlanner.Objects;
using Syncfusion.JavaScript.DataSources;

namespace TaskPlanner.Models
{
    public class StoryModel
    {
        /// <summary>
        /// Gets or sets object for IStoryBaseModel class.
        /// </summary>
        private readonly IStoryBaseModel iStoryBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryModel"/> class.
        /// </summary>
        public StoryModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryModel"/> class.
        /// </summary>
        /// <param name="story">story is the reference name for IStoryBaseModel</param>
        public StoryModel(IStoryBaseModel story = null)
        {
            this.iStoryBase = story ?? new StoryBaseModel();
        }

        /// <summary>
        ///  Gets or sets the total list
        /// </summary>
        internal int TotalListCount { get; set; }

        /// <summary>
        /// To get the stories list
        /// </summary>
        /// <param name="dataManager">data manager of reference manager</param>
        /// <param name="projectId">Project Id</param>
        /// <returns>returns the list of stories</returns>
        public List<StoryObjects> GetStoriesList(DataManager dataManager, int projectId)
        {
            var storiesList = this.iStoryBase.GetStoriesList(projectId).AsEnumerable();
            DataOperations operation = new DataOperations();
            if (dataManager.Sorted != null && dataManager.Sorted.Count > 0)
            {
                storiesList = (IEnumerable<StoryObjects>)operation.PerformSorting(storiesList, dataManager.Sorted);
            }

            if (dataManager.Where != null && dataManager.Where.Count > 0)
            {
                storiesList = (IEnumerable<StoryObjects>)operation.PerformWhereFilter(storiesList, dataManager.Where, dataManager.Where[0].Operator);
            }

            if (dataManager.Search != null && dataManager.Search.Count > 0)
            {
                storiesList = (IEnumerable<StoryObjects>)operation.PerformSearching(storiesList, dataManager.Search);
            }

            this.TotalListCount = storiesList.Count();
            if (dataManager.Skip != 0)
            {
                storiesList = (IEnumerable<StoryObjects>)operation.PerformSkip(storiesList, dataManager.Skip);
            }

            if (dataManager.Take != 0)
            {
                storiesList = (IEnumerable<StoryObjects>)operation.PerformTake(storiesList, dataManager.Take);
            }

            return storiesList.ToList();
        }
    }
}
