using dotnet_vsa_template.Common.Results;

namespace dotnet_vsa_template.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
  where TQuery : IQuery<TResponse>
{
  Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}
