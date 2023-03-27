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
        public Category _category { get; set; }
        public Person _ownerPerson { get; set; }
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

        public void SetCategory(Category newcategory)
        {
            this._category = newcategory;
        }
        public string ToFile()
        {
            return "";
        }
        public void SetDueTime(int _year, int _month, int _day)
        {
            this._dueTime = new DateOnly(_year, _month, _day);
        }

        public void SetOwnerPerson(Person Owner)
        {
            this._ownerPerson = Owner;
        }
        public override string ToString()
        {
            string category = "";
            if(_category != null)
            {
                category = _category._nameCategory.ToUpper();
            }
            return $"Descrição: {this._description} | ID: {_id} | Data/Inicio: {this.create} | Data/Entrega: {this._dueTime} | Status:{this._status} | Proprietário: {this._ownerPerson.ToPerson()} | Categoria: {category}";
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

    }
}
