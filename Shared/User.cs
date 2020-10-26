using System.Collections.Generic;

namespace TeamPlanner.Shared
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsTeamLeader { get; set; }
        public string Team { get; set; }
    }

    public enum UserRole
    {
        User,
        TeamLeader,
        Manager
    }
}