using System;

namespace wotNext.Models
{
    public class User
    {
        private long Id { get; set; }
        private string Name { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
        private DateTime DateJoined { get; set; }
        private bool IsConnected { get; set; }
    }
}