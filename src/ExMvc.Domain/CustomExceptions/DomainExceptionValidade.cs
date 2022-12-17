using System;

namespace ExMvc.Domain.CustomExceptions
{
    public class DomainExceptionValidade : Exception
    {
        public DomainExceptionValidade(string message) : base(message) { }
        

        public static void When(bool condition, string message)
        {
            if (condition)
                throw new DomainExceptionValidade(message); 
        }
    }
}
