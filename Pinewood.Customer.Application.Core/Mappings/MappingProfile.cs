using System;
using AutoMapper;
using Pinewood.Customer.Application.Core.Models;
using Pinewood.Customer.Application.Core.DTOs;

namespace Pinewood.Customer.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            CreateMap<CustomerInfoModel, CustomerInfoDto>().ReverseMap();
        }
	}
}

