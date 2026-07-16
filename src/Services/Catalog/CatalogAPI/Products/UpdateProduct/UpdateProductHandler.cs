using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    internal class UpdateProductCommandHandler
        (IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

            // Business logic to update a product

            // Step 1 : get Product from database
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }

            // Step 2 : update Product entity
            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            // Step 3 : save update Product entity to database
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            // Step 4 : return update product status result
            return new UpdateProductResult(true);


            throw new NotImplementedException();
        }
    }
}
