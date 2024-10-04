using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domina.Entities
{
    public class Personal:Entity
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual Customer Customer { get; set; }

    }
}
