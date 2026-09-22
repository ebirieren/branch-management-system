namespace Application.Requests;

public record UpdateBranchRequest
{
    public required string BranchName { get; init; }

}