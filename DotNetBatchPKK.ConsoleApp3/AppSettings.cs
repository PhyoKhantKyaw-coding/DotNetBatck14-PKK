using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp3;

internal class AppSettings
{
    public static SqlConnectionStringBuilder connectionBuilder { get; } = new SqlConnectionStringBuilder()
    {
        DataSource = ".\\SA",
        InitialCatalog = "Blog",
        UserID = "sa",
        Password = "sa@123",
        TrustServerCertificate = true,
    };
}
