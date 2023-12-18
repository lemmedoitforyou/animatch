using System;
using Npgsql;
using Faker;

namespace Animatch
{
    public class Program
    {
        private static void Main(string[] args)
        {
            // DisplayAllData();

            Insert();

            Console.ReadLine();  
        }

        private static void Insert()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();

                // Очистимо дані з усіх таблиць перед додаванням нових
                ClearTables(con);

                Random random = new Random();
                int rowCount = 51;
                for (int i = 1; i < rowCount; i++)
                {
                    // Генерація випадкових даних за допомогою Faker
                    var faker = new Bogus.Faker();
                    var animeName = faker.Random.Word();
                    var animeYear = faker.Random.Number(1950, 2024);
                    var animeImdbRate = faker.Random.Double(0, 10);
                    var animeText = faker.Lorem.Paragraph();
                    string[] photoPaths = { "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/BungoStrayDogs.jpg?raw=true", "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/AtackOnTitanS1.jpg?raw=true", "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/SpyFamily.jpg?raw=true" };
                    var animePhoto = photoPaths[random.Next(photoPaths.Length)];
                    var genreName = faker.Random.Word();

                    var username = faker.Internet.UserName();
                    var password = faker.Internet.Password();
                    var email = faker.Internet.Email();
                    var name = faker.Name.FirstName();
                    var text = faker.Lorem.Sentence();
                    var photo = "https://github.com/yuliiapalamar/animatch/blob/master/animatch/AniWPF/photo/defaultUserPhoto.jpg?raw=true";
                    var level = 1;
                    var watchedcount = 1;

                    var review = faker.Lorem.Paragraph();
                    var rate = faker.Random.Number(1, 4);

                    int Id = i;

                    // Вставка даних у таблиці anime
                    InsertDataIntoAnime(con, Id, animeName, animeYear, animeImdbRate, animeText, animePhoto);

                    // Вставка даних у таблицю genres
                    InsertDataIntoGenres(con, Id, genreName);

                    // Вставка даних у таблицю userinfo
                    InsertDataIntoUserInfo(con, Id, username, password, email, name, text, photo, level, watchedcount);

                    // Вставка даних у таблицю review
                    InsertDataIntoReview(con, Id, Id, Id, review, rate);

                    // Вставка даних у таблицю animegenres
                    InsertDataIntoAnimeGenres(con, Id, Id, Id);

                    // Вставка даних у таблицю added
                    InsertDataIntoAdded(con, Id, Id, Id);

                    // Вставка даних у таблицю liked
                    InsertDataIntoLiked(con, Id, Id, Id);

                    // Вставка даних у таблицю disliked
                    InsertDataIntoDisLiked(con, Id, Id, Id);

                    // Вставка даних у таблицю watched
                    InsertDataIntoWatched(con, Id, Id, Id);
                }
            }
        }

        private static void InsertDataIntoAnime(NpgsqlConnection connection,int id, string name, int year, double imdbrate, string text, string photo)
        {
            string insertQuery = "INSERT INTO public.\"Anime\" (\"Id\", \"Name\", \"Text\", \"Imdbrate\", \"Photo\", \"Year\") VALUES (@id, @name, @text, @imdbrate, @photo, @year)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id );
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@imdbrate", imdbrate);
                command.Parameters.AddWithValue("@photo", photo);
                command.Parameters.AddWithValue("@year", year);

                command.ExecuteNonQuery();
            }
        }
        private static void ClearTables(NpgsqlConnection connection)
        {
            // Видалення даних з усіх таблиць
            ExecuteNonQuery(connection, "DELETE FROM public.\"Anime\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"Genre\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"UserInfo\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"Review\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"AnimeGenre\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"AddedAnime\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"LikedAnime\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"DislikedAnime\";");
            ExecuteNonQuery(connection, "DELETE FROM public.\"WatchedAnime\";");
        }
        private static void ExecuteNonQuery(NpgsqlConnection connection, string query)
        {
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoGenres(NpgsqlConnection connection,int id, string name)
        {
            string insertQuery = "INSERT INTO public.\"Genre\" (\"Id\",\"Name\") VALUES (@id, @name)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoUserInfo(NpgsqlConnection connection, int id, string username, string password, string email, string name, string text, string photo, int level, int watchedcount)
        {
            string insertQuery = "INSERT INTO public.\"UserInfo\"(\"Id\", \"Username\", \"Password\", \"Email\", \"Name\", \"Level\", \"Text\", \"Photo\", \"WatchedCount\") VALUES (@id, @username, @password, @email, @name, @level, @text, @photo, @watchedcount)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
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

        private static void InsertDataIntoReview(NpgsqlConnection connection,int id, int userId, int animeId, string text, int rate)
        {
            string insertQuery = "INSERT INTO public.\"Review\" (\"Id\", \"UserId\", \"AnimeId\", \"Text\", \"Rate\") VALUES (@id, @userId, @animeId, @text, @rate)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@rate", rate);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoAnimeGenres(NpgsqlConnection connection, int id, int animeId, int genreId)
        {
            string insertQuery = "INSERT INTO public.\"AnimeGenre\" (\"Id\", \"AnimeId\", \"GenreId\") VALUES (@id, @animeId, @genreId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@animeId", animeId);
                command.Parameters.AddWithValue("@genreId", genreId);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoAdded(NpgsqlConnection connection, int id, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.\"AddedAnime\" (\"Id\", \"UserId\", \"AnimeId\") VALUES (@id, @userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoLiked(NpgsqlConnection connection, int id, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.\"LikedAnime\" (\"Id\", \"UserId\", \"AnimeId\") VALUES (@id, @userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoDisLiked(NpgsqlConnection connection, int id, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.\"DislikedAnime\" (\"Id\", \"UserId\", \"AnimeId\") VALUES (@id, @userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        private static void InsertDataIntoWatched(NpgsqlConnection connection, int id, int userId, int animeId)
        {
            string insertQuery = "INSERT INTO public.\"WatchedAnime\" (\"Id\", \"UserId\", \"AnimeId\") VALUES (@id, @userId, @animeId)";

            using (NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@animeId", animeId);

                command.ExecuteNonQuery();
            }
        }

        private static void DisplayAllData()
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

        private static void DisplayAnimeData(NpgsqlConnection connection)
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

        private static void DisplayGenresData(NpgsqlConnection connection)
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

        private static void DisplayUserInfoData(NpgsqlConnection connection)
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

        private static void DisplayReviewData(NpgsqlConnection connection)
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

        private static void DisplayAddedData(NpgsqlConnection connection)
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

        private static void DisplayWatchedData(NpgsqlConnection connection)
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

        private static void DisplayLikedData(NpgsqlConnection connection)
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

        private static void DisplayDislikedData(NpgsqlConnection connection)
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

        private static void DisplayAnimeGenresData(NpgsqlConnection connection)
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
