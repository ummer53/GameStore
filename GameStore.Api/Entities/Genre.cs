namespace GameStore.Api.Entities;

public class Genre
{
    public int Id { get; set; }
    public required Genres Name { get; set; }

}


public enum Genres
{
    Action,
    Adventure,
    RPG,
    Strategy,
    Simulation,
    Sports,
    Racing,
    Puzzle,
    Horror,
    Fighting
}