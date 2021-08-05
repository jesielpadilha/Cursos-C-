using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
  public class UserRepository : Repository<User>
  {
    private readonly SqlConnection _connection;

    public UserRepository(SqlConnection connection) : base(connection) => _connection = connection;

    public IList<User> GetWithRoles()
    {
      var query = @"
        select 
          u.*, r.* 
        from [User] u
          left join [UserRole] ur on u.Id = ur.UserId
          left join [Role] r on r.Id = ur.RoleId
      ";
      var users = new List<User>();

      var items = _connection.Query<User, Role, User>(query, (user, role) =>
      {
        var usr = users.FirstOrDefault(x => x.Id == user.Id);
        if (usr == null)
        {
          usr = user;
          if (role != null)
            usr.Roles.Add(role);
          users.Add(usr);
        }
        else
        {
          if (role != null)
            usr.Roles.Add(role);
        }
        return user;
      }, splitOn: "Id");

      return users;
    }

  }
}