using System;

namespace Collage_Management.Controllers
{
    internal class EmailAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}