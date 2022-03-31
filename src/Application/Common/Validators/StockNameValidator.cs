namespace Mortoff.Application.Common.Validators;
internal class StockNameValidator : AbstractValidator<string>
{
    public StockNameValidator()
    {
        RuleFor(x => x).MaximumLength(40).WithMessage(x => $"Részvény neve nem hosszabb-e mint 40 karakter. Amit megadtál {x.Length} hosszú")
            .Matches(@"^[A-Za-z0-9\s]*$").WithMessage("A részvény neve csak az angol ABC betűit, szóközt és számokat tartalmazhat");
    }
}
