using System;

namespace CMS.Model.Exceptions
{
    public class CanNotAddTalkException : Exception
    {
        public CanNotAddTalkException(string message) :base(message)
        {
        }
    }
}
