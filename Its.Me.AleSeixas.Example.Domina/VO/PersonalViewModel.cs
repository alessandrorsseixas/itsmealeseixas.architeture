using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Domina.VO
{
    [ExcludeFromCodeCoverage]
    [NotMapped]
    public class PersonalViewModel
    {
        public Guid Identifier { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAtUtc { get; set; }
        public DateTime? UpdateAtUtc { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
