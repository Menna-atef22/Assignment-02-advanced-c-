namespace Section01_Books;

// a) User-defined delegate with the same signature as the BookFunctions methods
public delegate string BookFunction(Book B);

public class LibraryEngine
{
    // a) Parameter is the user-defined delegate
    public static void ProcessBooks(List<Book> bList, BookFunction fPtr)
    {
        foreach (Book B in bList)
        {
            Console.WriteLine(fPtr(B));
        }
    }

    // b) Parameter is the built-in delegate Func<Book, string>
    // (different name so passing a lambda is not ambiguous)
    public static void ProcessBooksFunc(List<Book> bList, Func<Book, string> fPtr)
    {
        foreach (Book B in bList)
        {
            Console.WriteLine(fPtr(B));
        }
    }
}
