

using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Mod.Entities;
using Celeste.Mod.Registry;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[CustomEntity("GrogsGarage/Ports/FlagWhenPlayerCollideController")]
public class FlagWhenPlayerCollide : Entity
{

    private Player _player;
    private readonly string _flag;
    private readonly bool _invert;

    private readonly List<Type> _types;
    private List<Entity> _entities;

    public FlagWhenPlayerCollide(EntityData data, Vector2 offset) : base(data.Position + offset)
    {
        _flag = data.Attr("flag");
        _invert = data.Bool("invertFlag");
        _types = new List<Type>();
        _entities = new List<Entity>();




        string typelist = data.Attr("types");

        if (typelist == "") return;

        string[] types = typelist.Split(",");

        

        foreach (string type in types)
        {
            Type t = EntityRegistry.GetKnownTypesFromSid(type)?.First();

            if (t != null)
            {
                _types.Add(t);
            }
        }
    }


    public override void Awake(Scene scene)
    {
        base.Awake(scene);
        _player = Scene.Tracker.GetEntity<Player>();

        foreach (Type t in _types)
        {
            if (Scene.Tracker.Entities.TryGetValue(t, out var entity))
                _entities = _entities.Concat(entity).ToList();
        }
    }

    public override void Update()
    {
        base.Update();

        _entities = new List<Entity>();

        foreach (Type t in _types)
        {
            if (Scene.Tracker.Entities.TryGetValue(t, out var entity))
                _entities = _entities.Concat(entity).ToList();
        }

        foreach (Entity e in _entities)
        {
            bool result = _player.CollideCheck(e);
            if (result)
            {
                SceneAs<Level>().Session.SetFlag(_flag, !_invert);
                return;
            }
        }

        SceneAs<Level>().Session.SetFlag(_flag, _invert);
    }
}