using System.Collections.Concurrent;
using System.Reflection.Metadata.Ecma335;
using planejador_de_tarefas;

internal class Program
{
    private static void Main(string[] args)
    {
        int _optionMenu = 0;
        List<ToDoList> _unfishinedTask = new List<ToDoList>();
        List<ToDoList> _completedTask = new List<ToDoList>();
        List<Person> _registerPerson = new List<Person>();
        List<Category> _category = new List<Category>();

        do
        {
            Console.Clear();
            _optionMenu = Menu();
            switch (_optionMenu)
            {
                case 1:
                    CreateTask(_unfishinedTask, _registerPerson);
                    break;
                case 2:
                    ViewActiveTasks(_unfishinedTask, _category, _registerPerson);
                    break;
                case 3:
                    CompletedTasks();
                    break;
                case 4:
                    RegisterRemove(_registerPerson);
                    break;
                case 5:
                    SaveToFile();
                    break;
                case 6:
                    SaveToFile();
                    break;
                default:
                    Console.WriteLine("Opção inválida mané.");
                    break;
            }
        } while (_optionMenu != 6);
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
    private static void SaveToFile()
    {
        throw new NotImplementedException();
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
        int people = ChosingOwner(person);

        if (people == -1)
        {
            Console.Write("\nNão é possivel concluir a tarefa sem proprietário...");
            Thread.Sleep(2000);
            Console.Clear();
        }
        else
        {
            _unfishinedTask.Add(new ToDoList(description, person[people]));
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
    public static int ChosingOwner(List<Person> person)
    {
        int index = -1;
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
                return -1;
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

            for (int i = 0; i < person.Count; i++)
            {
                if (person[i]._id == checId)
                {
                    check = true;
                    index = i;
                }
            }
            if (index == -1)
            {
                Console.Write("ID incorreto...");
                Thread.Sleep(1000);
            }
            Console.Clear();
        }

        return index;
    }

    //Menu de opções exibido na função Main
    public static int Menu()
    {
        int op = 0;
        Console.WriteLine("Menu\n\n[1]- Criar tarefa\n[2]- Vizualizar Tarefas ativas\n[3]- Vizualizar Tarefas concluídas\n[4]- Cadastrar Pessoas\n[5]- Cadastrar Categorias\n[6]- Sair");
        while (!int.TryParse(Console.ReadLine(), out op))
        {
            Console.Clear();
            Console.WriteLine("Menu\n\n[1]- Criar tarefa\n[2]- Vizualizar Tarefas ativas\n[3]- Vizualizar Tarefas concluídas\n[4]- Sair");
        }
        return op;
    }

    //Percorrendo as tarefas não concluídas, e pode fazer alterações
    public static void ViewActiveTasks(List<ToDoList> _activeTasks, List<Category> _category, List<Person> _persons)
    {
        for (int i = 0; i < _activeTasks.Count; i++)
        {
            _activeTasks[i] = SubMenu(_activeTasks[i], _category, _persons);
        }
    }

    //Submenu utilizado para alterações ma função "ViewActiveTasks"
    public static ToDoList SubMenu(ToDoList end, List<Category> _category, List<Person> persons)
    {
        bool altering = false;
        string option = "";
        while (!altering)
        {
            Console.Clear();
            Console.WriteLine(end.ToString() + "\n");
            Console.Write("Deseja editar a tarefa: [S]im || [N]ão");
            var condition = Console.ReadLine().ToUpper();
            if (condition == "S")
            {
                while (option != "5")
                {
                    Console.Clear();
                    Console.WriteLine(end.ToString());
                    Console.WriteLine("\n[1]- Alterar Status\n[2]- Adicionar categoria\n[3]- Prazo de entrega\n[4]- Adicionar ou remover colaboradores\n[5]- Sair ");
                    option = Console.ReadLine().ToUpper();
                    switch (option)
                    {
                        case "1":
                            Console.Write("Concluída Tarefa?[S]im || [N]ão: ");
                            var status = Console.ReadLine().ToUpper();
                            end.SetStatusyes(status);
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine(end.ToString);
                            Console.Write("Adicio ou Trocar Categoria? [S]im || [N]ão: ");
                            var category = Console.ReadLine().ToUpper();
                            AddCategory(end,_category, category);
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine(end.ToString() + "\n");
                            Console.WriteLine("### Definir data de entrega ###\n");
                            Console.Write("Dia: ");
                            int _day = int.Parse(Console.ReadLine());
                            Console.Write("Mês: ");
                            int _month = int.Parse(Console.ReadLine());
                            Console.Write("Ano: ");
                            int _year = int.Parse(Console.ReadLine());
                            end.SetDueTime(_year, _month, _day);
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("### ADICIONAR COLABORADORES ###\n");
                            Console.WriteLine(end.ToString() + "\n");
                            Console.Write("[A]dicionar colaboradores || [R]emover colaboradores: ");
                            var positioncolaboratores = Console.ReadLine().ToUpper();
                            end = EditColaboratores(end, positioncolaboratores,persons); //Testar funcionalidades "Case 4" e "case 3"
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
        return end;
    }

    //Editar colaboradores de uma tarefa, podendo adicionar um existente, adicionar um novo ou remover algum colaborador
    private static ToDoList EditColaboratores(ToDoList end, string option, List<Person> persons)
    {
        if(option == "A")
        {
                for (int i = 0; i < persons.Count; i++)
                {
                    Console.WriteLine(persons[i].ToString());
                }
   
      
            Console.Write("\n\n[1]- Selecionar id || [2]- Adicionar novo: ");
            var _addId = Console.ReadLine().ToUpper();
            switch(_addId) 
            {
                case "1":
                    Console.Write("\nId: ");
                    var _setid = Console.ReadLine();
                    while(!end.SetPersonExists(persons, _setid))
                    {
                        Console.Clear();
                        Console.WriteLine("### ADICIONAR COLABORADORES ###\n");
                        Console.WriteLine(end.ToString);
                        for (int i = 0; i < persons.Count; i++)
                        {
                            Console.WriteLine(persons[i].ToString());
                        }
                        Console.WriteLine("Digite um Id correto: ");
                        _setid = Console.ReadLine();
                    }
                    break;
                case "2":
                    Console.Write("\nInforme o nome: ");
                    var _setName = Console.ReadLine();
                    Person personcopy = new Person(_setName);
                    persons.Add(end.SetPerson(personcopy));
                    break;
                default:
                    Console.Write("\nOpção inválida...");
                    Thread.Sleep(1000);
                    break;
            }
        }else if(option == "R")
        {
            if (end._morePeople.Count != 0) 
            {
                for (int i = 0; i < end._morePeople.Count; i++)
                {
                    Console.WriteLine(end._morePeople[i].ToString());
                }
            }
            else
            {
                Console.WriteLine("\nSem colaboradores.\n");
                Thread.Sleep(1000);
                return end;
            }
            
            bool _infoId = false;
            Console.Write("\nInforme o id: ");
            var _setid = Console.ReadLine().ToUpper();
            for(int i = 0;i < persons.Count;i++)
            {
                if (persons[i].ExistsPeson(_setid))
                {
                    end.RemovePersons(_setid);
                    _infoId = true;
                }
            }
            if(_infoId == false)
            {
                Console.Write("\nId incorreto...");
                Thread.Sleep(1000);
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
            Thread.Sleep(1000);
        }
        return end;
    }

    //adicionar categoria na edição da tarefa "Case 2" da função SubMenu
    public static void AddCategory (ToDoList end, List<Category> _category, string answer)
    {
        if (answer == "S")
        {
            if (_category.Count == 0)
            {
                Console.Write("Não existe categorias.Registrar uma? [S]im || [N]ão: ");
                var registedCategory = Console.ReadLine().ToUpper();
                if (registedCategory == "S")
                {
                    Console.Write("Nome da categoria: ");
                    var categoryregister = Console.ReadLine().ToUpper();
                    _category.Add(new Category(categoryregister));
                }
                else
                {
                    return;
                }
            }
            Console.Clear();
            Console.WriteLine(end.ToString() + "\n");
            for (int i = 0; i < _category.Count; i++)
            {
                Console.WriteLine(_category[i].ToCategory());
            }
            Console.Write("\n\nDigite o nome da categoria: ");
            var repeat = Console.ReadLine().ToUpper();
            for (int i = 0; i < _category.Count; i++)
            {
                if (_category[i]._nameCategory == repeat)
                {
                    end._category = _category[i];
                }
                else
                {
                    break;
                }
            }
        }
    }



}