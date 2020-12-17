using System;

namespace InfSec.SRP
{
    public class AuthenticationFailedException: Exception
    {
        public AuthenticationFailedException()
        :base("Authentication failed. Parameter \"A\" can not be zero.")
        {
            
        }
    }
}