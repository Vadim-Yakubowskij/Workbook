using node.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using JecubNode;
using JecubNode.Repository;
using System.Windows;

namespace Jecub.App
{
    public class RegisterViewModel : ObservableObject
    {
        public string LoginUser { get => _login; set { _login = value; OnPropertyChanged("LoginUser"); } }
        private string _login;
        public string PasswordUser { get => _password; set { _password = value; OnPropertyChanged("PasswordUser"); } }
        private string _password;

        public int UserId{ get; set; }

        private UserRepository userRepository;
        private RelayCommand registerCommand;
        private RelayCommand loginCommand;

        public RegisterViewModel()
        {
            userRepository = new UserRepository();
        }

        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand ??
                (registerCommand = new RelayCommand(obj =>
                {
                    User user = new User
                    {
                        Id = 65,
                        Login = LoginUser,
                        Password = PasswordUser
                    };
                    userRepository.Register(user);


                }));
            }
        }
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                (loginCommand = new RelayCommand(obj =>
                {
                    User user = new User
                    {
                        Login = LoginUser,
                        Password = PasswordUser
                    };
                    Task.Run(()=>DoLogin(user)).Wait();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }));
            }
        }

        private async void DoLogin(User user)
        {
            UserId = await userRepository.Login(user);
        }
    }
}
