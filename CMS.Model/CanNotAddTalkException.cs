using System;

namespace CMS.Model
{
    public class CanNotAddTalkException : Exception
    {
        public CanNotAddTalkException(string message) :base(message)
        {
        }
    }
}
