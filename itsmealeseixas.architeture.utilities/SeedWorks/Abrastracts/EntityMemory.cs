using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itsmealeseixas.architeture.utilities.Seedworks.Abrastracts
{
    [ExcludeFromCodeCoverage]
    public class EntityMemory
    {
        [Key]
        public Guid Identifier { get; set; }
    }
}
