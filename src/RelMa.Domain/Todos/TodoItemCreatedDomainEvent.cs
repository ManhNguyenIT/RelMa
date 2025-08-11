using RelMa.Shared.Abstractions.Entity;

namespace RelMa.Domain.Todos;

public sealed record TodoItemCreatedDomainEvent(Ulid EventId) : IDomainEvent;
