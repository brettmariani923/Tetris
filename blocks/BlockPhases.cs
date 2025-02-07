using System.Collections.Specialized;

namespace Tetris;

public abstract class BlockPhases
{
    public static Position Start { get; set; }

    public Position CurrentSpace { get; set; } 
  
    public int WhichBlock { get; set; }
    
}