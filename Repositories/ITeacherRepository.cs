using aeronology_tech_exam_no2.Dtos;
using aeronology_tech_exam_no2.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace aeronology_tech_exam_no2.Repositories
{
    public interface ITeacherRepository
    {
        Task<int> Create(CreateTeacherDto entity);
        Task<int> Delete(int id);
        Task<IEnumerable<TeacherDto>> GetAll(Expression<Func<Teacher, bool>> filter = null);
        Task<TeacherDto> GetSingle(Expression<Func<Teacher, bool>> filter = null);
        Task<int> Update(EditTeacherDto entity);
    }
}