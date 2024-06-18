using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;


namespace Notatnik_WPF;

public class LoginFormWindowViewModel : BaseViewModel, ICloseWindows
{
    public string Username { get; set; }
    public string Password { get; set; }
    private List<User> users;
    public ICommand LoginCommand { get; }
    public ICommand RegisterCommand { get; }
    public Action Close { get; set; }
    public bool IsDialogCanceled { get; set; } = true;

    public LoginFormWindowViewModel(List<User> users)
    {
        this.users = users;
        LoginCommand = new RelayCommand(Login);
        RegisterCommand = new RelayCommand(Register);
    }

    private void Login()
    {
        string username = Username ?? "";
        string password = Password ?? "";

        var userExists = users.Any(user => user.Username == username && user.Password == password);
        if (userExists)
        {
            Messenger.Send("login", username);
            Close?.Invoke();
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

    public bool CanClose()
    {
        return true;
    }
}