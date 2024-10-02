namespace MauiAppMinhasCompras
{
    //A classe MainPage herda de ContentPage, que representa uma página de conteúdo simples no .NET MAUI.
    public partial class MainPage : ContentPage
    {
        //Variável 'count' usada para contar o número de vezes que o botão foi clicado.
        int count = 0;

        //Construtor da classe MainPage, responsável por inicializar a página.
        public MainPage()
        {
            //Inicializa os componentes da interface.
            InitializeComponent();
        }

        //Método que lida com o evento de clique no botão. Esse método é chamado toda vez que o botão é clicado.
        private void OnCounterClicked(object sender, EventArgs e)
        {
            //Incrementa a variável 'count' cada vez que o botão é clicado.
            count++;

            //Se o contador for 1, ajusta o texto do botão para exibir "1 vez".
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";

            //Caso contrário, exibe o texto com o número de cliques no plural.
            else
                CounterBtn.Text = $"Clicked {count} times";

            /*Torna o aplicativo acessível a pessoas com deficiências visuais.
            Faz com que o texto do botão seja lido em voz alta por um leitor de tela sempre que o botão for clicado*/
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
