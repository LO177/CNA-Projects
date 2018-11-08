using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject {
    class Program {
        static int width, height, area, perimeter;
        static bool contProgram = false;
        static string programSeq = "";
        static int multNum = 0;
        static int diffNum = 0;
        static void Main(string[] args) {
            largestParameters();
        }

        static void addParameters() {
            while (!contProgram) {
                Console.WriteLine("Find out area an perimiter of a rectangle!");
                Console.Write("Enter width of rectangle: ");
                width = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter height of rectangle: ");
                height = Convert.ToInt32(Console.ReadLine());
                area = width * height;
                perimeter = (width * 2) + (height * 2);
                Console.WriteLine("Area of your rectangle: " + area);
                Console.WriteLine("Perimeter of your rectangle: " + perimeter);
                Console.WriteLine("Continue (Y) or end program (N)? ");
                programSeq = Convert.ToString(Console.ReadLine());

                if (programSeq == "N" || programSeq == "n") {
                    contProgram = !contProgram;
                }
            }
        }

        static void multParameters() {
            while (!contProgram) {
                Console.WriteLine("Find out the product of two numbers!");
                Console.Write("Enter first number: ");
                width = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter second number: ");
                height = Convert.ToInt32(Console.ReadLine());
                multNum = width * height;
                Console.WriteLine("Product of the two numbers: " + multNum);
                Console.WriteLine("Continue (Y) or end program (N)? ");
                programSeq = Convert.ToString(Console.ReadLine());

                if (programSeq == "N" || programSeq == "n") {
                    contProgram = !contProgram;
                }
            }
        }

        static void largestParameters() {
            while (!contProgram) {
                Console.WriteLine("Compare the size of two numbers!");
                Console.Write("Enter first number: ");
                width = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter second number: ");
                height = Convert.ToInt32(Console.ReadLine());
                if (width > height){
                    Console.WriteLine("Highest numb: " + width);
                    diffNum = width - height;
                    Console.WriteLine("Difference between numbers: " + diffNum);
                }
                else {
                    Console.WriteLine("Highest numb: " + height);
                    diffNum = height - width;
                    Console.WriteLine("Difference between numbers: " + diffNum);
                }
                
                Console.WriteLine("Continue (Y) or end program (N)? ");
                programSeq = Convert.ToString(Console.ReadLine());

                if (programSeq == "N" || programSeq == "n")
                {
                    contProgram = !contProgram;
                }
            }
        }
    }
}