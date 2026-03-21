using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;

namespace TaskManager_Toshmatov.Classes.Database
{
    public class Config
    {
        public static string ConnectionConfig = "server=127.0.0.1;uid=root;pwd=;database=TaskManager;";
        public static ServerVersion Version = ServerVersion.AutoDetect(ConnectionConfig);
    }
}