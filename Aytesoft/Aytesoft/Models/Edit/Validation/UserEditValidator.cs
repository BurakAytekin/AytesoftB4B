using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Mvc;

namespace Aytesoft.Models.Edit.Validation
{
    public class UserEditValidator : AbstractValidator<UserEdit>
    {
        public UserEditValidator()
        {
            RuleFor(User => User.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Bırakılamaz");
            RuleFor(User => User.UserName).MinimumLength(4).WithMessage("Kullanıcı Adı En Az 4 Karakterden Oluşmalıdır.");
            RuleFor(User => User.Password).NotEmpty().WithMessage("Parola Boş Bırakılamaz");
            RuleFor(User => User.Password).Length(3, 10).WithMessage("Parola 3 ile 10 Karakter Arası Olmalıdır.");
        }
    }
}