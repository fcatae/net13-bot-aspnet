using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot
{
    interface IMessageRepository
    {
        void postMessage(MessageBson message);
    }
}
