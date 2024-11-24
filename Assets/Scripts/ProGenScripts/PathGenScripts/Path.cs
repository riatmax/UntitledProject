using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path 
{
    private List<GameObject> path = new List<GameObject>();
    private List<GameObject> topTiles = new List<GameObject>();
    private List<GameObject> bottomTiles = new List<GameObject>();

    private int radius;
    private int currentTileIndex;

    private bool hasReachedX;
    private bool hasReachedZ;

    private GameObject startingTile;
    private GameObject endingTile;
    public Path(int radius)
    {
        this.radius = radius;
    }

    public List<GameObject> GetPath()
    {
        return path;
    }
    public void AssignTopAndBottomTiles(int z, GameObject tile)
    {
        if (z == 0)
        {
            topTiles.Add(tile);
        }
        if (z == radius - 1)
        {
            bottomTiles.Add(tile);
        }
    }

    private bool AssignAndCheckStartingAndEndingTile()
    {
        int xIndex = Random.Range(0, topTiles.Count - 1);
        int zIndex = Random.Range(0, bottomTiles.Count - 1);

        startingTile = topTiles[xIndex];
        endingTile = bottomTiles[zIndex];

        return startingTile != null && endingTile != null;
    }

    public void GeneratePath()
    {
        if(AssignAndCheckStartingAndEndingTile())
        {
            GameObject currentTile = startingTile;

            for(int i = 0; i < 3; i++)
            {
                MoveLeft(ref currentTile);
            }

            var safetyBreakx = 0;
            while (!hasReachedX)
            {
                safetyBreakx++;
                if (safetyBreakx > 100)
                {
                    break;
                }

                if (currentTile.transform.position.x > endingTile.transform.position.x)
                {
                    MoveDown(ref currentTile);
                }
                else if(currentTile.transform.position.x < endingTile.transform.position.x)
                {
                    MoveUp(ref currentTile);
                }
                else
                {
                    hasReachedX = true;
                }
            }

            var safetyBreakz = 0;
            while (!hasReachedZ)
            {
                safetyBreakz++;
                if (safetyBreakz > 100)
                {
                    break;
                }

                if (currentTile.transform.position.z > endingTile.transform.position.z)
                {
                    MoveRight(ref currentTile);
                }
                else if (currentTile.transform.position.z < endingTile.transform.position.z)
                {
                    MoveLeft(ref currentTile);
                }
                else
                {
                    hasReachedZ = true;
                }
            }

            path.Add(endingTile);
        }
    }

    private void MoveDown(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGen.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex - radius;
        currentTile = WorldGen.GeneratedTiles[n];
    }
    private void MoveUp(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGen.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex + radius;
        currentTile = WorldGen.GeneratedTiles[n];
    }

    private void MoveLeft(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGen.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex++;
        currentTile = WorldGen.GeneratedTiles[currentTileIndex];
    }
    private void MoveRight(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGen.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex--;
        currentTile = WorldGen.GeneratedTiles[currentTileIndex];
    }
}
