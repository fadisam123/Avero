namespace Avero.Core.Entities
{
    public class Neighborhood
    {
        int neighborhood_id { get; set; }
        String? name { get; set; }

        City? city_id { get; set; }
        public ICollection<User> users { get; set; } = new HashSet<User>();
    }
}