using System;
using System.Collections.Generic;
using System.Data.SqlClient;

// Klasa reprezentująca klienta
public class Customer
{
    public string CustomerID { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ContactTitle { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
}

// Klasa do obsługi operacji na bazie danych
public static class CustomerRepository
{
    // Łączenie z bazą danych
    private const string connectionString = "Data Source=Laptok;Initial Catalog=Northwind;Integrated Security=True;";




    // Funkcja pobierająca wszystkich klientów z bazy
    public static List<Customer> GetAllCustomers()
    {
        var customers = new List<Customer>();
        const string queryString = "SELECT * FROM Customers"; // Zapytanie SQL do pobrania wszystkich klientów

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Odczytywanie danych z bazy
            while (reader.Read())
            {
                var customer = new Customer
                {
                    CustomerID = reader["CustomerID"].ToString(),
                    CompanyName = reader["CompanyName"].ToString(),
                    ContactName = reader["ContactName"].ToString(),
                    ContactTitle = reader["ContactTitle"].ToString(),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString(),
                    Region = reader["Region"].ToString(),
                    PostalCode = reader["PostalCode"].ToString(),
                    Country = reader["Country"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Fax = reader["Fax"].ToString()
                };
                customers.Add(customer); // Dodawanie klienta do listy
            }
            reader.Close();
        }
        return customers; // Zwracanie listy wszystkich klientów
    }




    // Funkcja dodająca nowego klienta do bazy
    public static string AddCustomer(Customer customer)
    {
        const string queryString = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) " +
                                   "VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)";

        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command = new(queryString, connection);
            // Dodawanie parametrów do zapytania SQL
            command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
            command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
            command.Parameters.AddWithValue("@ContactName", customer.ContactName);
            command.Parameters.AddWithValue("@ContactTitle", customer.ContactTitle);
            command.Parameters.AddWithValue("@Address", customer.Address);
            command.Parameters.AddWithValue("@City", customer.City);
            command.Parameters.AddWithValue("@Region", customer.Region);
            command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
            command.Parameters.AddWithValue("@Country", customer.Country);
            command.Parameters.AddWithValue("@Phone", customer.Phone);
            command.Parameters.AddWithValue("@Fax", customer.Fax);

            command.ExecuteNonQuery(); // Wykonanie zapytania w bazie
        }
        return customer.CustomerID; // Zwracanie ID nowo dodanego klienta
    }




    // Funkcja usuwająca klienta na podstawie ID
    public static void RemoveCustomerById(string customerId)
    {
        const string queryString = "DELETE FROM Customers WHERE CustomerID = @CustomerID";

        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command = new(queryString, connection);
            command.Parameters.AddWithValue("@CustomerID", customerId);
            command.ExecuteNonQuery(); // Wykonanie zapytania usuwającego klienta z bazy
        }
    }





    // Funkcja aktualizująca dane klienta w bazie danych
    public static void UpdateCustomer(Customer customer)
    {
        const string queryString = "UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName, ContactTitle = @ContactTitle, " +
                                   "Address = @Address, City = @City, Region = @Region, PostalCode = @PostalCode, Country = @Country, " +
                                   "Phone = @Phone, Fax = @Fax WHERE CustomerID = @CustomerID";

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(queryString, connection);
            connection.Open();
            // Dodawanie parametrów do zapytania SQL
            command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
            command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
            command.Parameters.AddWithValue("@ContactName", customer.ContactName);
            command.Parameters.AddWithValue("@ContactTitle", customer.ContactTitle);
            command.Parameters.AddWithValue("@Address", customer.Address);
            command.Parameters.AddWithValue("@City", customer.City);
            command.Parameters.AddWithValue("@Region", customer.Region);
            command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
            command.Parameters.AddWithValue("@Country", customer.Country);
            command.Parameters.AddWithValue("@Phone", customer.Phone);
            command.Parameters.AddWithValue("@Fax", customer.Fax);

            command.ExecuteNonQuery(); // Wykonanie zapytania aktualizującego dane klienta
        }
    }




    // Funkcja pobierająca dane klienta na podstawie jego ID
    public static Customer GetCustomerById(string customerId)
    {
        Customer customer = null;
        const string queryString = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";

        using (SqlConnection connection = new(connectionString))
        {
            connection.Open();
            SqlCommand command = new(queryString, connection);
            command.Parameters.AddWithValue("@CustomerID", customerId);
            SqlDataReader reader = command.ExecuteReader();

            // Jeżeli klient został znaleziony
            if (reader.Read())
            {
                customer = new Customer
                {
                    CustomerID = reader["CustomerID"].ToString(),
                    CompanyName = reader["CompanyName"].ToString(),
                    ContactName = reader["ContactName"].ToString(),
                    ContactTitle = reader["ContactTitle"].ToString(),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString(),
                    Region = reader["Region"].ToString(),
                    PostalCode = reader["PostalCode"].ToString(),
                    Country = reader["Country"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Fax = reader["Fax"].ToString()
                };
            }
            reader.Close();
        }
        return customer; // Zwracanie klienta lub null, jeśli nie znaleziono
    }
}




