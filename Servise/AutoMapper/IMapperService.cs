using Domain.Entity;
using Domain.ViewModels;
using System.Collections.Generic;

namespace Servise.AutoMapper
{
    public interface IMapperService
    {
        CourseVM CourseToCourseVm(Course course);

        Course CourseVmToCourse(CourseVM courseVM);

        List<CourseVM> CourseListToCourseVmList(List<Course> courses);

        GroupVM GroupToGroupVm(Group group);

        Group GroupVmToGroup(GroupVM groupVM);

        List<GroupVM> GroupListToGroupVmList(List<Group> groups);

        StudentVM StudentToStudentVm(Student student);

        Student StudentVmToStudent(StudentVM studentVM);

        List<StudentVM> StudentListToStudentVmList(List<Student> students);
    }
}
