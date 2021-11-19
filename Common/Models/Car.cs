using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class Car : MongoDocument
    {
        public string Name  { get; set; }
        public List<string> Options  { get; set; }
        public decimal? Price   { get; set; }
        public string Specification { get; set; }
    }
}
