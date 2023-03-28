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
        public List<Person>? _morePeople { get; set; }
        public DateTime create { get; set; }
        public DateTime? _dueTime { get; set; }
        public bool _status;

        public ToDoList(string Description, Person Owner)
        {
            _ownerPerson = Owner;
            var key = Guid.NewGuid();
            _id = key.ToString().Substring(0, 4);
            _description = Description;
            create = DateTime.Now;
            _status = false;
        }


        public string ToFile()
        {
            return $"{this._description} ,{_id} ,{this.create}, {SetStatus()}, {this._ownerPerson.ToPerson()}, {_category}";
        }

        public string SetCategory()
        {
            int op;
            try
            {
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

            return $"Descrição: {this._description} | ID: {_id} | Data/Inicio: {this.create} | Status:{SetStatus()} | Proprietário: {this._ownerPerson.ToPerson()} | Categoria: {_category} |";
        }

        public void SetPerson(Person added)
        {
            this._morePeople.Add(added);
        }
    }
}
