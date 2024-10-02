//Importa o namespace.
using MauiAppMinhasCompras.Models;

//Define o namespace para a classe NovoProduto.
namespace MauiAppMinhasCompras.Views;

//Define a classe NovoProduto, que herda de ContentPage, representando uma p�gina para adicionar novos produtos
public partial class NovoProduto : ContentPage .
{
    public NovoProduto() //Construtor da classe.
    {
        InitializeComponent(); //Inicializa os componentes da interface gr�fica definidos no arquivo XAML associado.
    }

    //� acionado quando um item da barra de ferramentas (como um bot�o "Salvar") � clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e) 
    {
        try
        {
            //Cria um novo objeto Produto com os dados inseridos pelo usu�rio nos campos de texto.
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text, //Obt�m a descri��o do produto.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), //Converte o texto da quantidade para um n�mero double.
                Preco = Convert.ToDouble(txt_preco.Text) //Converte o texto do pre�o para um n�mero double.
            };

            //Insere o novo produto no banco de dados de forma ass�ncrona.
            await App.Db.Insert(p);
            //Exibe uma mensagem de sucesso ao usu�rio.
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
        }
        catch (Exception ex) //Captura exce��es que podem ocorrer durante a execu��o do c�digo.
        {
            //Exibe uma mensagem de erro ao usu�rio com a descri��o da exce��o.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
