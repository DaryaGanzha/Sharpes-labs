﻿using System;

namespace Shops
{
    public class ShopException : Exception
    {
        public ShopException()
        {
        }

        public ShopException(string message)
            : base(message)
        {
            Console.WriteLine(message);
        }

        public ShopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}