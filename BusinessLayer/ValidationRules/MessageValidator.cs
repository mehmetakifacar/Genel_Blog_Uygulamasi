using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).EmailAddress().NotEmpty().WithMessage("Mail Adresini Boş Geçemezsiniz !");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu Adını Boş Geçemezsiniz !");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Mesaj Kısmı Boş Geçilemez !");
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen En Az 3 Karakter Giriniz.");
            RuleFor(x => x.Subject).MaximumLength(100).WithMessage("Lütfen En Fazla 100 Karakter Giriniz.");


        }
    }
}
