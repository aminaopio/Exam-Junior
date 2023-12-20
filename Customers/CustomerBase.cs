using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }

        public abstract string GetName();
        
        public void CreateOrder(IOrder order)
        {
            _orderRepository.Add(order);
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }

        public float GetTotalSales()
        {
            //initialize
            float totalSales = 0;
            //nested for loop
            foreach (IOrder order in GetOrders()){
                foreach(OrderedProduct orderedProduct in order.Products){
                    //adds selling price of each prod to total
                    totalSales += orderedProduct.Product.GetSellingPrice();
                }
            }
            return totalSales;
        }

        public float GetTotalReturns()
        {
            throw new NotImplementedException();
        }

        public float GetTotalProfit()
        {
            //subtract returns from sales
            return GetTotalSales() - GetTotalReturns();
        }
    }
}
