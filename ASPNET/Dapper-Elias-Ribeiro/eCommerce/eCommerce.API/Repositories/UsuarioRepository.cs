﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using eCommerce.API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private IDbConnection _connection;

        public UsuarioRepository()
        {
            _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public List<Usuario> Get()
        {
            return _connection.Query<Usuario>("SELECT * FROM Usuarios").ToList();
        }

        public Usuario Get(int id)
        {
            return _connection.Query<Usuario, Contato, Usuario>(
                @"SELECT * FROM Usuarios as U 
                      LEFT JOIN Contatos C ON C.UsuarioId = U.Id 
                      WHERE U.Id = @Id",
                (usuario, contato) =>
                {
                    usuario.Contato = contato;
                    return usuario;
                },
                new { Id = id }
            ).SingleOrDefault();
        }

        public void Insert(Usuario usuario)
        {
            _connection.Open();

            var transaction = _connection.BeginTransaction();

            try
            {
                string sql = @"INSERT INTO Usuarios
                           (Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) 
                           VALUES (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro); 
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";
                usuario.Id = _connection.Query<int>(sql, usuario, transaction).Single();

                if (usuario.Contato != null)
                {
                    usuario.Contato.UsuarioId = usuario.Id;
                    string sqlContato = @"INSERT INTO Contatos
                                      (UsuarioId, Telefone, Celular) 
                                      VALUES (@UsuarioId, @Telefone, @Celular); 
                                      SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    usuario.Contato.Id = _connection.Query<int>(sqlContato, usuario.Contato, transaction).Single();
                }

                transaction.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    // Retornar para UsuárioController alguma mensagem. Lançar uma exception.
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Update(Usuario usuario)
        {
            _connection.Open();
            var transaction = _connection.BeginTransaction();

            try
            {
                string sql = "UPDATE Usuarios " +
                             "SET Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro " +
                             "WHERE Id = @Id";
                _connection.Execute(sql, usuario, transaction);

                if (usuario.Contato != null)
                {
                    string sqlContato = "UPDATE Contatos " +
                                        "SET UsuarioId = @UsuarioId, Telefone = @Telefone, Celular = @Celular " +
                                        "WHERE Id = @Id";
                    _connection.Execute(sqlContato, usuario.Contato, transaction);
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    // Retornar para UsuárioController alguma mensagem. Lançar uma exception.
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Usuarios WHERE Id = @Id", new { Id = id });
        }

        private static List<Usuario> _db = new List<Usuario>()
        {
            new Usuario(){ Id=1, Nome="Filipe Rodrigues", Email="filipe.rodrigues@gmail.com" },
            new Usuario(){ Id=2, Nome="Marcelo Rodrigues", Email="marcelo.rodrigues@gmail.com"},
            new Usuario(){ Id=3, Nome="Jessica Rodrigues", Email="jessica.rodrigues@gmail.com"}
        };
    }
}
