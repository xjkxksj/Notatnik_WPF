using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notatnik_WPF;

public class LoginFormWindowViewModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    private List<User> users;

    public LoginFormWindowViewModel(List<User> users)
    {
        this.users = users;
        LoginCommand = new RelayCommand(Login);
        RegisterCommand = new RelayCommand(Register);
    }

    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }


    private void Login()
    {
        string username = Username ?? "";
        string password = Password ?? "";

        var userExists = users.Any(user => user.Username == username && user.Password == password);
        if (userExists)
        {
            Messenger.Send("login", username);

        }
        else
        {
            MessageBox.Show("Invalid username or password");
        }
    }

    private void Register()
    {
        var userExists = users.Any(user => user.Username == Username);
        if (userExists)
        {
            MessageBox.Show("User already exist");
            
        }
        else
        {
            Messenger.Send("register", new User() { Username = Username, Password = Password});
            MessageBox.Show("Registration successful");
        }
    }

}