using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao= string.Empty;
        double _quantidade, _preco;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value;
            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                _quantidade = value;
            }
        }
        public double Preco
        {
            get => _preco;
            set
            {
                _preco = value;
            }
        }
        public double Total { get => Quantidade * Preco; }

    }
}
