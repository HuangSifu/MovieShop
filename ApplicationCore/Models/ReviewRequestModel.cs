using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ReviewRequestModel
    {
        public int userId { get; set; }
        public int movieId { get; set; }
        public string reviewText { get; set; }
        public decimal Rating { get; set; }
    }
}
