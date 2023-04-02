using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace planejador_de_tarefas
{
    internal class ToDoList
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Category;
        public Person OwnerPerson { get; set; }
        public DateTime? Create { get; set; }
        public DateTime? DueTime { get; set; }
        public bool Status;
        string dateCreate = "", dueTime = "";

        public ToDoList(string description, Person owner)
        {
            OwnerPerson = owner;
            var key = Guid.NewGuid();
            Id = key.ToString().Substring(0, 4);
            Description = description;
            Create = DateTime.Now;
            Status = false;
        }

        public ToDoList(string desciption, string id, string dateTime, string dueTime, string status, Person loadPerson, string category)
        {
            this.Description = desciption;
            this.Id = id;
            this.dateCreate = dateTime;
            this.dueTime = dueTime;
            if (status == "Não finalizada.")
            {
                this.Status = false;
            }
            else
            {
                this.Status = true;
            }
            this.OwnerPerson = loadPerson;
            this.Category = Category;
        }

        public void SetDueTime(int year, int month, int day)
        {
            DueTime = new DateTime(year, month, day);
        }
        public string ToFile()
        {
            return $"{this.Description},{Id},{this.dateCreate},{this.DueTime},{SetStatus()},{this.OwnerPerson.ToPerson()},{Category}";
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
                        return this.Category = "Importante";
                    case 2:
                        return this.Category = "Pessoal";
                    case 3:
                        return this.Category = "Profissional";
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
                this.Status = true;
            }
            else
            {
                this.Status = false;
            }
        }

        public string SetStatus()
        {
            if (this.Status == false)
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
            if (Create != null)
            {
                this.dateCreate = Create.ToString();
            }
            if (DueTime != null)
            {
                this.dueTime = DueTime.ToString();
            }

            return $"Descrição: {this.Description} | ID: {this.Id} | Data/Inicio: {this.dateCreate} | Data/termino: {this.dueTime} | Status:{SetStatus()} | Proprietário: {this.OwnerPerson.ToPerson()} | Categoria: {Category} |";
        }

        public void SetOwner(Person added)
        {
            this.OwnerPerson = added;
        }


    }
}
