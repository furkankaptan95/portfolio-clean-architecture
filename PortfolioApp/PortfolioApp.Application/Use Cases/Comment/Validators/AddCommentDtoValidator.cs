using FluentValidation;
using PortfolioApp.Core.DTOs.Web.Comment;

namespace PortfolioApp.Application.Use_Cases.Comment.Validators;
public class AddCommentDtoValidator : AbstractValidator<AddCommentDto>
{
    public AddCommentDtoValidator()
    {
        RuleFor(x => x)
           .Must(x =>
               (x.UnsignedCommenterName != null && x.UserId == null && x.SignedCommenterName == null) ||
               (x.UnsignedCommenterName == null && x.UserId != null && x.SignedCommenterName != null))
           .WithMessage("Eğer UnsignedCommenterName sağlanmışsa, UserId ve SignedCommenterName null olmalıdır. Aksi takdirde, hem UserId hem de SignedCommenterName sağlanmalıdır.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .When(x => x.UnsignedCommenterName is null)
            .WithMessage("Eğer UnsignedCommenterName sağlanmamışsa, UserId 0'dan büyük olmalıdır.");

        RuleFor(x => x.UnsignedCommenterName)
            .MaximumLength(30)
            .When(x => x.UnsignedCommenterName != null)
            .WithMessage("UnsignedCommenterName en fazla 30 karakter olmalıdır.");

        RuleFor(comment => comment.Content)
           .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("İçerik kısmı boş olamaz.")
           .MaximumLength(300).WithMessage("İçerik maksimum 300 karakter olabilir.");

        RuleFor(comment => comment.BlogPostId)
           .GreaterThan(0).WithMessage("0'dan büyük bir BlogPostId gerekli.");
    }
}
