using AutoMapper;
using Travel.Web.DTOs.UserDtos;
using Travel.Web.Entitites;

namespace Travel.Web.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<CreateUserDto,User>();
            CreateMap<UpdateUserDto,User>();
            CreateMap<User,ResultUserDto>();
            CreateMap<ResultUserDto,UpdateUserDto>();
        }
    }
}
