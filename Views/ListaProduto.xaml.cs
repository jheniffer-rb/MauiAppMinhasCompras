//Importa o namespace
using MauiAppMinhasCompras.Models; 

//Importa a cole��o observ�vel, que notifica a interface quando elementos s�o adicionados ou removidos.
using System.Collections.ObjectModel; 
//Define o namespace para a classe ListaProduto.
namespace MauiAppMinhasCompras.Views;

//Define a classe ListaProduto, que herda de ContentPage e representa uma p�gina no aplicativo.
public partial class ListaProduto : ContentPage 
{
    // Cria uma cole��o observ�vel para armazenar a lista de produtos.
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>(); 

    public ListaProduto() //Construtor da classe ListaProduto.
    {
        InitializeComponent(); //Inicializa os componentes da interface gr�fica definidos no arquivo XAML associado.

        lst_produtos.ItemsSource = lista; //Define a fonte de itens da ListView lst_produtos para a cole��o observ�vel.
    }

    //M�todo que � chamado quando a p�gina aparece.
    protected async override void OnAppearing() 
    {
        try
        {
            lista.Clear(); //Limpa a lista atual de produtos.

            //Obt�m todos os produtos do banco de dados de forma ass�ncrona.
            List<Produto> tmp = await App.Db.GetAll();

            //Adiciona cada produto obtido � cole��o observ�vel.
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex) //Captura exce��es que podem ocorrer.
        {
            await DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usu�rio.
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) //M�todo chamado ao clicar em um item da barra de ferramentas (Navega para a p�gina de adi��o de um novo produto, como um bot�o para adicionar um novo produto).
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto()); //Navega para a p�gina NovoProduto.
        }
        catch (Exception ex) //Captura exce��es que podem ocorrer.
        {
            DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usu�rio.
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e) //Atualiza a lista de produtos com base na busca do usu�rio.
    {
        try
        {
            string q = e.NewTextValue; //Obt�m o novo valor do texto.

            lista.Clear(); //Limpa a lista atual de produtos.

            //Realiza uma busca no banco de dados com o texto fornecido.
            List<Produto> tmp = await App.Db.Search(q);

            //Adiciona os produtos encontrados � cole��o observ�vel.
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex) //Captura exce��es que podem ocorrer.
        {
            await DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usu�rio.
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e) //Calcula e exibe o total dos produtos na lista.
    {
        double soma = lista.Sum(i => i.Total); //Calcula a soma do total dos produtos na lista.

        string msg = $"O total � {soma:C}"; //Formata a mensagem para exibir o total em formato de moeda.

        DisplayAlert("Total dos Produtos", msg, "OK"); //Exibe uma mensagem com o total dos produtos.
    }

    private async void MenuItem_Clicked(object sender, EventArgs e) //Permite que o usu�rio exclua um produto, com se fosse uma confirma��o.
    {
        try
        {
            MenuItem selecionado = sender as MenuItem; //Obt�m o item de menu selecionado.
            Produto p = selecionado.BindingContext as Produto; //Obt�m o produto associado ao item de menu.

            //Exibe um alerta de confirma��o para remo��o do produto.
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "N�o");

            if (confirm) //Se o usu�rio confirmar a remo��o.
            {
                await App.Db.Delete(p.Id); //Exclui o produto do banco de dados.
                lista.Remove(p); //Remove o produto da lista observ�vel.
            }
        }
        catch (Exception ex) //Captura exce��es que podem ocorrer.
        {
            await DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usu�rio.
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e) //Navega para a p�gina de edi��o do produto selecionado.
    {
        try
        {
            Produto p = e.SelectedItem as Produto; //Obt�m o produto selecionado.

            //Navega para a p�gina EditarProduto e passa o produto selecionado como contexto de dados.
            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex) //Captura exce��es que podem ocorrer.
        {
            DisplayAlert("Ops", ex.Message, "OK"); //Exibe uma mensagem de erro ao usu�rio.
        }
    }
}
