using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planejador_de_tarefas
{
    internal class Person
    {
        public string Name { get; set; }
        public string Id { get; set; }

        public Person(string n)
        {
            var key = Guid.NewGuid();
            Id = key.ToString().Substring(0, 4).ToUpper();
            this.Name = n;
        }

        public Person(string Name, string Id) 
        {
            this.Name = Name;
            this.Id = Id;
        }
        public bool ExistsPerson(string id)
        {
            if (Id == id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string ToPerson()
        {
            return $"{this.Name},{Id}";
        }
        public override string ToString()
        {
            return $"Nome: {this.Name} | Id: {this.Id}";
        }
    }
}
