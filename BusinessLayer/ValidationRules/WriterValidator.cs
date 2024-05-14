using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(m => m.WriterName).NotEmpty().WithMessage("Yazar Adı Boş Geçilemez !");
            RuleFor(m => m.WriterSurname).NotEmpty().WithMessage("Yazar Soyadı Boş Geçilemez !");
            RuleFor(m => m.About).NotEmpty().WithMessage("Hakkında Kısmı Boş Geçilemez !");
            RuleFor(m => m.WriterName).MinimumLength(2).WithMessage("Lütfen En Az 2 Karakter Giriniz.");
            RuleFor(m => m.WriterSurname).MinimumLength(2).WithMessage("Lütfen En Az 2 Karakter Giriniz.");
            RuleFor(m => m.Mail).NotEmpty().WithMessage("E-mail Boş Geçilemez !");
            RuleFor(m => m.WriterTitle).NotEmpty().WithMessage("Meslek Adı Boş Geçilemez !");
            RuleFor(m => m.WriterTitle).MinimumLength(2).WithMessage("Lütfen En Az 2 Karakter Giriniz.");
        }
    }
}
