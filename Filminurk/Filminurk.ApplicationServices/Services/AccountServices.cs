using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace Filminurk.ApplicationServices.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailsServices _emailsServices;


        public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailsServices emailsServices<)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailsServices = emailsServices;
        }
    }
}
