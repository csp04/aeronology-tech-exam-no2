using aeronology_tech_exam_no2.Dtos;
using aeronology_tech_exam_no2.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace aeronology_tech_exam_no2.Repositories
{
    public interface IStudentRepository
    {
        Task<int> Create(CreateStudentDto entity);
        Task<int> Delete(int id);
        Task<IEnumerable<StudentDto>> GetAll(Expression<Func<Student, bool>> filter = null);
        Task<StudentDto> GetSingle(Expression<Func<Student, bool>> filter = null);
        Task<int> Update(EditStudentDto entity);
    }
}