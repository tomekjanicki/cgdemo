namespace Demo.Common.Facades
{
    using System;
    using AutoMapper;
    using Handlers.Interfaces;
    using Shared;
    using Types;
    using Types.FunctionalExtensions;

    public static class Helper
    {
        public static IResult<TDto, Error> GetItem<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : class, IRequest<IResult<TObject, Error>>
        {
            return ErrorResultExtensions
                .OnSuccess(queryResult, mediator.Send, Error.CreateGeneric)
                .OnSuccess(dto => GetMappedResult<TDto, TObject>(dto, mapper));
        }

        public static IResult<TDto, Error> GetItemSimple<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            return GetItems<TDto, TQuery, TObject>(mediator, mapper, queryResult);
        }

        public static IResult<TDto, Error> GetItemSimple<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, TQuery query)
            where TQuery : IRequest<TObject>
        {
            return GetItems<TDto, TQuery, TObject>(mediator, mapper, query);
        }

        public static IResult<TDto, Error> GetItems<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, IResult<TQuery, NonEmptyString> queryResult)
            where TQuery : IRequest<TObject>
        {
            return queryResult.OnSuccess(query => GetMappedResult<TDto, TObject>(mediator.Send(query), mapper), Error.CreateGeneric);
        }

        public static IResult<TDto, Error> GetItems<TDto, TQuery, TObject>(IMediator mediator, IMapper mapper, TQuery query)
            where TQuery : IRequest<TObject>
        {
            return GetMappedResult<TDto, TObject>(mediator.Send(query), mapper);
        }

        public static IResult<Error> Delete<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<Error>>
        {
            return commandResult.OnSuccess(command => mediator.Send(command), Error.CreateGeneric);
        }

        public static IResult<Error> Update<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : IRequest<IResult<Error>>
        {
            return Delete(mediator, commandResult);
        }

        public static IResult<TDto, Error> Insert<TDto, TCommand, TObject>(IMediator mediator, IMapper mapper, IResult<TCommand, NonEmptyString> commandResult)
            where TCommand : class, IRequest<IResult<TObject, Error>>
        {
            return GetItem<TDto, TCommand, TObject>(mediator, mapper, commandResult);
        }

        public static IResult<Error> Execute<TCommand, TResult>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult, Action<TCommand, TResult> actionAfterExecutingCommand)
            where TCommand : IRequest<IResult<TResult, Error>>
        {
            return commandResult.OnSuccess(command => Execute(command, mediator, actionAfterExecutingCommand), Error.CreateGeneric);
        }

        public static IResult<Error> Execute<TCommand>(IMediator mediator, IResult<TCommand, NonEmptyString> commandResult, Action<TCommand> actionAfterExecutingCommand)
            where TCommand : IRequest
        {
            return commandResult.OnSuccess(command => Execute(command, mediator, actionAfterExecutingCommand), Error.CreateGeneric);
        }

        private static IResult<TDto, Error> GetMappedResult<TDto, TObject>(TObject obj, IMapper mapper)
        {
            return Result<TDto, Error>.Ok(mapper.Map<TDto>(obj));
        }

        private static IResult<Error> Execute<TCommand, TResult>(TCommand command, IMediator mediator, Action<TCommand, TResult> actionAfterExecutingCommand)
            where TCommand : IRequest<IResult<TResult, Error>>
        {
            var result = mediator.Send(command);

            return result.IsSuccess ? Result<Error>.Ok().Tee(r => actionAfterExecutingCommand(command, result.Value)) : result;
        }

        private static IResult<Error> Execute<TCommand>(TCommand command, IMediator mediator, Action<TCommand> actionAfterExecutingCommand)
            where TCommand : IRequest
        {
            mediator.Send(command);

            return Result<Error>.Ok().Tee(r => actionAfterExecutingCommand(command));
        }
    }
}
