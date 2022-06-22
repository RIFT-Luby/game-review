using GameReview.Domain.Core;
using System.Linq.Expressions;

namespace GameReview.Application.Params
{
    public abstract class BaseParams<T> where T : Register
    {
        public abstract Expression<Func<T, bool>> Filter();
        protected BaseParams() { }
    }
}
