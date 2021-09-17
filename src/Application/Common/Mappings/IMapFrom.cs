using AutoMapper;

namespace CleanArchitecture.Razor.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {   
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType(),MemberList.None).ReverseMap();
    }
}
