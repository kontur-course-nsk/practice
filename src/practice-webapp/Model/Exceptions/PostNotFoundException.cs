using System;

namespace Model.Exceptions
{
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException(string postId) : base($"Post with id {postId} not found.")
        {
        }
    }
}
