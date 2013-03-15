using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MongoDB;
using MongoDB.Linq;

namespace MappingExample
{
    class Program
    {

        static void Main(string[] args)
        {
            //Create a default mongo object.  This handles our connections to the database.
            //By default, this will connect to localhost, port 27017 
            //Need to have started server by running mongod
            var mongo = new Mongo();
            mongo.Connect();

            //Get the database  
            var db = mongo.GetDatabase("company");

            //Get the Employee collection.  
            var collection = db.GetCollection<Employee>();

            //this deletes everything out of the collection so we can run this over and over again.
            //collection.Delete(p => true);

            //Create employees to enter into the database.
            CreateEmployees(collection);

            //count all the employees
            var totalNumberOfEmployees = collection.Count();

            //count employees with exactly one project
            var query1 = collection.Count(e => e.Projects.Count == 1);

            // get all employees
            var query2 = from e in collection.Linq()
                         select e;

            IList<Employee> emps = query2.ToList<Employee>();

//          //find names of employees on a specific project
            var query3 = from e in collection.Linq()
                         where e.Projects.Any(p => p.ProjectName == "Secret Project")
                         select e.Name;

            foreach (var name in query3)
            {
                Console.WriteLine(name);
            }

            Console.ReadLine();
        }

        private static void CreateEmployees(IMongoCollection<Employee> collection)
        {
            var webshop = new Project() { ProjectCode = "P1", ProjectName = "Online Store" };
            var secret = new Project() { ProjectCode = "P2", ProjectName = "Secret Project" };
            var finance = new Project() { ProjectCode = "P3", ProjectName = "Finance System" };

            var emp = new Employee()
            {
                Name = "Fernando",
                PhoneNumber = "1234",
                Projects = new List<Project>
                {
                    { webshop },
                    { secret }
                }
            };
  
            collection.Save(emp);

            emp = new SalariedEmployee()
            {
                Name = "Felipe",
                PhoneNumber = "5678",
                PayGrade = 7,
                Projects = new List<Project>
                {
                    { secret },
                    { finance }
                }
            };

            collection.Save(emp);

            emp = new Employee()
            {
                Name = "Nico",
                PhoneNumber = "9876",
                Projects = new List<Project>
                {
                    { webshop },
                }
            };
 
            collection.Save(emp);
        }

    }
}
