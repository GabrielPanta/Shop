namespace Shop.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();
             
            var user = await this.userHelper.GetUserByEmailAsync("gabrielpanta1234@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Gabriel",
                    LastName = "Panta Jimenez",
                    Email = "gabrielpanta1234@gmail.com",
                    UserName = "gabrielpanta1234@gmail.comm",
                    PhoneNumber = "969060448"
                    
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }


            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone X",user);
                this.AddProduct("Magic Mouse",user);
                this.AddProduct("iWatch",user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name,User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User=user
            });
        }
    }

}
