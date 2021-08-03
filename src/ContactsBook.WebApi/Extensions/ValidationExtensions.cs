using ContactsBook.Utils;
using FluentValidation;
using FluentValidation.Validators;

namespace ContactsBook.WebApi.Extensions
{
    public static class ValidationExtensions
    {
        /// <summary>
        ///     Defines an email validator on the current rule builder for string properties.
        ///     Validation will fail if the value returned by the lambda is not a valid email address.
        /// </summary>
        /// <typeparam name="TEntity">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<TEntity, string> CbEmailAddress<TEntity>(
            this IRuleBuilderInitial<TEntity, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new ContactsBookEmailValidator<TEntity>());
        }

        /// <summary>
        ///     Defines an name validator on the current rule builder for string properties.
        ///     Validation will fail if the value returned by the lambda is not a valid name.
        /// </summary>
        /// <typeparam name="TEntity">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<TEntity, string> CbName<TEntity>(
            this IRuleBuilderInitial<TEntity, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull()
                .Length(3, 28);
        }

        /// <summary>
        ///     Defines an phone validator on the current rule builder for 'long' properties.
        ///     Validation will fail if the value returned by the lambda is not a valid phone number.
        /// </summary>
        /// <typeparam name="TEntity">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<TEntity, long> CbPhoneNumber<TEntity>(
            this IRuleBuilderInitial<TEntity, long> ruleBuilder)
        {
            return ruleBuilder.InclusiveBetween(CommonHelper.MIN_VALID_PHONE_NUMBER,
                CommonHelper.MAX_VALID_PHONE_NUMBER);
        }

        internal class ContactsBookEmailValidator<T> : AspNetCoreCompatibleEmailValidator<T>
        {
            public override bool IsValid(ValidationContext<T> context, string value)
            {
                return CommonHelper.IsValidEmail(value);
            }
        }
    }
}