using static Entities.Enums;

namespace Entities.DTOs
{
    public class GroupGetRequestDTO
    {
        /// <summary>
        /// unique identifier of Group
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Name of group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of Group
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Status of Group
        /// </summary>
        public GroupStatus Status { get; set; }

    }
}