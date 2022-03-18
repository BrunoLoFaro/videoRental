using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class summaryViewModel
    {
        public string UserName{ get; set; }
        public List<Movie> MovieList { get; set; }
        public Order Order { get; set; }
        
    }
}