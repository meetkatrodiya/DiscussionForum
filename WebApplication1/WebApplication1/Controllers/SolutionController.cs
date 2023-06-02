using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.DTO;
using System;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly DiscussionForumDbContext _context;
        public SolutionController(DiscussionForumDbContext context)
        {
            _context = context;

        }

        [HttpPost]
        [Route("PostSolution")]

        public async Task<Solution> PostSolution(PostSolutionDto psd)
        {
            if (psd != null)
            {
                Doubt u = await _context.Doubts.FindAsync(psd.doubtId);
                IQueryable<User> user = _context.Users.Where(t => (t.Email == psd.Email));
                var s = user.SingleOrDefault();
                Solution sol = new Solution();
                sol.DoubtRefId = psd.doubtId;
                sol.Answer = psd.answer;
                
                if(u != null && s != null)
                {
                    sol.Email = s.Email;
                    u.NoOfAnswer = u.NoOfAnswer + 1;
                    
                    _context.Solutions.Add(sol);
                    await _context.SaveChangesAsync();
                    return sol;
                }
                
            }

            return null;
        }

        [HttpDelete]
        [Route("DeleteSolution/{id:int}")]
        public async Task<string> DeleteSolution(int id)
        {

            Solution s = await _context.Solutions.FindAsync(id);
            if (s != null)
            {
                _context.Solutions.Remove(s);
                await _context.SaveChangesAsync();

                return "suceesfully deleted";
            }
            else
            {
                return "Deletion failed";
            }

        }


        [HttpGet]
        [Route("AllSolutions/{id:int}")]
        public async Task<List<Solution>> AllSolutions(int id)
        {
            List<Solution> s = await _context.Solutions.ToListAsync();
            List<Solution> all = new List<Solution>();

            for (int i = 0; i < s.Count; i++)
            {
                if (s[i].DoubtRefId == id)
                {
                    all.Add(s[i]);

                }
            }

            return all;

        }


        [HttpPut]
        [Route("UpdateSolution")]
        public async Task<Solution> UpdateSolution(UpdateSollutionDto usdt)
        {
           
            var a = await _context.Solutions.FindAsync(usdt.Id);
            if (a != null)
            {

                a.Answer = usdt.Answer;
                await _context.SaveChangesAsync();

            }

            return a;
        }
    }
}
