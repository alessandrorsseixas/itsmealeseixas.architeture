﻿using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Service.Interfaces;
using itsmealeseixas.architeture.repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Service.Services
{
    [ExcludeFromCodeCoverage]
    public class PersonalService : Service<Personal>, IPersonalService
    {
        public PersonalService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
