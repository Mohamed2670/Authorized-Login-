using AutoMapper;
using MwTesting.Dtos;
using MwTesting.Model;

namespace MwTesting.Profiles
{
    public class UserProfile : Profile
    {

        public UserProfile()
        {
            //       from    to 
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }

}