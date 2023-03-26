using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planejador_de_tarefas
{
    internal class ToDoList
    {
        public string _id { get; set; }
        public string _description { get; set; }
        public Category? _category { get; set; }
        public Person _ownerPerson { get; set; }
        public List<Person>? _morePeople { get; set; }
        public DateTime create { get; set; }
        public DateTime? _dueTime { get; set; }
        public bool _status { get; set; }

        public ToDoList(string Description, Person Owner)
        {
            _ownerPerson = Owner;
            var key = Guid.NewGuid();
            _id = key.ToString().Substring(0, 4);
            _description = Description;
            create = DateTime.UtcNow;
            _status = false;
        }


        public string ToFile()
        {
            return "";
        }

        public override string ToString()
        {
            string personss = "", category = "";
            if (_morePeople != null )
            {
                for (int i = 0; i < _morePeople.Count; i++)
                {
                    personss += _morePeople[i].ToPerson() + ",";
                }
            }else if(_category != null)
            {
                category = this._category.ToCategory();
            }
            return $"Descrição: {this._description} | ID: {_id} | Data/Inicio: {this.create} | Status:{this._status} | Proprietário: {this._ownerPerson.ToPerson()} | Categoria: {category} | Contribuidores: {personss}";
        }
        void SetStatus(string status)
        {
            if(status.ToUpper() == "S")
            {
                _status = true;
            }
            else
            {
                _status = false;
            }
        }

        public void SetPerson(Person added)
        {
            this._morePeople.Add(added);
        }
    }
}
