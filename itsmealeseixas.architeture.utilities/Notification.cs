using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using itsmealeseixas.architeture.utilities.Helpers;
using itsmealeseixas.architeture.utilities.Seedworks.Abrastracts;
using System.Diagnostics.CodeAnalysis;

namespace itsmealeseixas.architeture.utilities.Seedworks
{
    [ExcludeFromCodeCoverage]
    public class Notification : EntityMemory
    {
        [Key]
        public int CodeNumber { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }

        [NotMapped]
        public List<Notification> Notifications { get; set; }

        public Notification()
        {

        }
        public Notification(string message)
        {
            Message = message;
        }
        public Notification(int codeNumber, string description, string message, bool mock = false)
        {
            CodeNumber = codeNumber;
            Message = message;
            Description = description;
            Identifier = new Guid();
            if (mock)
            {
                CreateAt = UtilsHelpers.GetDatetime();
                CreateBy = "system";
            }

            Active = true;
        }



    }
}
