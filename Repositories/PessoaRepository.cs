using WebApplication3.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication3.Repositories
{
    public class PessoaRepository : AbstractRepository<Pessoa, int>
    {
        private string StringConnection;
        public PessoaRepository(string connectionString)
        {
            StringConnection = connectionString;
        }

        // Delete
        public override void Delete(Pessoa entity)
        {
            using (var conn = new SqlConnection(StringConnection)) // Usando a conexão do banco de dados 
            {
                string sql = "DELETE Pessoa Where Id=@Id"; // comando que será posteriormente executado
                SqlCommand cmd = new SqlCommand(sql, conn); // declara que cmd é um SqlCommand com o comando x e a conexão y
                cmd.Parameters.AddWithValue("@Id", entity.Id); // Adiciona valor ao parametro @Id com o Id do objeto entity
                try
                {
                    conn.Open(); // Abre conexão ao banco de dados
                    cmd.ExecuteNonQuery(); // Executa o comando
                }
                catch (Exception e) // Caso ocorra um erro
                {
                    throw e;
                }
            }
        }

        // Delete by ID
        public override void DeleteById(int id)
        {
            using (var conn = new SqlConnection(StringConnection)) // Abre conexão ao banco de dados numa variável
            {
                string sql = "DELETE Pessoa Where Id=@Id"; // declara a string sql que contém um comando "DELETE Pessoa onde a coluna Id=@Id" com um parametro @Id
                SqlCommand cmd = new SqlCommand(sql, conn); // declara que cmd é um SqlCommand com o comando x e a conexão y
                cmd.Parameters.AddWithValue("@Id", id); // Adiciona valor ao parametro @Id com o valor do argumento id
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        // Read
        public override List<Pessoa> GetAll()
        {
            string sql = "Select Id, Nome, Email, Cidade, Endereco FROM Pessoa ORDER BY Nome"; // " Selecione Id, Nome, Email, Cidade, Endereco da tabela pessoa na ordem crescente do nome(a,b,c...
            using (var conn = new SqlConnection(StringConnection)) // usando a conexão sql x
            {
                var cmd = new SqlCommand(sql, conn);  // declara que cmd é um SqlCommand com o comando x e a conexão y
                List<Pessoa> list = new List<Pessoa>();
                Pessoa p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Pessoa(); // p recebe um novo objeto Pessoa com as mesmas declarações
                            p.Id = (int)reader["Id"]; // o objeto p.Id = com o casting(int) a leitura da coluna Id
                            p.Nome = reader["Nome"].ToString(); // o objeto p.Nome = a leitura da coluna Nome com a conversão para string
                            p.Email = reader["Email"].ToString(); // o objeto p.Email = a leitura da coluna Nome com a conversão para string
                            p.Cidade = reader["Cidade"].ToString(); // o objeto p.Cidade = a leitura da coluna Cidade com a conversão para string
                            p.Endereco = reader["Endereco"].ToString(); // o objeto p.Endereco = a leitura da coluna Endereco com a conversão para string
                            list.Add(p); // A lista do Modelo Pessoa da list recebe o objeto de p
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }

        // Read By ID
        public override Pessoa GetById(int id) 
        {
            using (var conn = new SqlConnection(StringConnection)) // usando a conexão sql x
            {
                string sql = "Select Id, Nome, Email, Cidade, Endereco FROM Pessoa WHERE Id=@Id"; // " Selecione Id, Nome, Email, Cidade, Endereco da tabela pessoa na ordem crescente do nome(a,b,c...
                SqlCommand cmd = new SqlCommand(sql, conn); // declara que cmd é um SqlCommand com o comando x e a conexão y
                cmd.Parameters.AddWithValue("@Id", id); // Adiciona valor ao parametro @Id com o valor do argumento id
                Pessoa p = null; // declara do objeto Pessoa p equivale a nada
                try
                {
                    conn.Open(); // abre a conexão com a conexão sql
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection)) 
                    {
                        if (reader.HasRows) // se a leitura do banco de dados retorne true
                        {
                            if (reader.Read()) // se tiver mais rows retorna true
                            {
                                p = new Pessoa(); // p é um novo objeto p;
                                p.Id = (int)reader["Id"]; // o objeto p.Id = com o casting(int) a leitura da coluna Id
                                p.Nome = reader["Nome"].ToString(); // o objeto p.Nome = a leitura da coluna Nome com a conversão para string
                                p.Email = reader["Email"].ToString(); // o objeto p.Email = a leitura da coluna Nome com a conversão para string
                                p.Cidade = reader["Cidade"].ToString(); // o objeto p.Cidade = a leitura da coluna Cidade com a conversão para string
                                p.Endereco = reader["Endereco"].ToString(); // o objeto p.Endereco = a leitura da coluna Endereco com a conversão para string
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return p;
            }
        }

        // Create
        public override void Save(Pessoa entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO Pessoa (Nome, Email, Cidade, Endereco) VALUES (@Nome, @Email, @Cidade, @Endereco)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nome", entity.Nome); // Relaciona o parametro @Nome ao objeto entity.Nome
                cmd.Parameters.AddWithValue("@Email", entity.Email); // Relaciona o parametro @Email ao objeto entity.Email
                cmd.Parameters.AddWithValue("@Cidade", entity.Cidade); // Relaciona o parametro @Cidade ao objeto entity.Cidade
                cmd.Parameters.AddWithValue("@Endereco", entity.Endereco); // Relaciona o parametro @Endereco ao objeto entity.Endereco
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        // Update
        public override void Update(Pessoa entity) // Declara entity recebendo o objeto Pessoa
        {
            using (var conn = new SqlConnection(StringConnection)) // Faz a conexão SQL
            {
                string sql = "UPDATE Pessoa SET Nome=@Nome, Email=@Email, Cidade=@Cidade, Endereco=@Endereco Where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@Cidade", entity.Cidade);
                cmd.Parameters.AddWithValue("@Endereco", entity.Endereco);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
