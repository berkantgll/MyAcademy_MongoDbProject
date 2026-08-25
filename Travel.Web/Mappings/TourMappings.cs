using AutoMapper;
using Travel.Web.DTOs.TourDtos;
using Travel.Web.Entitites;
using Travel.Web.Entitites.TourDetails;

namespace Travel.Web.Mappings
{
    public class TourMappings : Profile
    {
        public TourMappings() 
        {
            CreateMap<CreateTourDateDto, TourDate>();
            CreateMap<CreateTourProgramDto, TourProgram>();
            CreateMap<CreateTourDto, Tour>();

            CreateMap<TourDate, ResultTourDateDto>();
            CreateMap<TourProgram,ResultTourProgramDto>();
            CreateMap<Tour, ResultTourDto>();

            CreateMap<ResultTourProgramDto, UpdateTourProgramDto>();
            CreateMap<ResultTourDateDto,UpdateTourDateDto>();
            CreateMap<ResultTourDto,UpdateTourDto>();

            CreateMap<UpdateTourDateDto,TourDate>();
            CreateMap<UpdateTourProgramDto,TourProgram>();
            CreateMap<UpdateTourDto , Tour>();
        }
    }
}
