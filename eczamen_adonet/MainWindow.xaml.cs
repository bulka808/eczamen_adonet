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
using System.Configuration;
using Microsoft.Data.Sql;


namespace eczamen_adonet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookService bookService;
        public MainWindow()
        {
            InitializeComponent();
            bookService = new BookService();
            foreach(var book in bookService.GetAllBooks())
            {
                BooksListBox.Items.Add(book.Title);
            }
            
        }
        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            var book = new Book
            {
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                Genre = GenreTextBox.Text,
                Pages = Convert.ToInt32(PagesTextBox.Text),
                Publisher = PublisherTextBox.Text,
                Year = Convert.ToInt32(YearTextBox.Text),
                Cost = Convert.ToInt32(costTextBox.Text),
                SalePrice = Convert.ToInt32(SalePriceTextBox.Text),
                IsSequel = (bool)SequelCheckBox.IsChecked,
                SequelOf = SequelOfTextBox.Text
            };

            bookService.AddBook(book);
            MessageBox.Show("Книга добавлена!");
            TitleTextBox.Text = "";
            AuthorTextBox.Text = "";
            GenreTextBox.Text = "";
            PagesTextBox.Text = "";
            PublisherTextBox.Text = "";
            YearTextBox.Text = "";
            costTextBox.Text = "";
            SalePriceTextBox.Text = "";
            SequelCheckBox.IsChecked = false;
            SequelOfTextBox.Text = "";
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
    {
        
        if (int.TryParse(idTextBox.Text, out int bookId)) 
        {
                var updatedBook = new Book
                {
                    Id = bookId,
                    Title = TitleTextBox.Text,
                    Author = AuthorTextBox.Text,
                    Publisher = PublisherTextBox.Text,
                    Pages = int.TryParse(PagesTextBox.Text, out int pages) ? pages : 0, 
                    Genre = GenreTextBox.Text,
                    Year = int.TryParse(YearTextBox.Text, out int year) ? year : 0,
                    Cost = decimal.TryParse(costTextBox.Text, out decimal cost) ? cost : 0m,
                    SalePrice = decimal.TryParse(SalePriceTextBox.Text, out decimal salePrice) ? salePrice : 0m,
                    IsSequel = (bool)SequelCheckBox.IsChecked ,
                SequelOf = SequelOfTextBox.Text
            };

            
            bool isUpdated = bookService.UpdateBook(updatedBook);

            if (isUpdated)
            {
                MessageBox.Show("Книга обновлена!");
            }
            else
            {
                MessageBox.Show("Не удалось обновить книгу. Проверьте данные.");
            }
        }
        else
        {
            MessageBox.Show("Некорректный ID книги.");
        }
    }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var books = bookService.SearchBooks(TitleTextBox.Text, AuthorTextBox.Text, GenreTextBox.Text);
            BooksListBox.ItemsSource = books;
        }




    }
}
