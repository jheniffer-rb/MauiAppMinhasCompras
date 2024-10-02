// Importa o namespace contendo o modelo Produto.
using MauiAppMinhasCompras.Models;
// Importa o namespace SQLite para utilizar suas classes e métodos.
using SQLite;

//Define o namespace Helpers, onde a classe SQLiteDatabaseHelper está localizada.
namespace MauiAppMinhasCompras.Helpers 
{
    //Define a classe pública SQLiteDatabaseHelper.
    public class SQLiteDatabaseHelper
    {
        //Declara uma variável privada e somente leitura para armazenar a conexão assíncrona com o banco de dados.
        readonly SQLiteAsyncConnection _conn;

        //Construtor da classe que recebe o caminho do banco de dados.
        public SQLiteDatabaseHelper(string path)
        {
            //Inicializa a conexão com o banco de dados SQLite.
            _conn = new SQLiteAsyncConnection(path);
            //Cria a tabela Produto no banco de dados, caso não exista.
            _conn.CreateTableAsync<Produto>().Wait();
        }

        //Método para inserir um novo produto no banco de dados.
        public Task<int> Insert(Produto p)
        {
            //Retorna a tarefa assíncrona que insere o produto na tabela.
            return _conn.InsertAsync(p);
        }

        //Método para atualizar um produto existente no banco de dados.
        public Task<List<Produto>> Update(Produto p)
        {
            //Define a consulta SQL para atualizar as informações do produto.
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            //Executa a consulta e retorna a lista de produtos afetados.
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        //Método para excluir um produto do banco de dados com base em seu Id.
        public Task<int> Delete(int id)
        {
            //Executa a operação de exclusão e retorna o número de registros excluídos.
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        //Método para obter todos os produtos do banco de dados.
        public Task<List<Produto>> GetAll()
        {
            //Retorna uma lista assíncrona de todos os produtos na tabela.
            return _conn.Table<Produto>().ToListAsync();
        }

        //Método para buscar produtos que correspondem a uma string de consulta.
        public Task<List<Produto>> Search(string q)
        {
            //Define a consulta SQL para buscar produtos com base na descrição.
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            //Executa a consulta e retorna a lista de produtos correspondentes.
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
