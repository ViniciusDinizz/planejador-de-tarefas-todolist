using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using planejador_de_tarefas;

internal class Program
{
    private static void Main(string[] args)
    {
        int _optionMenu = 0;
        string taskf = "task.txt";
        string people = "peolple.txt";
        List<ToDoList> task = new List<ToDoList>();
        List<ToDoList> _completedTask = new List<ToDoList>();
        List<Person> _registerPerson = new List<Person>();


        do
        {
            Console.Clear();
            _optionMenu = Menu();
            switch (_optionMenu)
            {
                case 1:
                    CreateTask(task, _registerPerson);
                    break;
                case 2:
                    ViewActiveTasks(task, _registerPerson);
                    break;
                case 3:
                    ViewCompletedTasks(task);
                    break;
                case 4:
                    SavePersonToFile(_registerPerson, people);
                    SaveTaskToFile(task, taskf);
                    break;
                default:
                    Console.WriteLine("Opção inválida mané.");
                    break;
            }
        } while (_optionMenu != 4);
    }

    //Registra ou remove Pessoas Obs: Implementar lista de pessoas enquanto a função estiver ativa
    private static void RegisterRemove(List<Person> _registerremove)
    {
        string _option = "";
        while (_option != "3")
        {
            Console.Clear();
            Console.Write("[1]- Registrar || [2]- Remover || [3]- Sair: ");
            _option = Console.ReadLine().ToUpper();
            switch (_option)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("### Registro de Pessoas ###\n\nDigite o nome: ");
                    var name = Console.ReadLine().ToUpper();
                    _registerremove.Add(new Person(name));
                    break;
                case "2":
                    if (_registerremove.Count == 0)
                    {
                        Console.Write("Sem pessoas Registradas...");
                        Thread.Sleep(2000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n### Remover Pessoa ###\n");
                        for (int i = 0; i < _registerremove.Count; i++)
                        {
                            Console.WriteLine(_registerremove[i].ToString());
                        }
                        Console.Write("\n\nDigite o id: ");
                        var _id = Console.ReadLine().ToUpper();
                        for (int i = 0; i < _registerremove.Count; i++)
                        {
                            if (_registerremove[i]._id == _id)
                            {
                                _registerremove.Remove(_registerremove[i]);
                            }
                        }
                    }
                    break;
                case "3":
                    Console.Write("Saindo...");
                    Thread.Sleep(1000);
                    break;
                default:
                    Console.Write("Opção inválida...");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
    private static void SavePersonToFile(List<Person> p, string f)
    {

        try
        {
            StreamWriter sw = new(f);
            if (File.Exists(f))
            {
                foreach (var i in p)
                {
                    sw.WriteLine(i.ToPerson());
                }
                sw.Close();
            }
            else
            {
                foreach (var i in p)
                {
                    sw.WriteLine(i.ToPerson());
                }
                sw.Close();
            }

        }
        catch
        {

        }
    }
    private static void SaveTaskToFile(List<ToDoList> p, string f)
    {

        try
        {
            StreamWriter sw = new(f);
            if (File.Exists(f))
            {
                foreach (var i in p)
                {
                    sw.WriteLine(i.ToFile());
                }
                sw.Close();
            }
            else
            {
                foreach (var i in p)
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
    private static void CompletedTasks()
    {
        throw new NotImplementedException();
    }

    //Cria e aloca um objeto do tipo TodoList
    private static void CreateTask(List<ToDoList> _unfishinedTask, List<Person> person)
    {
        Console.Clear();
        Console.Write("### Cadastrando Tarefa ###\n\nQual a descricao da tarefa: ");
        var description = Console.ReadLine().ToUpper();
        Console.Write("\nQual o proprietário: ");
        var people = ChosingOwner(person);

        if (person.Count == 0)
        {
            Console.Write("\nNão é possivel concluir a tarefa sem proprietário...");
            Thread.Sleep(2000);
            Console.Clear();
        }
        else
        {
            ToDoList todo = new(description, people);
            todo.SetCategory();
            _unfishinedTask.Add(todo);
            Console.Write("\nCriando tarefa...");
            Thread.Sleep(2000);
        }
    }

    //Aloca um objeto da classe Person
    public static void RegistPerson(List<Person> person)
    {
        Console.Clear();
        Console.Write("Qual nome da pessoa: ");
        var personregist = Console.ReadLine().ToUpper();
        person.Add(new Person(personregist));
    }

    //Cria o objeto do tipo Person caso não tenha, e seta o proprietário da tarefa. Utilizada na função "CreateTask"
    public static Person ChosingOwner(List<Person> person)
    {
        int index = 0;
        if (person.Count == 0)
        {
            Console.WriteLine("Não tem pessoas cadastradas.");
            Console.Write($"Deseja cadastrar? [S]IM | [N]AO: ");
            var registersucess = Console.ReadLine().ToUpper();
            if (registersucess == "S")
            {
                RegistPerson(person);
            }
            else
            {
                return null;
            }
        }
        Console.Clear();
        bool check = false;
        while (!check)
        {
            for (int i = 0; i < person.Count; i++)
            {
                Console.WriteLine(person[i].ToString());
            }
            Console.Write("\n\nDigite o Id do proprietário: ");
            var checId = Console.ReadLine().ToUpper();

            foreach (var i in person)
            {
                if (i._id == checId)
                {
                    return i;
                }
                else
                {
                    Console.WriteLine("Id não encontrado");
                    Thread.Sleep(1000);
                }
            }
        }
        return null;
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
    private static void ViewCompletedTasks(List<ToDoList> list)
    {

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]._status != false)
            {
                Console.WriteLine(list[i]);

            }
        }
        //while (!int.TryParse(Console.ReadLine(), out op))
        //{
        //    Console.Clear();
        //    Console.WriteLine("Menu\n\n[1]- Criar tarefa\n[2]- Vizualizar Tarefas ativas\n[3]- Vizualizar Tarefas concluídas\n[4]- Sair");
        //}
    }

    //Percorrendo as tarefas não concluídas, e pode fazer alterações
    public static void ViewActiveTasks(List<ToDoList> _activeTasks, List<Person> _persons)
    {
        for (int i = 0; i < _activeTasks.Count; i++)
        {
            _activeTasks[i] = SubMenu(_activeTasks[i], _persons);
        }
    }

    //Submenu utilizado para alterações ma função "ViewActiveTasks"
    public static ToDoList SubMenu(ToDoList end, List<Person> persons)
    {
        bool altering = false;
        string option = "";

        Console.Clear();
        Console.WriteLine(end.ToString() + "\n");
        Console.Write("Deseja editar a tarefa: [S]im || [N]ão");
        var condition = Console.ReadLine().ToUpper();
        if (condition == "S")
        {
            while (option != "3")
            {
                Console.Clear();
                Console.WriteLine(end.ToString());
                Console.WriteLine("\n[1]- Alterar Status\\n[2]- Prazo de entrega\n[3]- Sair ");
                option = Console.ReadLine().ToUpper();
                switch (option)
                {
                    case "1":
                        Console.Write("Concluída Tarefa?[S]im || [N]ão: ");
                        var status = Console.ReadLine().ToUpper();
                        if (status == "S")
                        {
                            end._status = true;
                        }
                        else
                        {
                            break;
                        }
                        break;
                    case "2":

                        break;
                    case "3":
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

        return end;
    }
}