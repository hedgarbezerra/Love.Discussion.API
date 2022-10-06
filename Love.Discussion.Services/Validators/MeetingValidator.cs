using FluentValidation;
using Love.Discussion.Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Services.Validators
{
    public class MeetingValidator : AbstractValidator<MeetingDto>
    {
        public MeetingValidator()
        {

        }
    }
}
