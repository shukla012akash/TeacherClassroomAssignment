using ApiDbLayer;
using ApiDbLayer.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDB.ApiModel;

namespace WebApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly DemoDbContext _context;
        private readonly IMapper _mapper;
       

        public TeacherController(DemoDbContext  context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region GET METHOD
        // GET: api/Teachers
        [HttpGet]


        [HttpGet]
        public ActionResult GetAllTeachers()
        {

            if (_context.Teachers == null)
            {
                return NotFound();
            }
            var Alldetailsofteacher = _context.Teachers.Include(cl => cl.classrooms).ToList();

            return Ok(Alldetailsofteacher);
        }
        

        // GET: api/Teachers
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherandClassroomApiModel>> GetTeacher(int id)
        {
            if (_context.Teachers == null)
            {
                return NotFound();
            }
            else
            {
                TeacherandClassroomApiModel teacherandClassroomApiModel = new();
                TeacherApiModel teacherApiModel = new();
                List<ClassroomApiModel>classroomApiModelsList = new List<ClassroomApiModel>();
               var teacher=  await _context.Teachers.FindAsync(id);
                var classroom = _context.Classrooms.Where(x => x.Teacher.TeacherId==id).ToList();


            teacherApiModel = _mapper.Map<TeacherApiModel>(teacher);
            classroomApiModelsList = _mapper.Map<List<ClassroomApiModel>>(classroom);
            teacherandClassroomApiModel.Teacher = teacherApiModel;
            teacherandClassroomApiModel.Classrooms=classroomApiModelsList;
            return teacherandClassroomApiModel;

        }

            }

        #endregion

        #region PostMETHOD

        // PUT: api/Teachers/5
        // To protect from overposting attacks, 
        [HttpPut]
        public async Task<IActionResult> PutTeacher(int id , TeacherandClassroomApiModel teacherAndClassroomApiModel)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null)
            {
                return NotFound();
            }
            else
            {
                var obj = _mapper.Map<Teacher>(teacherAndClassroomApiModel.Teacher);
                var classrooms = _mapper.Map<List<Classroom>>(teacherAndClassroomApiModel.Classrooms);

                var updateTeacher = _context.Teachers.Where(t => t.TeacherId == id).Include(c => c.classrooms).FirstOrDefault();

                updateTeacher.TeacherName = teacher.TeacherName;
                updateTeacher.TeacherAddress = teacher.TeacherAddress;
                updateTeacher.classrooms = classrooms;
                _context.Update(updateTeacher);
                _context.SaveChanges();
                return Ok();
            }
            
        }
        #endregion

        // POST: api/Teachers
        #region POST METHOD

        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherandClassroomApiModel teacherandClassroomApiModel)
        {
          if (_context.Teachers == null)
          {
              return Problem("Entity set 'DemoDbContext.Teachers'  is null.");
          }
           

            // converting TeacherApiModel type to datamodel Teacher type
            var obj = _mapper.Map<Teacher>(teacherandClassroomApiModel.Teacher);

            //converting list of classRoomApiModel to data model list of classroom
            var classrooms= _mapper.Map<List<Classroom>>(teacherandClassroomApiModel.Classrooms);

            obj.classrooms = classrooms;
            _context.Teachers.Add(obj);
            _context.SaveChanges();
            return Ok();
        }
        #endregion

        #region DELETE METHOD

        // DELETE: api/Teachers
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            if (_context.Teachers == null)
            {
                return NotFound();
            }
            var teacher = await _context.Teachers.Include(x => x.classrooms).Where(teacher => teacher.TeacherId == id).FirstOrDefaultAsync();
            if (teacher == null)
            {
                return NotFound();
            }
            //var teacher = _context.Teachers.Where(x => x.TeacherId == id).FirstOrDefault();
            //if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                await _context.SaveChangesAsync();
                return Ok();
            //}
            //else
            //{
            //    return NotFound();
            }
        }
        #endregion

        #region PATCH METHOD

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTeacher(string Name, string Address, int id)
        {
            var teacherNameAndAddressUpdate = _context.Teachers.Where(t => t.TeacherId == id).FirstOrDefault();
            if (teacherNameAndAddressUpdate == null)
            {
                return BadRequest();
            }
            teacherNameAndAddressUpdate.TeacherName = Name;
            teacherNameAndAddressUpdate.TeacherAddress = Address;
            _context.Update(teacherNameAndAddressUpdate);
            await _context.SaveChangesAsync();
            return Ok("updated  !");
        }

        #endregion

        private bool TeacherExists(int id)
        {
            return (_context.Teachers?.Any(x => x.TeacherId == id)).GetValueOrDefault();
        }
    }
}
