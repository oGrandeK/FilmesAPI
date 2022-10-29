using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public EnumGenre Genre { get; set; }
        public string Trailer { get; set; }
        public string Poster { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int DirectorId { get; set; }
        [JsonIgnore]
        public Director Director { get; set; }
    }
}
