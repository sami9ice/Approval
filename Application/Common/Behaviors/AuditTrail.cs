using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Application.Common.Behaviors
{
    /// <summary>
    /// The pipeline for audit trail.
    /// </summary>
    public class AuditTrail<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        /// <summary>
        /// Process method executes after the Handle method on your handler
        /// </summary>
        /// <param name="request">Request instance</param>
        /// <param name="response">Response instance</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// An await-able task
        /// </returns>

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            //todo
            return Task.CompletedTask;
        }
    }
}