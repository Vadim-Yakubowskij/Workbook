
using node.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using DataBase.Repository;
using Task = DataBase.Repository.Task;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Markup;
using System.Xml.Linq;
using System.Security.Cryptography;
using Jecub.App;
using JecubNode.Repository;
using JecubNode;

namespace node
{
    public class TododoViewModel : ObservableObject
    {
        private ObservableCollection<Todo> _taskListSunday;
        public ObservableCollection<Todo> TaskListSunday { get => _taskListSunday; set { _taskListSunday = value; OnPropertyChanged("TaskListSunday"); } }

        private ObservableCollection<Todo> _taskListMonday;
        public ObservableCollection<Todo> TaskListMonday { get => _taskListMonday; set { _taskListMonday = value; OnPropertyChanged("TaskListMonday"); } }

        private ObservableCollection<Todo> _taskListTuesday;
        public ObservableCollection<Todo> TaskListTuesday { get => _taskListTuesday; set { _taskListTuesday = value; OnPropertyChanged("TaskListTuesday"); } }

        private ObservableCollection<Todo> _taskListWedessday;
        public ObservableCollection<Todo> TaskListWednesday { get => _taskListWedessday; set { _taskListWedessday = value; OnPropertyChanged("TaskListWednesday"); } }

        private ObservableCollection<Todo> _taskListThursday;
        public ObservableCollection<Todo> TaskListThursday { get => _taskListThursday; set { _taskListThursday = value; OnPropertyChanged("TaskListThursday"); } }

        private ObservableCollection<Todo> _taskListFriday;
        public ObservableCollection<Todo> TaskListFriday { get => _taskListFriday; set { _taskListFriday = value; OnPropertyChanged("TaskListFriday"); } }

        private ObservableCollection<Todo> _taskListSaturday;
        public ObservableCollection<Todo> TaskListSaturday { get => _taskListSaturday; set { _taskListSaturday = value; OnPropertyChanged("TaskListSaturday"); } }
        public Todo SelectedTask { 
            get => _selectedTask; 
            set { 
                _selectedTask = value;
                OnPropertyChanged("SelectedTask"); } }

        public string DataTimee { get => _dataTimee; set { _dataTimee = value; OnPropertyChanged("DataTimee"); } }
        public string WinInfo { get => _winInfo; set { _winInfo = value; OnPropertyChanged("WinInfo"); } }
        private string _winInfo;
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }
        private string _name;
        public int Id { get => _id; set { _id = value; OnPropertyChanged("Id"); } }
        private int _id;
        public string Info { get => _info; set { _info = value; OnPropertyChanged("Info"); } }
        private string _info;
        public DateTime Date { get => _date; set { _date = value.Date; OnPropertyChanged("Date"); } }
        private DateTime _date;
        private List<Todo> tasks;
        private Todo _selectedTask;
        
        private string _dataTimee;
        private DateTime _datePointer;
        private TaskRepository _tasklistRepository;
        private TaskRepository TasklistRepository { get => _tasklistRepository; set => _tasklistRepository = value; }

        public int UserId{ get; set; }

