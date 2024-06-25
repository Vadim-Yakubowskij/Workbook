using node.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using JecubNode;

namespace Jecub.App
{
    class RegisterViewModel : ObservableObject
    {
        public string LoginUser { get => _login; set { _login = value; OnPropertyChanged("Login"); } }
        private string _login;
        public string PasswordUser { get => _password; set { _password = value; OnPropertyChanged("Password"); } }
        private string _password;
        private RelayCommand registerCommand;
        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand ??
                (registerCommand = new RelayCommand(obj =>
                {
                    User user = new User
                    {
                        Id = 0,
                        Login = LoginUser,
                        Password = PasswordUser
                    };

                        
                    }));
            }
        }


    }
}
