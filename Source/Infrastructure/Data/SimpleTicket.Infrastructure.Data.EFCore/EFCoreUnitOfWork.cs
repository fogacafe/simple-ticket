using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleTicket.Domain.SeedWork;
using SimpleTicket.Infrastructure.Data.EFCore.Contexts;

namespace SimpleTicket.Infrastructure.Data.EFCore;

public class EFCoreUnitOfWork(SimpleTicketContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    
    public async Task CommitAsync()
    {
        if(_transaction != null)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if(_transaction != null)
            await _transaction?.RollbackAsync()!;
    }

    public async Task BeginTransaction()
    {
        if(_transaction != null)
            _transaction = await context.Database.BeginTransactionAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _transaction = null;
    }
}