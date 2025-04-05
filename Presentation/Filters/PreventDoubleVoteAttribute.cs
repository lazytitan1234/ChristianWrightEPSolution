using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DataAccess.DataContext;
using System.Linq;
using System.Security.Claims;

namespace Presentation.Filters
{
    public class PreventDoubleVoteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = context.HttpContext.RequestServices.GetService(typeof(PollDbContext)) as PollDbContext;
            if (db == null)
            {
                context.Result = new StatusCodeResult(500); 
                return;
            }

            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ForbidResult(); 
                return;
            }

            if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is int pollId)
            {
                bool hasVoted = db.VoteRecords.Any(v => v.PollId == pollId && v.UserId == userId);
                if (hasVoted)
                {
                    context.Result = new RedirectToActionResult("Results", "Poll", new { id = pollId });
                }
            }
            else
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
