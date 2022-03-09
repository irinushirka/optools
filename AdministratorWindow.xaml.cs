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
using System.Windows.Shapes;

namespace IrynaSkurkoOptoolProgram
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        ApplicationContext db;
        public AdministratorWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            User currUser = UserSession.GetUser();
            List<User> users = db.User.ToList<User>();
            users.Remove(currUser);

            List<UserViewModel> users_model = new List<UserViewModel>();
            foreach (User u in users)
            {
                users_model.Add(new UserViewModel(u));
            }
            userGrid.ItemsSource = users_model;
        }
        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.Logout();
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void saveAllButton_Click(object sender, RoutedEventArgs e)
        {
            List<UserViewModel> uvm = (List<UserViewModel>)userGrid.ItemsSource;
            User tempUser = new User();
            foreach(UserViewModel u in uvm)
            {
                tempUser = db.User.FirstOrDefault(ud => ud.user_id == u.user_id);
                tempUser.user_id = u.user_id;
                tempUser.login = u.login;
                tempUser.password = u.password;
                tempUser.email = u.email;
                if (u.role == "Оператор")
                    tempUser.role_id = 1;
                else
                    tempUser.role_id = 2;
                if (u.access == "Нет")
                    tempUser.is_blocked = 0;
                else
                    tempUser.is_blocked = 1;

                db.Entry(db.User.FirstOrDefault(ud => ud.user_id == u.user_id)).CurrentValues.SetValues(tempUser);
                db.SaveChanges();

                MessageBox.Show("Данные успешно сохранены!", "Сохранение данных", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void usersPageButton_Click(object sender, RoutedEventArgs e)
        {
            usersControlPage.Visibility = System.Windows.Visibility.Visible;
            operatorsControlPage.Visibility = System.Windows.Visibility.Hidden;
            documentsPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Hidden;

            User currUser = UserSession.GetUser();
            List<User> users = db.User.ToList<User>();
            users.Remove(currUser);

            List<UserViewModel> users_model = new List<UserViewModel>();
            foreach (User u in users)
            {
                users_model.Add(new UserViewModel(u));
            }
            userGrid.ItemsSource = users_model;
        }

        private void operatorsButton_Click(object sender, RoutedEventArgs e)
        {
            usersControlPage.Visibility = System.Windows.Visibility.Hidden;
            operatorsControlPage.Visibility = System.Windows.Visibility.Visible;
            documentsPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Hidden;

            List<Operator> operators = db.Operator.ToList<Operator>();
            operatorsGrid.ItemsSource = operators;
        }

        private void docButton_Click(object sender, RoutedEventArgs e)
        {
            usersControlPage.Visibility = System.Windows.Visibility.Hidden;
            operatorsControlPage.Visibility = System.Windows.Visibility.Hidden;
            documentsPage.Visibility = System.Windows.Visibility.Visible;
            statsPage.Visibility = System.Windows.Visibility.Hidden;
        }

        private void statsButton_Click(object sender, RoutedEventArgs e)
        {
            usersControlPage.Visibility = System.Windows.Visibility.Hidden;
            operatorsControlPage.Visibility = System.Windows.Visibility.Hidden;
            documentsPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void usersToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = null;
            Microsoft.Office.Interop.Excel.Workbook wb = null;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.Worksheet ws = null;

            excel = new Microsoft.Office.Interop.Excel.Application();
            wb = excel.Workbooks.Add();
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
            ws.Columns.AutoFit();
            ws.Columns.EntireColumn.ColumnWidth = 25;

            for (int Idx = 0; Idx < userGrid.Columns.Count; Idx++)
            {
                ws.Range["A1"].Offset[0, Idx].Value = userGrid.Columns[Idx].Header;
            }

            List<String> cellNames = new List<String>();
            cellNames.Add("A");
            cellNames.Add("B");
            cellNames.Add("C");
            cellNames.Add("D");
            cellNames.Add("E");
            cellNames.Add("F");

            List<UserViewModel> uvm = (List<UserViewModel>)userGrid.ItemsSource;

            excel.Visible = true;
            wb.Activate();
        }

        private void saveOperatorsButton_Click(object sender, RoutedEventArgs e)
        {
            List<Operator> ops = (List<Operator>)operatorsGrid.ItemsSource;
            foreach (Operator o in ops)
            {
                db.Entry(db.Operator.FirstOrDefault(ud => ud.operator_id == o.operator_id)).CurrentValues.SetValues(o);
                db.SaveChanges();
            }
            MessageBox.Show("Данные успешно сохранены!", "Сохранение данных", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void operatorsToExcelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
