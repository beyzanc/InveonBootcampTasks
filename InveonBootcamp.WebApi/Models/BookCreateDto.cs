namespace InveonBootcamp.WebApi.Models
{
    public class BookCreatedDto
    {
        public int Id { get; init; }
        public string UserId { get; init; }
        public DateTime Created { get; set; }

        public BookCreatedDto(int id, string userId)
        {
            Id = id;
            UserId = userId;
            Created = DateTime.Now;
        }

        public static BookCreatedDto Create(int id, string userId)
        {
            return new BookCreatedDto(id, userId);
        }

        public bool Validate(out string errorMessage)
        {
            if (Id <= 0)
            {
                errorMessage = "Id must be a positive number.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(UserId))
            {
                errorMessage = "UserId cannot be null or empty.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }
    }
}
