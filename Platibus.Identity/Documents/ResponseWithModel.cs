using System;

namespace Platibus.Identity.Documents
{
    public class ResponseWithModel<T> : Response where T : class
    {
        public T Entity { get; }
        
        public ResponseWithModel(bool isSuccessful, T entity, string message = null, Exception exception = null) : base(isSuccessful, message, exception)
        {
            Entity = entity;
        }

        public static ResponseWithModel<T> Successfull(T entity)
        {
            return new ResponseWithModel<T>(true, entity);
        }

        public static ResponseWithModel<T> Unsuccessful(string message = null, Exception exception = null)
        {
            return new ResponseWithModel<T>(false, null, message, exception);
        }
    }
}