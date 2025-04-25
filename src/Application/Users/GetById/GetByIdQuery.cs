using Application.Abstractions.Messaging;

namespace Application.Users.GetById;

public sealed record GetByIdQuery(int UserId) : IQuery<UserResponse>;
