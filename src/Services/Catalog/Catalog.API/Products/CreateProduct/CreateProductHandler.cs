namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand
    (string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().WithMessage("產品名稱不可為空。");
        RuleFor(r => r.Category).NotEmpty().WithMessage("產品類別不可為空。");
        RuleFor(r => r.ImageFile).NotEmpty().WithMessage("產品圖片網址不可為空");
        RuleFor(r => r.Price).GreaterThan(0).WithMessage("產品價格必須大於0。");
    }
}
internal class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price,
        };
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(Guid.NewGuid());
    }
}
