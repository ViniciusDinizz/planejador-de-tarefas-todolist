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
        public string _id { get; set; }

        public Person(string n)
        {
            var key = Guid.NewGuid();
            _id = key.ToString().Substring(0, 4).ToUpper();
            this.Name = n;
        }

        public Person(string Name, string Id) 
        {
            this.Name = Name;
            this._id = Id;
        }
        public bool ExistsPeson(string id)
        {
            if (_id == id)
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
            return $"{this.Name},{_id}";
        }
        public override string ToString()
        {
            return $"Nome: {this.Name} | Id: {this._id}";
        }
    }
}
