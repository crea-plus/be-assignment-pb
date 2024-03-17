using AutoMapper;
using Data.Models;
using Service.DTOs;

namespace Service.Profiles;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
		CreateMap<Contact, ContactDto>();
	}
}
