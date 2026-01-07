using UnityEngine;
using static GridManager;

public class WallManager : MonoBehaviour
{
    public GridManager gridManager;
    public GameObject wallPrefab;

    float halfTile;

    void Awake()
    {
        halfTile = gridManager.tileSize * 0.5f;
    }

    public void BuildWalls()
    {
        // Clear old walls
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        for (int x = 0; x < gridManager.width; x++)
        {
            for (int z = 0; z < gridManager.height; z++)
            {
                GridTile tile = gridManager.grid[x, z];

                if (tile.state != TileState.Unlocked)
                    continue;

                Vector3 tilePos = tile.transform.position;

                // Right
                TryPlaceWall(x + 1, z, tilePos, Vector3.right);
                // Left
                TryPlaceWall(x - 1, z, tilePos, Vector3.left);
                // Forward
                TryPlaceWall(x, z + 1, tilePos, Vector3.forward);
                // Back
                TryPlaceWall(x, z - 1, tilePos, Vector3.back);
            }
        }
    }

    void TryPlaceWall(int checkX, int checkZ, Vector3 tilePos, Vector3 dir)
    {
        bool needWall =
            !gridManager.IsValidGridPos(checkX, checkZ) ||
            gridManager.grid[checkX, checkZ].state == TileState.Locked;

        if (!needWall)
            return;

        Vector3 wallPos = tilePos + dir * halfTile;

        Quaternion wallRot = Quaternion.identity;
        if (dir == Vector3.left || dir == Vector3.right)
            wallRot = Quaternion.Euler(0, 90, 0);

        Instantiate(wallPrefab, wallPos, wallRot, transform);
    }
}
