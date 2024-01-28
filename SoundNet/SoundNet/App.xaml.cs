using Microsoft.EntityFrameworkCore;
using SoundNet.Classes;
using SoundNet.EFCore.Entities;
using System.Windows;

namespace SoundNet
{
    public partial class App : Application
    {
        public static class GlobalResources
        {
            public static readonly AppDbContext _dbContext = new(new DbContextOptions<AppDbContext>());
            public static User UserSignedIn = new();
        }
    }
}