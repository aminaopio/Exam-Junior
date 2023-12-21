using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;
using InterviewTest.Customers;

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
            //nested for loop to iterate between each order/product
            foreach (IOrder order in GetOrders()){
                foreach(OrderedProduct orderedProduct in order.Products){
                    //add selling price of each prod to total sales
                    totalSales += orderedProduct.Product.GetSellingPrice();
                }
            }
            return totalSales;
        }

        public float GetTotalReturns()
        {
            //initialize
            float totalReturns = 0;
            foreach (Return returns in GetReturns()){
                foreach(ReturnedProduct returnedProduct in returns.ReturnedProducts){
                    //add selling price of each prod to total returns
                    totalReturns += returnedProduct.OrderProduct.Product.GetSellingPrice();
                }
            }
            return totalReturns;
        }

        public float GetTotalProfit()
        {
            //subtract returns from sales
            return GetTotalSales() - GetTotalReturns();
        }

        //attempted method for recording purchase date/time, could not resolve errors
        public float GetCurrentTime(){
            throw new NotImplementedException();

        //     foreach (IOrder order in orders)
        //     {
        //         foreach (OrderedProduct orderedProduct in order.Products)
        //         {
        //              ...
        //             }
        //             fullList[productKey].Add(order.purchaseTime);
        //         }
        //     }
        }
    }
}
