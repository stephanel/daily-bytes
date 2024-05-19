using Books.Domain.Books;
using Books.Infrastructure.Persistence.Models;


namespace Books.Infrastructure.Persistence;
internal static class SeedData
{
    public static List<BookDb> Books => _books;

    private static List<BookDb> _books = [
        new()
        {
            Id = 100001,
            Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
            Payload = new()
            {
                ISBN = "978-0201633610",
                Authors = [
                    new() { FirstName = "Erich", LastName = "Gamma", KnownFor = "Gang of Four" },
                    new() { FirstName = "Richard", LastName = "Helm", KnownFor = "Gang of Four" },
                    new() { FirstName = "Ralph", LastName = "Jonhson", KnownFor = "Gang of Four" },
                    new() { FirstName = "John", LastName = "Vlissides", KnownFor = "Gang of Four" },
                ],
                Language = "English",
                ThumbnailUrl = "https://covers.openlibrary.org/b/id/1754351-M.jpg"
            }
       },
        new()
        {
            Id = 100002,
            Title = "Clean Architecture: A Craftsman's Guide to Software Structure and Design",
            Payload = new()
            {
                ISBN = "978-0134494166",
                Authors = [
                    new() { FirstName = "Robert", LastName = "Martin", KnownFor = "Clean Code, Agile, Software Craftsmanship" }
                ],
                Language = "English",
                ThumbnailUrl = "https://ia903000.us.archive.org/view_archive.php?archive=/3/items/m_covers_0008/m_covers_0008_51.tar&file=0008510059-M.jpg"
            }
        },
        new()
        {
            Id = 100003,
            Title = "Test-driven development, by example",
            Payload = new()
            {
                ISBN = "978-0321146533",
                Authors = [
                    new() { FirstName = "Kent", LastName = "Beck", KnownFor = "Extreme Programming (XP), TDD" },
                ],
                Language = "English",
                ThumbnailUrl =  "https://ia800505.us.archive.org/view_archive.php?archive=/5/items/m_covers_0012/m_covers_0012_38.zip&file=0012381947-M.jpg"
            }
        }
    ];
}
