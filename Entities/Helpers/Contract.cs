namespace Entities
{
    using System;

    public class Contract
    {
        public static void Requires(bool condition, string message = null)
        {
            Requires<ArgumentNullException>(condition, message);
        }
        public static void Requires<TException>(bool condition, string message = null) where TException : Exception, new()
        {
            if (!condition)
            {
                TException e;
                try
                {
                    message = message ?? "Unexpected Condition";
                    e = Activator.CreateInstance(typeof(TException), message) as TException;
                }
                catch (MissingMethodException)
                {
                    e = new TException();
                }
                throw e;
            }
        }
    }
}
