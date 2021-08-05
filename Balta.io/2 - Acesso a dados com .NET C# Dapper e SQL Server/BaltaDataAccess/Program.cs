using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BaltaDataAccess.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
  class Program
  {
    static void Main(string[] args)
    {
      const string connectionString = @"Server=TITANIUM\SQLEXPRESS; Database=balta; User ID=sa; Password=htd@2020";

      using (var connection = new SqlConnection(connectionString))
      {
        // CreateCategory(connection);
        // UpdateCategory(connection);
        // CreateManyCategory(connection);
        // ListCategories(connection);
        // ExecuteProcedure(connection);
        // ExecuteReadProcedure(connection);
        // ExecuteScalar(connection);
        // ReadView(connection);
        // OneToOne(connection);
        // OneToMany(connection);
        // QueryMultiple(connection);
        // SelectIn(connection);
        // Like(connection);
        Transaction(connection);
      }
    }

    private static void ListCategories(SqlConnection connection)
    {
      var categories = connection.Query<Category>("SELECT * FROM Category");
      foreach (var item in categories)
      {
        Console.WriteLine($"{item.Id} - {item.Title}");
      }
    }

    private static void CreateCategory(SqlConnection connection)
    {
      var category = new Category
      {
        Id = Guid.NewGuid(),
        Title = "Amazon AWS",
        Featured = false,
        Url = "http://amazon.com",
        Summary = "",
        Order = 8,
        Description = ""
      };

      var insertSql = @"INSERT INTO [Category] 
        (Id, [Title], Featured, [url], Summary, [Order], [Description]) 
        VALUES (@Id, @Title, @Featured, @Url, @Summary, @Order, @Description)";

      var rows = connection.Execute(insertSql, new
      {
        category.Id,
        category.Title,
        category.Featured,
        category.Url,
        category.Summary,
        category.Order,
        category.Description
      });
      Console.WriteLine($"{rows} linhas inseridas");
    }

    private static void UpdateCategory(SqlConnection connection)
    {
      var updateSql = @"UPDATE [Category] SET Title = @Title WHERE Id = @Id";

      var rows = connection.Execute(updateSql, new
      {
        Id = "f6bb1b4b-aa98-49e8-912f-659aee8352f1",
        Title = "Amazon AWS - Updated",
      });
      Console.WriteLine($"{rows} linhas alteradas");
    }

    private static void CreateManyCategory(SqlConnection connection)
    {
      var category = new Category
      {
        Id = Guid.NewGuid(),
        Title = "Amazon AWS",
        Featured = false,
        Url = "http://amazon.com",
        Summary = "",
        Order = 8,
        Description = ""
      };

      var category2 = new Category
      {
        Id = Guid.NewGuid(),
        Title = "Amazon AWS2",
        Featured = false,
        Url = "http://amazon.com2",
        Summary = "",
        Order = 9,
        Description = ""
      };


      var insertSql = @"INSERT INTO [Category] 
        (Id, [Title], Featured, [url], Summary, [Order], [Description]) 
        VALUES (@Id, @Title, @Featured, @Url, @Summary, @Order, @Description)";

      var rows = connection.Execute(insertSql, new[]
      {
        new {
          category.Id,
          category.Title,
          category.Featured,
          category.Url,
          category.Summary,
          category.Order,
          category.Description
        },
        new {
          category2.Id,
          category2.Title,
          category2.Featured,
          category2.Url,
          category2.Summary,
          category2.Order,
          category2.Description
        }
      });
      Console.WriteLine($"{rows} linhas inseridas");
    }

    private static void ExecuteProcedure(SqlConnection connection)
    {
      var sql = "spDeleteStudent";
      var executeParamrs = new { StudentId = "C55390D4-71DD-4F3C-B978-D1582F51A327" };
      var rows = connection.Execute(
        sql,
        executeParamrs,
        commandType: CommandType.StoredProcedure
      );
      Console.WriteLine($"{rows} linhas afetadas");
    }

    private static void ExecuteReadProcedure(SqlConnection connection)
    {
      var sql = "[spGetCoursesByCategory]";
      var executeParamrs = new { CategoryId = "1BCAB9AA-D2C3-4A8A-864B-4DECA317C3BC" };
      var courses = connection.Query(
        sql,
        executeParamrs,
        commandType: CommandType.StoredProcedure
      );

      foreach (var item in courses)
      {
        Console.WriteLine(item.Id);
      }
    }

    private static void ExecuteScalar(SqlConnection connection)
    {
      var category = new Category
      {
        Title = "Amazon AWS Scalar",
        Featured = false,
        Url = "http://amazon.com",
        Summary = "",
        Order = 8,
        Description = ""
      };

      var insertSql = @"
      declare @id uniqueidentifier = NEWID(); 
      INSERT INTO 
        [Category] 
        (Id, [Title], Featured, [url], Summary, [Order], [Description]) 
        VALUES (@id, @Title, @Featured, @Url, @Summary, @Order, @Description);
      SELECT @id;
      ";
      //o Scalar além de executar um script, permite o retorno de alguma informação
      var id = connection.ExecuteScalar<Guid>(insertSql, new
      {
        category.Title,
        category.Featured,
        category.Url,
        category.Summary,
        category.Order,
        category.Description
      });
      Console.WriteLine($"A categoria inserida foi: {id}");
    }

    private static void ReadView(SqlConnection connection)
    {
      var sql = @"SELECT * FROM vwCourses";
      var courses = connection.Query<Category>(sql);
      foreach (var item in courses)
      {
        Console.WriteLine($"{item.Id} - {item.Title}");
      }
    }

    private static void OneToOne(SqlConnection connection)
    {
      var sql = @"
          select  
          * 
          from CareerItem ci
          inner join Course c on c.Id = ci.CourseId
      ";

      var items = connection.Query<CareerItem, Course, CareerItem>(sql, (CareerItem, course) =>
      {
        CareerItem.Course = course;
        return CareerItem;
      }, splitOn: "Id");
      foreach (var item in items)
      {
        Console.WriteLine($"{item.Title} - {item.Course.Title}");
      }
    }

    private static void OneToMany(SqlConnection connection)
    {
      var sql = @"
        select
          c.Id,
          c.Title,
          ci.CareerId,
          ci.Title 
        from CareerItem ci
        inner join Career c on c.Id = ci.CareerId
        order by c.Title
      ";

      var careers = new List<Career>();
      var items = connection.Query<Career, CareerItem, Career>(sql, (career, item) =>
     {
       var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
       if (car == null)
       {
         car = career;
         car.Items.Add(item);
         careers.Add(car);
       }
       else
       {
         car.Items.Add(item);
       }
       return career;
     }, splitOn: "CareerId");
      foreach (var career in careers)
      {
        Console.WriteLine($"Career: {career.Title}");
        foreach (var item in career.Items)
        {
          Console.WriteLine($"Career Item: {item.Title}");
        }
      }
    }

    private static void QueryMultiple(SqlConnection connection)
    {
      var query = "SELECT * FROM Category; SELECT * FROM Course;";
      using (var multi = connection.QueryMultiple(query))
      {
        var categories = multi.Read<Category>();
        var courses = multi.Read<Course>();

        Console.WriteLine("************ Categories:");
        foreach (var item in categories)
        {
          Console.WriteLine(item.Title);
        }

        Console.WriteLine("************ Courses:");
        foreach (var item in courses)
        {
          Console.WriteLine(item.Title);
        }
      }
    }

    private static void SelectIn(SqlConnection connection)
    {
      var query = @"select * from Career where Id IN (@id)";
      var careers = connection.Query<Career>(query, new
      {
        Id = new[]{
          "F247AF57-A273-4FCB-BB86-1677D5F282EE",
          "0E5749E1-4BF6-4545-B5B1-880142291C1C"
        }
      });
      foreach (var item in careers)
      {
        Console.WriteLine(item.Title);
      }
    }

    private static void Like(SqlConnection connection)
    {
      var query = @"select * from Course where Title like @exp";
      var courses = connection.Query<Course>(query, new
      {
        exp = "%j%"
      });
      foreach (var item in courses)
      {
        Console.WriteLine("********* Curso: " + item.Title);
      }
    }

    private static void Transaction(SqlConnection connection)
    {
      var category = new Category
      {
        Id = Guid.NewGuid(),
        Title = "Minha categoria não salvar",
        Featured = false,
        Url = "http://amazon.com",
        Summary = "",
        Order = 8,
        Description = ""
      };

      var insertSql = @"INSERT INTO [Category] 
        (Id, [Title], Featured, [url], Summary, [Order], [Description]) 
        VALUES (@Id, @Title, @Featured, @Url, @Summary, @Order, @Description)";

      connection.Open();
      using (var transaction = connection.BeginTransaction())
      {
        var rows = connection.Execute(insertSql, new
        {
          category.Id,
          category.Title,
          category.Featured,
          category.Url,
          category.Summary,
          category.Order,
          category.Description
        }, transaction);
        Console.WriteLine($"************* {rows} linhas inseridas");

        // transaction.Rollback();
        transaction.Commit();
      }
    }
  }
}