namespace SirajTech.Helpers.Core
{
    public enum ViolationType
    {
        Unknown,
        General,
        DatabaseException,
        Required,
        Invalid,
        Duplicated,
        NotFound,
        MaxLength,
        MinLength,
        Mismatch,
        Related,
        NotAllowed,
        ShouldBeBigger,
        ShouldBeSmaller,
        Null,
    }
}