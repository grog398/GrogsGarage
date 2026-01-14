using System;
using System.Collections.Generic;
using System.Linq;

using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;


namespace Celeste.Mod.GrogsGarage.Entities;

[CustomEntity("GrogsGarage/DecalCullController")]
public class DecalCullController : Entity
{
    private readonly List<Decal> _toCull;
    private readonly int _minDepth, _maxDepth;
    private readonly string[] _textures;
    
    public DecalCullController(EntityData data, Vector2 offset) : base(data.Position + offset)
    {
        
        _toCull = new List<Decal>();
        
        _minDepth = data.Int("minDepth", int.MinValue);
        _maxDepth = data.Int("maxDepth", int.MaxValue);
        _textures = data.Attr("textures").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        Collider = new Circle(data.Float("cullRadius", 400));
    }

    public override void Awake(Scene scene)
    {
        base.Awake(scene);

        foreach (Decal decal in scene.Entities.OfType<Decal>())
        {
            if ((_textures.Length == 0 || !decal.textures.Any() || _textures.Contains(decal.textures[0].AtlasPath)) && (decal.Depth >=  _minDepth && decal.Depth <= _maxDepth))
                _toCull.Add(decal);
        }
    }

    public override void Update()
    {
        base.Update();
        Position = SceneAs<Level>().Camera.Position + new Vector2(160, 92);
        foreach (Decal decal in _toCull)
        {
            decal.Active = decal.Collidable = decal.Visible = CollidePoint(decal.Center);
        }
    }
}