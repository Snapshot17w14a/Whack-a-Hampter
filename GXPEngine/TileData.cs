using GXPEngine;

struct TileData
{
    public Vec2 Position;
    public string CollisionType;

    public TileData(Vec2 position, string collisionType)
    {
        Position = position;
        CollisionType = collisionType;
    }
}
