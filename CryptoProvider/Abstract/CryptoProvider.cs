﻿using System.Collections.Generic;

namespace icModel.Abstract {
    public interface ICryptoProvider {
        ICryptoKey Key { get; set; }
        IAlphabet Alphabet { get; set; }
        string[] Encrypt(string[] message);
        string[] Decrypt(string[] message);
    }
}