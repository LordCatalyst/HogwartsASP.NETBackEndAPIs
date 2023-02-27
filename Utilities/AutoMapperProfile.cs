using AutoMapper;
using HogwartsBackEndAPIs.DTOs;
using HogwartsBackEndAPIs.Models;
using System.Globalization;

namespace HogwartsBackEndAPIs.Utilities
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region House
            CreateMap<House, HouseDTO>().ReverseMap();
            #endregion

            #region WizardRequest
            CreateMap<WizardRequest, WizardRequestDTO>().ForMember(destination => destination.HouseName, opt => opt.MapFrom(origin => origin.House.HouseName));
            CreateMap<WizardRequestDTO, WizardRequest>().ForMember(destination => destination.House, opt => opt.Ignore());
            #endregion
        }
    }
}
