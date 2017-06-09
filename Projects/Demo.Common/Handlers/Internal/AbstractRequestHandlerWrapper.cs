namespace Demo.Common.Handlers.Internal
{
    using Interfaces;

    internal abstract class AbstractRequestHandlerWrapper<TResult>
    {
        public abstract TResult Handle(IRequest<TResult> message);
    }
}