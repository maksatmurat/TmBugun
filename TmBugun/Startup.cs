using System;
using System.Threading.Tasks;
using CKSource.CKFinder.Connector.Config;
using CKSource.CKFinder.Connector.Core.Builders;
using CKSource.CKFinder.Connector.Host.Owin;
using CKSource.FileSystem.Local;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TmBugun.Startup))]

namespace TmBugun
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //регистрируем файловую систему для коннектора
            FileSystemFactory.RegisterFileSystem<LocalStorage>();

            //объявляем маршрут в приложении и сопоставляем его с коннектором
            //клиентская JS-библиотека CKFinder ожидает увидеть коннектор именно по этому маршруту
            app.Map("/ckfinder/connector", SetupConnector);
        } 
            
            //объявляем метод по настройке и инициализации коннектора
            private static void SetupConnector(IAppBuilder app)
        {
            //создаем экземпляры необходимых классов
            var connectorFactory = new OwinConnectorFactory();
            var connectorBuilder = new ConnectorBuilder();
            var customAuthenticator = new CKFinderAuthenticator();

            connectorBuilder
                .LoadConfig() //подгружаем конфигурацию из файла Web.config
                .SetAuthenticator(customAuthenticator) //устанавливаем ранее определенный аутентификатор
                .SetRequestConfiguration((request, config) => { config.LoadConfig(); }); //определяем конфигурацию для каждого отдельного запроса

            //создаем экземпляр коннектора
            var connector = connectorBuilder.Build(connectorFactory);

            //добавляем коннектор в pipeline
            app.UseConnector(connector);
        }
    }
}
