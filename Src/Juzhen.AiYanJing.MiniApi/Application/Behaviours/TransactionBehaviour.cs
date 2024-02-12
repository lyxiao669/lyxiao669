using Juzhen.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
       
        private readonly ApplicationDbContext _context;
        
        public TransactionBehaviour(ApplicationDbContext context,
            ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _context = context ?? throw new ArgumentException(null, nameof(context));
            _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetType().Name;

            try
            {
                if (_context.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = await _context.BeginTransactionAsync())
                    using (_logger.BeginScope("TransactionContext:{0}", transaction.TransactionId))
                    {
                        var transactionId = transaction.TransactionId;
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);
                        response = await next();
                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);
                        await _context.CommitTransactionAsync(transaction);
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                throw;
            }
        }
    }
}
