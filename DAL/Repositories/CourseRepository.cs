using DAL.Interfases;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Create(Course entity)
        {
            await _context.Courses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Course> Delete(Course entity)
        {
            _context.Courses.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Course>> GetAll()
        {
            return await _context.Courses.AsNoTracking().ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<Course> Update(Course entity)
        {
            _context.Courses.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


    }
}
