using DAL.Interfases;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Student> Create(Student entity)
        {
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Student> Delete(Student entity)
        {
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> Update(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Student>> GetAllFromGroup(int id)
        {
            return await _context.Students.AsNoTracking().Where(x => x.GroupId == id).ToListAsync();
        }
    }
}
