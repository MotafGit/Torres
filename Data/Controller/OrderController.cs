using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Torres.Data.Services;
using Torres.Components.Pages;

namespace Torres.Data.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : ControllerBase
    {

        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetEncomendas()
        {
            try
            {
                var orders = await _orderService.GetEncomendasAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro.");

                return StatusCode(500); 
            }
        }


        [HttpPost] 
        public async Task<IActionResult> AddEncomenda([FromBody] Order encomenda)
        {
            try
            {
                await _orderService.AddEncomendaAsync(encomenda);
                Response.Headers.Append("CustomMessage", "Encomenda criada.");
                return StatusCode(201);
            }
            catch (SqlException sqlEx)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro ao gravar.");
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro ao gravar.");
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEncomendaByIdAsync(int? id)
        {

            if (id == null)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro.");
                return StatusCode(500);
            }
            try
            {
                await _orderService.RemoveEncomenda(id);
                Response.Headers.Append("CustomMessage", "Encomenda apagada.");
                return StatusCode(204);
            }
            catch (SqlException sqlEx)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro ao apagar.");
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro ao apagar.");
                return StatusCode(500);
            }
        }


        [HttpGet ("estadosEncomenda")]
        public async Task<ActionResult<List<EstadoEncomenda>>> GetEstadosEncomendaAsync()
        {
            try
            {
               var result = await _orderService.GetEstadosEncomenda();
               return result.ToList();
            }
            catch
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro");
                return StatusCode(500);
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateEncomendaAsync(Order encomenda)
        {
            try
            {
                await _orderService.UpdateEncomenda(encomenda);
                Response.Headers.Append("CustomMessage", "Encomenda actualizada.");
                return StatusCode(201);
            }
            catch (SqlException sqlEx)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro ao actualizar.");
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                Response.Headers.Append("CustomMessage", "Ocorreu um erro ao actualizar.");
                return StatusCode(500);
            }
        }

        [HttpGet("getEncomendasByEstado/{state}")]
        public async Task<ActionResult<List<Order>>> getEncomendasByEstado(int state)
        {
                try
                {
                var encomendas = await _orderService.getEncomendasByEstado(state);
                return Ok(encomendas);
                }
                catch (SqlException sqlEx)
                {
                    Response.Headers.Append("CustomMessage", "Ocorreu um erro.");
                    return StatusCode(500);
                }
        }

        [HttpGet("getEncomendasByName/{filter}")]
        public async Task<ActionResult<List<Order>>> filterByName(string filter)
        {
                try
                {
                    var encomendas = await _orderService.filterByName(filter);
                    return Ok(encomendas);
                }
                catch (SqlException sqlEx)
                {
                    Response.Headers.Append("CustomMessage", "Ocorreu um erro.");
                    return StatusCode(500);
                }
        }


    }


}
