using System;

namespace TeamPlanner.Shared
{
    public class Activity : EntityBase
    {
        public Activity()
        {
            User = new User();
        }

        public Activity(User user)
        {
            User = user;
        }
        public DateTimeOffset DateTime { get; set; }
        public string Client { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Estimate { get; set; }
        public float? Remaining { get; set; }
        public User User { get; set; }
    }
    
}