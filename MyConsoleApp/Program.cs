using System;
using Microsoft.Data.Sqlite;

namespace MyConsoleApp
{
    class Program
    {
        /// <summary>
        /// Exemplo baseado no tutorial 
        /// disponível em https://developersoapbox.com/connecting-to-a-sqlite-database-using-net-core/
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var connStringBuilder = new SqliteConnectionStringBuilder();
            connStringBuilder.DataSource = "./MyConsoleApp.db";
            using (var connection = new SqliteConnection(connStringBuilder.ConnectionString))
            {
                connection.Open();

                var cmdDeletarTabela = connection.CreateCommand();
                cmdDeletarTabela.CommandText = "DROP TABLE IF EXISTS tarefas";
                cmdDeletarTabela.ExecuteNonQuery();

                var cmdCriarTabela = connection.CreateCommand();
                cmdCriarTabela.CommandText = "CREATE TABLE tarefas(nome VARCHAR(50))";
                cmdCriarTabela.ExecuteNonQuery();
                
                using (var transaction = connection.BeginTransaction())
                {
                    var cmdInserir = connection.CreateCommand();

                    cmdInserir.CommandText = "INSERT INTO tarefas VALUES('Estudar C#.Net')";
                    cmdInserir.ExecuteNonQuery();

                    cmdInserir.CommandText = "INSERT INTO tarefas VALUES('Estudar .Net Core')";
                    cmdInserir.ExecuteNonQuery();

                    cmdInserir.CommandText = "INSERT INTO tarefas VALUES('Estudar Python 3')";
                    cmdInserir.ExecuteNonQuery();

                    transaction.Commit();
                }

                
                var cmdSelect = connection.CreateCommand();
                cmdSelect.CommandText = "SELECT nome FROM tarefas";

                using (var reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }
                Console.ReadKey();

            }
        }
    }
}
