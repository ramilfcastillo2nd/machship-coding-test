using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace codingtest.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<GithubUserInfo, RetrieveUsersResponse>()
                 .ForMember(d => d.name, opt => opt.MapFrom(s => s.name))
                 .ForMember(d => d.company, opt => opt.MapFrom(s => s.company))
                 .ForMember(d => d.NoOfFollowers, opt => opt.MapFrom(s => s.followers))
                 .ForMember(d => d.NoOfPublicRepo, opt => opt.MapFrom(s => s.public_repos))
                 .ForMember(d => d.AverageNoOfFollowersPerRepo, opt => opt.MapFrom(s => s.public_repos))
                 .ForMember(d => d.login, opt => opt.MapFrom(s => s.login));

        }
    }
}
