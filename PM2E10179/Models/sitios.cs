using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E10179.Models
{
    public class sitios{

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public byte[] foto { get; set; }

        [MaxLength(200)]
        public string latitud { get; set; }

        [MaxLength(200)]
        public string longitud { get; set; }

        [MaxLength(200)]
        public string description { get; set; }
    }
}
