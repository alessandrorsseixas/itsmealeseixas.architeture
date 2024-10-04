using itsmealeseixas.architeture.domain.Seedworks.Abrastracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domina.Entities
{
    public class Customer : Entity
    {
        public string Code { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public Guid IdPersonal { get; set; }
        public virtual Personal Personal { get; set; }

    }
}
