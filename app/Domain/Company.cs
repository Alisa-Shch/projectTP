﻿namespace Domain
{
    internal class Company
    {
        //public Guid CID { get; }
        public string Name { get; }
    
        private Company(string name)
        {
            Name = name;
        }

        public static Company Create(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            return new(name);
        }
    }
}