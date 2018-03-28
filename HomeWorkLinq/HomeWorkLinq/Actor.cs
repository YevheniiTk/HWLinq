using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkLinq
{
    public class Actor
    {
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public abstract class ArtObject
    {

        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
    public class Film : ArtObject
    {

        public int Length { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }
    public class Book : ArtObject
    {

        public int Pages { get; set; }
    }
}
