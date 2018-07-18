using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessorOrder(Cart cart, User user);
    }
}
