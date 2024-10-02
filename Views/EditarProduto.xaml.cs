//Importa o namespace que contém os models do aplicativo.
using MauiAppMinhasCompras.Models;

//Define o namespace para a classe EditarProduto.
namespace MauiAppMinhasCompras.Views;

//Define a classe EditarProduto que herda de ContentPage, representando uma página do aplicativo.
public partial class EditarProduto : ContentPage
{
    //Construtor da classe EditarProduto.
    public EditarProduto() 
    {
        //Inicializa os componentes da interface definidos no arquivo XAML.
        InitializeComponent();
    }

    //É acionado quando um item (como um botão de salvar) é clicado.
    private async void ToolbarItem_Clicked(object sender, EventArgs e) 
    {
        try //Inicia um bloco try para capturar exceções que possam ocorrer.

            //Recupera o produto atual que está sendo editado através do BindingContext.
            Produto produto_anexado = BindingContext as Produto;

        //Cria um novo objeto Produto com as informações inseridas pelo usuário nos campos de texto (descrição, quantidade e preço).
        Produto p = new Produto
            {
                Id = produto_anexado.Id, //Mantém o ID do produto existente.
                Descricao = txt_descricao.Text, //Obtém a descrição do produto a partir de uma entrada de texto na interface.
                Quantidade = Convert.ToDouble(txt_quantidade.Text), //Converte o texto da quantidade para um número double.
                Preco = Convert.ToDouble(txt_preco.Text) //Converte o texto do preço para um número double.
            };

            //Chama o método Update do banco de dados para atualizar o registro do produto.
            await App.Db.Update(p);
            //Exibe uma mensagem de sucesso ao usuário.
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            //Navega de volta para a página anterior.
            await Navigation.PopAsync();
        }
        catch (Exception ex) //Captura qualquer exceção que possa ocorrer no bloco try.
        {
            //Exibe uma mensagem de erro ao usuário com a descrição da exceção.
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
