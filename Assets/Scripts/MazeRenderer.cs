using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private GameObject mazeCellPrefab;
    
    public float cellSize = 1f; // Must match prefab dimensions to avoid spacing gaps

    private void Start()
    {
        MazeCell[,] maze = mazeGenerator.GetMaze();

        for (int x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for (int y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                // Instantiate cell along the XZ global plane using data mapping coordinates
                Vector3 spawnPos = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject newCell = Instantiate(mazeCellPrefab, spawnPos, Quaternion.identity, transform);
                
                MazeCellObject cellObject = newCell.GetComponent<MazeCellObject>();

                bool top = maze[x, y].topWall;
                bool left = maze[x, y].leftWall;

                // Handle bottom/right border caps since cells only map top/left logic data
                bool right = false;
                bool bottom = false;

                if (x == mazeGenerator.mazeWidth - 1)
                {
                    right = true;
                }
                if (y == 0)
                {
                    bottom = true;
                }

                cellObject.Init(top, bottom, right, left);
            }
        }
    }
}