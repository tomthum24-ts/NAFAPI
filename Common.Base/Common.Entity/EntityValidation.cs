using NAFCommon.Base.Common.MethodResult;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NAFCommon.Base.Common.Entity
{
    public abstract class EntityValidator
    {
        protected Assembly GetAssembly() => GetType().Assembly;

        protected List<Result> _errorMessages = new();


        /// <summary>
        /// List of ErrorResult.
        /// </summary>
        [NotMapped] protected IReadOnlyCollection<Result> ErrorMessages => _errorMessages;


        /// <summary>
        /// Check If entity is valid or not.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            var context = new ValidationContext(this, null, null);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                foreach (var result in results)
                {
                    var errorResult = new Result
                    {
                        Message = result.ErrorMessage
                    };

                    errorResult.Message = errorResult.Message.StartsWith(Settings.CommonErrorPrefix, StringComparison.InvariantCulture) ? ErrorHelpers.GetCommonErrorMessage(result.ErrorMessage) : ErrorHelpers.GetErrorMessage(result.ErrorMessage, GetAssembly());

                    foreach (var property in result.MemberNames)
                    {
                        var propertyInfo = context.ObjectType.GetProperty(property);

                        var value = propertyInfo.GetValue(context.ObjectInstance, null);

                        errorResult.Values.Add(ErrorHelpers.GenerateErrorResult(property, value));
                    }
                    _errorMessages.Add(errorResult);
                }
            }

            return _errorMessages.Count == 0;
        }
    }
}
