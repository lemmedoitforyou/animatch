using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace animatch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayAllData();
            //Insert();

            Console.ReadLine();
        }

        private static void Insert()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();

                Random random = new Random();
                int rowCount = 50;
                for (int i = 1; i < rowCount; i++)
                {
                    // Дані для таблиці anime
                    string animeName = "AnimeName" + i;
                    int animeYear = random.Next(1950, 2024);
                    double animeImdbRate = random.NextDouble()*10.0;
                    string animeText = "AnimeDescription" + i;
                    string animePhoto = "Path" + i;

                    // Дані для таблиці genres
                    string genreName = "GenreName" + i;

                    // Дані для таблиці userifro
                    string username = "Username" + i;
                    string password = "Password" + i;
                    string email = "user" + i + "@example.com";
                    string name = "name" + i;
                    string text = "text" + i;
                    string photo = "Path" + i;
                    int level = i;
                    int watchedcount = i;

                    // Дані для таблиці review
                    string review = "text" + i;
                    int rate = i;

                    // Дані для foreign key
                    int animeId = i + 1;
                    int genreId = i + 1;
                    int userId = i + 1;

                    // Вставка даних у таблицю anime
                    InsertDataIntoAnime(con, animeName, animeYear, animeImdbRate, animeText, animePhoto);

                    // Вставка даних у таблицю genres
                    InsertDataIntoGenres(con, genreName);

                    // Вставка даних у таблицю userinfo
                    InsertDataIntoUserInfo(con, username, password, email, name, text, photo, level, watchedcount);

                    // Вставка даних у таблицю review
                    InsertDataIntoReview(con, userId, animeId, text, rate);

                    // Вставка даних у таблицю animegenres
                    InsertDataIntoAnimeGenres(con, animeId, genreId);

                    // Вставка даних у таблицю added
                    InsertDataIntoAdded(con, userId, animeId);

                    // Вставка даних у таблицю liked
                    InsertDataIntoLiked(con, userId, animeId);

                    // Вставка даних у таблицю disliked
                    InsertDataIntoDisLiked(con, userId, animeId);

                    // Вставка даних у таблицю watched
                    InsertDataIntoWatched(con, userId, animeId);
                }
            }
        }

        static void InsertDataIntoAnime(NpgsqlConnection connection, string name, int year, double imdbrate, string text, string photo)
        {
            string insertQuery = "INSERT INTO public.anime (name, text, imdbrate, photo, year) VALUES (@name, @text, @imdbrate, @photo, @year)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@imdbrate", imdbrate);
                command.Parameters.AddWithValue("@photo", photo);
                command.Parameters.AddWithValue("@year", year);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoGenres(NpgsqlConnection connection, string name)
        {
            string insertQuery = "INSERT INTO public.genres (name) VALUES (@name)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoUserInfo(NpgsqlConnection connection, string username, string password, string email, string name, string text, string photo, int level, int watchedcount)
        {
            string insertQuery = "INSERT INTO public.userinfo(username, password, email, name, level, text, photo, watchedcount) VALUES (@username, @password, @email, @name, @level, @text, @photo, @watchedcount)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@level", level);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@photo", photo);
                command.Parameters.AddWithValue("@watchedcount", watchedcount);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoReview(NpgsqlConnection connection, int userId, int animeId, string text, int rate)
        {
            string insertQuery = "INSERT INTO public.review (user_id, anime_id, text, rate) VALUES (@userId, @animeId, @text, @rate)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@rate", rate);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoAnimeGenres(NpgsqlConnection connection, int animeId, int genreId)
        {
            string insertQuery = "INSERT INTO public.animegenres (anime_id, genre_id) VALUES (@animeId, @genreId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@animeId", animeId);
                command.Parameters.AddWithValue("@genreId", genreId);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoAdded(NpgsqlConnection connection, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.added (user_id, anime_id) VALUES (@userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoLiked(NpgsqlConnection connection, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.liked (user_id, anime_id) VALUES (@userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoDisLiked(NpgsqlConnection connection, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.disliked (user_id, anime_id) VALUES (@userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        static void InsertDataIntoWatched(NpgsqlConnection connection, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.watched (user_id, anime_id) VALUES (@userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        static void DisplayAllData()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();
                DisplayAnimeData(con);
                DisplayGenresData(con);
                DisplayUserInfoData(con);
                DisplayReviewData(con);
                DisplayAnimeGenresData(con);
                DisplayAddedData(con);
                DisplayDislikedData(con);
                DisplayLikedData(con);
                DisplayWatchedData(con);
            }
        }

        static void DisplayAnimeData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.anime";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("Anime Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Year: {reader["year"]}, IMDb Rate: {reader["imdbrate"]}");
                    }
                }
            }
        }

        static void DisplayGenresData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.genres";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nGenres Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}");
                    }
                }
            }
        }

        static void DisplayUserInfoData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.userinfo";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nUserInfo Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Username: {reader["username"]}, Email: {reader["email"]}, Level: {reader["level"]}");
                    }
                }
            }
        }

        static void DisplayReviewData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.review";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nReview Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, User ID: {reader["user_id"]}, Anime ID: {reader["anime_id"]}, Rate: {reader["rate"]}");
                    }
                }
            }
        }

        static void DisplayAddedData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.added";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nAdded Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["user_id"]}, Anime ID: {reader["anime_id"]}");
                    }
                }
            }
        }

        static void DisplayWatchedData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.watched";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nWatched Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["user_id"]}, Anime ID: {reader["anime_id"]}");
                    }
                }
            }
        }

        static void DisplayLikedData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.liked";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nLiked Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["user_id"]}, Anime ID: {reader["anime_id"]}");
                    }
                }
            }
        }

        static void DisplayDislikedData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.disliked";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nDisliked Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"User ID: {reader["user_id"]}, Anime ID: {reader["anime_id"]}");
                    }
                }
            }
        }

        static void DisplayAnimeGenresData(NpgsqlConnection connection)
        {
            string query = "SELECT * FROM public.animegenres";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\n\nAnimeGenres Table Data:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Anime ID: {reader["anime_id"]}, Genre ID: {reader["genre_id"]}");
                    }
                }
            }
        }

        //private static void Test()
        //{
        //    using(NpgsqlConnection con=GetConnection())
        //    {
        //        con.Open();
        //        if(con.State==ConnectionState.Open) 
        //        {
        //            Console.WriteLine("yes");
        //        }
        //    }    
        //}
        private static NpgsqlConnection GetConnection() 
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=yuliya2005;Database=animatch;");
        }
    }
}
