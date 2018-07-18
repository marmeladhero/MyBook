using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryData.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { set; get; }
    }
}
