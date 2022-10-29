using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
