using System;
using System.Collections.Generic;

#nullable disable

namespace shoping.Models
{
    public partial class User
    {
        public User()
        {
            Baskets = new HashSet<Basket>();
        }

        public int UsersId { get; set; }
        public string UsersName { get; set; }
        public string UsersSurname { get; set; }
        public string UsersMail { get; set; }
        public int? UsersPassword { get; set; }

        public virtual ICollection<Basket> Baskets { get; set; }
    }
}
