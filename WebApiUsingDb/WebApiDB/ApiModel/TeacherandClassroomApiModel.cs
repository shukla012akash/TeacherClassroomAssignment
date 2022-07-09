namespace WebApiDB.ApiModel
{
    public class TeacherandClassroomApiModel
    {
        public TeacherApiModel? Teacher { get; set; }

       public List<ClassroomApiModel>? Classrooms { get; set; }
    }
}
