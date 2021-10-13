using AutoMapper;
using TheosTalk.Dto.Model;
using TheosTalk.Model;

namespace TheosTalk.AutoMapper
{
    public static class IgrejaDomainDtoMapper
    {
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IgrejaModel, IgrejaDto>()
                .ForMember(x => x.Endereco, map => map.MapFrom(src => new EnderecoDto
                {
                    Logradouro = src.EnderecoIgreja.Logradouro,
                    Numero = src.EnderecoIgreja.Numero,
                    Bairro = src.EnderecoIgreja.Bairro,
                    Cidade = $"{src.EnderecoIgreja.Cidade} / {src.EnderecoIgreja.UF}",
                    UF = src.EnderecoIgreja.UF,
                    CEP = src.EnderecoIgreja.CEP

                }));
                
                cfg.CreateMap<IgrejaDto, IgrejaModel>()
                .ForMember(x => x.EnderecoIgreja, map => map.MapFrom(src => new Endereco
                {
                    Logradouro = src.Endereco.Logradouro,
                    Numero = src.Endereco.Numero,
                    Bairro = src.Endereco.Bairro,
                    Cidade = $"{src.Endereco.Cidade} / {src.Endereco.UF}",
                    UF = src.Endereco.UF,
                    CEP = src.Endereco.CEP

                }));
            });

            return mapperConfiguration.CreateMapper();

        }
    }
}
