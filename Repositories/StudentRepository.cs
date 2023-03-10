using aeronology_tech_exam_no2.Configs;
using aeronology_tech_exam_no2.Data;
using aeronology_tech_exam_no2.Dtos;
using aeronology_tech_exam_no2.Exceptions;
using aeronology_tech_exam_no2.Models;
using aeronology_tech_exam_no2.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace aeronology_tech_exam_no2.Repositories
{

    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext db;
        private readonly AppSettings appSettings;

        public StudentRepository(SchoolDbContext db, IOptions<AppSettings> appSettingsOptions)
        {
            this.db = db;
            appSettings = appSettingsOptions.Value;
        }

        /// <summary>
        /// Creates a student and assign a random teacher.
        /// </summary>
        /// <param name="entity">Student entity</param>
        /// <returns>Generated ID</returns>
        public async Task<int> Create(CreateStudentDto entity)
        {
            var studentCount = await db.Students.CountAsync();
            if (studentCount >= appSettings.MaxStudent)
            {
                throw new StudentFullException();
            }

            var teacherCount = await db.Teachers.CountAsync();
            if(teacherCount == 0)
            {
                throw new NoTeacherException();
            }

            // equally distribute students to teachers
            var average = appSettings.MaxStudent / appSettings.MaxTeacher;

            var teachers = await db.Teachers
                .Where(t => t.Students.Count < average)
                .Select(t => new
                {
                    ID = t.ID,
                    IsStarSectionAdviser = t.IsStarSectionAdviser
                })
                .ToListAsync();

            if (teachers.Count == 0)
            {
                throw new TeacherMaxStudentException();
            }

            var isStarSection = entity.OldGPA >= appSettings.MinStarSectionGPA;
            var teacherID = 0;

            // if star section, get teachers who are in star section, else get all non star section
            var filterTeachers = teachers.Where(x => isStarSection == x.IsStarSectionAdviser || !x.IsStarSectionAdviser).ToArray();

            // this will only occur if the student is for lower section and we cannot find any teacher available
            if(filterTeachers.Length == 0)
            {
                throw new LowerSectionMaxStudentException();
            }

            var rnd = Rand.GetRandomNumber(filterTeachers.Length);

            teacherID = filterTeachers[rnd].ID;


            var student = new Student
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Age = entity.Age,
                BirthDay = entity.BirthDay,
                OldGPA = entity.OldGPA,
                TeacherID = teacherID
            };

            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();

            return student.ID;
        }

        /// <summary>
        /// Deletes a student using id.
        /// </summary>
        /// <param name="id">Student ID</param>
        /// <returns>Deleted student id</returns>
        public async Task<int> Delete(int id)
        {
            var student = await db.Students.FindAsync(id);

            if(student == null)
            {
                return 0;
            }

            db.Students.Remove(student);

            await db.SaveChangesAsync();

            return id;

        }

        /// <summary>
        /// Updates the student record.
        /// </summary>
        /// <param name="entity">Student entity that holds the data to be updated</param>
        /// <returns>Updated student ID</returns>
        public async Task<int> Update(EditStudentDto entity)
        {
            var student = await db.Students.FindAsync(entity.StudentIDNumber);

            if (student == null)
            {
                return 0;
            }

            if(!string.IsNullOrEmpty(entity.FirstName))
            {
                student.FirstName = entity.FirstName;
            }

            if (!string.IsNullOrEmpty(entity.LastName))
            {
                student.LastName = entity.LastName;
            }
            
            if(entity.Age.HasValue)
            {
                student.Age = entity.Age.Value;
            }
            
            if(entity.BirthDay.HasValue)
            {
                student.BirthDay = entity.BirthDay.Value;

            }
            
            if(entity.OldGPA.HasValue)
            {
                student.OldGPA = entity.OldGPA.Value;
            }
            

            db.Students.Update(student);
            await db.SaveChangesAsync();

            return student.ID;
        }

        /// <summary>
        /// Gets all the students.
        /// </summary>
        /// <param name="filter">To filter students to return</param>
        /// <returns>List of students</returns>
        public async Task<IEnumerable<StudentDto>> GetAll(Expression<Func<Student, bool>> filter = null)
        {
            var query = db.Students.AsQueryable();
           
            if(filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(x => new StudentDto()
            {
                StudentIDNumber = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                BirthDay = x.BirthDay,
                OldGPA = x.OldGPA,
                IsStarSection = x.Teacher.IsStarSectionAdviser,
            }).OrderBy(x => x.StudentIDNumber).ToListAsync();
        }

        /// <summary>
        /// Gets a student record.
        /// </summary>
        /// <param name="filter">To filter student to return</param>
        /// <returns>Student info</returns>
        public async Task<StudentDto> GetSingle(Expression<Func<Student, bool>> filter = null)
        {
            var query = db.Students.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(x => new StudentDto()
            {
                StudentIDNumber = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Age = x.Age,
                BirthDay = x.BirthDay,
                OldGPA = x.OldGPA,
                IsStarSection = x.Teacher.IsStarSectionAdviser,
            }).FirstOrDefaultAsync();
        }

    }

}