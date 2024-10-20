using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class ConnectionStringNotFoundException(string Key) : NotFoundException("Product", Key)
    {
    }
}
