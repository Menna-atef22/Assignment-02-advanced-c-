using Section01_Books;

List<Book> books = new List<Book>
{
    new Book("111", "Clean Code", new[] { "Robert Martin" }, new DateTime(2008, 8, 1), 45.50m),
    new Book("222", "C# in Depth", new[] { "Jon Skeet" }, new DateTime(2019, 3, 23), 55m),
    new Book("333", "Design Patterns", new[] { "Erich Gamma", "Richard Helm" }, new DateTime(1994, 10, 21), 60.25m)
};

Console.WriteLine("--- a) User-defined delegate (BookFunction) ---");
LibraryEngine.ProcessBooks(books, BookFunctions.GetTitle);
LibraryEngine.ProcessBooks(books, BookFunctions.GetAuthors);

Console.WriteLine("\n--- b) Built-in delegate (Func<Book, string>) ---");
LibraryEngine.ProcessBooksFunc(books, BookFunctions.GetPrice);

Console.WriteLine("\n--- c) Anonymous method (GetISBN) ---");
LibraryEngine.ProcessBooksFunc(books, delegate (Book B)
{
    return B.ISBN;
});

Console.WriteLine("\n--- d) Lambda expression (GetPublicationDate) ---");
LibraryEngine.ProcessBooksFunc(books, B => B.PublicationDate.ToString("yyyy-MM-dd"));
