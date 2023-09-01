///
/// Copyright (C) 2017 PedidosYa - All Rights Reserved
///
using RestSharp;
using RestSharp.Deserializers;

namespace ReceptionSdk.Models
{
    /// <summary>
    /// The Schedule class holds all the information about the schedule of a section.
    /// </summary>
    public class Schedule
    {
        /// <summary>
        /// The section schedule from hour. Example: 10:00'
        /// </summary>
        [DeserializeAs(Name = "from")]
        public string From { get; set; }

        /// <summary>
        /// The section schedule to hour. Example: 15:00'
        /// </summary>
        [DeserializeAs(Name = "to")]
        public string To { get; set; }

        /// <summary>
        /// The section schedule day
        /// </summary>
        [DeserializeAs(Name = "day")]
        public int? Day { get; set; }

        /// <summary>
        /// The entity.
        /// </summary>
        [DeserializeAs(Name = "entity")]
        public Entity Entity { get; set; }

        /// <summary>
        /// Default SectionSchedule constructor
        /// </summary>
        public Schedule() : base()
        {
        }

        public override string ToString()
        {
            return SimpleJson.SerializeObject(this);
        }
    }
}
