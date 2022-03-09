using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IrynaSkurkoOptoolProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
        
    public partial class MainWindow : Window
    {
        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new ApplicationContext();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            String login_text = loginBox.Text.ToLower().Trim();
            String pass_text = passwordBox.Password.ToLower().Trim();
            bool data_correct = false;

            if (login_text.Length == 0)
            {
                loginFailedBox.Text = "Это поле не может быть пустым.";
                loginFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else if (login_text.Length < 5)
            {
                loginFailedBox.Text = "Логин должен содержать не менее 5 символов.";
                loginFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else
            {
                loginFailedBox.Text = "";
                data_correct = true;
            }

            if (pass_text.Length == 0)
            {
                passwordFailedBox.Text = "Это поле не может быть пустым.";
                passwordFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else
            {
                passwordFailedBox.Text = "";
                data_correct = true;
            }

            if (data_correct)
            {
                User authUser = db.User.Where(b => b.login == login_text && b.password == pass_text).FirstOrDefault();
                if (authUser == null)
                    MessageBox.Show("Такого пользователя не существует.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    if (authUser.is_blocked == 0)
                    {
                        if (authUser.role_id == 1)
                        {
                            UserSession.Login(authUser);
                            OperatorWindow window = new OperatorWindow();
                            window.Show();
                            this.Close();
                        }
                        else if (authUser.role_id == 2)
                        {
                            UserSession.Login(authUser);
                            AdministratorWindow window = new AdministratorWindow();
                            window.Show();
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Администратор еще не подтвердил ваш аккаунт.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            String login_text = newLoginBox.Text;
            String pass_text = newPasswordBox.Password;
            String repeated_pass_text = repeatPasswordBox.Password;
            String email_text = emailBox.Text;

            bool data_correct = false;

            if (login_text.Length == 0 || login_text.Trim().Length == 0)
            {
                newLoginFailedBox.Text = "Это поле не может быть пустым.";
                newLoginFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else if (login_text.Length < 5)
            {
                newLoginFailedBox.Text = "Логин должен содержать не менее 5 символов.";
                newLoginFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else
            {
                newLoginFailedBox.Text = "";
                data_correct = true;
            }

            if (repeated_pass_text != pass_text)
            {
                repeatPasswordFailedBox.Text = "Повторно введенный пароль не совпадает.";
                repeatPasswordFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else
            {
                repeatPasswordFailedBox.Text = "";
                data_correct = true;
            }
            if (pass_text.Length == 0 || pass_text.Trim().Length == 0)
            {
                newPasswordFailedBox.Text = "Это поле не может быть пустым.";
                newPasswordFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else
            {
                newPasswordFailedBox.Text = "";
                data_correct = true;
            }

            if (email_text.Length == 0 || email_text.Trim().Length == 0)
            {
                emailFailedBox.Text = "Это поле не может быть пустым.";
                emailFailedBox.Foreground = Brushes.Red;
                data_correct = false;
            }
            else
            {
                emailFailedBox.Text = "";
                data_correct = true;
            }

            if (data_correct == true)
            {
                User newUser = new User(login:login_text, password:pass_text, role_id:db.Role.FirstOrDefault(r=>r.role_name == "user").role_id, email:email_text);
                var a = db.User.Add(newUser);
                var b = db.SaveChanges();
                Console.WriteLine(b);
                _ = MessageBox.Show("Регистрация выполнена успешно! Ожидайте подтвеждение Администратора.", "Регистрация", MessageBoxButton.OK, MessageBoxImage.Information); ;
                loginSwitchButton.IsChecked = true;
            }
        }
    }
}
