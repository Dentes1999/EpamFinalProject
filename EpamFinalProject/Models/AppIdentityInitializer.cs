using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
namespace EpamFinalProject.Models
{
    //CreateDatabaseIfNotExists
    public class AppIdentityInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "Admin" };
            var role2 = new IdentityRole { Name = "User" };
            var role3 = new IdentityRole { Name = "Manager" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "dteslenko777@gmail.com", UserName = "dteslenko777@gmail.com" };
            string password = "12345dtes";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
            }

            var manager = new ApplicationUser { Email = "denys.teslenko@nure.ua", UserName = "denys.teslenko@nure.ua" };
            string password1 = "2281488";
            result = userManager.Create(manager, password1);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                
                userManager.AddToRole(manager.Id, role2.Name);
                userManager.AddToRole(manager.Id, role3.Name);
            }
            var user = new ApplicationUser { Email = "dtes228@gmail.com", UserName = "dtes228@gmail.com" };
            password = "2281488";
            result = userManager.Create(user, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль

                userManager.AddToRole(user.Id, role2.Name);
                
            }
            user = new ApplicationUser { Email = "dtesla228@gmail.com", UserName = "dtesla228@gmail.com" };
            password = "2281488";
            result = userManager.Create(user, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль

                userManager.AddToRole(user.Id, role2.Name);

            }

            base.Seed(context);
        }
    }
}