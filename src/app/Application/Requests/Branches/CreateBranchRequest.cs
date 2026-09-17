namespace Application.Requests;

public record CreateBranchRequest
{
    public string BranchName { get; init; } 
}