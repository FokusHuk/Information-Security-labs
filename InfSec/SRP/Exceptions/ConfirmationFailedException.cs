﻿using System;

namespace InfSec.SRP.Exceptions
{
    public class ConfirmationFailedException: Exception
    {
        public ConfirmationFailedException()
        : base("Confirmation failed")
        {
            
        }
    }
}