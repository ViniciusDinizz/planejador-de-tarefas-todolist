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
        public List<Person> _morePeople = new List<Person>();
        public DateTime create { get; set; }
        public DateOnly? _dueTime { get; set; }
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
        public void SetDueTime(int _year, int _month, int _day)
        {
            _dueTime = new DateOnly(_year, _month, _day);
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
        public void SetStatusno(string status)
        {
            if(status.ToUpper() == "S")
            {
                this._status = false;
            }
            else
            {
                this._status = true;
            }
        }
        public void SetStatusyes(string status)
        {
            if(status.ToUpper() == "S")
            {
                this._status = true;
            }
            else
            {
                this._status = false;
            }
        }

        public void RemovePersons(string id)
        {
            for(int i = 0; i < this._morePeople.Count; i++)
            {
                if (this._morePeople[i]._id == id)
                {
                    this._morePeople.RemoveAt(i);
                }
            }
        }
        public Person SetPerson (Person _newPerson)
        {
            this._morePeople.Add(_newPerson);
            return _newPerson;
        }
        public bool SetPersonExists(List<Person> added, string id)
        {
            bool flag = false;
            for(int i = 0; i < added.Count; i++)
            {
                if (added[i]._id == id)
                {
                    Person person = added[i];           
                    _morePeople.Add(person);
                    flag = true;
                }
            }
            return flag;
        }
    }
}
