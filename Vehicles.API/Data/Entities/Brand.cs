﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class Brand
    {
            public int id { get; set; }

            [Display(Name = "Marca")]
            [MaxLength(50, ErrorMessage = "El campo{0} no puede tener mas de {1} caracteres")]
            [Required(ErrorMessage = "El campo {0} es obligatorio")]
            public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
