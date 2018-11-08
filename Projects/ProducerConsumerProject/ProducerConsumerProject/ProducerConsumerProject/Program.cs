using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person p = new Person("", 4);

            List<Person> thePeople = new List<Person>(5);
            thePeople.Add(new Person("French", 22));
            thePeople.Add(new Lecturer("Concurrency", "Reeves", 25));
            thePeople.Add(new Student("Ace", "Smith", 19));
            thePeople.Add(new Lecturer("Algorithmics", "Pratt", 30));
            thePeople.Add(new Student("Snappy", "Jones", 20));
            foreach (Person p in thePeople)
            {
                p.Print();
                Console.WriteLine();
            }
        }
    }

    public class Person
    {
        private string name;
        private int age;

        public Person()
        {
            name = "";
            age = 0;
        }

        public Person(string n, int a)
        {
            name = n;
            age = a;
        }

        public virtual void Print()
        {
            Console.Write(name + ", " + age);
        }
    }

    public class Student : Person
    {
        private string nickname;

        public Student() : base ()
        {
            nickname = "";
        }

        public Student(string nName, string n, int a) : base (n, a)
        {
            nickname = nName;
        }

        public override void Print()
        {
            base.Print();
            Console.Write(", " + nickname );
        }
    }

    public class Lecturer : Person
    {
        private string subject;

        public Lecturer() : base()
        {
            subject = "";
        }

        public Lecturer(string s, string n, int a) : base(n, a)
        {
            subject = s;
        }

        public override void Print()
        {
            base.Print();
            Console.Write(", " + subject);
        }
    }
}
