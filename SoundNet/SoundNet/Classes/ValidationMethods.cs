using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SoundNet.Classes
{
    public static class ValidationMethods
    {
        public static List<string> validationErrors = new List<string>();

        public static void SetErrorBorder(Control control)
        {
            control.BorderBrush = Brushes.Red;
        }

        public static void ClearErrorBorder(Control control)
        {
            control.BorderBrush = Brushes.Green;
        }

        public static bool ValidateAndSetErrorBorder(TextBoxBase textBox, Func<string, bool> validator)
        {
            var isValid = validator((textBox as TextBox)?.Text ?? string.Empty);
            if (!isValid)
            {
                SetErrorBorder(textBox);
            }
            else
            {
                ClearErrorBorder(textBox);
            }
            return isValid;
        }

        public static bool ValidateAndSetErrorBorder(PasswordBox passwordBox, Func<string, bool> validator)
        {
            var isValid = validator(passwordBox.Password);
            if (!isValid)
            {
                SetErrorBorder(passwordBox);
            }
            else
            {
                ClearErrorBorder(passwordBox);
            }
            return isValid;
        }

        public static bool ValidateLogin(string login)
        {
            if (login.Length < 3)
            {
                validationErrors.Add("Логин должен содержать не менее 3 символов");
                return false;
            }

            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            {
                validationErrors.Add("Логин должен содержать только английские символы и цифры");
                return false;
            }

            return true;
        }

        public static bool ValidateSongName(string songname)
        {
            if (songname.Length < 3)
            {
                validationErrors.Add("Название песни должно содержать не менее 3 символов");
                return false;
            }
            return true;
        }

        public static bool ValidatePhoneNumber(string phoneNumber)
        {
            var pattern = @"^\+375\d{9}$";
            var match = Regex.Match(phoneNumber, pattern);
            if (!match.Success)
            {
                validationErrors.Add("Номер телефона должен быть в формате +375XXXXXXXXX");
                return false;
            }
            return true;
        }

        public static bool ValidateEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    validationErrors.Add("Введите Email");
                    return false;
                }
                else if (Regex.IsMatch(email, @"^[а-яА-Я]+$"))
                {
                    validationErrors.Add("Адрес электронной почты не может содержать буквы русского алфавита");
                    return false;
                }
                else
                {
                    var mailAddress = new MailAddress(email);
                    return true;
                }
            }
            catch (FormatException ex)
            {
                if (ex.Message == "The specified string is not in the form required for an e-mail address.")
                {
                    validationErrors.Add($"Неверный формат адреса электронной почты.");
                }
                else
                {
                    validationErrors.Add("В заголовке адреса электронной почты обнаружен недопустимый символ");
                }

                return false;
            }
        }

        public static bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                validationErrors.Add("Пароль должен содержать не менее 8 символов");
                return false;
            }

            if (Regex.IsMatch(password, @"\p{IsCyrillic}"))
            {
                validationErrors.Add("Пароль не должен содержать символы кириллицы");
                return false;
            }

            return true;
        }

        public static bool ValidateName(string name)
        {
            if (name.Length < 3)
            {
                validationErrors.Add("Имя должно содержать не менее 3 символов");
                return false;
            }

            if (!Regex.IsMatch(name, @"^[А-Я][а-я]+$"))
            {
                validationErrors.Add("Имя должно начинаться с 1 заглавной буквы и содержать только буквы русского алфавита");
                return false;
            }

            // Проверка, что заглавная буква встречается только один раз
            if (name.Count(char.IsUpper) != 1)
            {
                validationErrors.Add("Имя должно содержать только одну заглавную букву");
                return false;
            }

            return true;
        }

        public static bool ValidateLastName(string lastname)
        {
            if (lastname.Length < 3)
            {
                validationErrors.Add("Фамилия должна содержать не менее 3 символов");
                return false;
            }

            if (!Regex.IsMatch(lastname, @"^[А-Я][а-я]+$"))
            {
                validationErrors.Add("Фамилия должна начинаться с 1 заглавной буквы и содержать только буквы русского алфавита");
                return false;
            }

            // Проверка, что заглавная буква встречается только один раз
            if (lastname.Count(char.IsUpper) != 1)
            {
                validationErrors.Add("Фамилия должна содержать только одну заглавную букву");
                return false;
            }

            return true;
        }

        public static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static bool AreFieldsUnique(TextBox login, TextBox email, TextBox phoneNumber)
        {
            if (!IsFieldUnique(login, u => u.Login == login.Text, "Пользователь с таким логином уже зарегистрирован.", ValidateLogin))
                return false;

            if (!IsFieldUnique(email, u => u.Email == email.Text, "Пользователь с такой электронной почтой уже зарегистрирован.", ValidateEmail))
                return false;

            if (!IsFieldUnique(phoneNumber, u => u.Phone == phoneNumber.Text, "Пользователь с таким номером телефона уже зарегистрирован.", ValidatePhoneNumber))
                return false;

            return true;
        }

        private static bool IsFieldUnique(TextBox field, Func<User, bool> predicate, string errorMessage, Func<string, bool> validationMethod)
        {
            // Materialize the results by using ToList()
            var users = App.GlobalResources._dbContext.Users.ToList();

            if (users.Any(u => predicate(u)))
            {
                SetErrorBorder(field);
                validationErrors.Add(errorMessage);
                return false;
            }

            if (validationMethod != null)
            {
                if (ValidateAndSetErrorBorder(field, validationMethod))
                    field.BorderBrush = Brushes.Green;
                else
                    return false;
            }

            return true;
        }

        public static bool AreFieldsUniqueChange(TextBox login, TextBox email, TextBox phoneNumber)
        {
            if (!IsFieldUniqueChange(login, u => u.Login == login.Text, "Пользователь с таким логином уже зарегистрирован.", ValidateLogin))
                return false;

            if (!IsFieldUniqueChange(email, u => u.Email == email.Text, "Пользователь с такой электронной почтой уже зарегистрирован.", ValidateEmail))
                return false;

            if (!IsFieldUniqueChange(phoneNumber, u => u.Phone == phoneNumber.Text, "Пользователь с таким номером телефона уже зарегистрирован.", ValidatePhoneNumber))
                return false;

            return true;
        }

        public static bool AreFieldsUniqueChange(TextBox login, TextBox email, TextBox phoneNumber, User selectedUser)
        {
            if (!IsFieldUniqueChange(login, u => u.Login == login.Text, "Пользователь с таким логином уже зарегистрирован.", ValidateLogin, selectedUser))
                return false;

            if (!IsFieldUniqueChange(email, u => u.Email == email.Text, "Пользователь с такой электронной почтой уже зарегистрирован.", ValidateEmail, selectedUser))
                return false;

            if (!IsFieldUniqueChange(phoneNumber, u => u.Phone == phoneNumber.Text, "Пользователь с таким номером телефона уже зарегистрирован.", ValidatePhoneNumber, selectedUser))
                return false;

            return true;
        }

        private static bool IsFieldUniqueChange(TextBox field, Func<User, bool> predicate, string errorMessage, Func<string, bool> validationMethod)
        {
            // Materialize the results by using ToList()
            var users = App.GlobalResources._dbContext.Users.ToList();
            users.Remove(App.GlobalResources.UserSignedIn);
            if (users.Any(u => predicate(u)))
            {
                SetErrorBorder(field);
                validationErrors.Add(errorMessage);
                return false;
            }

            if (validationMethod != null)
            {
                if (ValidateAndSetErrorBorder(field, validationMethod))
                    field.BorderBrush = Brushes.Green;
                else
                    return false;
            }

            return true;
        }

        private static bool IsFieldUniqueChange(TextBox field, Func<User, bool> predicate, string errorMessage, Func<string, bool> validationMethod, User selectedUser)
        {
            // Materialize the results by using ToList()
            var users = App.GlobalResources._dbContext.Users.ToList();
            users.Remove(selectedUser);
            if (users.Any(u => predicate(u)))
            {
                SetErrorBorder(field);
                validationErrors.Add(errorMessage);
                return false;
            }

            if (validationMethod != null)
            {
                if (ValidateAndSetErrorBorder(field, validationMethod))
                    field.BorderBrush = Brushes.Green;
                else
                    return false;
            }

            return true;
        }
    }
}