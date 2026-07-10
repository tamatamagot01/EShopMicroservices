using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using MediatR;

namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Business logic to create a product

            // Step 1 : create Product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            // Step 2 : save to database

            // Step 3 : return create product result
            return new CreateProductResult(Guid.NewGuid());


            throw new NotImplementedException();
        }
    }
}
