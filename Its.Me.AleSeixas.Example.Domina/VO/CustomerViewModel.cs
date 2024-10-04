using AutoMapper.Configuration.Annotations;
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
    public class CustomerViewModel
    {
        public Guid Identifier { get; set; }
        public string Code { get; set; }
        [Ignore]
        public string Name { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public Guid IdPersonal { get; set; }
        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAtUtc { get; set; }
        public DateTime? UpdateAtUtc { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string? UpdateBy { get; set; }
    }
}
