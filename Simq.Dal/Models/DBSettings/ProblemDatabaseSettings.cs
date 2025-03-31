namespace Simq.Dal.Models;

public record ProblemDatabaseSettings
{
    public string ConnectionString { get; init; }
    
    public string DatabaseName { get; init; }
    
    public string CollectionName { get; init; }
}