using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Linq;
using System.Windows;

namespace SoundNet
{
    public partial class ChangeUser : Window
    {
        private User ChangedUser = new();

        public ChangeUser(User changedUser)
        {
            InitializeComponent();
            ChangedUser = changedUser;

            NameTextBox.Text = changedUser.Name;
            LastNameTextBox.Text = changedUser.LastName;
            LoginTextBox.Text = changedUser.Login;
            EmailTextBox.Text = changedUser.Email;
            PhoneTextBox.Text = changedUser.Phone;
            RoleTextBox.Text = changedUser.Role.ToString();
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isValid = true;
                ValidationMethods.validationErrors.Clear();

                if (ChangedUser != null)
                {
                    // Проверяем существование пользователя в базе данных
                    bool userExists = App.GlobalResources._dbContext.Users.Any(u => u.Id == ChangedUser.Id);

                    if (userExists)
                    {
                        isValid &= ValidationMethods.ValidateAndSetErrorBorder(NameTextBox, ValidationMethods.ValidateName);
                        isValid &= ValidationMethods.ValidateAndSetErrorBorder(LastNameTextBox, ValidationMethods.ValidateLastName);
                        isValid &= ValidationMethods.ValidateAndSetErrorBorder(LoginTextBox, ValidationMethods.ValidateLogin);
                        isValid &= ValidationMethods.ValidateAndSetErrorBorder(EmailTextBox, ValidationMethods.ValidateEmail);
                        isValid &= ValidationMethods.ValidateAndSetErrorBorder(PhoneTextBox, ValidationMethods.ValidatePhoneNumber);

                        bool isValidUniq = ValidationMethods.AreFieldsUniqueChange(LoginTextBox, EmailTextBox, PhoneTextBox, ChangedUser);

                        if (isValid && isValidUniq)
                        {
                            if (int.Parse(RoleTextBox.Text) > 3 || int.Parse(RoleTextBox.Text) < 1)
                            {
                                MessageBox.Show("Роль может быть в пределах от 1 до 3\n1 - Слушатель\n2 - Исполнитель\n3 - Администратор");
                            }
                            else
                            {
                                ChangedUser.Name = NameTextBox.Text;
                                ChangedUser.LastName = LastNameTextBox.Text;
                                ChangedUser.Login = LoginTextBox.Text;
                                ChangedUser.Email = EmailTextBox.Text;
                                ChangedUser.Phone = PhoneTextBox.Text;
                                ChangedUser.Role = int.Parse(RoleTextBox.Text);

                                App.GlobalResources._dbContext.Users.Update(ChangedUser);
                                App.GlobalResources._dbContext.SaveChanges();

                                MessageBox.Show("Изменения сохранены");
                            }
                        }
                        else
                        {
                            MessageBox.Show(string.Join("\n", ValidationMethods.validationErrors), "Ошибки валидации");
                        }
                    }
                    else
                    {
                        // Пользователь не найден в базе данных. Возможно, он был удален.
                        MessageBox.Show("Пользователь не найден в базе данных. Невозможно сохранить изменения.");
                    }
                }
                else
                {
                    // ChangedUser равен null. Возможно, у вас есть какая-то логика обработки этого случая.
                    MessageBox.Show("Пользователь не найден. Невозможно сохранить изменения.");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки при сохранении изменений
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}