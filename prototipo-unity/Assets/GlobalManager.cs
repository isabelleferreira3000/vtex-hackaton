using System;
using System.Collections.Generic;
using UnityEngine;

public static class Database
{
    public static List<Seller> sellers = new List<Seller>();
    public static List<Customer> customers = new List<Customer>();
    public static List<Review> reviews = new List<Review>();
    public static List<Goods> goodsList = new List<Goods>();

    public static Seller getSellerById(int id_seller)
    {
        Seller sellerFinded = new Seller();

        foreach (Seller seller in sellers)
        {
            if (seller.id == id_seller)
            {
                sellerFinded = seller;
                break;
            }
        }

        return sellerFinded;
    }

    public static Customer getCustomerById(int id_customer)
    {
        Customer customerFinded = new Customer();

        foreach (Customer customer in customers)
        {
            if (customer.id == id_customer)
            {
                customerFinded = customer;
                break;
            }
        }

        return customerFinded;
    }

    public static List<Seller> searchSellersByType(string type){
        List<Seller> sellersByType = new List<Seller>();

        foreach(Seller seller in Database.sellers){
            if(seller.types.Contains(type)){
                sellersByType.Add(seller);
            }
        }

        return sellersByType;
    }
}

public class Review
{
    public static int count = 0;

    public int id;
    public int id_seller;
    public int id_customer;
    public double note;
    public string text;

    public Review(int id_seller, int id_customer, double note, string text)
    {
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
    public static int count = 0;

    public int id;
    public int id_seller;
    public string type;

    public Goods()
    {
        count = count + 1;
        this.id = count;
    }

    public Goods(string type)
    {
        count = count + 1;
        this.id = count;
        this.type = type;
    }
}

public class Seller
{
    public static int count = 0;

    public int id;
    public string cpf;
    public string name;
    public string address;
    public double note;

    public double xCord;
    public double yCord;
    public List<Review> reviews = new List<Review>();
    public List<Goods> goodsList = new List<Goods>();
    public List<string> types = new List<string>();

    public Seller()
    {
        count = count + 1;
        this.id = count;
    }

	public Seller(string cpf, string name, string address, double xCord, double yCord){
		count = count + 1;
		this.id = count;
		this.cpf = cpf;
		this.name = name;
		this.address = address;
        this.xCord = xCord;
        this.yCord = yCord;
        this.note = -1.0;

        Database.sellers.Add(this);
	}

    public void addGoods(string type)
    {
        Goods newGoods = new Goods(type);
        Database.goodsList.Add(newGoods);
        this.goodsList.Add(newGoods);

        if (!this.types.Contains(type))
        {
            this.types.Add(type);
        }
    }
    public void updateNote()
    {
        int totalReviews = this.reviews.Count;
        double totalNote = 0;
        if (totalReviews != 0)
        {
            foreach (Review review in this.reviews)
            {
                totalNote = totalNote + review.note;
            }
            this.note = totalNote / totalReviews;

        }
    }
}

public class Customer
{
    public static int count = 0;

    public int id;
    public string cpf;
    public string name;
    public List<Review> reviews = new List<Review>();

    public Customer()
    {
        count = count + 1;
        this.id = count;
    }

    public Customer(string cpf, string name)
    {
        count = count + 1;
        this.id = count;
        this.cpf = cpf;
        this.name = name;

        Database.customers.Add(this);
    }

    public void addReview(int id_seller, double note, string text)
    {
        Review newReview = new Review(id_seller, this.id, note, text);
        Database.reviews.Add(newReview);
        this.reviews.Add(newReview);
        Seller seller = Database.getSellerById(id_seller);
        seller.reviews.Add(newReview);
        seller.updateNote();
    }
}

public class GlobalManager : MonoBehaviour
{

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Awake()
    {
        Seller seller1 = new Seller("04616069336", "Isabelle Ferreira", "Rua H8A, 103", -23.202569, -45.884370);
        Seller seller2 = new Seller("12345678901", "Maria da Silva", "Rua Aruana, 133", -24.202569, -46.884370);
        Seller seller3 = new Seller("23456789012", "Chico Castro", "Av Duque de Caxias, 300", -20.202569, -46.884370);

        new Seller("19784625", "Maria Antonieta", "Rua Ola ola", -23.0, -50.0);
        new Seller("19784625", "Carlos da Silva", "Rua Ola ola", -27.0, -50.0);

        Customer customer1 = new Customer("11111111111", "Livia Pimentel");
        Customer customer2 = new Customer("22222222222", "Ana Maria");
        Customer customer3 = new Customer("33333333333", "Jonatan Rodrigues");
        Customer customer4 = new Customer("44444444444", "Jose Maria Rocha");

        seller1.addGoods("roupas");
        seller1.addGoods("eletronicos");
        seller2.addGoods("eletronicos");
        seller3.addGoods("comidas");

        customer1.addReview(seller1.id, 10, "Muito receptiva!");
        customer1.addReview(seller2.id, 5, "Meu carregador de celular veio quebrado");
        customer1.addReview(seller3.id, 8, "Pastel muito bom, mas mto oleoso");

        customer2.addReview(seller1.id, 9, "A blusa desbota mto rapido, mas eh linda");

        Database.sellers.Sort((p1,p2)=>p2.note.CompareTo(p1.note));
    }
    void Start() {
        UIManager.instance.GenerateVendorListItens();
    }
}