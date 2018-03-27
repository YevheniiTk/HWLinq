using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<object>() {
                        "Hello",
                        new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
                        new Book() { Author = "Stephen King", Name = "1! Guards!", Pages = 10 },
                        new Book() { Author = "a", Name = "Guards! Guards!", Pages = 6 },
                        new Book() { Author = "Terry Pratchett", Name = "1! Guards!", Pages = 5 },
                        new Book() { Author = "Stephen King", Name = "Guards! Guards!", Pages = 4 },
                        new Book() { Author = "Terry Pratchett", Name = "1! Guards!", Pages = 3 },
                        new Book() { Author = "a", Name = "Guards! Guards!", Pages = 2 },
                        new Book() { Author = "Terry Pratchett", Name = "1! Guards!", Pages = 7 },
                        new Book() { Author = "Stephen King", Name = "Guards! Guards!", Pages = 1 },
                        new Book() { Author = "Terry Pratchett", Name = "1! Guards!", Pages = 5 },
                        new Book() { Author = "Stephen King", Name = "Guards! Guards!", Pages = 4 },
                        new Book() { Author = "Terry Pratchett", Name = "1! Guards!", Pages = 7 },
                        new Book() { Author = "Stephen King", Name = "Guards! Guards!", Pages = 5 },
                        new Book() { Author = "Terry Pratchett", Name = "1! Guards!", Pages = 9 },
                        new List<int>() {4, 6, 8, 2},
                        new string[] {"Hello inside array"},
                        new Film() { Author = "Martin Scorsese", Name= "The Departed", Actors = new List<Actor>() {
                            new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
                            new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
                            new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
                        }},
                        new Film() { Author = "Gus Van Sant", Name = "Good Will Hunting", Actors = new List<Actor>() {
                            new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
                            new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
                        }},
                        new Film() { Author = "Gus Sant", Name = "Good Hunting", Actors = new List<Actor>() {
                            new Actor() { Name = "Damon Damon", Birthdate = new DateTime(1970, 8, 10)},
                            new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
                        }},
                        new Book() { Author = "Stephen King", Name="Finders Keepers", Pages = 200},
                        "Leonardo DiCaprio"
                    };

            //1. Output all elements excepting ArtObjects
            var allElementsWithoutArtObjects = data.Except(data.Where(_ => _ is ArtObject)).ToList();

            //2.Output all actors names
            var allActorsNames = data.Where(_ => _ is Film)
                                     .SelectMany(_ => (_ as Film).Actors)
                                     .Select(actor => actor.Name)
                                     .ToList();

            //3.Output number of actors born in august
            var actorsBornInAugust = data.Where(_ => _ is Film)
                                         .SelectMany(_ => (_ as Film).Actors)
                                         .Where(actor => actor.Birthdate.Month == 8)
                                         .Select(actor => actor)
                                         .Count();

            //4.Output two oldest actors names
            var twoOldestActorsNames = data.Where(_ => _ is Film)
                                           .SelectMany(_ => (_ as Film).Actors)
                                           .OrderBy(actor => actor.Birthdate)
                                           .Take(2)
                                           .Select(actor => actor.Name)
                                           .ToList();

            //5.Output number of books per authors
            var numberOfBooksPerAuthors = data.Where(_ => _ is Book)
                                              .GroupBy(_ => (_ as Book).Author)
                                              .Select(_ => new { NameAuthor = _.Key, CountOfBooks = _.Count() })
                                              .ToList();

            //6.Output number of books per authors and films per director
            var numberOfBooksPerAuthorsAndFilmsPerDirector
                        = data.Where(_ => _ is Book || _ is Film)
                              .GroupBy(_ => _.GetType())
                              .Select(_ => new { Author = _.Key, CountArts = _.Count() })
                              .ToList();

            //7.Output how many different letters used in all actors names
            var howManyDifferentLettersInNames = data.Where(_ => _ is Film)
                                     .SelectMany(_ => (_ as Film).Actors)
                                     .SelectMany(actor => actor.Name)
                                     .Distinct()
                                     .Where(_ => _ != ' ')   // Is the space character a symbol?
                                     .Count();

            //8.Output names of all books ordered by names of their authors and number of pages

            var namesBooksByAuthorsAndPages = data.Where(_ => _ is Book)
                                                  .Select(_ => (_ as Book))
                                                  .OrderBy(_ => _.Pages)
                                                  .OrderBy(_ => _.Author)
                                                  .Select(_ => $"{_.Name} - {_.Pages}")
                                                  .ToList();

            //9.Output actor name and all films with this actor
            var actorNameAndAllFilmsWithHim = data.Where(_ => _ is Film)
                                         .SelectMany(_ => (_ as Film).Actors)
                                         .Where(actor => actor.Birthdate.Month == 8)
                                         .Select(actor => actor)
                                         .Count();

            //10.Output sum of total number of pages in all books and all int values inside all sequences in data
            var pages = data.Where(_ => _ is Book)
                            .Select(_ => (_ as Book).Pages)
                            .Sum();

            //11.Get the dictionary with the key - book author, value - list of author's books
            
            var dictionaryBooks = data.Where(_ => _ is Book)
                                                  .Select(_ => (_ as Book))
                                                  .GroupBy(_ => _.Author)
                                                  .Select(_ => new
                                                  {
                                                      Author = _.Key,
                                                      Count = _.Count(),
                                                      Books = _
                                                  })
                                                  .ToList();

            foreach (var group in dictionaryBooks)
            {
                Console.WriteLine($"{group.Author} : {group.Count}");
                foreach (Book book in group.Books)
                    Console.WriteLine("\t" + book.Name);
                Console.WriteLine();
            }

            //12.Output all films of "Matt Damon" excluding films with actors whose name are presented in data as strings

            var filmsMattDamon = data.Where(_ => _ is Film)
                                     .Select(_ => (_ as Film)
                                     .Actors.Where(x => x.Name == "Matt Damon"))
                                     .ToList();

            Console.ReadLine();
        }
    }
}
