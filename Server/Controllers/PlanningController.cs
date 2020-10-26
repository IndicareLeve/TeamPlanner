using System;
using System.Collections.Generic;
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
        [HttpGet("users")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _db.GetCollection<User>("Users").FindAsync(new BsonDocument());
            return await users.ToListAsync();
        }

        [HttpGet("activities")]
        public async Task<IEnumerable<Activity>> GetActivities(int weekNumber)
        {
            var cursor = await _db.GetCollection<Activity>("Activities").FindAsync(new BsonDocument());
            return await cursor.ToListAsync();
        }
    }
}
