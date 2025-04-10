using System.ComponentModel.DataAnnotations;

namespace Torres.Data
{
    public class Order
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "É necessário preencher o nome da encomenda.")]
        [StringLength(20, ErrorMessage = "O nome da encomenda nao pode ter mais de 20 caracters")]
        public string EncomendaName { get; set; }

        [Required(ErrorMessage = "É necessário preencher o nome do utilizador.")]
        [StringLength(20, ErrorMessage = "O nome do utilizador nao pode ter mais de 20 caracters")]
        public string CustomerName { get; set; }
      
        [Required(ErrorMessage = "É necessário preencher a morada.")]
        [StringLength(50, ErrorMessage = "A morada nao pode ter mais de 50 caracters")]
        public string Address { get; set; }

        [StringLength(150, ErrorMessage = "A descricao nao pode ter mais de 150 caracters")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "É necessário escolher o estado.")]
        public int Status { get; set; } // Pendente, Em preparação, Despachada, Entregue

        public DateTime CreatedAt { get; set; }

    }

}
