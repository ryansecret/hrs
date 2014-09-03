using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using FluentValidation;
using Hsr.Models;
using Microsoft.Ajax.Utilities;

namespace Hsr.Validators
{
    public class RyanValidator : AbstractValidator<Ryan>
    {
        public RyanValidator()
        {
            RuleFor(d => d.Name).NotEmpty().Length(5,20);
        }
    }
}