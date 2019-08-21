using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository
    {
        private string StringConexao = "Data Source =.\\SqlExpress; Initial Catalog = T_Peoples; User Id = sa; Pwd=132";

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "SELECT IdFuncionarios, Nome, Sobrenome FROM Funcionarios";
                con.Open();

                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    sdr = cmd.ExecuteReader();
                    
                    while(sdr.Read())
                    {
                        FuncionarioDomain funcionario = new FuncionarioDomain
                        {
                            IdFuncionarios = Convert.ToInt32(sdr["IdFuncionarios"]),
                            Nome = sdr["Nome"].ToString(),
                            Sobrenome = sdr["Sobrenome"].ToString()
                        };
                        funcionarios.Add(funcionario);
                    }
                }
            }
            return funcionarios;
        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            string Query = "SELECT IdFuncionarios, Nome, Sobrenome FROM Funcionarios WHERE IdFuncionarios = @IdFuncionarios";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                con.Open();
                SqlDataReader sdr;

                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@IdFuncionarios", id);
                    sdr = cmd.ExecuteReader();

                    if(sdr.HasRows)
                    {
                        while(sdr.Read())
                        {
                            FuncionarioDomain funcionario = new FuncionarioDomain
                            {
                                IdFuncionarios = Convert.ToInt32(sdr["IdFuncionarios"]),
                                Nome = sdr["Nome"].ToString(),
                                Sobrenome = sdr["Sobrenome"].ToString()
                            };
                            return funcionario;
                        }
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(FuncionarioDomain funcionarioDomain)
        {
            string Query = "INSERT INTO Funcionarios (Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";

            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Nome", funcionarioDomain.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionarioDomain.Sobrenome);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Deletar(int id)
        {
            string Query = "DELETE FROM Funcionarios WHERE IdFuncionarios = @idFuncionarios";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                using (SqlCommand cmd = new SqlCommand(Query, con))
                {
                    cmd.Parameters.AddWithValue("@idFuncionarios", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(FuncionarioDomain funcionarioDomain)
        {
            string Query = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdFuncionarios = @Id";
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Id", funcionarioDomain.IdFuncionarios);
                cmd.Parameters.AddWithValue("@Nome", funcionarioDomain.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", funcionarioDomain.Sobrenome);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
