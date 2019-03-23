using System;
using System.Collections.Generic;

public static class Database
{
	public static List<Seller> sellers = new List<Seller>();
	public static List<Customer> customers = new List<Customer>();
	public static List<Review> reviews = new List<Review>();
	
	public static Seller getSellerById(int id_seller) {
		Seller sellerFinded = new Seller();
		
		foreach(Seller seller in sellers) {
            if(seller.id == id_seller){
				sellerFinded = seller;
				break;
			}
        }
		
		return sellerFinded;
	}
	
	public static Customer getCustomerById(int id_customer) {
		Customer customerFinded = new Customer();
		
		foreach(Customer customer in customers) {
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
	public static int count = 0;
	
	public int id;
	public int id_seller;
	public int id_customer;
	public int note;
	public string text;
	
	public Review(int id_seller, int id_customer, int note, string text){
		count = count + 1;
		this.id = count;
		this.id_seller = id_seller;
		this.id_customer = id_customer;
		this.note = note;
		this.text = text;
	}
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
	
	public void addReview(int id_seller, int note, string text){
		Review newReview = new Review(id_seller, this.id, note, text);
		Database.reviews.Add(newReview);
	}
}

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Hello World");
	}
}