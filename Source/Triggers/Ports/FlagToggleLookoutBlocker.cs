using Microsoft.Xna.Framework;
using Celeste.Mod.Entities;
using Monocle;


namespace Celeste.Mod.GrogsGarage.Triggers.Ports;

[TrackedAs(typeof(LookoutBlocker))]
[CustomEntity("GrogsGarage/Ports/FlagToggleLookoutBlocker")]
public class FlagToggleLookoutBlocker : LookoutBlocker
{
    private readonly string _flag;
    private readonly bool _invert;
    private readonly Collider _temp;
    public FlagToggleLookoutBlocker(EntityData data, Vector2 offset) : base(data, offset)
    {
        _flag = data.Attr("flag");
        _invert = data.Bool("invert");
        _temp = Collider;
    }

    public override void Update()
    {
        base.Update();
        if (SceneAs<Level>().Session.GetFlag(_flag) ^ _invert)
            Collider = _temp;
        else
            Collider = null;
    }
}