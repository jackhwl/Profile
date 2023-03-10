using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public class ProductService
{
    private readonly WenlinContext context;
	public ProductService(WenlinContext context)
	{
		this.context = context;
	}

	public async Task<Product> CreateProduct(string name, string description)
	{
		var product = new Product
		{
			Name = name,
			Description = description
		};

		await context.AddAsync(product);
		await context.SaveChangesAsync();

		return product;
	}
}
