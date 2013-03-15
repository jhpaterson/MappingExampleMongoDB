using System.Collections.Generic;
using MongoDB;

namespace MappingExample
{
    public class Employee 
    {
        public Oid Id { get; private set; }       
        public string Name{get; set;} 
        public string PhoneNumber{get; set;} 
        public IList<Project> Projects { get; set; }
    }
}
