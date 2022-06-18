using Avero.Core.Enum;

namespace Avero.Core.Entities
{
    public class Rating
    {
        int UserRating_id { get; set; }
        Rate? rating { get; set; }

        User? user_id { get; set; }
    }
}