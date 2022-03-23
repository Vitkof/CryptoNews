using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.Queries.Role
{
    public class GetRoleNameByEmailQuery : IQuery<string>
    {
        public string Email { get; set; }
    }
}
