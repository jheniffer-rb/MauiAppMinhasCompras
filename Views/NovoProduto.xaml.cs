//Importa o namespace.
using MauiAppMinhasCompras.Models;

//Define o namespace para a classe NovoProduto.
namespace MauiAppMinhasCompras.Views;

//Define a classe NovoProduto, que herda de ContentPage, representando uma página para adicionar novos produtos
public partial class NovoProduto : ContentPage .
{
    public NovoProduto() //Construtor da classe.
    {
        InitializeComponent(); //Inicializa os componentes da interface gráfica definidos no arquivo XAML associado.
    }

    //É acionado quando um item da barra de ferramentas (como um botão "Salvar") é clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e) 
    {
        try
        {
            //Cria um novo objeto Produto com os dados inseridos pelo usuário nos campos de texto.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, //Obtém a descrição do produto.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), //Converte o texto da quantidade para um número double.
                Preco = Convert.ToDouble(txt_preco.Text) //Converte o texto do preço para um número double.
            };

            //Insere o novo produto no banco de dados de forma assíncrona.
            await App.Db.Insert(p);
            //Exibe uma mensagem de sucesso ao usuário.
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
        }
        catch (Exception ex) //Captura exceções que podem ocorrer durante a execução do código.
        {
            //Exibe uma mensagem de erro ao usuário com a descrição da exceção.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
