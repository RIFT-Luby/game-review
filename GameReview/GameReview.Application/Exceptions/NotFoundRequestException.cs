using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace GameReview.Application.Exceptions
{
    public class NotFoundRequestException: Exception
    {
        public NotFoundRequestException()
        {
        }

        public NotFoundRequestException(string? message) : base(message)
        {
        }

    }
}
