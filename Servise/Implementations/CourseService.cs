using DAL.Interfases;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.Extensions.Logging;
using Servise.AutoMapper;
using Servise.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servise.Implementations
{
    public class CourseService : ICourseService
    {
        public readonly ICourseRepository _courseRepository;
        public readonly IMapperService _mapperService;
        public readonly ILogger<CourseService> _logger;
        public CourseService(ICourseRepository courseRepository,
            IMapperService mapperService,
            ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _mapperService = mapperService;
            _logger = logger;
        }

        public async Task<List<CourseVM>> GetAll()
        {
            try
            {
                var courses = await _courseRepository.GetAll();
                if (courses == null)
                {
                    throw new NullReferenceException(nameof(courses));
                }
                var coursesVM = _mapperService.CourseListToCourseVmList(courses);
                return coursesVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CourseService.GetAll error: {ex.Message}");
                return new List<CourseVM>();
            }

        }

        public async Task<CourseVM> GetFromId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException(nameof(id));
                }
                var course = await _courseRepository.GetById(id);
                if (course == null)
                {
                    throw new NullReferenceException(nameof(course));
                }
                var courseVM = _mapperService.CourseToCourseVm(course);
                return courseVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CourseService.GetFromId error: {ex.Message}");
                return new CourseVM { CourseName = "Objects not found!", Description = "Objects not found!" };
            }

        }

        public async Task<CourseVM> Create(CourseVM vievModel)
        {
            try
            {
                Course toCreate = _mapperService.CourseVmToCourse(vievModel);
                await _courseRepository.Create(toCreate);
                return vievModel;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CourseService.Create error: {ex.Message}");
                return new CourseVM() { CourseName = "Object creation error!", Description = "Object creation error!" };

            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException(nameof(id));
                }

                var course = await _courseRepository.GetById(id);

                if (course == null)
                {
                    throw new NullReferenceException(nameof(course));
                }

                await _courseRepository.Delete(course);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CourseService.Delete error: {ex.Message}");
                return false;
            }

        }

        public async Task<bool> Update(CourseVM vievModel)
        {
            try
            {
                var toUppdate = _mapperService.CourseVmToCourse(vievModel);
                await _courseRepository.Update(toUppdate);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"CourseService.Update error: {ex.Message}");
                return false;
            }
        }

    }
}
