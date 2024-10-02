using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    //Classe principal do aplicativo, herda de Application, que representa o ponto de entrada do app.
    public partial class App : Application
    {
        //Campo estático que armazena a instância do helper do banco de dados SQLite.
        static SQLiteDatabaseHelper _db;

        //cria uma propriedade estática que gerencia o acesso ao banco de dados SQLite dentro do aplicativo
        //Garante que o banco de dados seja instanciado apenas uma vez (padrão Singleton)
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // Verifica se o helper do banco de dados já foi instanciado.
                if (_db == null)
                {
                    //Define o caminho onde o banco de dados SQLite será armazenado.
                    //Ele será salvo na pasta de dados local da aplicação.
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    //Cria uma nova instância da classe SQLiteDatabaseHelper, que será utilizada para gerenciar o banco de dados SQLite.
                    _db = new SQLiteDatabaseHelper(path);
                }

                //Retorna a instância do helper de banco de dados.
                return _db;
            }
        }

        //Construtor da classe App, responsável por inicializar os componentes do app e definir a página principal.
        public App()
        {
            //Inicializa os componentes da interface.
            InitializeComponent();

            //Define a página de navegação inicial como a 'ListaProduto', que exibe uma lista de produtos.
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}
