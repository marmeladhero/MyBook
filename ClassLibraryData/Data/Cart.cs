using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData.Data
{
    public class Cart
    {
        private List<CartLine> lstCart = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lstCart; } }

        public void AddItem(Book book, int quantity)
        {
            CartLine item = lstCart.Where(b => b.book.Id == book.Id)
                .FirstOrDefault();

            if(item == null)
            {
                this.lstCart.Add(new CartLine { book = book, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(Book book)
        {
            this.lstCart.RemoveAll(i => i.book.Id == book.Id);
        }

        public decimal ComputeTotalValue()
        {
            return this.lstCart.Sum(e => e.book.Price * e.Quantity);
        }

        public void Clear()
        {
            this.lstCart.Clear();
        }
    }

    public class CartLine
    {
        public Book book { get; set; }
        public int Quantity { get; set; }
    }   
}
