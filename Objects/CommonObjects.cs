using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel; 

namespace TaskPlanner.Objects
{
    /// <summary>
    /// Data manger extended class
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [DefaultValue(null)]
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [DefaultValue(null)]
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is RequiresCounts or not
        /// </summary>
        [DefaultValue(null)]
        public bool RequiresCounts { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<string> Group { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<string> Select { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<string> Expand { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<Sort> Sorted { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<SearchFilter> Search { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<WhereFilter> Where { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public List<Aggregate> Aggregates { get; set; }
    }
}
