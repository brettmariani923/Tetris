### Tetris Console Game

This is a Tetris game I made with the intention of seeing what the console was capable of. The game features relaxing music, unicode character rewards, eye catching ascii art of big fat cats, and much more! The game starts off slow and relaxing, but as you clear lines and the speed increases it becomes quite challenging. If you'd like even more of a challenge, feel free to turn up the difficulty for yourself in the program file! Thanks for looking, and have fun

-Press the space bar to pause the screen and see the controls-

---

### Explaining the core logic of the game:

---

### Explaining the Tetris Piece Movement Logic:

```
x→    0 1 2 3 4 5 6 7 8 9 10
y↓
 0    . . . . X P X . . . .
 1    . . . . . X . . . . .
 2    . . . . . . . . . . .
 3    . . . . . . . . . . .
 4    . . . . . . . . . . .
 5    . . . . . . . . . . .
 6    . . . . . . . . . . .
 7    . . . . . . . . . . .
 8    . . . . . . . . . . .
 9    . . . . . . . . . . .
10    . . . . . . . . . . .
```

In the above example, lets assume we are using the t. shaped piece  
`(0,0), (-1,0), (1,0), (0,1)`

- `P = static (int x, int y) positionOfPiece;`  
  The P value represents the point expressed in the tuple `(5,0)`. This is the starting point that will be the basis from which we define the center of our tetromino going forward, this anchor point is also the value that the offset blocks use to generate around.

- `X = static (int x, int y)[] currentPiece;`  
  The variable that holds the piece we are using from the pieces pool. It provides the values that make up our block `[(0,0), (-1,0), (1,0), (0,1)]`, and will help determine the occupied positions of offset blocks that build around the anchor point.

- `.` = empty position

---

So the starting anchor point we want to use for `P` or the `positionOfPiece` is `(width/2, 0)` or in other words, `(5,0)`.  
The starts the piece at the top center of the grid to begin with, which is where we want our pieces to spawn.

From there, the formula used to determine how and where the piece appears is based off the anchor point:  
`positionOfPiece = (5,0) + the values of the blocks that make up the piece: [(0,0), (-1,0), (1,0), (0,1)]`

```
[x, y],       // center (anchor point)
[x - 1, y],   // left
[x + 1, y],   // right
[x, y + 1]    // bottom
```

So for our T block, the relative positions would be:
```
[5, 0],       // center
[4, 0],       // left
[6, 0],       // right
[5, 1]        // bottom
```

If you want to `MovePiece(0,1)`, or in other words move the piece down one space from the starting position, we would need to update the anchor point of the piece to be `(5,1)` + `[(0,0), (-1,0), (1,0), (0,1)]`, which results in:
```
[5, 1],       // center
[4, 1],       // left
[6, 1],       // right
[5, 2]        // bottom
```

Likewise, if you want to `MovePiece(1,0)`, or in other words move the piece to the right one space from the starting position, we would update the anchor point to be `(6,0)` + `[(0,0), (-1,0), (1,0), (0,1)]`, resulting in:
```
[6, 0],       // center
[5, 0],       // left
[7, 0],       // right
[6, 1]        // bottom
```

Each of these sets of numbers represents a position on the game grid. This is essentially the core logic of the entire game. It is just updating the values of these different arrays to determine where the piece is, and then printing the values to the grid.

---

To make sure we can accomplish this and have it work correctly with the game logic, where pieces can't go through other pieces or outside the game grid, we need to check if the piece placement is valid.

We can do this using this method:

```csharp
public static bool CanMove(int dx, int dy)  
{                                                       
    foreach (var (px, py) in currentPiece)    
    {                                                 
        int x = positionOfPiece.x + px + dx;
        int y = positionOfPiece.y + py + dy;
        if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.newGrid[y, x] != 0))
            return false;
    }
    return true;
}
```

The `CanMove` method checks if the piece is allowed to move to a new position.  
It does this by adding the value of user inputs `(dx, dy)` to the anchor point (center) of the current piece's position `(positionOfPiece)`, as well as foreach of the values of the offset blocks `(px, py)`.

If the resulting position is outside the grid or collides with an already occupied cell (`Grid.newGrid[y, x] != 0`), then the piece isn't allowed to move.  
If all blocks can move without issue, then the piece is allowed to move.

---

So to put the method in more straightforward terms:
```
The piece moves by receiving (left and right inputs, and up and down inputs)

{
    and by using these input values, as well as the values foreach (of the offset blocks on the x axis and y axis that make up the tetromino shape)  
    {
        ---
        we can sum the x value of the anchor point + x axis values for the offset blocks that make up the tetromino + x axis value directional inputs to get the value for int x;
        ---
        and we can sum the y value of the anchor point + y axis values for the offset blocks that make up the tetromino + y axis value directional inputs to get the value for int y;
        ---
        then we can use the values of int x and int y to determine if (the values of the x axis of the tetris piece are outside the grid on the left on the right,
        or if the sum of the values of y are outside the top of the grid or the bottom of it, or if the piece is wthin the grid but the cell is already occupied (!= 0, 1 means occupied)
        ---
        If any of these scenarios are the case, the piece isn't allowed to move;
    }
    ```
    but if these scenarios are not the case, the piece is allowed to move;
}
```

This method essentially expresses the core logic the entire game is based off of. Many of the other methods that make up the game incorporate or are built around this logic.

---

For example, our `NewPiece` method uses this logic to determine if the new piece can be placed at the starting position. If it can't, the game is over.

```csharp
public static void NewPiece()                                                                         
{
    currentPieceIndex = random.Next(piecesPool.Length);
    currentPiece = piecesPool[currentPieceIndex];
    positionOfPiece = ((width / 2), 0);
    if (!CanMove(0, 0)) gameOver = true;
}
```

Our `CanRotate` method uses this logic to determine if the piece can be rotated without colliding with other pieces or going out of bounds.

```csharp
public static bool CanRotate((int x, int y)[] rotatedPiece)
{
    foreach (var (px, py) in rotatedPiece)
    {
        int x = positionOfPiece.x + px;
        int y = positionOfPiece.y + py;
        if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.newGrid[y, x] != 0))
            return false;
    }
    return true;
}
```
