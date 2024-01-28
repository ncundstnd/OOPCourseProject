using Microsoft.EntityFrameworkCore;
using SoundNet.Classes.Interfaces;
using SoundNet.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoundNet.Classes
{
    public static class DBMethods
    {
        public static User GetUserByLogin(string login)
        {
            User user = App.GlobalResources._dbContext.Users.FirstOrDefault(u => u.Login == login);
            return user;
        }

        public static User GetUserById(Guid id)
        {
            User user = App.GlobalResources._dbContext.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public static Playlists GetPlaylistById(Guid id)
        {
            Playlists Playlist = App.GlobalResources._dbContext.Playlists.FirstOrDefault(u => u.Id == id);
            return Playlist;
        }

        public static Albums GetAlbumById(Guid id)
        {
            Albums Album = App.GlobalResources._dbContext.Albums.FirstOrDefault(u => u.Id == id);
            return Album;
        }

        public static Audio GetAudioById(Guid id)
        {
            Audio Audio = App.GlobalResources._dbContext.Music.FirstOrDefault(u => u.Id == id);
            return Audio;
        }

        public static List<IMedia> GetMedia()
        {
            var albums = App.GlobalResources._dbContext.Albums.ToList();
            var audios = App.GlobalResources._dbContext.Music.ToList();
            var mediaList = albums.Cast<IMedia>().Concat(audios).ToList();

            return mediaList;
        }

        public static List<Audio> LoadMusicData()
        {
            List<Audio> audios = App.GlobalResources._dbContext.Music.ToList();
            return audios;
        }

        public static List<User> LoadAuthorData()
        {
            List<User> users = App.GlobalResources._dbContext.Users.ToList();
            return users;
        }

        public static List<Albums> LoadAlbumData()
        {
            List<Albums> albums = App.GlobalResources._dbContext.Albums.ToList();
            return albums;
        }

        public static List<Playlists> LoadPlaylistData()
        {
            List<Playlists> playlists = App.GlobalResources._dbContext.Playlists.ToList();
            return playlists;
        }

        public static List<Audio> LoadMusicDataForUser(User user)
        {
            List<Audio> audios = App.GlobalResources._dbContext.FavoritesAudio
                .Where(favorite => favorite.User.Id == user.Id)
                .Select(favorite => favorite.Audio)
                .ToList();
            return audios;
        }

        public static List<User> LoadAuthorDataForUser(User user)
        {
            List<User> audios = App.GlobalResources._dbContext.UserSubscription
                .Where(favorite => favorite.SubscriberId == user.Id)
                .Select(favorite => favorite.Subscription)
                .ToList();
            return audios;
        }

        public static List<Albums> LoadAlbumDataForUser(User user)
        {
            List<Albums> audios = App.GlobalResources._dbContext.FavoritesAlbum
                .Where(favorite => favorite.User.Id == user.Id)
                .Select(favorite => favorite.Album)
                .ToList();
            return audios;
        }

        public static List<Playlists> LoadPlaylistDataForUser(User user)
        {
            List<Playlists> audios = App.GlobalResources._dbContext.FavoritesPlaylist
                .Where(favorite => favorite.User.Id == user.Id)
                .Select(favorite => favorite.Playlist)
                .ToList();
            return audios;
        }

        public static List<IMedia> SearchMedia(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            var albums = App.GlobalResources._dbContext.Albums.Where(a => a.Name.ToLower().Contains(searchTerm)).ToList();
            var audios = App.GlobalResources._dbContext.Music.Where(a => a.Name.ToLower().Contains(searchTerm)).ToList();
            var playlists = App.GlobalResources._dbContext.Playlists.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            var filteredMediaList = new List<IMedia>();

            if (searchTerm.Length > 2)
            {
                for (int i = searchTerm.Length; i > 2; i--)
                {
                    var modifiedSearchTerm = SupportMethods.RemoveCharacters(searchTerm, i);

                    var filteredAlbums = App.GlobalResources._dbContext.Albums.Where(a => a.Name.ToLower().Contains(modifiedSearchTerm)).ToList();
                    var filteredAudios = App.GlobalResources._dbContext.Music.Where(a => a.Name.ToLower().Contains(modifiedSearchTerm)).ToList();
                    var filteredPlaylists = App.GlobalResources._dbContext.Playlists.Where(p => p.Name.ToLower().Contains(modifiedSearchTerm)).ToList();

                    filteredMediaList.AddRange(filteredAlbums.Cast<IMedia>());
                    filteredMediaList.AddRange(filteredAudios.Cast<IMedia>());
                    filteredMediaList.AddRange(filteredPlaylists.Cast<IMedia>());
                }
            }

            var filteredAlbumsWithTwoChars = App.GlobalResources._dbContext.Albums.Where(a => a.Name.ToLower().Contains(searchTerm)).ToList();
            var filteredAudiosWithTwoChars = App.GlobalResources._dbContext.Music.Where(a => a.Name.ToLower().Contains(searchTerm)).ToList();
            var filteredPlaylistsWithTwoChars = App.GlobalResources._dbContext.Playlists.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            filteredMediaList.AddRange(filteredAlbumsWithTwoChars.Cast<IMedia>());
            filteredMediaList.AddRange(filteredAudiosWithTwoChars.Cast<IMedia>());
            filteredMediaList.AddRange(filteredPlaylistsWithTwoChars.Cast<IMedia>());

            return filteredMediaList.Distinct().ToList();
        }

        public static List<User> SearchAuthors(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();

            var playlists = App.GlobalResources._dbContext.Users.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            var filteredAuthorList = new List<User>();

            if (searchTerm.Length > 2)
            {
                for (int i = searchTerm.Length; i > 2; i--)
                {
                    var modifiedSearchTerm = SupportMethods.RemoveCharacters(searchTerm, i);

                    var filteredUsers = App.GlobalResources._dbContext.Users.Where(p => p.Name.ToLower().Contains(modifiedSearchTerm)).ToList();

                    filteredAuthorList.AddRange(filteredUsers.Cast<User>());
                }
            }

            var filteredUsersWithTwoChars = App.GlobalResources._dbContext.Users.Where(p => p.Name.ToLower().Contains(searchTerm)).ToList();

            filteredAuthorList.AddRange(filteredUsersWithTwoChars.Cast<User>());
            filteredAuthorList.Remove(App.GlobalResources.UserSignedIn);
            filteredAuthorList.RemoveAll(user => user.Role != 2);
            return filteredAuthorList.Distinct().ToList();
        }

        public static void AddPlaylistToFavorites(Playlists playlist)
        {
            // Проверка, что плейлист ещё не добавлен в избранное
            if (!IsPlaylistInFavorites(playlist))
            {
                // Создание новой записи в избранном
                FavoritesPlaylist favoritesPlaylist = new();
                favoritesPlaylist.PlaylistId = playlist.Id;
                favoritesPlaylist.UserId = App.GlobalResources.UserSignedIn.Id;

                App.GlobalResources._dbContext.FavoritesPlaylist.Add(favoritesPlaylist);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static void RemovePlaylistFromFavorites(Playlists playlist)
        {
            // Поиск записи об избранном плейлисте
            var favoritesPlaylist = App.GlobalResources._dbContext.FavoritesPlaylist
                .FirstOrDefault(favorite => favorite.PlaylistId == playlist.Id && favorite.UserId == App.GlobalResources.UserSignedIn.Id);

            // Проверка, что запись об избранном плейлисте найдена
            if (favoritesPlaylist != null)
            {
                // Удаление из списка избранных
                App.GlobalResources._dbContext.FavoritesPlaylist.Remove(favoritesPlaylist);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static void AddAlbumToFavorites(Albums album)
        {
            // Проверка, что альбом ещё не добавлен в избранное
            if (!IsAlbumInFavorites(album))
            {
                // Создание новой записи в избранном
                FavoritesAlbum favoritesAlbum = new();
                favoritesAlbum.AlbumId = album.Id;
                favoritesAlbum.UserId = App.GlobalResources.UserSignedIn.Id;

                App.GlobalResources._dbContext.FavoritesAlbum.Add(favoritesAlbum);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static void RemoveAlbumFromFavorites(Albums album)
        {
            // Поиск записи об избранном альбоме
            var favoritesAlbum = App.GlobalResources._dbContext.FavoritesAlbum
                .FirstOrDefault(favorite => favorite.AlbumId == album.Id && favorite.UserId == App.GlobalResources.UserSignedIn.Id);

            // Проверка, что запись об избранном альбоме найдена
            if (favoritesAlbum != null)
            {
                // Удаление из списка избранных
                App.GlobalResources._dbContext.FavoritesAlbum.Remove(favoritesAlbum);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static bool IsPlaylistInFavorites(Playlists playlist)
        {
            // Проверка, находится ли плейлист в списке избранных
            return App.GlobalResources._dbContext.FavoritesPlaylist
                .Any(favorite => favorite.PlaylistId == playlist.Id && favorite.UserId == App.GlobalResources.UserSignedIn.Id);
        }

        public static bool IsAlbumInFavorites(Albums album)
        {
            // Проверка, находится ли альбом в списке избранных
            return App.GlobalResources._dbContext.FavoritesAlbum
                .Any(favorite => favorite.AlbumId == album.Id && favorite.UserId == App.GlobalResources.UserSignedIn.Id);
        }

        public static void AddAuthorToSubscriptions(User author)
        {
            // Проверка, что автор ещё не добавлен в подписки
            if (!IsAuthorInSubscriptions(author))
            {
                // Создание новой записи о подписке
                UserSubscription subscription = new();
                subscription.SubscriptionId = author.Id;
                subscription.SubscriberId = App.GlobalResources.UserSignedIn.Id;

                App.GlobalResources._dbContext.UserSubscription.Add(subscription);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static void RemoveAuthorFromSubscriptions(User author)
        {
            // Поиск записи о подписке
            var subscription = App.GlobalResources._dbContext.UserSubscription
                .FirstOrDefault(sub => sub.SubscriptionId == author.Id && sub.SubscriberId == App.GlobalResources.UserSignedIn.Id);

            // Проверка, что запись о подписке найдена
            if (subscription != null)
            {
                // Удаление из списка подписок
                App.GlobalResources._dbContext.UserSubscription.Remove(subscription);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static bool IsAuthorInSubscriptions(User author)
        {
            // Проверка, находится ли автор в списке подписок
            return App.GlobalResources._dbContext.UserSubscription
                .Any(sub => sub.SubscriptionId == author.Id && sub.SubscriberId == App.GlobalResources.UserSignedIn.Id);
        }

        public static void AddAudioToFavorites(Audio audio)
        {
            // Проверка, что аудиозапись ещё не добавлена в избранное
            if (!IsAudioInFavorites(audio))
            {
                // Создание новой записи в избранном
                FavoritesAudio favoritesAudio = new();
                favoritesAudio.AudioId = audio.Id;
                favoritesAudio.UserId = App.GlobalResources.UserSignedIn.Id;

                App.GlobalResources._dbContext.FavoritesAudio.Add(favoritesAudio);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static void RemoveAudioFromFavorites(Audio audio)
        {
            // Поиск записи об избранной аудиозаписи
            var favoritesAudio = App.GlobalResources._dbContext.FavoritesAudio
                .FirstOrDefault(favorite => favorite.AudioId == audio.Id && favorite.UserId == App.GlobalResources.UserSignedIn.Id);

            // Проверка, что запись об избранной аудиозаписи найдена
            if (favoritesAudio != null)
            {
                // Удаление из списка избранных
                App.GlobalResources._dbContext.FavoritesAudio.Remove(favoritesAudio);
                App.GlobalResources._dbContext.SaveChanges();
            }
        }

        public static bool IsAudioInFavorites(Audio audio)
        {
            // Проверка, находится ли аудиозапись в списке избранных
            return App.GlobalResources._dbContext.FavoritesAudio
                .Any(favorite => favorite.AudioId == audio.Id && favorite.UserId == App.GlobalResources.UserSignedIn.Id);
        }
    }
}