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
        public string _category;
        public Person _ownerPerson { get; set; }
        public DateTime? _create { get; set; }
        public DateTime? _dueTime { get; set; }
        public bool _status;
        string _dateCreateCop = "", _dueTimeCop = "";

        public ToDoList(string Description, Person Owner)
        {
            _ownerPerson = Owner;
            var key = Guid.NewGuid();
            _id = key.ToString().Substring(0, 4);
            _description = Description;
            _create = DateTime.Now;
            _status = false;
        }

        public ToDoList(string Desciption, string Id, string DateTime, string DueTime, string status, Person loadPerson, string Category)
        {
            this._description = Desciption;
            this._id = Id;
            this._dateCreateCop = DateTime;
            this._dueTimeCop = DueTime;
            if (status == "Não finalizada.")
            {
                this._status = false;
            }
            else
            {
                this._status = true;
            }
            this._ownerPerson = loadPerson;
            this._category = Category;
        }

        public void SetDueTime(int year, int month, int day)
        {
            _dueTime = new DateTime(year, month, day);
        }
        public string ToFile()
        {
            return $"{this._description},{_id},{this._dateCreateCop},{this._dueTimeCop},{SetStatus()},{this._ownerPerson.ToPerson()},{_category}";
        }

        public string SetCategory()
        {
            int op;
            try
            {
                Console.Clear();
                Console.WriteLine("Informe a catégoria para a tarefa!\n[1] Importante\n[2] Pessoal\n[3] Profissional");
                int.TryParse(Console.ReadLine(), out op);
                switch (op)
                {
                    case 1:
                        return this._category = "Importante";
                    case 2:
                        return this._category = "Pessoal";
                    case 3:
                        return this._category = "Profissional";
                    default:
                        Console.WriteLine("Opção não cadastrada!");
                        break;
                }
            }
            catch
            {
            }
            return null;


        }

        public void GetStatus(string status)
        {
            if (status == "S")
            {
                this._status = true;
            }
            else
            {
                this._status = false;
            }
        }

        public string SetStatus()
        {
            if (_status == false)
            {
                return $"Não finalizada.";
            }
            else
            {
                return $"Finalizada";
            }
        }
        public override string ToString()
        {
            if (_create != null)
            {
                this._dateCreateCop = _create.ToString();
            }
            if (_dueTime != null)
            {
                this._dueTimeCop = _dueTime.ToString();
            }

            return $"Descrição: {this._description} | ID: {this._id} | Data/Inicio: {this._dateCreateCop} | Data/termino: {this._dueTimeCop} | Status:{SetStatus()} | Proprietário: {this._ownerPerson.ToPerson()} | Categoria: {_category} |";
        }

        public void SetOwner(Person added)
        {
            this._ownerPerson = added;
        }


    }
}
