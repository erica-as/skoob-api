namespace Skoob.API.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> ObterTodos();
    T? ObterPorId(int id);
    void Adicionar(T entidade);
    void Atualizar(T entidade);
}