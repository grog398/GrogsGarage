using System;
using System.Collections.Generic;
using System.Linq;

using Celeste.Mod.Entities;
using Celeste.Mod.Registry;
using Microsoft.Xna.Framework;
using Monocle;


namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[CustomEntity("GrogsGarage/Ports/EntityCullController")]
public class EntityCullController : Entity
{
    private readonly List<Type> _types;

    private List<Entity> _toCull;
    
    public EntityCullController(EntityData data, Vector2 offset) : base(data.Position + offset)
    {
        // ReSharper disable once InconsistentNaming
        string[] SIDs = data.Attr("types", "*").Split(',');

        _types = new List<Type>();
        _toCull = new List<Entity>();

        if (!SIDs.Contains("*"))
            foreach (String s in SIDs)
            {
                if (EntityRegistry.GetKnownTypesFromSid(s) is { } t && t.Count != 0)
                    _types.Add(t.First());
            }
        else
            _types.Add(typeof(Trigger));

        Collider = new Circle(data.Float("cullRadius", 400));
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);

        foreach (Type type in _types)
        {
            _toCull = _toCull.Concat(scene.Entities.Where(e => e.GetType() == type)).ToList();
        }
    }

    public override void Update()
    {
        base.Update();
        Position = SceneAs<Level>().Camera.Position + new Vector2(160, 92);
        foreach (Entity entity in _toCull)
        {
            entity.Active = entity.Collidable = entity.Visible = CollidePoint(entity.Center);
        }
    }
}