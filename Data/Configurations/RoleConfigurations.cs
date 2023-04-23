using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            List<Role> roles = new List<Role>();
            roles.Add(new Role
            {
                RoleId = 1,
                Name = "Supplier"
            });
            roles.Add(new Role
            {
                RoleId = 2,
                Name = "Client"
            });
            roles.Add(new Role
            {
                RoleId = 3,
                Name = "Manger"
            });
            builder.HasData(roles);
        }

    }
}
