using System.Collections.Generic;

namespace Dealership.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }

        void AddComment(IComment comment);

        void RemoveComment(IComment comment);
    }
}
