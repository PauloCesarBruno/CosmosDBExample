﻿using CosmosDBExample.Models;
using CosmosDBExemple.Data;

namespace CosmosDBExemple.Services
{
    public class PessoasService : IPessoasService
    {
        private readonly NoSQLDatabase<Pessoa> _noSQLDataBase;

        public string container = "Pessoas";

        public PessoasService()
        {
            _noSQLDataBase = new();
        }

        public async Task<IEnumerable<Pessoa>> GetPessoas() 
        {
            return await _noSQLDataBase.GetAllItens(container);
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasPorId(string id)
        {
            return await _noSQLDataBase.GetByPredicate(container, i => i.Id == id);
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasPorNome(string name)
        {
            return await _noSQLDataBase.GetByPredicate(container, n => n.Nome == name);
        }

        public async Task AddPessoa(Pessoa pessoa)
        {
            await _noSQLDataBase.Add(container, pessoa, pessoa.Id.ToString());
        }                 

        public async Task UpdatePessoa(string Id, Pessoa pessoa)
        {
            await _noSQLDataBase.UpdatePessoa(container, Id, pessoa);
        }

        public async Task DeletePessoa(string Id)
        {
            await _noSQLDataBase.DeletePessoa(container, Id);
        }
    }
}
