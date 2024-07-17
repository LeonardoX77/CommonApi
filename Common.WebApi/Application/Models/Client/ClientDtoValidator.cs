using FluentValidation;
using Common.Core.CustomExceptions;

namespace Common.WebApi.Application.Models.Client
{
    public class ClientDtoValidator : AbstractValidator<ClientDto>
    {
        public ClientDtoValidator() : this(false) { }
        public ClientDtoValidator(bool isPatch = false)
        {
            if (!isPatch)
            {
                RuleFor(dto => dto.Name)
                    .NotEmpty()
                    .WithErrorCode($"{(int)ApiErrorCode.REQUIRED_FIELD}")
                    .MaximumLength(100)
                    .WithErrorCode($"{(int)ApiErrorCode.STRING_MAX_LENGTH}");

                RuleFor(dto => dto.Email)
                    .NotEmpty()
                    .WithErrorCode($"{(int)ApiErrorCode.REQUIRED_FIELD}")
                    .EmailAddress()
                    .WithErrorCode($"{(int)ApiErrorCode.INVALID_EMAIL_FORMAT}");

                RuleFor(dto => dto.PhoneNumber)
                    .NotEmpty()
                    .WithErrorCode($"{(int)ApiErrorCode.REQUIRED_FIELD}")
                    .MaximumLength(15)
                    .WithErrorCode($"{(int)ApiErrorCode.STRING_MAX_LENGTH}");
            }
            else
            {
                When(dto => dto.Name != null, () =>
                {
                    RuleFor(dto => dto.Name)
                        .MaximumLength(100)
                        .WithErrorCode($"{(int)ApiErrorCode.STRING_MAX_LENGTH}");
                });

                When(dto => dto.Email != null, () =>
                {
                    RuleFor(dto => dto.Email)
                        .EmailAddress()
                        .WithErrorCode($"{(int)ApiErrorCode.INVALID_EMAIL_FORMAT}");
                });

                When(dto => dto.PhoneNumber != null, () =>
                {
                    RuleFor(dto => dto.PhoneNumber)
                        .MaximumLength(15)
                        .WithErrorCode($"{(int)ApiErrorCode.STRING_MAX_LENGTH}");
                });
            }
        }
    }
}
