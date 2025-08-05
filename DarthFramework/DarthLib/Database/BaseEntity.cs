using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DarthLib.Database
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
