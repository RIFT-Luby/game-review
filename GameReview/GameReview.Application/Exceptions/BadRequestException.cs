using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
        public BadRequestException()
        {
        }

        public BadRequestException(string? message): base(message)
        {
        }

        public BadRequestException(string prop, string message) : this()
        {
            Errors.Add(new ValidationFailure(prop, message));
        }

        public BadRequestException(ValidationResult validationResult) : this()
        {
            Errors.AddRange(validationResult.Errors);
        }
    }
}
