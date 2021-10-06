using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Reflection;

namespace _GP_Job.Core
{
    static class SQL
    {
        static SqlConnection connection = new SqlConnection("Data Source=MARK;Initial Catalog=GamePort;User ID=sa;Password=1221628;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public static void initializeConnection()
        {
            connection.ConnectionString = ResourceManager.manager.GetPrivateString("SQL", "connectionstring");
        }
        public static async Task InsertPost(Models.Article article, int sourceID)
        {
            await Task.Run(async () =>
            {
                object locker = new object();//заглушка синхронизации потоков
                SqlCommand insertCmd = new SqlCommand("sp_AddNewPost", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Parameters =
                    {
                        new SqlParameter()
                        {
                            ParameterName = "@title",
                            Value = article.Title
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@shorttext",
                            Value = article.ShortText,
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@fulltext",
                            Value = article.FullText
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@image",
                            Value = article.PreviewImageLink,
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@link",
                            Value = article.Link,
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@source",
                            Value = sourceID,
                        },
                        new SqlParameter()
                        {
                            ParameterName = "@date",
                            Value = DateTime.Now,
                        }
                    }
                };
                try
                {
                    lock (locker)
                    {
                        if (connection.State.ToString().ToLower() == "closed") { connection.Open(); };
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{DateTime.Now} - Записано строк:{insertCmd.ExecuteNonQuery()}, Статья: {article}\n");
                        Console.ForegroundColor = default;
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (connection.State.ToString().ToLower() != "closed") { connection.Close(); };
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[{DateTime.Now}] {ex.Message}\n");
                    Console.ForegroundColor = default;
                }
            });
        }
    }
}
