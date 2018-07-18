using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData.Data
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Desription")]
        public string Desription { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }



        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Book other = obj as Book;
            return other != null && other.Id == this.Id;
        }
    }
}
