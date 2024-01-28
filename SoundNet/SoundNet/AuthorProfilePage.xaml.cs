using Microsoft.EntityFrameworkCore;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System;
using System.Windows;

namespace SoundNet
{
    public partial class AuthorProfilePage : Window
    {

        private ByteToImageConverter converter = new ByteToImageConverter();
        private User Author = new();

        public AuthorProfilePage(User user)
        {
            InitializeComponent();

            Author = user;
            DataContext = user;
            if (Author.Description != null)
                authorDescription.Text = Author.Description;
            authorImage.Source = converter.ConvertToImage(Author.Avatar);
            authorName.Text = Author.Name + " " + Author.LastName;
        }

        public AuthorProfilePage(Guid id)
        {
            InitializeComponent();

            Author = DBMethods.GetUserById(id);
            DataContext = Author;
            if (Author.Description != null)
                authorDescription.Text = Author.Description;
            authorImage.Source = converter.ConvertToImage(Author.Avatar);
            authorName.Text = Author.Name + " " + Author.LastName;

            label.Visibility = Visibility.Collapsed;
            DescriptionTextBox.Visibility = Visibility.Collapsed;
            ChangeButton.Visibility = Visibility.Collapsed;
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DescriptionTextBox.Text != "")
            {
                authorDescription.Text = DescriptionTextBox.Text;
                Author.Description = DescriptionTextBox.Text;
                App.GlobalResources._dbContext.Users.Update(Author);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }
    }
}