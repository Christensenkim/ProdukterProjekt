﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProdukterProjekt.Core.Entity
{
    public class User
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public bool isAdmin { get; set; }
    }
}
