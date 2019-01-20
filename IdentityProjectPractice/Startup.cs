using IdentityProjectPractice.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Web;

[assembly: OwinStartupAttribute(typeof(IdentityProjectPractice.Startup))]
namespace IdentityProjectPractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoleUser();
        }

        private void CreateRoleUser()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManage = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            //in startUp I am Creating the first Admin role And creating the defualt adminUser

            if (!roleManage.RoleExists("Admin"))
            {

                //First Create Teh Admin role

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManage.Create(role);


                //Here We Will create th Admin User who will mange the WEbsite

                var user = new ApplicationUser();

                user.UserName = "Admin";
                user.Email = "Admin@gmail.com";

                string UserPwd = "A@Z200711";

                var checkUser = userManager.Create(user, UserPwd);


                //Add Default User To Admin

                if (checkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }

            }
          



        }
    }
}
