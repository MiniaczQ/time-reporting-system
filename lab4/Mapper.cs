using AutoMapper;

namespace lab4.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Persistence.Schemas.User, Models.User>();
        }
    }
}