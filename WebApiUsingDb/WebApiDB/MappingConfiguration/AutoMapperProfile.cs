using ApiDbLayer.Entities;
using AutoMapper;
using WebApiDB.ApiModel;

namespace WebApiDB.MappingConfiguration
{
    public class AutoMapperProfile:Profile
    {
        private readonly IMapper _mapper;
        public AutoMapperProfile()
        {
            CreateMap<Teacher, TeacherApiModel>().ReverseMap(); ;
            CreateMap<Classroom, ClassroomApiModel>().ReverseMap();
        }
    }
}
