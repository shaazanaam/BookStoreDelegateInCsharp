// A set of class for handling a BookStore:
namespace delegateBookStore
{
    using System.Collections;
    //Describes a book int book list;
    public struct Book
    {
        public string Title;
        public string Author;
        public decimal Price;
        public bool Paperback;
        public Book(string title, string author, decimal price, bool paperBack)
        {
            Title = title;
            Author = author;
            Price = price;
            Paperback = paperBack;
        }
    }
    /***Declaring a delegate****/
    // Declare a delegate type for processing a book:
    // delegate object will be created in the Client Code below
    //Notice how a delegate describes the number and type of the arguments 
    //and the type of the return value of the methods that it can encapsulate
    //Whenever a new set of argument types or return value type is needed a new delegate must 
    //be declared.
    public delegate void ProcessBookCallBack(Book book);

    // Maintains a book Database:
    public class BookDB
    {
        // List of all the  books in the data base
        ArrayList list = new ArrayList();
        // add a book to the data base :
        public void AddBook( string title, string author, decimal price,bool paperBack)
        {
            list.Add(new Book(title, author, price, paperBack));
        }

        // Call a passed-in delegate on each paperback book to process it :
        // the delegate that is passed in is the processBook
        public void ProcessPaperbackBooks(ProcessBookCallBack processBook)
        {
            foreach (Book book in list)
            {
                if(book.Paperback)
                /** Calling a Delegate**/
                // After a delegate object was created in the client code
                // it is typically passed to other code which inthis case is this code
                //No this code will call the delegate.
                // for example the delegate object del and del2 is being called from here 
                // del and del2 being passed in is now being called using the parameter processBook
                //for all the books in the library.
                //Notice how in order to call a delegate object we are using the name of the delegate
                //object followed by the paranthesized areguments which we intend to pass to the delegates del and del2 here
                // call the delegate
                processBook(book);
            }
        }

    }

}

// Using the Book Store classes
namespace BookTestClient
{
    using delegateBookStore;
    // Class to total and average the prices of the books:
    class PriceTotaller
    {
        int countBooks = 0;
        decimal priceBooks = 0.0m;
        internal void AddBookToTotal(Book book)
        {
            countBooks+=1;
            priceBooks += book.Price;
        }

        internal  decimal AveragePrice()
        {
            return priceBooks/countBooks;
        }
    }


    // Class to test the book database

    class Test
    {
        //Print the title of the book.
        // We will not be calling this  method directly to print the book 
        // We will be passing the PrintTitle method to the method ProcessPaperbackBooks using a delegate object 
        //of the type ProcessBookCallback delegate.
        static void PrintTitle(Book b)
        {
            Console.Write($"    {b.Title}");
        }

        // Execution starts here
        static void Main()
        {
            BookDB bookDB = new BookDB();

            // Initialize the database with some books:
            AddBooks(bookDB);
            // Print all the titles of the paperbacks:
            Console.WriteLine("Paperback Book Titles:" );

            
            /***After a delegate type has been declared, 
            a delegate object must be created and associated with a particular method. 
            In the previous example, you do this by passing the PrintTitle method 
            to the ProcessPaperbackBooks method as in the following example: So 
            essentially in this case the delagte ProcessBookCallBack is now associated with
            the method ProcessPaperbackBooks
            Notice how I am passing the methods PrintTitle and the AddBookToTotal to the method
            ProcessPaperbackBooks using the delgate object 
            and remeber once a delagate object is created then the method associated
            with it never changes; delegate objects are immutable***/
            
            // Below we create  a new delegate object associated with the static method 
            // Test.PrintTitle

            ProcessBookCallBack del = PrintTitle;
            bookDB.ProcessPaperbackBooks(del);

            // Get the average price of the paperback by using 
            // a PriceTotaller object:
            PriceTotaller totaller = new PriceTotaller();

            // Create a new delegate object associated with the non static method 
            //AddBookToTotal on the 
            //object totaller:
            ProcessBookCallBack del2 =  totaller.AddBookToTotal;
            bookDB.ProcessPaperbackBooks(del2);

            // In both the case a new delegate object is passed to the ProcessPaperbackBooks method

            Console.WriteLine("Average Paperback BookProce : ${0:#.##}" , totaller.AveragePrice());

        }

        static void AddBooks(BookDB bookDB)
        {
            bookDB.AddBook("The C Programming Language", "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
            bookDB.AddBook("The Unicode Standard 2.0", "The Unicode Consortium", 39.95m, true);
            bookDB.AddBook("The MS-DOS Encyclopedia", "Ray Duncan", 129.95m, false);
            bookDB.AddBook("Dogbert's Clues for the Clueless", "Scott Adams", 12.00m, true);
        }
    }
}
