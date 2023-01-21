using eCommerce.API.Models;
using Dapper.FluentMap.Mapping;

namespace eCommerce.API.Mappers
{
    public class UsuarioTwoMap : EntityMap<UsuarioTwo>
    {
        public UsuarioTwoMap()
        {
            Map(p => p.Cod).ToColumn("Id");
            Map(p => p.NomeCompleto).ToColumn("Nome");
            Map(p => p.NomeCompletoMae).ToColumn("NomeMae");
            Map(p => p.Situacao).ToColumn("SituacaoCadastro");
        }
    }
}
