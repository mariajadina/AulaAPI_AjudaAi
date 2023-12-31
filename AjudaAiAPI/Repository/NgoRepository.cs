﻿using AjudaAiAPI.Contracts.Repository;
using AjudaAiAPI.DTO;
using AjudaAiAPI.Entity;
using AjudaAiAPI.Infrastructure;
using Dapper;

namespace AjudaAiAPI.Repository
{
    public class NgoRepository : Connection, INgoRepository
    {
        public async Task Add(NgoDTO ngo)
        {
            string sql = @"INSERT INTO
                           NGO (Name, Social, Headperson, Telephone, Email, Password, CityId, CausesId, Role)
                           VALUE (@Name, @Social, @Headperson, @Telephone, @Email, @Password, @CityId, @CausesId, @Role)";
            await Execute(sql, ngo);
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM NGO WHERE Id = @Id";
            await Execute(sql, new { id });
        }

        public async Task<IEnumerable<NgoEntity>> Get()
        {
            string sql = "SELECT * FROM NGO";
            return await GetConnection().QueryAsync<NgoEntity>(sql);
        }

        public async Task<NgoEntity> GetById(int id)
        {
            string sql = "SELECT * FROM NGO WHERE Id = @Id";
            return await GetConnection().QueryFirstAsync<NgoEntity>(sql, new { id });
        }

        public async Task<TokenDTO> Login(LoginDTO ngo)
        {
            string sql = "SELECT * FROM ngo WHERE Email = @Email AND Password = @Password";
            NgoEntity ngoLogin = await GetConnection().QueryFirstAsync<NgoEntity>(sql, ngo);
            return new TokenDTO
            {
                Token = Authentication.GenerateToken(ngoLogin),
                Ngo = ngoLogin
            };
        }

        public async Task Update(NgoEntity ngo)
        {
            string sql = @"UPDATE NGO
                           SET Name = @Name,
                               Social = @Social,
                               Headperson = @Headperson,
                               Telephone = @Telephone,
                               Email = @Email,
                               Password = @Password,
                               CityId = @CityId,
                               CausesId = @CausesId,
                               Role = @Role
                           WHERE Id = @Id";
            await Execute(sql, ngo);
        }
    }
}
