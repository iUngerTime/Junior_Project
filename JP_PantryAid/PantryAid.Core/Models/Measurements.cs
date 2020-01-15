using PantryAid.Core.Models;

namespace PantryAid.Core
{
    /// <summary>
    /// A class for ingredient measurements
    /// </summary>
    public class Measurements
    {
        public Metric metric { get; set; }
        public Us_Imperial us { get; set; }


        /// <summary>
        /// A class for US Imperial ingredient measurements
        /// </summary>
        public class Us_Imperial
        {
            public double amount { get; set; }
            public string unitLong { get; set; }
            /// <summary></summary>
            public string unitShort { get; set; }
        }

        /// <summary>
        /// A class for Metric ingredient measurements
        /// </summary>
        public class Metric
        {
            public double amount { get; set; }
            public string unitLong { get; set; }
            public string unitShort { get; set; }
        }
    }
}