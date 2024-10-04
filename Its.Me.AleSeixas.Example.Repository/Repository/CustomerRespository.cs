﻿using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Repository.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Repository.Repository
{
    [ExcludeFromCodeCoverage]
    public class CustomerRespository : Repository<Customer>
    {
        public CustomerRespository(ApiDbContext context) : base(context) { }
    }
}
