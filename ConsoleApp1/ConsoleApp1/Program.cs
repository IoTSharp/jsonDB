using Jint;
using Jint.Native.Json;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var _engine = new Engine(options =>
            {

                // Limit memory allocations to MB
                options.LimitMemory(4_000_000);

                // Set a timeout to 4 seconds.
                options.TimeoutInterval(TimeSpan.FromSeconds(6));

                // Set limit of 1000 executed statements.
                // options.MaxStatements(1000);
                // Use a cancellation token.
            });
            var _parser = new JsonParser(_engine);
            //创建一张user数据表，并定义一个DB的jsonDB别名
            var data = "[{\"username\":\"张三\",\"sex\":\"男\",\"birthday\":{\"year\":2000,\"month\":6,\"day\":18}},{\"username\":\"李红\",\"sex\":\"女\",\"birthday\":{\"year\":1986,\"month\":9,\"day\":22}}]";
            _engine.SetValue("data", data);
            _engine.Execute(Properties.Resources.jsonDB);
            _engine.Execute("var jDB = jsonDB(JSON.parse(data) , 'user').init('jDB');");
            var sql= "select sex from user where (username=\"李红\")";
            _engine.Execute($"var result = jDB.query('{sql}');");
            var result=  _engine.GetValue("result");
            var objresult = result.ToObject();



        }
    }
}
