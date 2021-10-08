using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Visma_dotnet
{
    public class WorkingWithFiles
    {
        public string BookCount(List<PeopleWithBooks> people)
        {
            people = LoadPeopleFromJson();
            Console.WriteLine("Input your name: ");
            string name = Console.ReadLine();
            Dictionary<string, int> count = new Dictionary<string, int>();
            foreach (var person in people)
            {
                if (count.ContainsKey(person.Name))
                {
                    count[person.Name]++;
                }
                else
                {
                    count.Add(person.Name, 1);
                }
            }
            if (count[name] >= 3)
            {
                Console.WriteLine("Return books in order to take more");
                return "false";
            }
            else if (count[name] == 1)
            {
                return "false";
            }
            else
            {
                Console.WriteLine("You are free to take more books");
                return name;
            }
        }
        public List<LibraryBooks> LoadBooksFromJson()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\Admin\source\repos\Visma_dotnet\dataBooks.txt"))
            {
                string json = r.ReadToEnd();
                List<LibraryBooks> items = JsonConvert.DeserializeObject<List<LibraryBooks>>(json);
                r.Dispose();
                r.Close();
                return items;
            }
        }
        public List<PeopleWithBooks> LoadPeopleFromJson()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\Admin\source\repos\Visma_dotnet\dataTaken.txt"))
            {
                string json = r.ReadToEnd();
                List<PeopleWithBooks> people = JsonConvert.DeserializeObject<List<PeopleWithBooks>>(json);
                r.Dispose();
                r.Close();
                return people;
            }
        }
        public void ToAddBookToLibrary(List<LibraryBooks> books)
        {
            List<LibraryBooks> items = LoadBooksFromJson();
            items.AddRange(books);
            string json = JsonConvert.SerializeObject(items);
            TextWriter tsw = new StreamWriter(@"C:\Users\Admin\source\repos\Visma_dotnet\dataBooks.txt");
            tsw.WriteLine(json);
            tsw.Close();
        }
        public void ToAddPeopleWithBooks(List<PeopleWithBooks> people)
        {
            List<PeopleWithBooks> items = LoadPeopleFromJson();
            items.AddRange(people);
            string json = JsonConvert.SerializeObject(items);
            TextWriter tsw = new StreamWriter(@"C:\Users\Admin\source\repos\Visma_dotnet\dataTaken.txt");
            tsw.WriteLine(json);
            tsw.Close();
        }
        public void ToRemoveBookFromLibrary(List<LibraryBooks> books)
        {
            Console.WriteLine("Input the ISBN of the book you want to remove from library");
            string isbn = Console.ReadLine();
            foreach (var book in books)
            {
                if (isbn == book.ISBN)
                {
                    book.Name = ""; book.Author = ""; book.Category = ""; book.Language = ""; book.PublicationDate = ""; book.ISBN = "";
                }
            }
            string json = JsonConvert.SerializeObject(books);
            File.WriteAllText(@"C:\Users\Admin\source\repos\Visma_dotnet\dataBooks.txt", json);

        }
        public void ToRemovePeopleWithBooks(List<PeopleWithBooks> people)
        {
            string json = JsonConvert.SerializeObject(people);
            File.WriteAllText(@"C:\Users\Admin\source\repos\Visma_dotnet\dataTaken.txt", json);
        }
    }
}