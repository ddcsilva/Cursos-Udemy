using MagicVilla.API.Models;

namespace MagicVilla.API.Repository.IRepository;

public interface IVilaRepository : IRepository<Vila>
{
    Task<Vila> Atualizar(Vila entity);
}