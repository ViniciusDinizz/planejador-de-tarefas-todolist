using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using planejador_de_tarefas;

internal class Program
{
    private static void Main(string[] args)
    {
        int optionMenu = 0;
        string dataFile = "task.txt";
        List<ToDoList> Task = new List<ToDoList>();
        List<Person> RegisterPerson = new List<Person>();

        ReadFile(dataFile, RegisterPerson, Task);


        do
        {
            Console.Clear();
            optionMenu = Menu();
            switch (optionMenu)
            {
                case 1:
                    CreateTask(Task, RegisterPerson);
                    break;
                case 2:
                    ViewActiveTasks(Task, RegisterPerson, dataFile);
                    break;
                case 3:
                    ViewCompletedTasks(Task, dataFile);
                    break;
                case 4:
                    SaveTaskToFile(Task, dataFile);
                    break;
                default:
                    Console.WriteLine("Opção inválida mané.");
                    break;
            }
        } while (optionMenu != 4);
    }

    private static void ReadFile(string dataFile, List<Person> RegisterPerson, List<ToDoList> Tasks)
    {
        if(!File.Exists(dataFile)) 
        {
            StreamWriter sw = new StreamWriter(dataFile);
            sw.Close();
            Console.Write("Criando lista...");
            Thread.Sleep(1000);
            return;
        }
        else
        {
            StreamReader sr = new StreamReader(dataFile);
            while(!sr.EndOfStream)
            {
                string[] _position = sr.ReadLine().Split(",");
                Person _newPerson = new Person(_position[5], _position[6]);
                ToDoList newTask = new ToDoList(_position[0], _position[1], _position[2], _position[3], _position[4], _newPerson ,_position[7]);
                Tasks.Add(newTask);
                RegisterPerson.Add(_newPerson);
            }
            sr.Close();
        }
    }

    private static void SaveTaskToFile(List<ToDoList> Tasks, string dataFile)
    {
        try
        {
            StreamWriter sw = new(dataFile);
            if (File.Exists(dataFile))
            {
                foreach (var i in Tasks)
                {
                    sw.WriteLine(i.ToFile());
                }
                sw.Close();
            }
            else
            {
                foreach (var i in Tasks)
                {
                    sw.WriteLine(i.ToFile());
                }
                sw.Close();
            }

        }
        catch
        {

        }
    }


    //Cria e aloca um objeto do tipo TodoList
    private static void CreateTask(List<ToDoList> Tasks, List<Person> Person)
    {
        Console.Clear();
        Console.Write("### Cadastrando Tarefa ###\n\nQual a descricao da tarefa: ");
        var description = Console.ReadLine().ToUpper();
        Console.Write("\nQual o proprietário - ");
        int people = ChosingOwner(Person);

        if (Person.Count == 0)
        {
            Console.Write("\nNão é possivel concluir a tarefa sem proprietário...");
            Thread.Sleep(2000);
            Console.Clear();
        }
        else
        {
            ToDoList todo = new(description, Person[people]);
            todo.SetCategory();
            Tasks.Add(todo);
            Console.Write("\nCriando tarefa...");
            Thread.Sleep(1000);
        }
    }

    //Aloca um objeto da classe Person
    public static int RegistPerson(List<Person> Person)
    {
        int _indice = 0;
        Console.Clear();
        Console.Write("Qual nome da pessoa: ");
        var personregist = Console.ReadLine().ToUpper();
        Person newPerson = new Person(personregist);
        Person.Add(newPerson);
        for (int i = 0; i < Person.Count; i++)
        {
            if (Person[i].Id == newPerson.Id)
            {
                _indice = i;
            }
        }
        return _indice;
    }

