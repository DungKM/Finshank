using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser: IdentityUser
    {
        // Additional properties can be added here if needed
        // For example, you might want to add FirstName, LastName, etc.
    }
 
}