using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_Toshmatov.Classes.Database
{
    public class Config
    {
        public static readonly string connection = "server =;" +
            "uid = root;" +
            "pwd=root" +
            "database=TaskManager;";

        public static readonly MySqlServerVersion version = new MySqlServerVersion(new version(0, 0, 11));
    }
}
