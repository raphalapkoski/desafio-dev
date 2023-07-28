namespace DesafioDev.Application.Response;

public sealed record BaseResponse<T>(bool Success, T Data, List<Error> Errors);
