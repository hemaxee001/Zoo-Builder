using System.Drawing;
using UnityEngine;
using static GridManager;

public class GridManager : MonoBehaviour
{
    public enum TileState
    { 
        Locked,
        Unlocked
    }

    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float tileSize = 1f;

    [Header("References")]
    public GameObject tilePrefab;
    public WallManager wallManager;

    [Header("Initial Unlock Area")]
    //public Point[] unlockAreaSize;
    public Vector2Int[] unlockAreaSize;
    public int unlockStartX = 3;
    public int unlockEndX = 6;
    public int unlockStartZ = 3;
    public int unlockEndZ = 6;

    public GridTile[,] grid;

    void Start()
    {
        GenerateGrid();
        UnlockInitialArea();
        BuildWalls();
    }

    // -------------------------
    // GRID CREATION
    // -------------------------
    void GenerateGrid()
    {
        grid = new GridTile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);
                GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                tileObj.name = $"Tile_{x}_{z}";

                GridTile tile = tileObj.GetComponent<GridTile>();
                tile.x = x;
                tile.z = z;

                tile.SetState(TileState.Locked);

                grid[x, z] = tile;
            }
        }
    }

    // INITIAL UNLOCK (START AREA)
    void UnlockInitialArea()
    {
        for (int i = 0; i < unlockAreaSize.Length; i++)
        {
            Vector2Int vector2Int = unlockAreaSize[i];
            if (IsValidGridPos(vector2Int.x, vector2Int.y))
                {
                    grid[vector2Int.x, vector2Int.y].SetState(TileState.Unlocked);
                }
        }
        //for (int x = unlockStartX; x <= unlockEndX; x++)
        //{
        //    for (int z = unlockStartZ; z <= unlockEndZ; z++)
        //    {
        //        if (IsValidGridPos(x, z))
        //        {
        //            grid[x, z].SetState(TileState.Unlocked);
        //        }
        //    }
        //}
    }

    // UNLOCK SINGLE TILE (BUY LAND)
    public void UnlockTile(int x, int z)
    {
        if (!IsValidGridPos(x, z))
            return;

        GridTile tile = grid[x, z];

        if (tile.state == TileState.Unlocked)
            return;

        tile.SetState(TileState.Unlocked);
        BuildWalls();
    }

    // CHECK TILE STATE
    public bool IsTileUnlocked(int x, int z)
    {
        if (!IsValidGridPos(x, z))
            return false;

        return grid[x, z].state == TileState.Unlocked;
    }

    // GRID BOUNDS CHECK
    public bool IsValidGridPos(int x, int z)
    {
        return x >= 0 && z >= 0 && x < width && z < height;
    }

    // WORLD → GRID POSITION
    public bool WorldToGrid(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.RoundToInt(worldPos.x / tileSize);
        z = Mathf.RoundToInt(worldPos.z / tileSize);

        return IsValidGridPos(x, z);
    }

    // GRID → WORLD POSITION
    public Vector3 GridToWorld(int x, int z)
    {
        return new Vector3(x * tileSize, 0, z * tileSize);
    }

    // WALL SUPPORT
    void BuildWalls()
    {
        if (wallManager != null)
        {
            wallManager.BuildWalls();
        }
    }
    public Vector3 GetCenterWorldPosition()
    {
        float centerX = (width - 1) * tileSize * 0.5f;
        float centerZ = (height - 1) * tileSize * 0.5f;
        return new Vector3(centerX, 0f, centerZ);
    }
}

