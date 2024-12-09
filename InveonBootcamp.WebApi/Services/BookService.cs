using InveonBootcamp.WebApi.Models;
using InveonBootcamp.WebApi.Exceptions;

namespace InveonBootcamp.WebApi.Services
{
    public class BookService
    {
        private readonly List<BookItem> _bookItems;
        private int _nextId = 1;

        public BookService()
        {
            _bookItems = new List<BookItem>();
        }

        public Result<BookCreatedDto> Create(BookItem bookItem, string referenceCode)
        {
            try
            {
                if (string.IsNullOrEmpty(referenceCode))
                    throw new ValidationException("Reference code is required.");

                if (!ValidateBookItem(bookItem))
                    throw new ValidationException("Invalid book item details.");

                if (_bookItems.Any(b => b.Title == bookItem.Title && b.Author == bookItem.Author))
                    throw new BusinessRuleException("A book with this title and author already exists.");

                bookItem.Id = _nextId++;

                _bookItems.Add(bookItem);

                var createdDto = BookCreatedDto.Create(bookItem.Id, referenceCode);
                return Result<BookCreatedDto>.Success(createdDto, 201);
            }
            catch (ValidationException vex)
            {
                return Result<BookCreatedDto>.Fail(vex.Message, vex.StatusCode);
            }
            catch (BusinessRuleException brex)
            {
                return Result<BookCreatedDto>.Fail(brex.Message, brex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<BookCreatedDto>.Fail($"Unexpected error: {ex.Message}", 500);
            }
        }

        public Result<List<BookItem>> GetAll()
        {
            return Result<List<BookItem>>.Success(_bookItems, 200);
        }

        public Result<PaginatedResult<BookItem>> GetAllPaginated(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1)
                pageSize = 10;

            var totalItems = _bookItems.Count;

            var paginatedItems = _bookItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var paginatedResult = new PaginatedResult<BookItem>(
                paginatedItems,
                totalItems,
                pageNumber,
                pageSize
            );

            return Result<PaginatedResult<BookItem>>.Success(paginatedResult, 200);
        }

        public Result<BookItem> GetById(int id)
        {
            var bookItem = _bookItems.FirstOrDefault(b => b.Id == id);

            if (bookItem == null)
                throw new NotFoundException("Book", id);

            return Result<BookItem>.Success(bookItem, 200);
        }

        public Result Update(BookUpdateDto updateBook)
        {
            try
            {
                var bookItem = _bookItems.FirstOrDefault(b => b.Id == updateBook.Id);

                if (bookItem == null)
                    throw new NotFoundException("Book", updateBook.Id);

                bookItem.Title = updateBook.Title ?? bookItem.Title;
                bookItem.Author = updateBook.Author ?? bookItem.Author;
                bookItem.Genre = updateBook.Genre ?? bookItem.Genre;
                bookItem.Year = updateBook.Year > 0 ? updateBook.Year : bookItem.Year;

                return Result<string>.Success("Book updated successfully.", 200);
            }
            catch (NotFoundException nfex)
            {
                return Result<string>.Fail(nfex.Message, nfex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"An error occurred: {ex.Message}", 500);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var bookItem = _bookItems.FirstOrDefault(b => b.Id == id);

                if (bookItem == null)
                    throw new NotFoundException("Book", id);

                _bookItems.Remove(bookItem);

                return Result<string>.Success("Book deleted successfully.", 200);
            }
            catch (NotFoundException nfex)
            {
                return Result<string>.Fail(nfex.Message, nfex.StatusCode);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail($"An error occurred: {ex.Message}", 500);
            }
        }

        private bool ValidateBookItem(BookItem item)
        {
            return !string.IsNullOrWhiteSpace(item.Title) &&
                   !string.IsNullOrWhiteSpace(item.Author) &&
                   item.Year > 0 &&
                   item.Year <= DateTime.Now.Year;
        }
    }
}