using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationFailedException : ApplicationException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationFailedException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures have occured.")
        {
            Errors = failures.GroupBy(f => f.PropertyName, f => f.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
