using aeronology_tech_exam_no2.Configs;
using aeronology_tech_exam_no2.Data;
using aeronology_tech_exam_no2.Dtos;
using aeronology_tech_exam_no2.Exceptions;
using aeronology_tech_exam_no2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace aeronology_tech_exam_no2.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDbContext db;
        private readonly AppSettings appSettings;

        public TeacherRepository(SchoolDbContext db, IOptions<AppSettings> appSettingsOptions)
        {
            this.db = db;
            appSettings = appSettingsOptions.Value;
        }
        
        /// <summary>
        /// Creates a teacher.
        /// </summary>
        /// <param name="entity">Teacher entity</param>
        /// <returns>Generated ID</returns>
        public async Task<int> Create(CreateTeacherDto entity)
        {
            var teacherCount = await db.Teachers.CountAsync();

            if(teacherCount >= appSettings.MaxTeacher)
            {
                throw new TeacherFullException();
            }

            var Teacher = new Teacher()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age,
                BirthDay = entity.BirthDay,
                IsStarSectionAdviser = entity.IsStarSectionAdviser
            };

            await db.Teachers.AddAsync(Teacher);

            await db.SaveChangesAsync();

            return Teacher.ID;
        }

        /// <summary>
        /// Deletes a teacher by id
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns>Deleted teacher ID</returns>
        public async Task<int> Delete(int id)
        {
            var Teacher = await db.Teachers.FindAsync(id);

            if (Teacher == null)
            {
                return 0;
            }

            db.Teachers.Remove(Teacher);

            await db.SaveChangesAsync();

            return id;

        }

        /// <summary>
        /// Updates the teacher's record
        /// </summary>
        /// <param name="entity">Teacher entity</param>
        /// <returns>Updated teacher ID</returns>
        public async Task<int> Update(EditTeacherDto entity)
        {
            var teacher = await db.Teachers.FindAsync(entity.TeacherIDNumber);

            if (teacher == null)
            {
                return 0;
            }


            if (!string.IsNullOrEmpty(entity.FirstName))
            {
                teacher.FirstName = entity.FirstName;
            }

            if (!string.IsNullOrEmpty(entity.LastName))
            {
                teacher.LastName = entity.LastName;
            }

            if (entity.Age.HasValue)
            {
                teacher.Age = entity.Age.Value;
            }

            if (entity.BirthDay.HasValue)
            {
                teacher.BirthDay = entity.BirthDay.Value;

            }

            if (entity.IsStarSectionAdviser.HasValue)
            {
                teacher.IsStarSectionAdviser = entity.IsStarSectionAdviser.Value;
            }

            db.Teachers.Update(teacher);
            await db.SaveChangesAsync();

            return teacher.ID;
        }

        /// <summary>
        /// Gets all teachers.
        /// </summary>
        /// <param name="filter">To filter teachers to return<</param>
        /// <returns>List of teachers</returns>
        public async Task<IEnumerable<TeacherDto>> GetAll(Expression<Func<Teacher, bool>> filter = null)
        {
            var query = db.Teachers.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(x => new TeacherDto()
            {
                TeacherIDNumber = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                BirthDay = x.BirthDay,
                IsStarSectionAdviser = x.IsStarSectionAdviser,
                HandledStudents = x.Students.Count
            }).OrderBy(x => x.TeacherIDNumber).ToListAsync();
        }

        /// <summary>
        /// Gets a teacher record.
        /// </summary>
        /// <param name="filter">To filter teacher to return</param>
        /// <returns>Teacher info</returns>
        public async Task<TeacherDto> GetSingle(Expression<Func<Teacher, bool>> filter = null)
        {
            var query = db.Teachers.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(x => new TeacherDto()
            {
                TeacherIDNumber = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                BirthDay = x.BirthDay,
                IsStarSectionAdviser = x.IsStarSectionAdviser,
                HandledStudents = x.Students.Count
            }).FirstOrDefaultAsync();
        }
    }
}