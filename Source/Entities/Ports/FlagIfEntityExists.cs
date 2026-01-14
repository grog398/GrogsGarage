using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[CustomEntity("GrogsGarage/Ports/FlagIfEntityExists")]
public class FlagIfEntityExists(EntityData data, Vector2 offset) : Entity(data.Position + offset)
{
    private readonly string _entityName = data.Attr("entityName");
    private readonly string _flagName = data.Attr("flagName");

    public override void Update()
    {
        base.Update();
        SceneAs<Level>().Session.SetFlag(_flagName, false);
        foreach (Entity e in Scene.Entities)
        {
            if (e.GetType().Name == _entityName) 
                
                SceneAs<Level>().Session.SetFlag(_flagName);
        }
    }
}