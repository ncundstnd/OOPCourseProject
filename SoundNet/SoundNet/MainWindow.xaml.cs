using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Windows;
using System.Windows.Input;

namespace SoundNet
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //this.Loaded += Window_Loaded;
            Cursor = new Cursor(Application.GetResourceStream(new Uri("Resources/arrow.cur", UriKind.Relative)).Stream);
            App.GlobalResources._dbContext.Database.EnsureCreated();
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Color resetColor = (Color)ColorConverter.ConvertFromString("#9158ff");
        //    SelectLoginButton.Background = new SolidColorBrush(resetColor);
        //}

        private void SelectLoginButton_Click(object sender, RoutedEventArgs e)
        {
            SupportMethods.UpdateVisibilityAndButtonBackground(LoginPanel, RegisterPanel, SelectLoginButton, SelectRegisterButton);
        }

        private void SelectRegisterButton_Click(object sender, RoutedEventArgs e)
        {
            SupportMethods.UpdateVisibilityAndButtonBackground(RegisterPanel, LoginPanel, SelectRegisterButton, SelectLoginButton);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ValidationMethods.validationErrors.Clear();
            var login = LoginUsernameTextBox.Text;
            var password = ValidationMethods.SecureStringToString(LoginPasswordBox.SecurePassword);

            bool isLoginValid = ValidationMethods.ValidateAndSetErrorBorder(LoginUsernameTextBox, ValidationMethods.ValidateLogin);
            bool isPasswordValid = ValidationMethods.ValidateAndSetErrorBorder(LoginPasswordBox, ValidationMethods.ValidatePassword);

            if (!isLoginValid || !isPasswordValid)
            {
                MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
                return;
            }

            User loginUser = DBMethods.GetUserByLogin(login);

            if (loginUser == null)
            {
                MessageBox.Show("Пользователь с таким логином отсутствует.", "Ошибка!", MessageBoxButton.OK);
                ValidationMethods.SetErrorBorder(LoginUsernameTextBox);
            }
            else if (loginUser.Password != password)
            {
                MessageBox.Show("Неверный пароль.", "Ошибка!", MessageBoxButton.OK);
                ValidationMethods.SetErrorBorder(LoginPasswordBox);
            }
            else
            {
                ValidationMethods.ClearErrorBorder(LoginUsernameTextBox);
                ValidationMethods.ClearErrorBorder(LoginPasswordBox);

                App.GlobalResources.UserSignedIn = loginUser;

                var thisWindow = Window.GetWindow(this);
                var mainAppWindow = new MainAppWindow(thisWindow);
                mainAppWindow.Show();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            ValidationMethods.validationErrors.Clear();

            isValid &= ValidationMethods.ValidateAndSetErrorBorder(RegisterPasswordBox, ValidationMethods.ValidatePassword);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(FirstNameTextBox, ValidationMethods.ValidateName);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(LastNameTextBox, ValidationMethods.ValidateLastName);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(RegisterUsernameTextBox, ValidationMethods.ValidateLogin);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(EmailTextBox, ValidationMethods.ValidateEmail);
            isValid &= ValidationMethods.ValidateAndSetErrorBorder(PhoneNumberTextBox, ValidationMethods.ValidatePhoneNumber);

            bool isValidUniq = ValidationMethods.AreFieldsUnique(RegisterUsernameTextBox, EmailTextBox, PhoneNumberTextBox);

            if (isValid && isValidUniq)
            {
                User NewUser = new()
                {
                    Name = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Login = RegisterUsernameTextBox.Text,
                    Email = EmailTextBox.Text,
                    Phone = PhoneNumberTextBox.Text,
                    Password = RegisterPasswordBox.Password
                };

                App.GlobalResources._dbContext.Users.Add(NewUser);
                App.GlobalResources._dbContext.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно");
            }
            else
            {
                MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
            }
        }

        private void LoginPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}