using BP.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Api.Service
{
    public interface IContactService
    {
        ContactDTO GetContactById(int id);
    }
}
