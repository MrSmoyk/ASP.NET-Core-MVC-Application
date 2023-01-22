using AutoMapper;
using Domain.Entity;
using Domain.ViewModels;

namespace Servise.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Course, CourseVM>().ReverseMap();
            CreateMap<Group, GroupVM>().ReverseMap();
            CreateMap<Student, StudentVM>().ReverseMap();
        }
    }
}
