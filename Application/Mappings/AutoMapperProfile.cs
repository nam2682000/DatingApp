using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Security;
using AutoMapper;
using Domain.Entities.Entity;
using System;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<UserRegisterRequest, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHasher.HashPassword(src.Password)));
        CreateMap<User, UserRegisterRequest>();
    }
}