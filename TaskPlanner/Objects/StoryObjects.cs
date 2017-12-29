using System;

namespace TaskPlanner.Objects
{
    /// <summary>
    /// Story objects
    /// </summary>
    public class StoryObjects
    {
        /// <summary>
        /// Gets or sets the Story Id
        /// </summary>
        public int StoryId { get; set; }

        /// <summary>
        /// Gets or sets the Task Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the Story Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Theme name
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// Gets or sets the Epic name
        /// </summary>
        public string EpicName { get; set; }

        /// <summary>
        /// Gets or sets the Priority
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the Benifit
        /// </summary>
        public int? Benifit { get; set; }

        /// <summary>
        /// Gets or sets the Penalty
        /// </summary>
        public int? Penalty { get; set; }

        /// <summary>
        /// Gets or sets the Story Point
        /// </summary>
        public decimal? StoryPoints { get; set; }

        /// <summary>
        /// Gets or sets the Tag
        /// </summary>
        public string Tag { get; set; }
        public int ProjectId { get; set; }
        public int? PriorityId { get; set; }
        public int? ThemeId { get; set; }
        public int? EpicId { get; set; }
        public int? SortOrder { get; set; }
        public string CreatedBy { get; set; }
        public string AssigneeName { get; set; }
        public string SprintName { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
