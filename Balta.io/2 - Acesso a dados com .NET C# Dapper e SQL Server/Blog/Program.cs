using System;
using System.Collections.Generic;
using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

namespace Blog
{
  public class Program
  {
    private const string CONNECTION_STRING = @"Server=TITANIUM\SQLEXPRESS; Database=Blog; User ID=sa; Password=htd@2020";

    private static void Main(string[] args)
    {
      var connection = new SqlConnection(CONNECTION_STRING);
      var userRepository = new UserRepository(connection);
      var roleRepository = new Repository<Role>(connection);
      var tagRepository = new Repository<Tag>(connection);
      connection.Open();

      var users = ((List<User>)userRepository.GetWithRoles());
      // ((List<User>)userRepository.Get()).ForEach(x => Console.WriteLine(x.Name));
      // ((List<Role>)roleRepository.Get()).ForEach(x => Console.WriteLine(x.Name));
      // ((List<Tag>)tagRepository.Get()).ForEach(x => Console.WriteLine(x.Name));

      foreach (var user in users)
      {
        Console.WriteLine("User: " + user.Name);
        ((List<Role>)user.Roles).ForEach(x => Console.WriteLine("Role: " + x.Name));
      }

      connection.Close();
    }
  }
}
