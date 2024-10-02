//Importa o namespace SQLite, permitindo o uso de classes e atributos relacionados ao banco de dados SQLite.
using SQLite; 

//Define o namespace para agrupar classes relacionadas ao modelo do aplicativo.
namespace MauiAppMinhasCompras.Models
{
    //Define a classe Produto, representando um item a ser armazenado no banco de dados.
    public class Produto
    {
        //Declara uma variável privada para armazenar a descrição do produto.
        string _descricao;

        //Define a propriedade Id, que é a chave primária da tabela e será incrementada automaticamente.
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //Define a propriedade Descricao, que é uma string representando a descrição do produto.
        public string Descricao
        {
            //Retorna o valor atual da descrição.
            get => _descricao;
            //Define um novo valor para a descrição.
            set
            {
                //Verifica se o valor atribuído é nulo. Se for, lança uma exceção.
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                //Atribui o valor à variável privada _descricao.
                _descricao = value;
            }
        }

        //Define a propriedade Quantidade, representando a quantidade do produto em estoque.
        public double Quantidade { get; set; }

        //Define a propriedade Preco, que representa o preço unitário do produto.
        public double Preco { get; set; }

        //Define a propriedade Total, que calcula e retorna o total com base na quantidade e no preço do produto.
        public double Total { get => Quantidade * Preco; }
    }
}
