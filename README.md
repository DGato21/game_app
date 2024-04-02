# Game Application (based for TurtleChallenge)

Game Application based in TurtleChallenge.

This application was created with the main purprose to respond to the TurtleChallenge but to be prepared to be used to quickly create other game or challenges with abstract and generic uses of Game concepts that can be shared across all games.

## Constraints and Assumptions

A default settings is read by default if no arguments are provided to the application.
By default it will read the following files: game-settings.json & moves.json.

These two files can be used as an example to describe the input of both files needed for the application
- game-settings.json: Settings definition to define the TurtleChallenge Game Board
- moves.json: Settings definition to define the TurtleChallenge Sequence of Moves to be executed

In the class Infrastructure.Crosscutting.TurtleChallengeSettings, it is described for each class and field the intent of itself. In the next section it will be provided that same information

## Settings Input Files

```
/// <summary>
/// Settings definition to define the TurtleChallenge Game Board
/// defined in a '.json' file with the specifications defined below
/// Default: settings/turtleChallenge/game-settings.json
/// </summary>
public class TurtleGameBoardSettings
{
    /// <summary>
    /// Board size
    /// The remaining elements should be defined to be inside the board (meaning min: 0x0; max: BoardSize)
    /// The input will follow the same pattern as the other elements: "XxY"
    /// </summary>
    public string boardSize { get; set; }

    /// <summary>
    /// Turtle element defined for the board.
    /// The input will follow the same pattern as the other elements: "XxY"
    /// </summary>
    public string turtle { get; set; }

    /// <summary>
    /// Exit element defined for the board.
    /// The input will follow the same pattern as the other elements: "XxY"
    /// </summary>
    public string exit { get; set; }

    /// <summary>
    /// Set of Mines defined for the board.
    /// The input will follow the same pattern as the other elements: "XxY"
    /// </summary>
    public string[] mines { get; set; }

    /// <summary>
    /// Turtle direction. It can be NORTH, SOUTH, EAST, WEST (or N,S,E,W)
    /// </summary>
    public string turtleDirection { get; set; }

    /// <summary>
    /// Board Maximum Size of X or Y
    /// </summary>
    public int? maxSize { get; set; }
}

/// <summary>
/// Settings definition to define the TurtleChallenge Sequence of Moves to be executed
/// defined in a '.json' file with the specifications defined below
/// Default: settings/turtleChallenge/moves.json
/// </summary>
public class MovesSettings
{
    /// <summary>
    /// Sequence of moves defined to be played
    /// Each Move is defined with a string separated by a whitespace (e.g. MOVE ROTATE ...)
    /// The Move can be the following: MOVE (or M); ROTATE (or R)
    /// </summary>
    public IEnumerable<string> moves { get; set; }
}
```

## Execute Instructions
As required, no binary folders were provided (neither any .exe) so the solution is delivered completely clean. To execute, it will be necessary to Build the solution and then execute it (through Visual Studio or by executing the .exe generated in the binary folder).
