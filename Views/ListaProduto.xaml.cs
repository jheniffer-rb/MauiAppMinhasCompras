//Importa o namespace
using MauiAppMinhasCompras.Models; 

//Importa a coleção observável, que notifica a interface quando elementos são adicionados ou removidos.
using System.Collections.ObjectModel; 
//Define o namespace para a classe ListaProduto.
namespace MauiAppMinhasCompras.Views;

//Define a classe ListaProduto, que herda de ContentPage e representa uma página no aplicativo.
public partial class ListaProduto : ContentPage 
{
    // Cria uma coleção observável para armazenar a lista de produtos.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>(); 

    public ListaProduto() //Construtor da classe ListaProduto.
    {
        InitializeComponent(); //Inicializa os componentes da interface gráfica definidos no arquivo XAML associado.

        lst_produtos.ItemsSource = lista; //Define a fonte de itens da ListView lst_produtos para a coleção observável.
    }

    //Método que é chamado quando a página aparece.
    protected async override void OnAppearing() 
    {
        try
        {
            lista.Clear(); //Limpa a lista atual de produtos.

            //Obtém todos os produtos do banco de dados de forma assíncrona.
            List<Produto> tmp = await App.Db.GetAll();

            //Adiciona cada produto obtido à coleção observável.
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex) //Captura exceções que podem ocorrer.
        {
            await DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usuário.
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) //Método chamado ao clicar em um item da barra de ferramentas (Navega para a página de adição de um novo produto, como um botão para adicionar um novo produto).
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto()); //Navega para a página NovoProduto.
        }
        catch (Exception ex) //Captura exceções que podem ocorrer.
        {
            DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usuário.
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) //Atualiza a lista de produtos com base na busca do usuário.
    {
        try
        {
            string q = e.NewTextValue; //Obtém o novo valor do texto.

            lista.Clear(); //Limpa a lista atual de produtos.

            //Realiza uma busca no banco de dados com o texto fornecido.
            List<Produto> tmp = await App.Db.Search(q);

            //Adiciona os produtos encontrados à coleção observável.
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex) //Captura exceções que podem ocorrer.
        {
            await DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usuário.
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e) //Calcula e exibe o total dos produtos na lista.
    {
        double soma = lista.Sum(i => i.Total); //Calcula a soma do total dos produtos na lista.

        string msg = $"O total é {soma:C}"; //Formata a mensagem para exibir o total em formato de moeda.

        DisplayAlert("Total dos Produtos", msg, "OK"); //Exibe uma mensagem com o total dos produtos.
    }

    private async void MenuItem_Clicked(object sender, EventArgs e) //Permite que o usuário exclua um produto, com se fosse uma confirmação.
    {
        try
        {
            MenuItem selecionado = sender as MenuItem; //Obtém o item de menu selecionado.
            Produto p = selecionado.BindingContext as Produto; //Obtém o produto associado ao item de menu.

            //Exibe um alerta de confirmação para remoção do produto.
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm) //Se o usuário confirmar a remoção.
            {
                await App.Db.Delete(p.Id); //Exclui o produto do banco de dados.
                lista.Remove(p); //Remove o produto da lista observável.
            }
        }
        catch (Exception ex) //Captura exceções que podem ocorrer.
        {
            await DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usuário.
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e) //Navega para a página de edição do produto selecionado.
    {
        try
        {
            Produto p = e.SelectedItem as Produto; //Obtém o produto selecionado.

            //Navega para a página EditarProduto e passa o produto selecionado como contexto de dados.
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex) //Captura exceções que podem ocorrer.
        {
            DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usuário.
        }
    }
}
