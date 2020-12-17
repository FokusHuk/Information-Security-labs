using System;

namespace InfSec.SRP
{
    public class ConfirmationFailedException: Exception
    {
        public ConfirmationFailedException()
        : base("Confirmation failed")
        {
            
        }
    }
}