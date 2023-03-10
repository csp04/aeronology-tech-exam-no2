
using aeronology_tech_exam_no2.Dtos;
using aeronology_tech_exam_no2.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aeronology_tech_exam_no2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly ITeacherRepository teacherRepository;

        public SchoolController(IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            this.studentRepository = studentRepository;
            this.teacherRepository = teacherRepository;
        }

        /// <summary>
        /// This method handles the HTTP POST request to create a new student entity
        /// </summary>
        /// <param name="createStudent">New student record</param>
        /// <returns>Generated student id</returns>
        [HttpPost("students")]
        public async Task<IActionResult> CreateStudent(CreateStudentDto createStudent)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await studentRepository.Create(createStudent);
            
            return Created($"/api/school/students", createStudent);

        }

        /// <summary>
        /// This method handles the HTTP PUT request to edit an existing student entity
        /// </summary>
        /// <param name="editStudent">Student record to be updated</param>
        /// <returns>Updated student record</returns>
        [HttpPut("students")]
        public async Task<IActionResult> EditStudent(EditStudentDto editStudent)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await studentRepository.Update(editStudent);

            if(id == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// This method handles the HTTP DELETE request to delete an existing student entity
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <returns>Deleted student id</returns>
        [HttpDelete("students/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedId = await studentRepository.Delete(id);

            if(deletedId == 0)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// This method handles the HTTP GET request to retrieve all student entities
        /// </summary>
        /// <returns>List of students</returns>
        [HttpGet("students")]
        public async Task<IEnumerable<StudentDto>> GetAllStudents() => await studentRepository.GetAll();

        /// <summary>
        /// This method handles the HTTP GET request to retrieve a single student entity by ID or name
        /// </summary>
        /// <param name="id">Student's id</param>
        /// <param name="name">Student's name</param>
        /// <returns>Student info</returns>
        [HttpGet("student")]
        public async Task<IActionResult> GetStudent(int id, string name)
        {
            if (id == 0 && string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var result = await (id > 0 ? studentRepository.GetSingle(x => x.ID == id)
                : studentRepository.GetSingle(x => x.FirstName.ToLower().StartsWith(name.ToLower())));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// This method handles the HTTP POST request to create a new teacher entity
        /// </summary>
        /// <param name="createTeacher">New teacher record</param>
        [HttpPost("teachers")]
        public async Task<IActionResult> CreateTeacher(CreateTeacherDto createTeacher)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await teacherRepository.Create(createTeacher);

            return Created($"/api/school/students", createTeacher);

        }

        /// <summary>
        /// This method handles the HTTP PUT request to edit an existing teacher entity
        /// </summary>
        /// <param name="editTeacher">Teacher record to be updated</param>
        /// <returns>Updated teacher record</returns>
        [HttpPut("teachers")]
        public async Task<IActionResult> EditTeacher(EditTeacherDto editTeacher)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await teacherRepository.Update(editTeacher);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok();
        }


        /// <summary>
        /// This method handles the HTTP GET request to retrieve all teacher entities
        /// </summary>
        /// <returns>List of teachers</returns>
        [HttpGet("teachers")]
        public async Task<IEnumerable<TeacherDto>> GetAllTeachers() => await teacherRepository.GetAll();

        /// <summary>
        /// This method handles the HTTP GET request to retrieve a single student entity by ID or name
        /// </summary>
        /// <param name="id">Teacher id</param>
        /// <param name="name">Teacher's name</param>
        /// <returns>Teacher info</returns>
        [HttpGet("teacher")]
        public async Task<IActionResult> GetTeacher(int id, string name)
        {
           if(id == 0 && string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var result =  await (id > 0 ? teacherRepository.GetSingle(x => x.ID == id) 
                : teacherRepository.GetSingle(x => x.FirstName.ToLower().StartsWith(name.ToLower())));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
