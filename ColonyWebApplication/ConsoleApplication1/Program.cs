using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.WebService1SoapClient s = new ServiceReference1.WebService1SoapClient();
            //string data = s.HelloWorld();
            string data = JsonConvert.DeserializeObject(s.HelloWorld()).ToString();
            Console.WriteLine(data);
        }
    }
}
