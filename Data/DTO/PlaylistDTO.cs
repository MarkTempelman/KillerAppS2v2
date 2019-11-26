using System;
using System.Collections.Generic;
using System.Text;
using Org.BouncyCastle.Asn1.Microsoft;

namespace Data.DTO
{
    public class PlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public bool IsPublic { get; set; }
        public List<MovieDTO> Movies { get; set; }
    }
}
