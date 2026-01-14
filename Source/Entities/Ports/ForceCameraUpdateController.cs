using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[CustomEntity("GrogsGarage/Ports/ForceCameraUpdateController")]

#pragma warning disable CS9113
public class ForceCameraUpdateController(EntityData data, Vector2 _) : Entity(Vector2.Zero)
{
    private Player _player;
    private readonly string _flag = data.Attr("flag");
    private bool Flag => SceneAs<Level>().Session.GetFlag(_flag);

    public override void Awake(Scene scene)
    {
        base.Awake(scene);

        _player = scene.Tracker.GetEntity<Player>();
    }

    public override void Update()
    {
        base.Update();

        if (Flag || _flag == "")
        {
            _player.ForceCameraUpdate = true;
        }
    }
}