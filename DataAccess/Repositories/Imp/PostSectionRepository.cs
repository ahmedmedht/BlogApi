using Microsoft.EntityFrameworkCore;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Imp
{
    public class PostSectionRepository : IPostSectionRepository
    {
        private readonly ApplicationDbContext _context;

        public PostSectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostSectionModel>> GetAllSectionsAsync(Guid postId)
        {
            return await _context.PostSections
                                 .Where(ps => ps.PostId == postId)
                                 .OrderBy(ps => ps.SectionOrder)
                                 .ToListAsync();
        }

        public async Task<PostSectionModel> GetSectionByIdAsync(int sectionId)
        {
            return await _context.PostSections
                                 .FirstOrDefaultAsync(ps => ps.Id == sectionId);
        }

        public async Task AddSectionAsync(PostSectionModel section)
        {
            await _context.PostSections.AddAsync(section);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSectionAsync(PostSectionModel section)
        {
            _context.PostSections.Update(section);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            var section = await _context.PostSections.FindAsync(sectionId);
            if (section != null)
            {
                _context.PostSections.Remove(section);
                await _context.SaveChangesAsync();
            }
        }
    }
}
