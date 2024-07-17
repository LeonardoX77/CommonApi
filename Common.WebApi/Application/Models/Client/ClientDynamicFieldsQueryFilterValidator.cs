using FluentValidation;

namespace Common.WebApi.Application.Models.Client
{
    namespace Common.WebApi.Application.Validators
    {
        /// <summary>
        /// Validator for ClientDynamicFieldsQueryFilter.
        /// </summary>
        public class ClientDynamicFieldsQueryFilterValidator : AbstractValidator<ClientDynamicFieldsQueryFilter>
        {
            public ClientDynamicFieldsQueryFilterValidator()
            {
                RuleFor(x => x.MaxId)
                    .GreaterThan(0).WithMessage("MaxId must be greater than 0.")
                    .When(x => x.MaxId.HasValue);

                RuleFor(x => x.MinId)
                    .GreaterThan(0).WithMessage("MinId must be greater than 0.")
                    .When(x => x.MinId.HasValue);

                RuleFor(x => x.ContainsName)
                    .NotEmpty().WithMessage("ContainsName cannot be empty.")
                    .MaximumLength(100).WithMessage("ContainsName cannot exceed 100 characters.")
                    .When(x => !string.IsNullOrEmpty(x.ContainsName));

                RuleFor(x => x.ListName)
                    .NotEmpty().WithMessage("ListName cannot be empty.")
                    .MaximumLength(100).WithMessage("ListName cannot exceed 100 characters.")
                    .When(x => !string.IsNullOrEmpty(x.ListName));

                RuleFor(x => x.FromDateCreated)
                    .LessThanOrEqualTo(x => x.ToDateCreated).WithMessage("FromDateCreated must be less than or equal to ToDateCreated.")
                    .When(x => x.FromDateCreated.HasValue && x.ToDateCreated.HasValue);

                RuleFor(x => x.ToDateCreated)
                    .GreaterThanOrEqualTo(x => x.FromDateCreated).WithMessage("ToDateCreated must be greater than or equal to FromDateCreated.")
                    .When(x => x.FromDateCreated.HasValue && x.ToDateCreated.HasValue);
            }
        }
    }
}
