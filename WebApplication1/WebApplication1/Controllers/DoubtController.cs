using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoubtController : ControllerBase
    {
        private readonly DiscussionForumDbContext _context;
        public DoubtController(DiscussionForumDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<Doubt> GetById(int id)
        {
            Doubt d = await _context.Doubts.FindAsync(id);
            return d;
        }

        [HttpPost]
        [Route("PostQuestion")]

        public async Task<Doubt> PostQuestion(PostQuestionDto doubt)
        {
            if (doubt != null)
            {
                IQueryable<User> u = _context.Users.Where(t => (t.Email == doubt.Email ));
                Console.WriteLine(u.ToString());
                var s = u.SingleOrDefault();
                Doubt d = new Doubt();
                d.UserId = s.Id;
                d.Question = doubt.Question;
                d.Description = doubt.Description;
                d.NoOfAnswer = 0;
                _context.Doubts.Add(d);
                await _context.SaveChangesAsync();
                return d;
            }

            return null;
        }

        [HttpDelete]
        [Route("DeleteDoubt/{id:int}")]
        public async Task<string> DeleteDoubt(int id)
        {

            Doubt s = await _context.Doubts.FindAsync(id);
            if (s != null)
            {
                List<Solution> solutions = await _context.Solutions.ToListAsync();
                for(int i=0; i < solutions.Count; i++)
                {
                    if(solutions[i].DoubtRefId == id)
                        _context.Solutions.Remove(solutions[i]);
                }
                _context.Doubts.Remove(s);
                await _context.SaveChangesAsync();

                return "suceesfully deleted";
            }
            else
            {
                return "Deletion failed";
            }

        }


        [HttpGet]
        [Route("AllQuestion")]
        public async Task<List<Doubt>> AllQuestion()
        {
            return await _context.Doubts.ToListAsync();

        }
    }
}
