using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClient.Model;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TestDBContext())
            {
                var students = context.Students.ToArray();

                foreach (var student in students)
                {
                    Console.WriteLine("{0} : {1}", student.Id, student.Name);
                }
            }
            Console.Read();
        }
    }
}
