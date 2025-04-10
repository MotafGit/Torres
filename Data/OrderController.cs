using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic; // For IEnumerable<T>
using System.Threading.Tasks; // For Task and async/await

namespace Torres.Data
{
    public class OrderController 
    {
        public IConfiguration Configuration;
        private const string DATABASE = "Torrestir";
        string connectionString = "Server=DESKTOP-BEAEJ8D;Database=Torrestir;Integrated Security=True;TrustServerCertificate=True;";

        public OrderController(IConfiguration configuration)
        {
            Configuration = configuration; //Inject configuration to access Connection string from appsettings.json.
        }


        public async Task<List<EstadoEncomenda>> GetEstadosEncomendaAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))

                try
                {
                    await connection.OpenAsync();
                    IEnumerable<EstadoEncomenda> result = await connection.QueryAsync<EstadoEncomenda>("select * from EstadoEncomenda");
                    return result.ToList();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                    throw;
                }
        }



        public async Task<List<Order>> GetEncomendasAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))

            try
            {
                await connection.OpenAsync();
                IEnumerable<Order> result = await connection.QueryAsync<Order>("select * from Encomendas");
                return result.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General error: " + ex.Message);
                throw;
            }
        }

        public async Task AddEncomendaAsync(Order encomenda)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
             try
            {
                connection.Open();
                await connection.ExecuteAsync("INSERT INTO Encomendas (EncomendaName, CustomerName, Address, CreatedAt, Descricao, Status) values (@EncomendaName, @CustomerName, @Address, @CreatedAt, @Descricao, @Status)", encomenda);
                }
            catch (SqlException sqlEx)
            {

                Console.Error.WriteLine($"Database error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw; 
            }
        }

        public async Task RemoveEncomendaByIdAsync(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {
                    connection.Open();
                    await connection.ExecuteAsync("delete from Encomendas Where ID=@ID", new { ID = id });
                }
                catch (SqlException sqlEx)
                {

                    Console.Error.WriteLine($"Database error: {sqlEx.Message}");
                    throw; 
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    throw; 
                }
        }

        public async Task UpdateEncomendaAsync(Order encomenda)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            try
            {
                connection.Open();
                await connection.ExecuteAsync("update Encomendas set EncomendaName=@EncomendaName, CustomerName=@CustomerName, Address=@Address, Descricao=@Descricao, Status=@Status  where ID=@ID", encomenda);
            }
            catch (SqlException sqlEx)
            {

                Console.Error.WriteLine($"Database error: {sqlEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<Order> GetEncomendaByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))

                try
                {
                    await connection.OpenAsync();
                    var result = await connection.QueryAsync<Order>("select * from Encomendas where ID = @id", new { ID = id });
                    return result.Single();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                    throw;
                }
        }

        public async Task<List<Order>> getEncomendasByEstado(int state)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {
                    await connection.OpenAsync();
                    IEnumerable <Order> result = await connection.QueryAsync<Order>("select * from Encomendas where Status=@Status", new { Status = state });
                    return result.ToList();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                    throw;
                }
        }

        public async Task<List<Order>> filterByName(string filter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
                try
                {
                    await connection.OpenAsync();
                    IEnumerable<Order> result = await connection.QueryAsync<Order>($"select * from Encomendas where CustomerName like '%{filter}%' OR EncomendaName like '%{filter}%'");
                    return result.ToList();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General error: " + ex.Message);
                    throw;
                }
        }


    }



}
