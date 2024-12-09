namespace InveonBootcamp.WebApi.Models
{
    public class BookItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
    }

    public class Book
    {
        public string ReferenceCode { get; private set; }
        private List<BookItem> Items { get; set; }

        public Book()
        {
            Items = new List<BookItem>();
        }

        public void AddBook(BookItem item)
        {
            if (!Validate(item, out string errorMessage))
            {
                throw new Exception(errorMessage);
            }

            Items.Add(item);
        }

        public bool Validate(BookItem item, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
            {
                errorMessage = "Title cannot be empty.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(item.Author))
            {
                errorMessage = "Author cannot be empty.";
                return false;
            }

            if (item.Year < 0 || item.Year > DateTime.Now.Year)
            {
                errorMessage = "Year must be a valid year.";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        public void CreateCode(string referenceCode)
        {
            if (string.IsNullOrEmpty(referenceCode))
            {
                throw new Exception("Reference code cannot be null or empty.");
            }

            ReferenceCode = referenceCode;
        }
    }
}
