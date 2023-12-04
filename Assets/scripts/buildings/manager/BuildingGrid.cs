using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid
{
    bool[][] spaceTaken;

    public BuildingGrid(int width, int height)
    {
        spaceTaken = new bool[width][];
        for (int i = 0; i < width; i++)
        {
            spaceTaken[i] = new bool[height];
        }
    }

    public bool CanBuild(List<Vector2Int> positions)
    {
        bool canBuild = true;
        for (int i = 0; i < positions.Count; ++i)
        {
            if (spaceTaken[positions[i].x][positions[i].y])
            {
                canBuild = false;
                break;
            }
        }
        return canBuild;
    } 

    public void Build(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; ++i)
        {
            spaceTaken[positions[i].x][positions[i].y] = true;
        }
    }

    public void Destroy(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; ++i)
        {
            spaceTaken[positions[i].x][positions[i].y] = false;
        }
    }
}
