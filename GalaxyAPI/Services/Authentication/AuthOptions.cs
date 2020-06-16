using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyAPI.Services.Authentication
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        public const string KEY = "0d5b3235a8b403c3dab9c3f4f65c07fcalskd234n1k41230";   // ключ для шифрации
        public const int LIFETIME = 600; // время жизни токена - 600 минут test
    }
}
