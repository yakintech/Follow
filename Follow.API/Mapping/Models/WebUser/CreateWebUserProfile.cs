using AutoMapper;
using Follow.API.DTO.WebUser;
using Follow.Data.Models;

namespace Follow.API.Mapping.Models
{
    public class CreateWebUserProfile : Profile
    {
        public CreateWebUserProfile()
        {
            CreateMap<CreateWebUserRequestDTO, WebUser>()
                .AfterMap((src, dest) =>
                {
                    dest.Country = "Turkey";
                });
        }
    }
}
