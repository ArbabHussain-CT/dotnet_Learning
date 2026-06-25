namespace demoWithDotnet10AndEfCore.Dtos;

public class UpdateCharacterRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;

    public string Game { get; set; } = String.Empty;

    public string Role { get; set; } = String.Empty;
}