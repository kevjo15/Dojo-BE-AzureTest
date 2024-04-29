using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.ModuleQueries.GetModuleById
{
    public class GetModuleByIdQueryValidator : AbstractValidator<GetModuleByIdQuery>
    {
        public GetModuleByIdQueryValidator()
        {
            RuleFor(query => query.ModuleId).MustBeValidId();
        }
    }
}
