using AutoMapper;

namespace SendEmail.Application.MappersProfile;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}