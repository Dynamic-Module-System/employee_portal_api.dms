using Application.DTOs;
using AutoMapper;
using System.Data;

namespace Application.IMapperProfile
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DataRow, ProofPaymentDTO>()
                .ForMember(dest => dest.IdContrato, opt => opt.MapFrom(src => src["id_contrato"]))
                .ForMember(dest => dest.IdEmpresa, opt => opt.MapFrom(src => src["id_empresa"]))
                .ForMember(dest => dest.NameEmpresa, opt => opt.MapFrom(src => src["nombre_empresa"]))
                .ForMember(dest => dest.IdNomina, opt => opt.MapFrom(src => src["id_nomina"]))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src["ano"]))
                .ForMember(dest => dest.MonthDescription, opt => opt.MapFrom(src => src["mes"].ToString()))
                .ForMember(dest => dest.IdPeriodo, opt => opt.MapFrom(src => src["id_periodo"]))
                .ForMember(dest => dest.DateStart, opt => opt.MapFrom(src => src["fecha_inicial"]))
                .ForMember(dest => dest.DateEnd, opt => opt.MapFrom(src => src["fecha_final"]));
            
            CreateMap<DataRow, BussineDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["id"]))
                .ForMember(dest => dest.Nit, opt => opt.MapFrom(src => src["nit"]))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["nombre"]));
            
            CreateMap<DataRow, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src["id"]))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src["nombre"]))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src["email"]));
        }
    }
}
