using Microsoft.Extensions.Logging; //Importa o namespace necessário para usar o sistema de logging.

namespace MauiAppMinhasCompras
{
    //Classe estática que contém o método principal para a configuração do aplicativo .NET MAUI.
    public static class MauiProgram
    {
        //Método responsável por criar e configurar a aplicação MauiApp.
        public static MauiApp CreateMauiApp()
        {
            //Cria um construtor para a aplicação MAUI.
            var builder = MauiApp.CreateBuilder();

            //Configura a aplicação MAUI.
            builder
                .UseMauiApp<App>() //Define a classe App como a aplicação principal.
                .ConfigureFonts(fonts => //Configura as fontes do aplicativo.
                {
                    //Adiciona fontes personalizadas ao aplicativo.
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); // Fonte regular.
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold"); // Fonte semibold.
                });

            //Se o aplicativo estiver em modo de depuração, adiciona logging de debug.
#if DEBUG
    		builder.Logging.AddDebug(); // Permite visualizar logs de debug durante o desenvolvimento.
#endif

            //Constrói e retorna a aplicação configurada.
            return builder.Build();
        }
    }
}
