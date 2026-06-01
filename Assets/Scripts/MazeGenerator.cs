using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data structure representing a single logical grid square
public class MazeCell
{
    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall;

    public Vector2Int Position => new Vector2Int(x, y);

    public MazeCell(int x, int y)
    {
        this.x = x;
        this.y = y;
        visited = false;
        topWall = true;
        leftWall = true;
    }
}

public class MazeGenerator : MonoBehaviour
{
    [Range(5, 500)]
    public int mazeWidth = 5;
    [Range(5, 500)]
    public int mazeHeight = 5;

    public int startX = 0;
    public int startY = 0;

    private MazeCell[,] maze;
    private Vector2Int currentCell;

    public enum Direction { Up, Down, Left, Right }

    // Standard list used to fetch random order configurations
    private List<Direction> directions = new List<Direction>()
    {
        Direction.Up, Direction.Down, Direction.Left, Direction.Right
    };

    public MazeCell[,] GetMaze()
    {
        maze = new MazeCell[mazeWidth, mazeHeight];

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }
        }

        CarvePath(startX, startY);

        return maze;
    }

    private List<Direction> GetRandomDirections()
    {
        List<Direction> dirCopy = new List<Direction>(directions);
        List<Direction> randDir = new List<Direction>();

        while (dirCopy.Count > 0)
        {
            int rnd = Random.Range(0, dirCopy.Count);
            randDir.Add(dirCopy[rnd]);
            dirCopy.RemoveAt(rnd);
        }

        return randDir;
    }

    private bool IsCellValid(int x, int y)
    {
        if (x < 0 || y < 0 || x >= mazeWidth || y >= mazeHeight || maze[x, y].visited)
        {
            return false;
        }
        return true;
    }

    private Vector2Int CheckNeighbors()
    {
        List<Direction> randDirs = GetRandomDirections();

        for (int i = 0; i < randDirs.Count; i++)
        {
            Vector2Int neighbor = currentCell;

            switch (randDirs[i])
            {
                case Direction.Up:    neighbor.y++; break;
                case Direction.Down:  neighbor.y--; break;
                case Direction.Right: neighbor.x++; break;
                case Direction.Left:  neighbor.x--; break;
            }

            if (IsCellValid(neighbor.x, neighbor.y))
            {
                return neighbor;
            }
        }

        return currentCell; // Returns itself if no valid neighbors exist (Dead End)
    }

    private void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
            maze[primaryCell.x, primaryCell.y].leftWall = false;
        }
        else if (primaryCell.x < secondaryCell.x)
        {
            maze[secondaryCell.x, secondaryCell.y].leftWall = false;
        }
        else if (primaryCell.y < secondaryCell.y)
        {
            maze[primaryCell.x, primaryCell.y].topWall = false;
        }
        else if (primaryCell.y > secondaryCell.y)
        {
            maze[secondaryCell.x, secondaryCell.y].topWall = false;
        }
    }

    private void CarvePath(int x, int y)
    {
        if (x < 0 || y < 0 || x >= mazeWidth || y >= mazeHeight)
        {
            Debug.LogWarning("Starting coordinates are out of bounds. Defaulting to (0,0).");
            x = 0; y = 0;
        }

        currentCell = new Vector2Int(x, y);
        List<Vector2Int> path = new List<Vector2Int>();

        bool deadEnd = false;

        while (!deadEnd)
        {
            Vector2Int nextCell = CheckNeighbors();

            if (nextCell == currentCell)
            {
                // Backtracking logic
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbors();

                    if (nextCell != currentCell)
                    {
                        break;
                    }
                }

                if (nextCell == currentCell)
                {
                    deadEnd = true;
                }
            }
            else
            {
                BreakWalls(currentCell, nextCell);
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell;
                path.Add(currentCell);
            }
        }
    }
}