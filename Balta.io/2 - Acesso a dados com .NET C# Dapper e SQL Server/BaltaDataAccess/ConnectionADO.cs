using System;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
  public class ConnectonADO
  {
    public void ConnectionWithAdo(string connectionString)
    {
      using (var connection = new SqlConnection(connectionString))
      {
        connection.Open();
        Console.WriteLine("\n************ Contectado");
        using (var command = new SqlCommand())
        {
          command.Connection = connection;
          command.CommandType = System.Data.CommandType.Text;
          command.CommandText = "SELECT Id, Title FROM Category";

          var reader = command.ExecuteReader();
          while (reader.Read())
          {
            Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
          }
        }
      }
    }
  }
}