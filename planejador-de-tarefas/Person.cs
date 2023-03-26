using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planejador_de_tarefas
{
    internal class Person
    {
        public string _name { get; set; }
        public string _id { get; set; }

        public Person(string name)
        {
            var key = Guid.NewGuid();
            _id = key.ToString().Substring(0, 4).ToUpper();
            _name = name;
        }

        void SetName(string Name)
        {
            this._name = Name;
        }

        public bool ExistsPeson(string id)
        {
            if(_id == id)
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
            return $"{this._name}";
        }
        public override string ToString()
        { 
            return $"Nome: {this._name} | Id: {this._id}";
        }
    }
}
