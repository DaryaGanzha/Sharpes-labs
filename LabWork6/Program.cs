using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork6
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isauthorized = false;
            bool isInApp = true;
            var currentUser = new UserModel();

            while (!isauthorized)
            {
                Console.Clear();
                string login = String.Empty;
                string password = String.Empty;

                Console.WriteLine("Вы не авторизованы, введите логин и пароль!");
                Console.WriteLine("Введите логин");
                login = Console.ReadLine();
                Console.WriteLine("Введите пароль");
                password = Console.ReadLine();
                Console.WriteLine("Подождите, мы проверяем информацию.....");
                currentUser = UserModel.CheckAuthorizeUser(login, password);
                if (string.IsNullOrEmpty(currentUser.UserLogin) && string.IsNullOrEmpty(currentUser.UserPassword))
                    Console.WriteLine("Попытка авторизации неудачна!");
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Добро пожаловать, {currentUser.UserLogin}. Авторизация прошла успешно!");
                    isauthorized = true;
                }
            }

            while (isInApp)
            {
                var sprint = SprintModel.CheckSprint(currentUser.Id);
                Console.Clear();
                string result;

                Console.WriteLine("1. Просмотр сотрудников");
                Console.WriteLine("2. Просмотр задач");
                if (sprint.IsActive4User)
                    Console.WriteLine($"3. У вас есть не заполненный спринт. Дата окончания спринта: {sprint.EndDate}. ");
                if (!sprint.IsActive4User)
                    Console.WriteLine($"Ваш спринт заполнен на эту неделю. Дата окончания спринта: {sprint.EndDate}. ");

                Console.WriteLine("0. Выход");

                result = Console.ReadLine();
                if (result == "1")
                {
                    Console.Clear();
                    string sortString = "";
                    Console.WriteLine("1. Создать подчиненного");
                    Console.WriteLine("2. Получить иерархию сотрудников");
                    Console.WriteLine("3. Изменить начальника у подчиненного");
                    Console.WriteLine("4. Получить сотрудника по Id");
                    Console.WriteLine("5. Создать сотрудника");
                    Console.WriteLine("6. Удалить сотрудника");
                    Console.WriteLine("7. Изменить пароль");
                    sortString = Console.ReadLine();
                    if (sortString == "1")
                    {
                        string newLogin = "";
                        string newPassword = "";
                        Console.WriteLine("Придумайте логин подчиненного:");
                        newLogin = Console.ReadLine();
                        Console.WriteLine("Придумайте пароль подчиненного:");
                        newPassword = Console.ReadLine();

                        if(String.IsNullOrEmpty(newLogin) || String.IsNullOrEmpty(newPassword))
                            Console.WriteLine("Операцияя прекращена, данные не были введены коректно!");
                        else
                        {
                          string message = UserModel.CreateUser(newLogin, newPassword, currentUser.Id);
                            if (!String.IsNullOrEmpty(message))
                            {
                                Console.WriteLine("Произошла ошибка при создании пользователя! Error: " + message);
                            }
                            else
                            {
                                Console.WriteLine($"Пользователь , {newLogin} , был успешно создан!");
                            }
                        }
                    }
                    else if (sortString == "2")
                    {
                        var parentModel = UserModel.GetSubordinates();
                        Console.WriteLine("Древо сотрудников:");
                        Action<UserModel> actionTree = null;
                        actionTree = model =>
                        {
                            foreach(UserModel item in model.Children)
                            {
                                if (item.Id == currentUser.Id)
                                    Console.WriteLine("ВЫ => UserLogin: " + item.UserLogin);
                                else
                                    Console.WriteLine("UserLogin: " + item.UserLogin);
                                foreach (UserModel model2 in item.Children)
                                    actionTree(model2);
                            }
                        };
                        Console.WriteLine("UserLogin: " + parentModel.UserLogin);
                        actionTree(parentModel);
                        Console.ReadKey();
                    }
                    else if(sortString == "3")
                    {
                        var listUsers = UserModel.GetSubordinatesByParentId(currentUser.Id);
                        int selectedUser = 0;
                        if(listUsers.Children !=null || listUsers.Children.Count() > 0)
                        {
                            Console.WriteLine("Выберите номер пользователя, у которого , вы хотите изменить начальника");
                            int index = 0;
                            foreach(UserModel item in listUsers.Children)
                            {
                                Console.WriteLine($"№: {index} Login: {item.UserLogin}");
                                index++;
                            }
                            selectedUser = Convert.ToInt32(Console.ReadLine());
                            if (selectedUser > listUsers.Children.Count() || selectedUser < 0) {
                                Console.WriteLine("Данного номера не существует");
                                return;
                            }

                            if (currentUser.ParentId==null)
                                Console.WriteLine("Вы не можете сделать пользователя вышим начальником, вы и так начальник)");
                            else
                            {
                                UserModel.SetAdministrator(listUsers.Children[selectedUser].Id, Convert.ToInt32(currentUser.ParentId));
                                Console.WriteLine("Замена выполнена, успешно");
                            }
                        }
                        else
                        {
                            Console.WriteLine("У вас нет подчиненных");
                        }
                    }
                    else if(sortString == "4")
                    {
                        int workerId = 0;
                        Console.Clear();
                        Console.WriteLine("Введите Id сотрудника");
                        workerId = Convert.ToInt32(Console.ReadLine());
                        var worker = UserModel.GetWorkerById(workerId);

                        if (!string.IsNullOrEmpty(worker.UserLogin))
                        {
                            Console.WriteLine($"Сотрудник найден! Id: {worker.Id} , Login: {worker.UserLogin}, Password: {worker.UserPassword}");
                        }
                        else
                        {
                            Console.WriteLine($"Сотрудник с Id: {workerId} не найден!");
                        }
                        Console.ReadKey();
                    }
                    else if(sortString == "5")
                    {
                        Console.Clear();
                        string tmpLogin = "";
                        string tmpPassword = "";
                        Console.WriteLine("Введите логин сотрудника!");
                        tmpLogin = Console.ReadLine();
                        Console.WriteLine("Введите пароль сотрудника!");
                        tmpPassword = Console.ReadLine();
                        string error= UserModel.CreateUser(tmpLogin, tmpPassword, currentUser.Id);
                        if (String.IsNullOrEmpty(error))
                            Console.WriteLine($"Сотрудник с логином {tmpLogin}, был успешно создан");

                        Console.ReadKey();
                    }
                    else if (sortString == "6")
                    {
                        int workerId = 0;
                        Console.Clear();
                        Console.WriteLine("Введите Id сотрудника");
                        workerId = Convert.ToInt32(Console.ReadLine());
                        UserModel.DeleteUser(workerId, currentUser.Id);
                        Console.WriteLine("Сотрудник был успешно удален");
                        Console.ReadKey();

                    }
                    else if (sortString == "7")
                    {
                        string newPassword = String.Empty;
                        Console.WriteLine("Введите новый пароль");
                        newPassword = Console.ReadLine();
                        UserModel.UpdatePassword(currentUser.Id, newPassword);
                    }
                }
                else if (result == "2")
                {
                    Console.Clear();
                    string sortString = String.Empty;
                    Console.WriteLine("1. Создать задачу");
                    Console.WriteLine("2. Взять задачу в работу");
                    Console.WriteLine("3. Получить все задачи!");
                    Console.WriteLine("4. Получить  задачу по Id!");
                    Console.WriteLine("5. Получить  задачи закрепленные за мной!");
                    sortString = Console.ReadLine();

                    if (sortString =="1")
                    {
                        string taskName = String.Empty;
                        string description = String.Empty;
                        Console.Clear();
                        Console.WriteLine("Введите наименование задачи на АНГЛ.!");
                        taskName = Console.ReadLine();
                        Console.WriteLine("Введите описание задачи на АНГЛ.!");
                        description = Console.ReadLine();
                        TaskModel.CreateNewTAsk(taskName, description, currentUser.Id);

                    }
                    else if (sortString == "2")
                    {
                        int taskId = 0;
                        Console.WriteLine("Введите Id задачи , которую вы хотите взять в работу!");
                        taskId = Convert.ToInt32(Console.ReadLine());

                      Console.WriteLine( TaskModel.ChangeTaskStatus(taskId, 2, currentUser.Id));
                        Console.ReadKey();
                    }
                    else if (sortString == "3")
                    {
                        List<TaskModel> tasks = TaskModel.GetAllTasks();
                        foreach(TaskModel item in tasks)
                        {
                            Console.WriteLine($"Id : {item.Id} , Name : {item.Name} , CretionDate: {item.CreationDate}");
                        }
                        Console.ReadKey();
                    }
                    else if(sortString == "4")
                    {
                        Console.WriteLine("Введите номер задачи");
                        int taskNum = 0;
                        taskNum = Convert.ToInt32(Console.ReadLine());
                        var task = TaskModel.GetTaskById(taskNum);
                        if (task.Id != 0)
                            Console.WriteLine($"Id : {task.Id} , Name : {task.Name} , CretionDate: {task.CreationDate}");
                        else
                            Console.WriteLine("Задача не найдена!");

                        Console.ReadKey();

                    }
                    else if(sortString == "5")
                    {
                        var taskModels = new List<TaskModel>();
                        taskModels = TaskModel.GetMyTasks(currentUser.Id);
                        if (taskModels.Count() > 0)
                        {
                            foreach (TaskModel item in taskModels)
                            {
                                Console.WriteLine($"Id : {item.Id} , Name : {item.Name} , CretionDate: {item.CreationDate}");
                            }
                        }
                        else
                            Console.WriteLine("За вами не закрепленна ни одна задача!");

                        Console.ReadKey();
                    }
                }
                else if (result == "3" && sprint.IsActive4User)
                {
                    int taskNumber = 0;
                    string comment = String.Empty;
                    Console.WriteLine("Введите номер задачи , к которой вы хотите оставить спринт");
                    taskNumber =Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите комментарий, который  вы хотите оставить  к спринту");
                    comment = Console.ReadLine();
                    SprintModel.InsertSprint(taskNumber, comment, currentUser.Id);
                }
                else if (result == "0")
                {
                    isInApp = false;
                }
                else
                {
                    Console.WriteLine("Таких опций не найдено!");
                    Console.ReadKey();
                }
            }
        }
    }
}
