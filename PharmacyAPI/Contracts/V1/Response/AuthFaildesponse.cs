using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyAPI.Contracts.V1.Response
{
    public class Faildesponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
    public class AuthFaildesponse : Faildesponse
    {

    }
}

