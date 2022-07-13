using Dapper;
using System.Data;

public class ChangeRepository : IChangeRepository
{
  private readonly IDbConnection dbConnection;

  public ChangeRepository(IDbConnection dbConnection)
  {
    this.dbConnection = dbConnection;
  }

  public IEnumerable<Change> GetAll()
  {
    const string sql = $"SELECT * FROM \"Changes\"";

    return dbConnection.Query<Change>(sql);
  }
}