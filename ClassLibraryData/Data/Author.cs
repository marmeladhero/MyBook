using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClassLibraryData.Data
{
    public class Author
    {
        [Display(Name="ID")]
        public int Id { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Surname")]
        public string SurName { get; set; }

        [Display(Name = "Second name")]
        public string SecondName { get; set; }
    }
}
