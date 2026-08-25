using AutoMapper;
using Travel.Web.DTOs.QuestionDtos;
using Travel.Web.Entitites;

namespace Travel.Web.Mappings
{
    public class QuestionMappings:Profile
    {
        public QuestionMappings()
        {
            CreateMap<CreateQuestionDto,Question>();
            CreateMap<Question,ResultQuestionDto>();
            CreateMap<ResultQuestionDto, UpdateQuestionDto>();
        }
    }
}
