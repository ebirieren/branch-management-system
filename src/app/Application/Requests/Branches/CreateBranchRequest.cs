namespace Application.Requests;

public record CreateBranchRequest
{
    public required string BranchName { get; init; }
}