    //Cria o objeto do tipo Person caso não tenha, e seta o proprietário da tarefa. Utilizada na função "CreateTask"
    public static int ChosingOwner(List<Person> Person)
    {
        int index = 0;
        if (Person.Count == 0)
        {
            Console.WriteLine("Não tem pessoas cadastradas.");
            Console.Write($"Deseja cadastrar? [S]IM | [N]AO: ");
            var registersucess = Console.ReadLine().ToUpper();
            if (registersucess == "S")
            {
                return RegistPerson(Person);
            }
            else
            {
                return -1;
            }
        }
        else
        {
            Console.WriteLine("\n\n# Pessoas #\n");
            for (int i = 0; i < Person.Count; i++)
            {
                Console.WriteLine(Person[i].ToString());
            }
            Console.Write("\n\n[1]- Id existente || [2]- Nova pessoa: ");
            var _setnewPerson = Console.ReadLine().ToUpper();
            switch (_setnewPerson)
            {
                case "1":
                    int _count = 0;
                    Console.Write("\nID: ");
                    var _setId = Console.ReadLine().ToUpper();
                    for (int i = 0; i < Person.Count; i++)
                    {
                        if (Person[i].Id == _setId)
                        {
                            return i;
                        }
                    }
                    if (_count == 0)
                    {
                        Console.Write("Id inválido...");
                        Thread.Sleep(1000);
                    }
                    break;
                case "2":
                    return RegistPerson(Person);
                    break;
                default:
                    Console.Write("\nOpção inválida");
                    Thread.Sleep(1000);
                    break;
            }
        }
        return index;
    }

    //Menu de opções exibido na função Main
    public static int Menu()
    {
        int op = 0;
        try
        {
            Console.WriteLine("Menu\n\n[1]- Criar tarefa\n[2]- Vizualizar Tarefas ativas\n[3]- Vizualizar Tarefas concluídas\n[4]- Sair");
            int.TryParse(Console.ReadLine(), out op);

        }
        catch
        {

        }
        return op;
    }

    //Percorrendo as tarefas não concluídas, e pode fazer alterações
    private static void ViewCompletedTasks(List<ToDoList> Tasks, string dataFile)
    {
        int _count = 0;
        for (int i = 0; i < Tasks.Count; i++)
        {
            if (Tasks[i].Status != false)
            {
                Tasks[i] = SubMenuStatus(Tasks[i]);
                _count++;
                SaveTaskToFile(Tasks, dataFile);
            }
        }
        if (_count == 0)
        {
            Console.Write("Sem tarefas concluídas...");
            Thread.Sleep(1000);
        }
    }

    //Percorrendo as tarefas não concluídas, e pode fazer alterações
    public static void ViewActiveTasks(List<ToDoList> Tasks, List<Person> Persons, string dataFile)
    {
        int _count = 0;
        for (int i = 0; i < Tasks.Count; i++)
        {
            if (Tasks[i].Status == false)
            {
                Tasks[i] = SubMenu(Tasks[i], Persons);
                _count++;
                SaveTaskToFile(Tasks, dataFile);
            }
        }
        if (_count == 0)
        {
            Console.Write("Sem tarefas pendentes...");
            Thread.Sleep(1000);
        }

    }

