using System.Collections.Concurrent;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using planejador_de_tarefas;

internal class Program
{
    private static void Main(string[] args)
    {
        int _optionMenu = 0;
        List<ToDoList> _unfishinedTask = new List<ToDoList>();
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
                    ViewCompletedTasks(_unfishinedTask);
                    break;
                case 4:
                    //SaveToFile();
                    break;
                default:
                    Console.WriteLine("Opção inválida mané.");
                    break;
            }
        } while (_optionMenu != 4);
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
        Console.WriteLine("Menu\n\n[1]- Criar tarefa\n[2]- Vizualizar Tarefas ativas\n[3]- Vizualizar Tarefas concluídas\n[4]- Sair");
        while (!int.TryParse(Console.ReadLine(), out op))
        {
            Console.Clear();
            Console.WriteLine("Menu\n\n[1]- Criar tarefa\n[2]- Vizualizar Tarefas ativas\n[3]- Vizualizar Tarefas concluídas\n[4]- Sair");
        }
        return op;
    }

    //Percorrendo as tarefas não concluídas, e pode fazer alterações
    private static void ViewCompletedTasks(List<ToDoList> list)
    {
        int cont = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i]._status != false)
            {
                list[i] = SubMenuStatus(list[i]);
                cont++;
            }
        }
        if(cont == 0)
        {
            Console.WriteLine("Não possui tarefas concluídas.");
            Thread.Sleep(1500);
        }
        
    }

    public static void ViewActiveTasks(List<ToDoList> _activeTasks, List<Category> _category, List<Person> _persons)
    {
        for (int i = 0; i < _activeTasks.Count; i++)
        {
            if (_activeTasks[i]._status != true)
            {
                _activeTasks[i] = SubMenu(_activeTasks[i], _category, _persons);
            }
        }
    }

    //Submenu tarefas concluídas podendo alterar somente status
    private static ToDoList SubMenuStatus(ToDoList person)
    {
        string option = "";
        Console.Clear();
        Console.WriteLine(person.ToString() + "\n");
        Console.Write("Deseja editar a tarefa: [S]im || [N]ão");
        var condition = Console.ReadLine().ToUpper();
        if (condition == "S")
        {
            while (option != "5")
            {
                Console.Clear();
                Console.WriteLine(person.ToString());
                Console.WriteLine("\n[1]- Alterar Status\n[2]- Adicionar categoria\n[3]- Prazo de entrega\n[4]- Adicionar ou remover colaboradores\n[5]- Sair ");
                option = Console.ReadLine().ToUpper();
                switch(option)
                {
                    case "1":
                        Console.Write("Concluída Tarefa?[S]im || [N]ão: ");
                        var status = Console.ReadLine().ToUpper();
                        person.SetStatusyes(status);
                        break;
                        break;
                    case "5":
                        Console.Write("Saindo...");
                        Thread.Sleep(1000);
                        break;
                    default:
                        Console.Write("É possivel alterar somente os status...");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }
        return person;
    }

    //Submenu utilizado para alterações nas tarefas não concluídas
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
                    Console.WriteLine("\n[1]- Alterar Status\n[2]- Adicionar categoria\n[3]- Prazo de entrega\n[4]- Trocar Proprietário\n[5]- Sair ");
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
                            Console.WriteLine(end.ToString());
                            Console.Write("\nAdicionar ou Trocar Categoria? [S]im || [N]ão: ");
                            var category = Console.ReadLine().ToUpper();
                            AddCategory(end, _category, category);
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
                            Console.WriteLine("### TROCAR PROPRIETÁRIO ###\n");
                            Console.WriteLine(end.ToString() + "\n\n"+$"Proprietário atual: {end._ownerPerson}");
                            Console.Write("\n\nAdicionar novo proprietário para tarefa? [S]im || [N]ão: ");
                            var positioncolaboratores = Console.ReadLine().ToUpper();
                            end = EditColaboratores(end, positioncolaboratores, persons);
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
        if (option == "S")
        {
            bool status = false;
            while (!status)
            {
                for (int i = 0; i < persons.Count; i++)
                {
                    Console.WriteLine(persons[i].ToString());
                }
                Console.Write("\n[1]- Digitar Id existente || [2]- Adicionar um novo: ");
                var position = Console.ReadLine().ToUpper();
                Console.WriteLine();
                switch (position)
                {
                    case "1":
                        int cont = 0;
                        Console.Write("\nId: ");
                        var _setId = Console.ReadLine();
                        for (int i = 0; i < persons.Count; i++)
                        {
                            if (persons[i].ExistsPeson(_setId))
                            {
                                end.SetOwnerPerson(persons[i]);
                                cont++;
                                status = true;
                            }
                        }
                        if(cont == 0)
                        {
                            Console.Write("\nId incorreto...");
                            Thread.Sleep(1000);
                        }
                        break;
                    case "2":
                        Console.Write("\nNome do novo proprietário: ");
                        var _setName = Console.ReadLine().ToUpper();
                        Person newPerson = new Person(_setName);
                        end.SetOwnerPerson(newPerson);
                        status = true;
                        break;
                }
            }
        }
        
        return end;
    }

    //adicionar categoria na edição da tarefa "Case 2" da função SubMenu
    public static void AddCategory(ToDoList end, List<Category> _category, string answer)
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
                    Category Cop = new Category(categoryregister);
                    _category.Add(Cop);
                    end.SetCategory(Cop);
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("# Categorias #\n");
                for (int i = 0; i < _category.Count; i++)
                {
                    Console.WriteLine(_category[i].ToCategory());
                }
                Console.Write("\n\nUsar alguma categoria existente  [S]im || [N]ão: ");
                var registedCategory = Console.ReadLine().ToUpper();
                if (registedCategory == "N")
                {
                    Console.Write("Nome da categoria: ");
                    var categoryregister = Console.ReadLine().ToUpper();
                    Category Cop = new Category(categoryregister);
                    _category.Add(Cop);
                    end.SetCategory(Cop);
                }
                else
                {
                    int cont = 0;
                    Console.Write("Nome da categoria: ");
                    var _setCategory = Console.ReadLine().ToUpper();
                    for(int i = 0; i < _category.Count; i++)
                    {
                        if (_category[i]._nameCategory == _setCategory)
                        {
                            end.SetCategory(_category[i]);
                            cont++;
                        }
                    }
                    if(cont == 0)
                    {
                        Console.Write("Nome inválido...");
                        Thread.Sleep(1000);
                        return;
                    }
                }
            }
        }
    }

}