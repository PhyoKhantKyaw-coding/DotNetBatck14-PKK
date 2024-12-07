using DotNetBatch14PKK.Kpay.Features.User;
using Microsoft.Data.SqlClient;
using ServiceStack;
using System.Data;

namespace DotNetBatch14PKK.Kpay.Features.KpayTransaction;
public class UserServices : IDapperUserService
{
    private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;

    public List<UserModel> GetUsers()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("select * from tblUser", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new();
        adapter.Fill(dt);
        connection.Close();

        var list = new List<UserModel>();
        foreach (DataRow drow in dt.Rows)
        {
            UserModel user = new()
            {
                UserId = drow["UserId"].ToString(),
                UserName = drow["UserName"].ToString(),
                MobileNo = drow["MobileNo"].ToString(),
                Balance = Convert.ToInt32(drow["Balance"]),
                Password = drow["Password"].ToString(),
            };
            list.Add(user);
        }
        return list;
    }
    public ResponseModel CreateUser(UserModel requestModel)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = @"INSERT INTO [dbo].[tblUser]
           ([UserName]
           ,[MobileNO]
           ,[Balance]
           ,[Password])
     VALUES
           (@UserName
           ,@MobileNo
           ,@Balance
           ,@Password);";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@UserName", requestModel.UserName);
        cmd.Parameters.AddWithValue("@MobileNo", requestModel.MobileNo);
        cmd.Parameters.AddWithValue("@Balance", requestModel.Balance);
        cmd.Parameters.AddWithValue("@Password", requestModel.Password);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Saving successful." : "Saving failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public UserModel GetUser(string mobile)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new("select * from tblUser where MobileNo = @mobile", connection);
        cmd.Parameters.AddWithValue("@mobile", mobile);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new();
        adapter.Fill(dt);
        connection.Close();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("no data found!");
            return null;
        }
        DataRow drow = dt.Rows[0];
        UserModel blog = new UserModel()
        {
            UserId = drow["UserId"].ToString(),
            UserName = drow["UserName"].ToString(),
            MobileNo = drow["MobileNo"].ToString(),
            Balance = Convert.ToInt32(drow["Balance"]),
            Password = drow["Password"].ToString(),
        };
        return blog;
    }

    public ResponseModel Deposit(string mobileNo, int depositAmount, string password)
    {
        var user = GetUser(mobileNo);
        if (user == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "User not found."
            };
        }

        if (user.Password != password)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "The password is not correct."
            };
        }

        user.Balance = (user.Balance ?? 0) + depositAmount; 
        using SqlConnection connection = new(_connectionString);
        connection.Open();
            string query = @"
                UPDATE [dbo].[tblUser]
                SET 
                    [Balance] = @Balance
                WHERE 
                    MobileNo = @MobileNo";

        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
        cmd.Parameters.AddWithValue("@Balance", user.Balance);

        int result = cmd.ExecuteNonQuery();

        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = result > 0 ? "Deposit successful." : "Deposit failed."
        };
    }

    public ResponseModel Withdraw(string mobileNo, int withdrawAmount, string password)
    {
        var user = GetUser(mobileNo);
        if (user == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "User not found."
            };
        }
        if (user.Password != password)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "The password is not correct."
            };
        }
        if ((user.Balance ?? 0) < withdrawAmount)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Insufficient balance."
            };
        }
        user.Balance = (user.Balance ?? 0) - withdrawAmount;
        using SqlConnection connection = new(_connectionString);
        connection.Open();
        string query = @"
            UPDATE [dbo].[tblUser]
            SET 
                [Balance] = @Balance
            WHERE 
                MobileNo = @MobileNo";

        using SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
        cmd.Parameters.AddWithValue("@Balance", user.Balance);
        int result = cmd.ExecuteNonQuery();
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = result > 0 ? "Withdrawal successful." : "Withdrawal failed."
        };
    }
}
