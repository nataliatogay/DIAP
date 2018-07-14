﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Interfaces
{
    public interface ISender
    {
        void Send(string to, string topic, string body, ICollection<string> file);
    }
}
