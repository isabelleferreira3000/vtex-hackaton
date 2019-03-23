using System;
using System.Collections.Generic;

public class Database
{
	public List<Seller> sellers = new List<Seller>();
	public List<Customer> customers = new List<Customer>();
	public List<Review> reviews = new List<Review>();
	
	public Seller getSellerById(int id_seller) {
		Seller sellerFinded = new Seller();
		
		foreach(Seller seller in this.sellers) {
            if(seller.id == id_seller){
				sellerFinded = seller;
				break;
			}
        }
		
		return sellerFinded;
	}
	
	public Customer getCustomerById(int id_customer) {
		Customer customerFinded = new Customer();
		
		foreach(Customer customer in this.customers) {
            if(customer.id == id_customer){
				customerFinded = customer;
				break;
			}
        }
		
		return customerFinded;
	}
}

public class Review
{
	public int id_seller;
	public int id_customer;
	public int note;
	public string text;
}

public class Goods
{
	public int id;
	public int id_seller;
	public string type;
}

public class Seller
{
	public int id;
	public int cpf;
	public string name;
	public string address;
	// public List<Review> reviews = new List<Review>();
}

public class Customer
{
	public int id;
	public int cpf;
	public string name;
	// public List<Review> reviews = new List<Review>();
	
	public void addReview(int id_seller, string text, int note){
		
		
	}
}

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Hello World");
	}
}