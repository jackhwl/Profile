using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Infrastructure.DesignTime;

internal class DesignTimeModelConfiguration : IModelConfiguration
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
    }
}
