using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using System.Threading.Tasks;
using Data;
using System.Threading;
using DTO;
using System.Linq;
namespace usercustom
{
    
    public class Usercustom: UserStore<IdentityUser>{
           public Usercustom(EcommerContext context, IdentityErrorDescriber describer = null) : base(context,describer)
        {

        }
        public virtual Task<IdentityUser> FindbyPhonenumberasync(string phonenumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return Users.FirstOrDefaultAsync(u => u.PhoneNumber == phonenumber, cancellationToken);
        }
    } 
}