    //Submenu utilizado para alterações ma função "ViewActiveTasks"
    public static ToDoList SubMenu(ToDoList End, List<Person> Persons)
    {
        bool altering = false;
        string option = "";
        while (!altering)
        {
            Console.Clear();
            Console.WriteLine(End.ToString() + "\n");
            Console.Write("Deseja editar a tarefa: [S]im || [N]ão: ");
            var condition = Console.ReadLine().ToUpper();
            if (condition == "S")
            {
                while (option != "5")
                {
                    Console.Clear();
                    Console.WriteLine(End.ToString());
                    Console.WriteLine("\n[1]- Alterar Status\n[2]- Adicionar categoria\n[3]- Prazo de entrega\n[4]- Trocar Proprietário\n[5]- Sair ");
                    option = Console.ReadLine().ToUpper();
                    switch (option)
                    {
                        case "1":
                            Console.Write("Concluída Tarefa?[S]im || [N]ão: ");
                            var status = Console.ReadLine().ToUpper();
                            End.GetStatus(status);
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine(End.ToString());
                            Console.Write("\nAdicionar ou Trocar Categoria? [S]im || [N]ão: ");
                            var _setCategory = Console.ReadLine().ToUpper();
                            if (_setCategory == "S")
                            {
                                End.SetCategory();
                            }
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine(End.ToString() + "\n");
                            Console.WriteLine("### Definir data de entrega ###\n");
                            Console.Write("Dia: ");
                            int _day = int.Parse(Console.ReadLine());
                            Console.Write("Mês: ");
                            int _month = int.Parse(Console.ReadLine());
                            Console.Write("Ano: ");
                            int _year = int.Parse(Console.ReadLine());
                            End.SetDueTime(_year, _month, _day);
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("### TROCAR PROPRIETÁRIO ###\n");
                            Console.WriteLine(End.ToString() + "\n\n" + $"Proprietário atual: {End.OwnerPerson}");
                            Console.Write("\n\nAdicionar novo proprietário para tarefa? [S]im || [N]ão: ");
                            var positioncolaboratores = Console.ReadLine().ToUpper();
                            End = EditColaboratores(End, positioncolaboratores, Persons);
                            break;
                        case "5":
                            Console.Write("\n\nSaindo da edição...");
                            Thread.Sleep(1000);
                            break;
                        default:
                            Console.Write("Valor incorreto. Tente novamente...");
                            Thread.Sleep(2000);
                            break;
                    }
                }
            }
            else
            {
                altering = true;
            }
        }
        return End;
    }
    private static ToDoList EditColaboratores(ToDoList End, string option, List<Person> Persons)
    {
        if (option == "S")
        {
            Console.WriteLine("\n# Colaboradores #\n");
            for (int i = 0; i < Persons.Count; i++)
            {
                Console.WriteLine(Persons[i].ToString());
            }

            Console.Write("\n\n[1]- Inserir Id existente || [2]- Inserir nova pessoa : ");
            var _setPosition = Console.ReadLine().ToUpper();
            if (_setPosition == "1")
            {
                int _cont = 0;
                Console.Write("\nId: ");
                var _SetId = Console.ReadLine().ToUpper();
                for (int i = 0; i < Persons.Count; i++)
                {
                    if (Persons[i].Id == _SetId)
                    {
                        End.SetOwner(Persons[i]);
                        _cont++;
                    }
                }
                if (_cont == 0)
                {
                    Console.WriteLine("Id inválido;");
                    Thread.Sleep(1000);
                }
            }
            if (_setPosition == "2")
            {
                Console.Write("Nome da pessoa: ");
                var _setNewPerson = Console.ReadLine().ToUpper();
                Person newPerson = new Person(_setNewPerson);
                Persons.Add(newPerson);
                End.SetOwner(newPerson);
            }
        }
        return End;
    }

    public static ToDoList SubMenuStatus(ToDoList End)
    {
        Console.Clear();
        Console.WriteLine(End.ToString() + "\n");
        Console.Write("Deseja editar a tarefa: [S]im || [N]ão: ");
        var condition = Console.ReadLine().ToUpper();
        if (condition == "S")
        {
            Console.Clear();
            Console.WriteLine(End.ToString() + "\n");
            Console.Write("\n[1]- Alterar Status\n[2]- Sair: ");
            var option = Console.ReadLine().ToUpper();
            switch (option)
            {
                case "1":
                    Console.Write("\nTarefa Concluída ? [S]im || [N]ão: ");
                    var _setStatus = Console.ReadLine().ToUpper();
                    End.GetStatus(_setStatus);
                    break;
                case "2":
                    Console.Write("Saindo...");
                    Thread.Sleep(1000);
                    break;
                default:
                    Console.Write("Opção inválida...");
                    Thread.Sleep(1000);
                    break;
            }

        }
        return End;
    }

}