using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using static System.Reflection.Metadata.BlobBuilder;

namespace eczamen_adonet
{
    internal class BookService
    {
        private readonly string connectionString;

        public BookService()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["conn_string"].ToString();
        }


        public void AddBook(Book book)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Books (Title, Author, Publisher, Pages, Genre, Year, Cost, SalePrice, IsSequel, SequelOf)
                             VALUES (@Title, @Author, @Publisher, @Pages, @Genre, @Year, @Cost, @SalePrice, @IsSequel, @SequelOf)";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@Pages", book.Pages);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Year", book.Year);
                cmd.Parameters.AddWithValue("@Cost", book.Cost);
                cmd.Parameters.AddWithValue("@SalePrice", book.SalePrice);
                cmd.Parameters.AddWithValue("@IsSequel", book.IsSequel);
                cmd.Parameters.AddWithValue("@SequelOf", book.SequelOf);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool UpdateBook(Book book)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"
                UPDATE Books
                SET Title = @Title,
                    Author = @Author,
                    Publisher = @Publisher,
                    Pages = @Pages,
                    Genre = @Genre,
                    Year = @Year,
                    Cost = @Cost,
                    SalePrice = @SalePrice,
                    IsSequel = @IsSequel,
                    SequelOf = @SequelOf
                WHERE Id = @Id";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", book.Id);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@Publisher", book.Publisher);
                cmd.Parameters.AddWithValue("@Pages", book.Pages);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@Year", book.Year);
                cmd.Parameters.AddWithValue("@Cost", book.Cost);
                cmd.Parameters.AddWithValue("@SalePrice", book.SalePrice);
                cmd.Parameters.AddWithValue("@IsSequel", book.IsSequel);
                cmd.Parameters.AddWithValue("@SequelOf", book.SequelOf);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0; 
            }
        }

        public void RemoveBook(int bookId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Books WHERE Id = @Id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", bookId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Books";
                var cmd = new SqlCommand(query, conn);
                conn.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Publisher = reader["Publisher"].ToString(),
                        Pages = (int)reader["Pages"],
                        Genre = reader["Genre"].ToString(),
                        Year = (int)reader["Year"],
                        Cost = (decimal)reader["Cost"],
                        SalePrice = (decimal)reader["SalePrice"],
                        IsSequel = (bool)reader["IsSequel"],
                        SequelOf = reader["SequelOf"].ToString()
                    });

                }
            }
            return books;
        }

        public List<Book> SearchBooks(string title, string author, string genre)
        {
            List<Book> books = new List<Book>();

            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Books WHERE Title LIKE @Title OR Author LIKE @Author OR Genre LIKE @Genre";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", "%" + title + "%");
                cmd.Parameters.AddWithValue("@Author", "%" + author + "%");
                cmd.Parameters.AddWithValue("@Genre", "%" + genre + "%");

                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        Id = (int)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Author = reader["Author"].ToString(),
                        Publisher = reader["Publisher"].ToString(),
                        Pages = (int)reader["Pages"],
                        Genre = reader["Genre"].ToString(),
                        Year = (int)reader["Year"],
                        Cost = (decimal)reader["Cost"],
                        SalePrice = (decimal)reader["SalePrice"],
                        IsSequel = (bool)reader["IsSequel"],
                        SequelOf = reader["SequelOf"].ToString()
                    });
                }
            }

            return books;
        }
    }
}
