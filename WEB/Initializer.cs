using DAL.Interfases;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Servise.AutoMapper;
using Servise.Implementations;
using Servise.Interfaces;

namespace WEB
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IMapperService, MapperService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();

        }
    }
}
