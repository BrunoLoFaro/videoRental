using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class GenreDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public GenreDto(byte id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}