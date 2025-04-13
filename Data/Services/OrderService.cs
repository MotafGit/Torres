
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Options;

namespace Torres.Data.Services
{
    public class OrderService
    {
        string _connectionString = "Server=DESKTOP-BEAEJ8D;Database=Torrestir;Integrated Security=True;TrustServerCertificate=True;";

        public async Task<List<Order>> GetEncomendasAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<Order>("SELECT * FROM Encomendas");
                    return result.AsList();
            }
        }

        public async Task AddEncomendaAsync(Order encomenda)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))

                {
                    connection.Open();
                    await connection.ExecuteAsync("INSERT INTO Encomendas (EncomendaName, CustomerName, Address, CreatedAt, Descricao, Status) values (@EncomendaName, @CustomerName, @Address, @CreatedAt, @Descricao, @Status)", encomenda);
                }
        }


        public async Task RemoveEncomenda(int? id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync("delete from Encomendas Where ID=@ID", new { ID = id });
                }
        }

        public async Task<List<EstadoEncomenda>> GetEstadosEncomenda()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))

                {
                    await connection.OpenAsync();
                    IEnumerable<EstadoEncomenda> result = await connection.QueryAsync<EstadoEncomenda>("select * from EstadoEncomenda");
                    return result.ToList();
                }

        }

        public async Task UpdateEncomenda(Order encomenda)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    await connection.ExecuteAsync("update Encomendas set EncomendaName=@EncomendaName, CustomerName=@CustomerName, Address=@Address, Descricao=@Descricao, Status=@Status  where ID=@ID", encomenda);
                }

        }

        public async Task<Order> GetEncomendaByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<Order>("select * from Encomendas where ID = @id", new { ID = id });
                    return result.Single();
                }

        }

        public async Task<List<Order>> getEncomendasByEstado(int state)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                    await connection.OpenAsync();
                    IEnumerable<Order> result = await connection.QueryAsync<Order>("select * from Encomendas where Status=@Status", new { Status = state });
                    return result.ToList();
                }

        }


        public async Task<List<Order>> filterByName(string filter)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    IEnumerable<Order> result = await connection.QueryAsync<Order>($"select * from Encomendas where CustomerName like '%{filter}%' OR EncomendaName like '%{filter}%'");
                    return result.ToList();
                }
        }



    }
}
