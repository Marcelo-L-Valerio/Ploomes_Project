﻿using AutoMapper;
using Ploomers_Project_API.Mappers.DTOs.InputModels;
using Ploomers_Project_API.Mappers.DTOs.ViewModels;
using Ploomers_Project_API.Models.Entities;

namespace Ploomers_Project_API.Mappers
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            // One way only, the login shouldn't retrieve the user data
            CreateMap<LoginInputModel, Employee>();
        }
    }
}
