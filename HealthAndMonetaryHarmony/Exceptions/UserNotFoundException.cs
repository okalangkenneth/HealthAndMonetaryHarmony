﻿using System;

namespace HealthAndMonetaryHarmony.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found.")
        {
        }
    }
}