// Główna klasa programu
public class Program
{
    public static void Main()
    {
        // Pętla, która wykonuje program aż do wyjścia
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1 - Dodaj nowego klienta");
            Console.WriteLine("2 - Pokaż wszystkich klientów");
            Console.WriteLine("3 - Usuń klienta");
            Console.WriteLine("4 - Edytuj dane klienta");
            Console.WriteLine("5 - Pokaż dane klienta");
            Console.WriteLine("6 - Wyjście");

            // Wczytywanie opcji od użytkownika
            string option = Console.ReadLine();

            // Obsługa wyboru opcji
            switch (option)
            {
                case "1":
                    AddCustomerMenu(); // Dodanie nowego klienta
                    break;
                case "2":
                    ShowAllCustomers(); // Pokazanie wszystkich klientów
                    break;
                case "3":
                    RemoveCustomerMenu(); // Usunięcie klienta
                    break;
                case "4":
                    UpdateCustomerMenu(); // Edytowanie danych klienta
                    break;
                case "5":
                    ShowCustomerMenu(); // Pokazanie danych klienta
                    break;
                case "6":
                    return; // Zakończenie programu
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Naciśnij dowolny klawisz, aby spróbować ponownie...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Menu do dodawania nowego klienta
    private static void AddCustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("Podaj dane klienta:");

        var newCustomer = new Customer();

        // Pobieranie danych od użytkownika
        Console.Write("CustomerID: ");
        newCustomer.CustomerID = Console.ReadLine();

        Console.Write("CompanyName: ");
        newCustomer.CompanyName = Console.ReadLine();

        Console.Write("ContactName: ");
        newCustomer.ContactName = Console.ReadLine();

        Console.Write("ContactTitle: ");
        newCustomer.ContactTitle = Console.ReadLine();

        Console.Write("Address: ");
        newCustomer.Address = Console.ReadLine();

        Console.Write("City: ");
        newCustomer.City = Console.ReadLine();

        Console.Write("Region: ");
        newCustomer.Region = Console.ReadLine();

        Console.Write("PostalCode: ");
        newCustomer.PostalCode = Console.ReadLine();

        Console.Write("Country: ");
        newCustomer.Country = Console.ReadLine();

        Console.Write("Phone: ");
        newCustomer.Phone = Console.ReadLine();

        Console.Write("Fax: ");
        newCustomer.Fax = Console.ReadLine();

        // Dodanie klienta do bazy danych
        CustomerRepository.AddCustomer(newCustomer);
        Console.WriteLine("Nowy klient został dodany. Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    // Menu do wyświetlania wszystkich klientów
    private static void ShowAllCustomers()
    {
        Console.Clear();
        Console.WriteLine("Wszyscy klienci:");

        // Pobieranie listy klientów z bazy danych
        var customers = CustomerRepository.GetAllCustomers();

        foreach (var customer in customers)
        {
            // Wyświetlanie klientów
            Console.WriteLine($"ID: {customer.CustomerID}, Firma: {customer.CompanyName}, Kontakt: {customer.ContactName}, Telefon: {customer.Phone}");
        }

        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    // Menu do usuwania klienta
    private static void RemoveCustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("Podaj ID klienta do usunięcia:");
        string customerId = Console.ReadLine();

        // Usuwanie klienta na podstawie ID
        CustomerRepository.RemoveCustomerById(customerId);
        Console.WriteLine("Klient został usunięty. Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    // Menu do edytowania danych klienta
    private static void UpdateCustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("Podaj ID klienta do edycji:");
        string customerId = Console.ReadLine();

        // Pobieranie danych klienta z bazy
        var customer = CustomerRepository.GetCustomerById(customerId);
        if (customer != null)
        {
            // Pobieranie nowych danych
            Console.WriteLine("Podaj nowe dane klienta:");

            Console.Write("CompanyName: ");
            customer.CompanyName = Console.ReadLine();

            Console.Write("ContactName: ");
            customer.ContactName = Console.ReadLine();

            Console.Write("ContactTitle: ");
            customer.ContactTitle = Console.ReadLine();

            Console.Write("Address: ");
            customer.Address = Console.ReadLine();

            Console.Write("City: ");
            customer.City = Console.ReadLine();

            Console.Write("Region: ");
            customer.Region = Console.ReadLine();

            Console.Write("PostalCode: ");
            customer.PostalCode = Console.ReadLine();

            Console.Write("Country: ");
            customer.Country = Console.ReadLine();

            Console.Write("Phone: ");
            customer.Phone = Console.ReadLine();

            Console.Write("Fax: ");
            customer.Fax = Console.ReadLine();

            // Aktualizacja danych w bazie
            CustomerRepository.UpdateCustomer(customer);
            Console.WriteLine("Dane klienta zostały zaktualizowane. Naciśnij dowolny klawisz, aby kontynuować...");
        }
        else
        {
            Console.WriteLine("Klient o podanym ID nie został znaleziony.");
        }

        Console.ReadKey();
    }

    // Menu do wyświetlania danych pojedynczego klienta
    private static void ShowCustomerMenu()
    {
        Console.Clear();
        Console.WriteLine("Podaj ID klienta:");
        string customerId = Console.ReadLine();

        // Pobieranie danych klienta z bazy
        var customer = CustomerRepository.GetCustomerById(customerId);
        if (customer != null)
        {
            // Wyświetlanie danych klienta
            Console.WriteLine($"ID: {customer.CustomerID}");
            Console.WriteLine($"Firma: {customer.CompanyName}");
            Console.WriteLine($"Kontakt: {customer.ContactName}");
            Console.WriteLine($"Telefon: {customer.Phone}");
        }
        else
        {
            Console.WriteLine("Klient o podanym ID nie został znaleziony.");
        }

        Console.ReadKey();
    }
}
