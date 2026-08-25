using AutoMapper;
using Travel.Web.DTOs.ReservationDtos;
using Travel.Web.Entitites;

namespace Travel.Web.Mappings
{
    public class ReservationMappings : Profile
    {
        public ReservationMappings() 
        {
            CreateMap<CreateReservationDto, Reservation>();
            CreateMap<Reservation,ResultReservationDto>();
        }
    }
}
