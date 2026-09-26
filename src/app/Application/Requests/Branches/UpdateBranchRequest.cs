namespace Application.Requests;

public record UpdateBranchRequest
{
    public required int BranchId { get; init; }
    public required string BranchName { get; init; }

}