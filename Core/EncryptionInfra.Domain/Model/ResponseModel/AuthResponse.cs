﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionInfra.Domain.Model
{
    public class AuthResponse : ResponseBase
    {
        public string Token { get; set; }
    }
}
