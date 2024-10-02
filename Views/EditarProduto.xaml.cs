//Importa o namespace que cont�m os models do aplicativo.
using MauiAppMinhasCompras.Models;

//Define o namespace para a classe EditarProduto.
namespace MauiAppMinhasCompras.Views;

//Define a classe EditarProduto que herda de ContentPage, representando uma p�gina do aplicativo.
public partial class EditarProduto : ContentPage
{
    //Construtor da classe EditarProduto.
    public EditarProduto() 
    {
        //Inicializa os componentes da interface definidos no arquivo XAML.
        InitializeComponent();
    }

    //� acionado quando um item (como um bot�o de salvar) � clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e) 
    {
        try //Inicia um bloco try para capturar exce��es que possam ocorrer.

            //Recupera o produto atual que est� sendo editado atrav�s do BindingContext.
            Produto produto_anexado = BindingContext as Produto;

        //Cria um novo objeto Produto com as informa��es inseridas pelo usu�rio nos campos de texto (descri��o, quantidade e pre�o).
        Produto p = new Produto
            {
                Id = produto_anexado.Id, //Mant�m o ID do produto existente.
                Descricao = txt_descricao.Text, //Obt�m a descri��o do produto a partir de uma entrada de texto na interface.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), //Converte o texto da quantidade para um n�mero double.
                Preco = Convert.ToDouble(txt_preco.Text) //Converte o texto do pre�o para um n�mero double.
            };

            //Chama o m�todo Update do banco de dados para atualizar o registro do produto.
            await App.Db.Update(p);
            //Exibe uma mensagem de sucesso ao usu�rio.
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            //Navega de volta para a p�gina anterior.
            await Navigation.PopAsync();
        }
        catch (Exception ex) //Captura qualquer exce��o que possa ocorrer no bloco try.
        {
            //Exibe uma mensagem de erro ao usu�rio com a descri��o da exce��o.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
