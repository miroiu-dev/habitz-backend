using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.SignIn;

public class AuthenticationResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
