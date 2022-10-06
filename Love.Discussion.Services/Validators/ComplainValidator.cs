using FluentValidation;
using Love.Discussion.Core.Entities;
using Love.Discussion.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Services.Validators
{
    public class ComplainValidator : AbstractValidator<ComplainDto>
    {
        public ComplainValidator()
        {
            RuleFor(c => c.Title).NotEmpty()
                .Length(1, 12)
                .WithErrorCode("Aaa")
                .WithMessage("afsjhu");
        }
    }
}
