using System;
using System.Collections.Generic;
using System.Text;

namespace ppedv.VollE.Model.Faults
{
    public class ConcurrencyException : Exception
    {

        public Action UserWins { get; set; }
        public Action DbWins { get; set; }

        public ConcurrencyException(string msg) : base(msg)
        { }
    }
}
