using pr_3.models;
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
using pr_3.pages;
using System.Security.Cryptography;

namespace pr_3.pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private string role;

        public Autho()
        {
            InitializeComponent();
            
            tboxCaptcha.Visibility = Visibility.Hidden;
            tblockCaptcha.Visibility = Visibility.Hidden;
            btnCaptcha.Visibility = Visibility.Hidden;
        }

        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client());
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxLogin.Text) && !String.IsNullOrEmpty(pasboxPassword.Password))
            {
                LoginUser();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль");
            }
        }
        int countUnsuccessful = 0;
      
        private void LoginUser()
        {
            PhotoStudioEntities1 dbContext = new PhotoStudioEntities1();
            clients user = new clients();
            string Login = tbxLogin.Text;
            string Password = HashPasswords.HashPasswords.Hash(pasboxPassword.Password.Replace("\"", ""));

            user = dbContext.clients.Where(p => p.login == Login).FirstOrDefault();
            if (countUnsuccessful < 1)
            {
                if (user != null)
                {
                    if (user.password == Password)
                    {
                        LoadForm(user.RoleId.ToString());
                        tbxLogin.Text = "";
                        tboxCaptcha.Text = "";
                        MessageBox.Show("вы вошли под логином: " + user.login.ToString());
                    }
                    else
                    {
                        MessageBox.Show("неверный пароль");
                        GenerateCaptcha();
                        countUnsuccessful++;
                    }
                }
                else
                {
                    MessageBox.Show("пользователя с логином '" + Login + "' не существует");
                }
            }
        }

        private void LoadForm(string _role)
        {
            switch (_role)
            {
                //клиент -- посмотреть свои данные и обьекты 
                case "1":
                    NavigationService.Navigate(new Client());
                    break;
                //админ -- умеет все 
                case "2":
                    NavigationService.Navigate(new Admin());
                    break;
                //Сотрудник отдела вневедомственной охраны -- составляет договоры 
                case "3":
                    NavigationService.Navigate(new Employee());
                    break;
            }
        }


        private void GenerateCaptcha()
        {
            tboxCaptcha.Visibility = Visibility.Visible;
            tblockCaptcha.Visibility = Visibility.Visible;
            btnCaptcha.Visibility = Visibility.Visible;

            Random rdm = new Random();
            int rndNum = rdm.Next(1, 3);
            switch (rndNum)
            {
                case 1:
                    tblockCaptcha.Text = "ju2sT8Cds";
                    tblockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 2:
                    tblockCaptcha.Text = "iNmK2cl";
                    tblockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 3:
                    tblockCaptcha.Text = "uOozGk95";
                    tblockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
            }
        }
        private void btnEnterGuests_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnEnter_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
     


        private void btnCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (countUnsuccessful >= 1)
            {
                if (tboxCaptcha.Text != tblockCaptcha.Text)
                {
                    MessageBox.Show("Неверная капча");
                    GenerateCaptcha();
                }
                if (tboxCaptcha.Text == tblockCaptcha.Text)
                {
                    countUnsuccessful = 0;
                    MessageBox.Show("капча введена правильно");
                    tboxCaptcha.Visibility = Visibility.Hidden;
                    tblockCaptcha.Visibility = Visibility.Hidden;
                    btnCaptcha.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
    }
    }