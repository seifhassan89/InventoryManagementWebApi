using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class RequestTypeConfigurations : IEntityTypeConfiguration<RequestType>
    {
        public void Configure(EntityTypeBuilder<RequestType> builder)
        {
            List<RequestType> requestTypes = new List<RequestType>();
            requestTypes.Add(new RequestType
            {
                RequestTypeId = 1,
                Name = "Supply Request"
            });
            requestTypes.Add(new RequestType
            {
                RequestTypeId = 2,
                Name = "Withdraw Request"
            });
            requestTypes.Add(new RequestType
            {
                RequestTypeId = 3,
                Name = "Transfer Request"
            });
            builder.HasData(requestTypes);
        }

    }
}
