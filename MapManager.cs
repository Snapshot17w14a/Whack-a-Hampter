using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledMapParser;

public class MapManager
{
    private Tile[,] tiles; // 2D array holding all tiles

    public MapManager(Tile[,] tiles)
    {
        this.tiles = tiles;
    }

    public Tile GetTileAt(int x, int y)
    {
        if (x >= 0 && x < tiles.GetLength(0) && y >= 0 && y < tiles.GetLength(1))
        {
            return tiles[x, y];
        }
        return null; // Return null if the coordinates are out of the map bounds
    }
}
