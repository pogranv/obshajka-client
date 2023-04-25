using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Models
{
    internal sealed record User(string Email, string Password, string Name);
}
