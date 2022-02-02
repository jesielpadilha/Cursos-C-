namespace NSE.Bff.Compras.Models
{
    public class ItemCarrinhoDTO
    {
        public Guid ProdutoId { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        private string _nome { get; set; }
        public string Nome
        {
            get => string.IsNullOrEmpty(_nome) ? "" : _nome;
            set => _nome = value;
        }

        private string _imagem { get; set; }
        public string Imagem
        {
            get => string.IsNullOrEmpty(_imagem) ? "" : _imagem;
            set => _imagem = value;
        }
    }
}