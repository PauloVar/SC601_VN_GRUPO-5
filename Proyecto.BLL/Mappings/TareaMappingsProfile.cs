using AutoMapper;
using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Mappings
{
    public class TareaMappingsProfile : Profile
    {
        public TareaMappingsProfile()
        {
            // Mapear desde entidad Tarea hacia DTO de entrada (Request)
            CreateMap<Tarea, TareaRequest>().ReverseMap();

            // Mapear desde entidad Tarea hacia DTO de salida (Response)
            CreateMap<Tarea, TareaResponse>()
                .ForMember(dest => dest.Prioridad, opt => opt.MapFrom(src => src.IdPrioridadNavigation.Prioridad1))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.IdEstadoTareaNavigation.EstadoTarea1));

            // No es necesario mapear entre Response y Request, porque son de propósito diferente.
            // Pero si lo querés, podés hacerlo así:
            CreateMap<TareaRequest, TareaResponse>()
                .ForMember(dest => dest.Prioridad, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore())
                .ForMember(dest => dest.IdTarea, opt => opt.Ignore());

            CreateMap<TareaResponse, TareaRequest>()
                .ForMember(dest => dest.IdUsuario, opt => opt.Ignore())
                .ForMember(dest => dest.IdEstadoTarea, opt => opt.Ignore())
                .ForMember(dest => dest.IdPrioridad, opt => opt.Ignore())
                .ForMember(dest => dest.CreadaPor, opt => opt.Ignore());
        }
    }

}
