using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(m => m.CategoryName).NotEmpty().WithMessage("Kategori Adı Boş Geçilemez !");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Açıklama Boş Geçilemez !");
            RuleFor(m => m.CategoryName).MinimumLength(3).WithMessage("Lütfen En Az 3 Karakter Giriniz.");
            RuleFor(m => m.CategoryName).MaximumLength(20).WithMessage("Lütfen En Fazla 20 Karakter Giriniz.");

        }
    }
}
