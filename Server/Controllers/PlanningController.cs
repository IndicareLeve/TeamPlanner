using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using TeamPlanner.Shared;

namespace TeamPlanner.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PlanningController : ControllerBase
    {
        private readonly ILogger<PlanningController> _logger;
        IMongoDatabase _db;

        public PlanningController(ILogger<PlanningController> logger, IMongoDatabase db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("activity")]
        public async Task<Activity> InsertActivity(Activity activity)
        {
            await _db.GetCollection<Activity>("Activities").InsertOneAsync(activity);
            return activity;
        }
        [HttpGet("users/{teamName?}")]
        public async Task<IEnumerable<User>> GetUsers(string? teamName = null)
        {
            var cursor = await _db.GetCollection<User>("Users").FindAsync(new BsonDocument());
            var users = await cursor.ToListAsync();
            
            if (teamName != null)
                return users.Where(u => u.Team.Equals(teamName, StringComparison.InvariantCultureIgnoreCase));

            return users;
        }

        [HttpGet("activities/{year}/{week}/{teamName?}")]
        public async Task<IEnumerable<Activity>> GetActivities(int year, int week, string? teamName = null)
        {
            var weekEnd = new DateTimeOffset(ISOWeek.ToDateTime(year, week, DayOfWeek.Sunday));
            var weekEndFilter = Builders<Activity>.Filter.Lte(a => a.DateTime, weekEnd);
            var weekStart = weekEnd.Subtract(TimeSpan.FromDays(6));
            var startWeekFilter = Builders<Activity>.Filter.Gte(a => a.DateTime, weekStart);

            Console.WriteLine($"{year} {week} {weekStart} {weekEnd} {teamName}");

            var cursor = await _db.GetCollection<Activity>("Activities").FindAsync(weekEndFilter & startWeekFilter);
            return await cursor.ToListAsync();
        }
    }
}
