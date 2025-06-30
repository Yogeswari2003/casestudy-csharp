using Microsoft.Data.SqlClient;
using System;
class program
{
    static void Main(string[] args)
    {
        
    }
}

public class DbConnection
{
    private string connectionString = @"Data Source=YOGIRAJ\SQLEXPRESS;Initial Catalog=pms;Integrated Security=True;Trust Server Certificate=True";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}

public class employee
{
    public int empid { get; set; }
    public string name { get; set; }
    public string designation { get; set; }
    public string gender { get; set; }
    public decimal salary { get; set; }
    public int projectid { get; set; }
}

public class project
{
    public int projectid { get; set; }
    public string projectname { get; set; }
    public string description { get; set; }
    public DateTime startdate { get; set; }
    public string status { get; set; }
}

public class task
{
    public int taskid { get; set; }
    public string taskname { get; set; }
    public int projectid { get; set; }
    public int empid { get; set; }
    public string status { get; set; }
}

