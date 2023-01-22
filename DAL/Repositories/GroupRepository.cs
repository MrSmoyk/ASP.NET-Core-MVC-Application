using DAL.Interfases;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        public readonly ApplicationDbContext _context;

        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Group> Create(Group entity)
        {
            await _context.Groups.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Group> Delete(Group entity)
        {
            _context.Groups.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Group>> GetAll()
        {
            return await _context.Groups.AsNoTracking().ToListAsync();
        }

        public async Task<Group> GetById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }


        public async Task<Group> Update(Group entity)
        {
            _context.Groups.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Group>> GetAllFromCourse(int id)
        {
            return await _context.Groups.AsNoTracking().Where(x => x.CourseId == id).ToListAsync();
        }
    }
}
