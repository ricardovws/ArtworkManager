﻿using ArtworkManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models
{
    public class Artwork
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Author Owner { get; set; }
        public ArtworkStatus Status { get; set; }
        public DateTime BirthDate { get; set; }

        public Artwork()
        {

        }

        
        public Artwork(int id, string code, Author owner)
        {
            Id = id;
            Code = code;
            Owner = owner;
            Status = ArtworkStatus.FreeToUse;
        }
    }
}