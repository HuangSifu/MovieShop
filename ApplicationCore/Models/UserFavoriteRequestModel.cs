using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class UserFavoriteRequestModel
    {
        public int UserId { set; get; }
        public int MovieId { set; get; }
    }
}
