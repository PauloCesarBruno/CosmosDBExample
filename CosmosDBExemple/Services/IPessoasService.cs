using CosmosDBExample.Models;

namespace CosmosDBExemple.Services
{
    public interface IPessoasService
    {
        Task<IEnumerable<Pessoa>> GetPessoas();

        Task<IEnumerable<Pessoa>> GetPessoasPorNome(string name);

        Task<IEnumerable<Pessoa>> GetPessoasPorId(string id);   

        Task AddPessoa(Pessoa pessoa);
            
        Task UpdatePessoa(string Id, Pessoa pessoa);

        Task DeletePessoa(string Id);
    }
}
