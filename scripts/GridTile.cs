using UnityEngine;
using static GridManager;

public class GridTile : MonoBehaviour
{
    public int x;
    public int z;

    public TileState state = TileState.Unlocked;

    public GameObject lockVisual;
    public GameObject unlockVisual;
   public void SetState(TileState newState)
    {
        state = newState;
        lockVisual.SetActive(state == TileState.Locked);
        unlockVisual.SetActive(state == TileState.Unlocked);
    }
}
