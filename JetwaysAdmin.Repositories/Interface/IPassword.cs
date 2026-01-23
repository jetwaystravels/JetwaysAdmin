using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetwaysAdmin.Repositories.Interface
{
    public interface IPassword
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
