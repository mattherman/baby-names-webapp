namespace BabyNames.Models;

public class Result<TResult, TError>
{
	public static Result<TResult, TError> Success(TResult result)
	{
		return new Result<TResult, TError> { Value = result };
	}

	public static Result<TResult, TError> Failure(TError error)
	{
		return new Result<TResult, TError> { Error = error };
	}
	public TResult? Value { get; init; }
	public TError? Error { get; init; }

	public bool IsError => Error != null;
	public bool IsSuccess => !IsError;
}
