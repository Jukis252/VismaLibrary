using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Visma_dotnet
{
    public class Program
    {
        static void Main(string[] args)
        {
            Functions functions = new Functions();
            Program filterCycle = new Program();
            List<LibraryBooks> booksNew = new List<LibraryBooks>();
            List<PeopleWithBooks> people = new List<PeopleWithBooks>();
            people = MainCycle(functions, filterCycle, booksNew, people);
        }

        private static List<PeopleWithBooks> MainCycle(Functions functions, Program filterCycle, List<LibraryBooks> booksNew, List<PeopleWithBooks> people)
        {
            bool works = true;
            string option;
            while (works)
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("1. Add book to library");
                Console.WriteLine("2. Show all books in library");
                Console.WriteLine("3. Take the book");
                Console.WriteLine("4. Show all taken books");
                Console.WriteLine("5. Return book");
                Console.WriteLine("6. Show books by filter");
                Console.WriteLine("7. Remove book from library");
                Console.WriteLine("8. Exit");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Choose what to do: ");
                option = Console.ReadLine();
                if (option == "1")
                {
                    functions.AddBook(booksNew);
                    functions.ToAddBookToLibrary(booksNew);
                }
                else if (option == "2")
                {
                    List<LibraryBooks> booksExist = functions.LoadBooksFromJson();
                    functions.ShowAllBooks(booksExist);
                }
                else if (option == "3")
                {
                    List<PeopleWithBooks> peopleExist = functions.LoadPeopleFromJson();
                    string name = functions.BookCount(peopleExist);
                    if (name != "false")
                    {
                        people = functions.TakeTheBook(name);
                        functions.ToAddPeopleWithBooks(people);
                    }
                    else
                    {

                    }

                }
                else if (option == "4")
                {
                    List<PeopleWithBooks> peopleExist = functions.LoadPeopleFromJson();
                    List<LibraryBooks> booksExist = functions.LoadBooksFromJson();
                    functions.ShowAllTakenBooks(booksExist, peopleExist);
                }
                else if (option == "5")
                {
                    List<PeopleWithBooks> peopleExist = functions.LoadPeopleFromJson();
                    functions.ReturnBook(peopleExist);
                }
                else if (option == "6")
                {
                    filterCycle.Filter(functions);
                }
                else if (option == "7")
                {
                    List<LibraryBooks> books = functions.LoadBooksFromJson();
                    functions.ShowAllBooks(books);
                    functions.ToRemoveBookFromLibrary(books);
                }
                else if (option == "8")
                {
                    works = false;
                }

            }

            return people;
        }

        public void Filter(Functions functions)
        {
            int i = 1;
            List<LibraryBooks> books = functions.LoadBooksFromJson();
            List<PeopleWithBooks> peopleExist = functions.LoadPeopleFromJson();
            int choice = 0;
            bool goes = true;
            while (goes)
            {
                Console.WriteLine("Choose filter:");
                Console.WriteLine("1. By Author");
                Console.WriteLine("2. By Category");
                Console.WriteLine("3. By Language");
                Console.WriteLine("4. By ISBN");
                Console.WriteLine("5. By Name");
                Console.WriteLine("6. Taken books");
                Console.WriteLine("7. Exit");
                choice = Int32.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Input authors full name(first name and last name");
                    string author = Console.ReadLine();
                    foreach (var book in books)
                    {
                        if (book.Author == author)
                        {
                            Console.WriteLine(i + " " + book.Name + " " + book.Author + " " + book.Category + " " + book.Language + " " + book.PublicationDate + " " + book.ISBN);
                            i++;
                            goes = false;
                        }
                    }
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Input category");
                    string category = Console.ReadLine();
                    foreach (var book in books)
                    {
                        if (book.Category == category)
                        {
                            Console.WriteLine(i + " " + book.Name + " " + book.Author + " " + book.Category + " " + book.Language + " " + book.PublicationDate + " " + book.ISBN);
                            i++;
                            goes = false;
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Input language");
                    string language = Console.ReadLine();
                    foreach (var book in books)
                    {
                        if (book.Language == language)
                        {
                            Console.WriteLine(i + " " + book.Name + " " + book.Author + " " + book.Category + " " + book.Language + " " + book.PublicationDate + " " + book.ISBN);
                            i++;
                            goes = false;
                        }
                    }
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Input ISBN");
                    string ISBN = Console.ReadLine();
                    foreach (var book in books)
                    {
                        if (book.ISBN == ISBN)
                        {
                            Console.WriteLine(i + " " + book.Name + " " + book.Author + " " + book.Category + " " + book.Language + " " + book.PublicationDate + " " + book.ISBN);
                            i++;
                            goes = false;
                        }
                    }
                }
                else if (choice == 5)
                {
                    Console.WriteLine("Input books name");
                    string nameBook = Console.ReadLine();
                    foreach (var book in books)
                    {
                        if (book.Name == nameBook)
                        {
                            Console.WriteLine(i + " " + book.Name + " " + book.Author + " " + book.Category + " " + book.Language + " " + book.PublicationDate + " " + book.ISBN);
                            i++;
                            goes = false;
                        }
                    }
                }
                else if (choice == 6)
                {
                    functions.ShowAllTakenBooks(books, peopleExist);
                    goes = false;
                }
                else if (choice == 7)
                {
                    goes = false;
                }
            }
        }
    }
}