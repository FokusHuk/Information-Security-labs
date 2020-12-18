using System;

namespace InfSec.SRP.Exceptions
{
    public class ConnectionInterruptedException: Exception
    {
        public ConnectionInterruptedException()
            :base("Connection interrupted. Parameter \"u\" can not be zero.")
        {
            
        }
    }
}