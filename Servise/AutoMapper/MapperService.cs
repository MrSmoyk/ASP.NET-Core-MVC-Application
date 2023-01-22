using AutoMapper;
using Domain.Entity;
using Domain.ViewModels;
using System.Collections.Generic;


namespace Servise.AutoMapper
{
    public class MapperService : IMapperService
    {
        public readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public CourseVM CourseToCourseVm(Course course)
        {
            CourseVM courseVM = _mapper.Map<CourseVM>(course);
            return courseVM;
        }

        public Course CourseVmToCourse(CourseVM courseVM)
        {
            Course course = _mapper.Map<Course>(courseVM);
            return course;
        }

        public List<CourseVM> CourseListToCourseVmList(List<Course> courses)
        {
            List<CourseVM> listCourseVM = _mapper.Map<List<CourseVM>>(courses);
            return listCourseVM;
        }

        public GroupVM GroupToGroupVm(Group group)
        {
            GroupVM groupVM = _mapper.Map<GroupVM>(group);
            return groupVM;
        }

        public Group GroupVmToGroup(GroupVM groupVM)
        {
            Group group = _mapper.Map<Group>(groupVM);
            return group;
        }

        public List<GroupVM> GroupListToGroupVmList(List<Group> groups)
        {
            List<GroupVM> listGroupVM = _mapper.Map<List<GroupVM>>(groups);
            return listGroupVM;
        }

        public StudentVM StudentToStudentVm(Student student)
        {
            StudentVM studentVM = _mapper.Map<StudentVM>(student);
            return studentVM;
        }

        public Student StudentVmToStudent(StudentVM studentVM)
        {
            Student student = _mapper.Map<Student>(studentVM);
            return student;
        }

        public List<StudentVM> StudentListToStudentVmList(List<Student> students)
        {
            List<StudentVM> listStudentVM = _mapper.Map<List<StudentVM>>(students);
            return listStudentVM;
        }
    }
}
