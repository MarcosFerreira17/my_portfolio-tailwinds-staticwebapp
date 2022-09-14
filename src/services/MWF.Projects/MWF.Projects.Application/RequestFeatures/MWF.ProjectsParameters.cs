namespace MWF.Projects.Application.RequestFeatures;

/// <summary>
/// Here you can apply all parameters for filtering your entity, the actual implementation it' is just a example;
/// </summary>
public class MWF.ProjectsParameters : RequestParameters
{
    public MWF.ProjectsParameters()
    {
        OrderBy = "name";
    }
    public uint MinId { get; set; }
    public uint MaxId { get; set; } = int.MaxValue;
    public bool ValidIdRange => MaxId > MinId;
    public string SearchTerm { get; set; }
}