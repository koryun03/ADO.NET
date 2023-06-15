using ConsoleApp56;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

MyClass ee = new MyClass();
try
{
    // var a = await ee.Create();
    //Console.WriteLine(a);


    // User u = new User(3,"eer",325);
    //bool v= await ee.InsertData(u);
    // Console.WriteLine(v);

    //var t = await ee.GetAll();
    //t.ForEach(x => Console.WriteLine($"{x.Id} {x.Name} {x.Age}")); 


    //var e = await ee.GetAll();
    //foreach (var item in e)
    //{
    //    Console.WriteLine($"{item.Id} {item.Name} {item.Age}");
    //}

    //var e = await ee.GetTop();
    //foreach (var item in e)
    //{
    //    Console.WriteLine($"{item.Id} {item.Name} {item.Age}");

    //}

    //var ll = await ee.Select();
    //foreach (var item in ll)
    //{
    //    Console.WriteLine(item.Id + item.Name + item.Age);
    //}

    //var z = await ee.Selectt();
    //foreach (var item in z)
    //{
    //    Console.WriteLine(item.Id + item.Name + item.Age);
    //}

    int? min = await ee.Min();
    Console.WriteLine(min);
    Console.WriteLine("===============");

    var a = await ee.returnuserminage();
    foreach (var item in a)
    {
        Console.WriteLine(item.Id + item.Name + item.Age);
    }


    Console.WriteLine("===========");

    var z = await ee.GetMin();
    Console.WriteLine(z);

    Console.WriteLine("===========");

    ee.Testt();
    Console.WriteLine("========");

    ee.Test2();
    Console.WriteLine("=============");

    Dictionary<int, string> abranq = new Dictionary<int, string>();
    abranq.Add(3, "ereq");
    abranq.Add(9, "iny");
    abranq.Add(2, "erku");

    string hi = abranq[3];
    Console.WriteLine(hi);

    Console.WriteLine("==================");
    await ee.Hoo();



}
catch (Exception ex)
{
    Console.WriteLine("sxal");
    Console.WriteLine(ex);
}



class MyClass
{
    string connectionString = "Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;";
    string connectionString1 = "Server=(localdb)\\mssqllocaldb;Database=retdeg;Trusted_Connection=True;";
    
    public async Task<bool> CreateDbAsync(string dbname)
    {
        bool result = false;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();

            SqlCommand command = new SqlCommand();

            command.CommandText = $"CREATE DATABASE {dbname}";

            command.Connection = connection;

            await command.ExecuteNonQueryAsync();
            result = true;
        }
        return result;
    }


    public async Task<bool> Create()
    {
        bool r = false;
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {

            await connection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = $"create table Users(id int,name nvarchar(20),age int)";
            //sqlCommand.CommandText = $"create table Users({user.Id}, {user.Name},{user.Age})";
            sqlCommand.Connection = connection;
            await sqlCommand.ExecuteNonQueryAsync();
            r = true;
        }
        return r;
    }

    public async Task<bool> InsertData(User u)
    {
        bool t = false;
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand();
            command.CommandText = $"insert into Users values('{u.Id}','{u.Name}','{u.Age}')";
            command.Connection = connection;
            await command.ExecuteNonQueryAsync();
            t = true;
        }
        return t;
    }


    public async Task<IEnumerable<User>> GetTop()
    {
        List<User> l = new List<User>();
        string sqlexpression = "select * from users where id =1 or id= 3 ";
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(sqlexpression, connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                Console.WriteLine($"{reader.GetName(0)}\t{reader.GetName(0)}\t{reader.GetName(0)}");

                while (await reader.ReadAsync())
                {
                    User u = new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    l.Add(u);

                }

            }
            await reader.CloseAsync();
        }
        return l;
    }


    public async Task<IEnumerable<User>> Selectt()
    {
        string expression = "select * from users";
        List<User> uu = new List<User>();
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(expression, connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    User u = new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    uu.Add(u);
                }
            }

            await reader.CloseAsync();
        }

        return uu;
    }

    public async Task<int?> Min()
    {
        int? min = null;

        string expression = "select min(age) from users";
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(expression, connection);
            min = (int)await command.ExecuteScalarAsync();

            await connection.CloseAsync(); //esem avalcre chgitem petq te che
        }

        return min;

    }

    public async Task<IEnumerable<User>> returnuserminage() //tpuma en toxy vori meji ge-n amanapoqrna
    {
        MyClass ee = new MyClass();
        List<User> uu = new List<User>();
        string expression = $"select * from users where age={await ee.Min()}";
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(expression, connection);
            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    User u = new User(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    uu.Add(u);
                }
            }
            await reader.CloseAsync();//esem avalcre chgitem petq te che
        }
        return uu;
    }

    public async Task<int?> GetMin()
    {
        int? min = null;
        string expression = "select min(age) from users";

        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(expression, connection);

            // min = await command.ExecuteNonQueryAsync();
            min = (int)await command.ExecuteScalarAsync();


            //SqlDataReader reader = await command.ExecuteReaderAsync();
            await connection.CloseAsync();
        }

        return min;

    }

    public void Testt()
    {

        int age = 23;
        string name = "anun";
        string sqlExpression = "INSERT INTO Users (Name, Age) VALUES (@name, @age)";
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            // создаем параметр для имени
            SqlParameter nameParam = new SqlParameter("@name", name);
            // добавляем параметр к команде
            command.Parameters.Add(nameParam);
            // создаем параметр для возраста
            SqlParameter ageParam = new SqlParameter("@age", age);
            // добавляем параметр к команде
            command.Parameters.Add(ageParam);


            int number = command.ExecuteNonQuery();
            Console.WriteLine("Добавлено объектов: {0}", number); // 1
        }

    }

    public void Test2() //ogtagorcel sqlparametry
    {
        int a = 4;
        int b = 5;
        string expression = "declare @x int,@y int set @x=@a; set @y=@b; begin select @x+@y end";
        using (SqlConnection connection = new SqlConnection(connectionString1))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(expression, connection);

            SqlParameter sqlParameteraa = new SqlParameter("@a", a);
            command.Parameters.Add(sqlParameteraa);
            SqlParameter sqlParameterbb = new SqlParameter("@b", b);
            command.Parameters.Add(sqlParameterbb);


            //   var num= command.ExecuteNonQuery();
            var num = command.ExecuteScalar();
            Console.WriteLine(num);


        }

    }

    public async Task Hoo()
    {

        string expression = "create database tu";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            SqlCommand command = new SqlCommand(expression, connection);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();


        }


    }


}