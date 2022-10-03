using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;
using UniversalAuthenticator.Domain.Data;
using UniversalAuthenticator.Domain.Entities;

namespace UniversalAuthenticator.Common.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<ApplicationUser> _appUserRepository;

        public AuthService(IRepository<ApplicationUser> appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        

        #region Private Methods
        

        #endregion

    }
}
