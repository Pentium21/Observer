using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsSharp
{
    interface IObserver
    {
        void Update(double p);
    }

    interface IObservable
    {
        void AddObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void Notify();
    }

    class Product : IObservable
    {
        private List<IObserver> observers;
        private double price;
        public Product(double p)
        {
            price = p;
            observers = new List<IObserver>();
        }

        public void ChangePrice(double p)
        {
            price = p;
            Notify();
        }

        public void AddObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void Notify()
        {
            foreach (IObserver o in observers.ToList())
                o.Update(price);
        }
        
    }

    class Wholesale : IObserver
    {
        private IObservable product;
        public Wholesale(IObservable odj)
        {
            product = odj;
            odj.AddObserver(this);
        }

        public void Update(double p)
        {
            if (p<300)
            {
                Console.WriteLine("Оптовик закупил товар по цене " + p);
                product.RemoveObserver(this);
            }
        }

    }

    class Buyer: IObserver
    {
        private IObservable product;
        public Buyer(IObservable obj)
        {
            product= obj;
            obj.AddObserver(this);
        }
        public void Update(double p)
        {
            if (p < 350)
            {
                Console.WriteLine("Пoкупатель закупил товар по цене " + p);
                product.RemoveObserver(this);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product(400);
                Wholesale wholesale = new Wholesale(product);
            Buyer buyer = new Buyer(product);

            product.ChangePrice(320);
            product.ChangePrice(280);
        }
    }
}