        public TododoViewModel(TaskRepository taskRepository, int userId)
        {
            Date = DateTime.Now;
            DateTime today = DateTime.Today;
            _datePointer = DateTime.Today;
            int day = today.Day;
            int month = today.Month;
            int year = today.Year;
            DataTimee = GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}");
            string monday = DataTimee.Split(" - ", StringSplitOptions.None)[0];
            string sunday = DataTimee.Split(" - ", StringSplitOptions.None)[1];
            TasklistRepository = taskRepository;
            UserId = userId;
            System.Threading.Tasks.Task.Run(() => Update()).Wait();

        }

        private async void Update()
        {
            UpdateWeekDays(await TasklistRepository.read(UserId));
        }

        private void UpdateWeekDays(List<Todo> tasks)
        {
            List<Todo> tmp = new List<Todo>();
            tasks.ForEach(x =>
            {
                tmp.Add(new Todo(x));
            });

            for (int i = 0; i < tmp.Count(); i++)
            {
                tmp[i].Date_time = tmp[i].Date_time.Replace("-", ".");
            }
            tmp = tmp.Where(x =>
            {
                string[] splitted = x.Date_time.Split('.');
                int day = int.Parse(splitted[2]);
                int month = int.Parse(splitted[1]);
                int year = int.Parse(splitted[0]);
                return GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}") == DataTimee;
            }).OrderBy(x =>
            {
                string[] splitted = x.Date_time.Split('.');
                return new DateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]));
            }).ToList();


            Dictionary<DayOfWeek, List<Todo>> tasksByDayOfWeek = new Dictionary<DayOfWeek, List<Todo>>();

            foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                tasksByDayOfWeek[dayOfWeek] = new List<Todo>();
            }

            foreach (Todo task in tmp)
            {
                string[] splitted = task.Date_time.Split('.');
                DateTime date = new DateTime(int.Parse(splitted[0]), int.Parse(splitted[1]), int.Parse(splitted[2]));
                DayOfWeek dayOfWeek = date.DayOfWeek;
                tasksByDayOfWeek[dayOfWeek].Add(task);
            }

            TaskListMonday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Monday]);
            TaskListTuesday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Tuesday]);
            TaskListWednesday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Wednesday]);
            TaskListThursday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Thursday]);
            TaskListFriday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Friday]);
            TaskListSaturday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Saturday]);
            TaskListSunday = new ObservableCollection<Todo>(tasksByDayOfWeek[DayOfWeek.Sunday]);
            
        }

        public string GetCurrentWeekDay(string inputDate)
        {
            int delta = 0;
            if (DateTime.TryParseExact(inputDate, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {

                DayOfWeek today = date.DayOfWeek;
                if (today == DayOfWeek.Sunday)
                {
                    delta = -6;
                }
                else
                {
                    delta = DayOfWeek.Monday - today;
                }
                DateTime monday = date.AddDays(delta);
                DateTime sunday = monday.AddDays(6);
                return $"{monday:dd.MM} - {sunday:dd.MM}";
            }
            else
            {
                return "";
            }

        }

        private RelayCommand nextWeekcommand;
        public RelayCommand NextWeekCommand
        {
            get
            {
                return nextWeekcommand ??
                    (nextWeekcommand = new RelayCommand(obj =>
                    {
                        _datePointer = _datePointer.AddDays(7);

                        int day = _datePointer.Day;
                        int month = _datePointer.Month;
                        int year = _datePointer.Year;
                        DataTimee = GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}");
                        Update();
                    }));
            }
        }
        private RelayCommand prevWeekcommand;
        public RelayCommand PrevWeekCommand
        {
            get
            {
                return prevWeekcommand ??
                    (prevWeekcommand = new RelayCommand(obj =>
                    {
                        _datePointer = _datePointer.AddDays(-7);

                        int day = _datePointer.Day;
                        int month = _datePointer.Month;
                        int year = _datePointer.Year;
                        DataTimee = GetCurrentWeekDay($"{day:00}.{month:00}.{year:00}");
                        Update();
                    }));
            }
        }
        private RelayCommand createTaskCommand;

        private RelayCommand deleteTaskCommand;

        private RelayCommand upadateTaskCommand;

        public string NameBox()
        {
            return MainWindow.AddWindow.InfoBox.Text;
        }
        public static string ConvertDateFormat(string inputDate)
        {
            if (DateTime.TryParseExact(inputDate, "dd.MM.yyyy H:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result.ToString("yyyy-MM-dd");
            }

            return string.Empty;
        }
        public RelayCommand CreateTaskCommand
        {
            get
            {
                return createTaskCommand ??
                    (createTaskCommand = new RelayCommand(obj =>
                    {
                        
                        TasklistRepository.create(Name, ConvertDateFormat(Date.ToString()),Info);
                        UpdateWeekDays(TasklistRepository.read());
                        Name= string.Empty;
                        Info = string.Empty;
                        Date = DateTime.Now;
                    }));
            }
        }
        public RelayCommand DeleteTaskCommand
        {
            get
            {
                return deleteTaskCommand ??
                    (deleteTaskCommand = new RelayCommand(obj =>
                    {

                        TasklistRepository.delete(SelectedTask.Id) ;
                        UpdateWeekDays(TasklistRepository.read());
                    }));
            }
        }
        public RelayCommand UpadateTaskCommand
        {
            get
            {
                return upadateTaskCommand ??
                    (upadateTaskCommand = new RelayCommand(obj =>
                    {

                        TasklistRepository.Update(SelectedTask.Id, SelectedTask.Name, SelectedTask.Date_time.ToString(), SelectedTask.More_details);
                        UpdateWeekDays(TasklistRepository.read());
                        Name = string.Empty;
                        Info = string.Empty;
                        Date = DateTime.Now;
                    }));
            }
        }
    }


}
