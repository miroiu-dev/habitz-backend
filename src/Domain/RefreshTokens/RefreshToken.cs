using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Users;

namespace Domain.RefreshTokens;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public int UserID {  get; set; }
    public DateTime ExpiresOnUtc {  get; set; }

    public User User { get; set; }
}
