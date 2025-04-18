﻿using NextHome.Application.UseCases.Properties.Interfaces;
using NextHome.Domain.Entities;
using NextHome.Domain.Interfaces.Repositories;

namespace NextHome.Application.UseCases.Properties;

public class DeletePropertyUseCase : IDeletePropertyUseCase
{
    private readonly IRepository<Property> _repository;

    public DeletePropertyUseCase(IRepository<Property> repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}
