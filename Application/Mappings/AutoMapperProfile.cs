using Application.DTOs.DTO;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.DTOs.Responses.User;
using Application.Security;
using AutoMapper;
using Domain.Entities.Entity;
using System;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserProfileReponse>().ReverseMap();
        CreateMap<UserProfileReponse, UserDTO>().ReverseMap();
        CreateMap<UserRegisterRequest, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));
        CreateMap<User, UserRegisterRequest>();
        CreateMap<User, UserMessageResponse>().ReverseMap();
    }
}