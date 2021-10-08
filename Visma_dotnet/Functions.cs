using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Visma_dotnet
{
    public class Functions : WorkingWithFiles
    {
        public List<LibraryBooks> AddBook(List<LibraryBooks> books)
        { 
            Console.WriteLine("Input books name, authors name, category, language, publication date, ISBN");
            books.Add( new LibraryBooks {Name = Console.ReadLine(), Author = Console.ReadLine(), Category = Console.ReadLine(), Language=Console.ReadLine(), PublicationDate=Console.ReadLine(), ISBN=Console.ReadLine()});
            return books;
        }

        public void ShowAllBooks(List<LibraryBooks> items)
        {
            int i = 1;
            foreach (var book in items)
            {
                if (book.Name == "")
                {
                    
                }
                else
                {
                    Console.WriteLine(i+" "+book.Name+" "+book.Author+" "+book.Category+" "+book.Language+" "+book.PublicationDate+" "+book.ISBN);
                    i++;
                }

            }
        }
        public List<PeopleWithBooks> TakeTheBook(string name)
        {
            Console.WriteLine("Input your name: ");
            List<PeopleWithBooks> people = new List<PeopleWithBooks>();
            Console.WriteLine("For how many days are you taking the book ? (No longer than 2 months - 60 days)");
            int duration = Int32.Parse(Console.ReadLine());
            DateTime today = DateTime.Today;
            DateTime toReturn = DateTime.Today.AddDays(duration);
            if(duration > 60)
            {
                Console.WriteLine("Can't take book for that long");
            }
            else
            {
                Console.WriteLine("Choose book by entering the books ISBN: ");
                List<LibraryBooks> items = LoadBooksFromJson();
                ShowAllBooks(items);
                string bookISBN = Console.ReadLine();
                people.Add(new PeopleWithBooks { Name = name, Today = today, ToReturn = toReturn, BooksISBN = bookISBN });
                return people;
            }
            return people;

        }

        public void ShowAllTakenBooks (List<LibraryBooks> books, List<PeopleWithBooks> people)
        {
            int i=1;
            foreach(var code in people)
            {
                foreach(var book in books)
                {
                    if(code.BooksISBN == book.ISBN)
                    {
                        Console.WriteLine(i+" "+book.Name+" "+book.Author+" "+book.Category+" "+book.Language+" "+book.PublicationDate+" "+book.ISBN+" book is taken by "+code.Name);
                        i++;
                    }
                }
            }
        }

        public void ReturnBook(List<PeopleWithBooks> people)
        {
            string returnISBN = "";
            DateTime returnDate = DateTime.Today;
            List<LibraryBooks> books = LoadBooksFromJson();
            Console.WriteLine("Input your name");
            string name = Console.ReadLine();
            foreach(var person in people)
            {
                if(person.Name == name)
                {
                    Console.WriteLine("Input books ISBN to return it");
                    ShowAllTakenBooks(books,people);
                    returnISBN = Console.ReadLine();
                    int result = person.ToReturn.CompareTo(returnDate);
                    if(result > 0)
                    {
                        Console.WriteLine("I hate being late, but I'm so good at it");
                    }
                    else
                    {
                        Console.WriteLine("You are not late");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("You have no books to return");
                    break;
                }
            }
            foreach(var person in people)
            {
                if(person.BooksISBN == returnISBN && person.Name == name)
                {
                    person.BooksISBN = ""; person.Name=""; person.Today.ToString("");person.ToReturn.ToString("");
                    ToRemovePeopleWithBooks(people);
                    break;
                }
            }
        }
    }
}
