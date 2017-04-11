using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> obj = new List<Customer> {new GoldCustomer(), new SilverCustomer()};
            foreach (Customer customer in obj)
            {
                customer.AddCustomer();    
            }


            ICustomer custOld = new Customer();
            custOld.AddCustomer();

            IRead custNew = new Customer();
            custNew.AddCustomer();
            custNew.CalculateDiscount();
            custNew.Read();
        }

        public interface IErrorHandler
        {
            void HandleError(string err);
        }

        public class FileErrorHandler : IErrorHandler
        {
            public void HandleError(string err)
            {
                File.WriteAllText("", err);
            }
        }

        public class EventVwrErr : IErrorHandler
        {
            public void HandleError(string err)
            {
                //throw new NotImplementedException();
            }
        }

        public class  SqlErr:IErrorHandler
        {
            public void HandleError(string err)
            {
                //throw new NotImplementedException();
            }
        }



        public class Customer : ICustomer, IRead
        {
            public virtual double CalculateDiscount()
            {
                return 0;
            }

            public virtual void AddCustomer()
            {
                try
                {

                }
                catch(Exception ex)
                {
                    ErrorLogger el = new ErrorLogger();
                    el.LogException(ex.ToString());
                }
            }

            public void Read()
            {
                throw new NotImplementedException();
            }
        }

        class ErrorLogger
        {
            public void LogException(string errString)
            {
                File.WriteAllText(@"C:/Test.txt", errString);

            }
        }
        public class GoldCustomer: Customer
        {
            public override double CalculateDiscount()
            {
                return base.CalculateDiscount() + 10;
            }
        }

        public class  SilverCustomer: Customer
        {
            
            public override double CalculateDiscount()
            {
                return base.CalculateDiscount() + 5;
            }
        }

        public class EnquiryCustomer : IEnquiry
        {
            public double CalculateDiscount()
            {
                return 2;
            }
        }

        public interface IEnquiry
        {
            double CalculateDiscount();
        }

        public interface ICustomer : IEnquiry
        {
            void AddCustomer();
        }
        public interface IRead:ICustomer
        {
            void Read();
            
        }
    }
}
