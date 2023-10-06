using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject parentQuadrant;
    
    private int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };
    
    // Start is called before the first frame update
    void Start()
    {
        ClearManualLevel();
        GenerateQuadrant();
        MirrorLevel();
        CreateMiddleRow();
    }

    void ClearManualLevel()
    {
        Destroy(GameObject.Find("ManualLevel"));
    }

    void GenerateQuadrant()
    {
        var width = levelMap.GetLength(1);
        var height = levelMap.GetLength(0);

        for (var i = 0; i < height-1; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var tileType = levelMap[i, j];
                var tilePrefab = tilePrefabs[tileType];
                var position = new Vector2(-(width - j)+0.5f, (height - i)-0.5f);
                var setTile = Instantiate(tilePrefab, position, Quaternion.identity, parentQuadrant.transform);
                CheckTileRotation(i,j,setTile);
            }
        }
    }

    void MirrorLevel()
    {
        var topRight = Instantiate(parentQuadrant, transform);
        topRight.transform.Rotate(0,180,0);
        foreach (Transform child in topRight.transform)
        {
            if (child.CompareTag("PowerPellet"))
            {
                child.localScale = new Vector3(-1, 1, 1);
            }
        }
        var bottomRight = Instantiate(topRight, transform);
        bottomRight.transform.Rotate(180,0,0);
        bottomRight.transform.position = new Vector3(0, 1, 0);
        foreach (Transform child in bottomRight.transform)
        {
            if (child.CompareTag("PowerPellet"))
            {
                child.localScale = new Vector3(-1, -1, 1);
            }
        }
        var bottomLeft = Instantiate(parentQuadrant, transform);
        bottomLeft.transform.Rotate(180,0,0);
        bottomLeft.transform.position = new Vector3(0, 1, 0);
        foreach (Transform child in bottomLeft.transform)
        {
            if (child.CompareTag("PowerPellet"))
            {
                child.localScale = new Vector3(1, -1, 1);
            }
        }
    }

    void CreateMiddleRow()
    {
        var middleRow = new GameObject("MiddleRow");
        middleRow.transform.parent = transform;
        var width = levelMap.GetLength(1);
        var height = levelMap.GetLength(0);
        
        for (var j = 0; j < width; j++)
        {
            var tileType = levelMap[height-1, j];
            var tilePrefab = tilePrefabs[tileType];
            var position = new Vector2(-(width - j)+0.5f, (0.5f));
            var setTile = Instantiate(tilePrefab, position, Quaternion.identity, middleRow.transform);
            if (tileType == 2 || tileType == 4)
            {
                setTile.transform.Rotate(0,0,90);
            }
        }
        
        var middleRowMirror = Instantiate(middleRow, transform);
        middleRowMirror.transform.Rotate(0,180,0);
    }

    void CheckTileRotation(int i, int j, GameObject tile)
    {
        var tileType = levelMap[i, j];

        if (tileType == 1)
        {
            if (i == 0 && j == 0)
            {
                return;
            }

            if (j == 0 && i > 0)
            {
                var aboveNeighborTile = levelMap[j, i - 1];
                if (aboveNeighborTile == 0)
                {
                    tile.transform.Rotate(0,0,0);

                }
                tile.transform.Rotate(0,0,90);
            }

            if (j > 0)
            {
                var leftNeighborTile = levelMap[i, j - 1];
                if (i == 0)
                {
                    if (leftNeighborTile == 0 || leftNeighborTile == 1)
                    {
                        tile.transform.Rotate(0,0,0);
                        return;
                    }
                    tile.transform.Rotate(0,0,270);
                    return;
                }
                
                var aboveNeighborTile = levelMap[i - 1, j];
                
                if (leftNeighborTile == 2)
                {
                    if (aboveNeighborTile == 2)
                    {
                        tile.transform.Rotate(0,0,180);
                        return;
                    }

                    if (aboveNeighborTile == 1)
                    {
                        tile.transform.Rotate(0,0,180);
                        return;
                    }
                }

                if (leftNeighborTile == 0 || leftNeighborTile == 5 || leftNeighborTile == 6)
                {
                    if (aboveNeighborTile == 0)
                    {
                        tile.transform.Rotate(0,0,0);
                        return;
                    }
                    tile.transform.Rotate(0,0,90);
                    return;
                }

                if (leftNeighborTile == 1 && aboveNeighborTile == 2)
                {
                    tile.transform.Rotate(0,0,180);
                    return;
                }
                tile.transform.Rotate(0,0,270);
            }
        }

        if (tileType == 2)
        {
            if (j == 0)
            {
                var aboveNeighborTile = levelMap[i - 1, j];
                
                if (aboveNeighborTile == 0)
                {
                    tile.transform.Rotate(0,0,90);
                }

                if (aboveNeighborTile == 1 && i>1)
                {
                    tile.transform.Rotate(0,0,0);
                    return;
                }
                tile.transform.Rotate(0,0,90);
            }
            
            if (j > 0)
            {
                int leftNeighborType = levelMap[i, j - 1];
                
                if (leftNeighborType == 1 || leftNeighborType == 2)
                {
                    tile.transform.Rotate(0,0,0);
                }

                if (leftNeighborType == 0)
                {
                    tile.transform.Rotate(0,0,90);
                }

                if (i > 0)
                {
                    var aboveNeighbourType = levelMap[i - 1, j];
                    if (leftNeighborType == 2)
                    {
                        if (aboveNeighbourType == 7 || aboveNeighbourType == 1 || aboveNeighbourType == 2)
                        {
                            tile.transform.Rotate(0,0,90);
                        }
                    }
                    
                }
            }
        }

        if (tileType == 3)
        {
            var aboveNeighborTile = levelMap[i - 1, j];
            var leftNeighborTile = levelMap[i, j - 1];

            if (leftNeighborTile == 4)
            {
                if (aboveNeighborTile == 4)
                {
                    var belowNeighborTile = levelMap[i + 1, j];
                    if (belowNeighborTile == 4)
                    {
                        tile.transform.Rotate(0,0,270);
                        return;
                    }

                    if (belowNeighborTile == 3)
                    {
                        tile.transform.Rotate(0,0,90);
                        return;
                    }
                    tile.transform.Rotate(0,0,180);
                    return;
                }

                if (aboveNeighborTile == 3)
                {
                    var belowNeighborTile = levelMap[i + 1, j];
                    if (belowNeighborTile == 4)
                    {
                        tile.transform.Rotate(0,0,0);
                        return;
                    }
                    tile.transform.Rotate(0,0,180);
                }

                if (aboveNeighborTile == 0 || aboveNeighborTile == 5 || aboveNeighborTile == 6)
                {
                    tile.transform.Rotate(0,0,270);
                }
            }

            if (leftNeighborTile == 3)
            {
                if (aboveNeighborTile == 4)
                {
                    tile.transform.Rotate(0,0,180);
                }

                if (aboveNeighborTile == 3)
                {
                    tile.transform.Rotate(0,0,180);
                }
                
                if (aboveNeighborTile == 0 || aboveNeighborTile == 5 || aboveNeighborTile == 6)
                {
                    tile.transform.Rotate(0,0,270);
                }
            }

            if (leftNeighborTile == 0 || leftNeighborTile == 5 || leftNeighborTile == 6)
            {
                if (aboveNeighborTile == 0 || aboveNeighborTile == 5 || aboveNeighborTile == 6)
                {
                    tile.transform.Rotate(0,0,0);
                }

                if (aboveNeighborTile == 4 || aboveNeighborTile == 3)
                {
                    tile.transform.Rotate(0,0,90);
                }

                if (aboveNeighborTile == 7)
                {
                    tile.transform.Rotate(0,0,90);
                }
            }
        }

        if (tileType == 4)
        {
            var leftNeighborTile = levelMap[i, j-1];
            var aboveNeighborTile = levelMap[i-1, j];
            var belowNeighborTile = levelMap[i + 1, j];
            
            if (leftNeighborTile == 3 || leftNeighborTile == 4)
            {
                tile.transform.Rotate(0,0,0);
            }
            if (leftNeighborTile == 0 || leftNeighborTile == 5 || leftNeighborTile == 6)
            {
                tile.transform.Rotate(0,0,90);
            }
            if (leftNeighborTile == 4 && aboveNeighborTile == 4)
            {
                tile.transform.Rotate(0,0,90);
            }
            if (aboveNeighborTile == 3 && belowNeighborTile == 4 && leftNeighborTile == 4)
            {
                tile.transform.Rotate(0,0,90);
            }

            if (aboveNeighborTile == 4 && leftNeighborTile == 4 && belowNeighborTile != 4 && belowNeighborTile != 3)
            {
                tile.transform.Rotate(0,0,90);
            }
        }

        if (tileType == 7)
        {
            if (j == 0)
            {
                tile.transform.Rotate(0,0,90);
            }

            if (i == 0)
            {
                tile.transform.Rotate(0, 0, 0);
            }

            if (j > 0)
            {
                var leftNeighborTile = levelMap[i, j-1];
                if (i > 0)
                {
                    var aboveNeighborTile = levelMap[i - 1, j];
                    if (leftNeighborTile is 3 or 4)
                    {
                        tile.transform.Rotate(0,0,270);
                        return;
                    }
                    if (leftNeighborTile is 2 or 7)
                    {
                        tile.transform.Rotate(0,0,0);

                        if (leftNeighborTile == 7 || aboveNeighborTile == 1)
                        {
                            tile.transform.Rotate(0,0,90);
                        }
                    }
                }
            }
        }
    }
}