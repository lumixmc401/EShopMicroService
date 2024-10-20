namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand
        (Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidation()
        {
            RuleFor(r=>r.Id).NotEmpty().WithMessage("產品Id不可為空");
            RuleFor(r => r.Name).NotEmpty().WithMessage("產品名稱不可為空")
                .Length(2, 150).WithMessage("產品名稱長度必須介於2到150字元長度");
            RuleFor(r => r.Price).GreaterThan(0).WithMessage("產品價格必須大於0");
        }
    }

    public class UpdateProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is not null)
            {
                product.Name = command.Name;
                product.Category = command.Category;
                product.Description = command.Description;
                product.ImageFile = command.ImageFile;
                product.Price = command.Price;
                session.Update(product);
                await session.SaveChangesAsync(cancellationToken);
                return new UpdateProductResult(true);
            }
            throw new ProductNotFoundException(command.Id);
        }
    }
}
