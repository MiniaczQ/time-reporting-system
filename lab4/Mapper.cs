using AutoMapper;

namespace lab4.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Persistence.Schemas.User, Models.UserAll>()
                .ReverseMap();
            CreateMap<Persistence.Schemas.Activity, Models.ActivityAll>()
                .ForMember(d => d.ProjectName, o => o.MapFrom(src => src.Project.ProjectName));
            CreateMap<Models.ActivityAll, Persistence.Schemas.Activity>();
            CreateMap<Models.ActivityAdd, Persistence.Schemas.Activity>();
        }
    }
}