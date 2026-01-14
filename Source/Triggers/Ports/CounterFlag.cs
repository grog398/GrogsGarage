using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.GrogsGarage.Triggers.Ports;

[CustomEntity("GrogsGarage/Ports/CounterFlagTrigger")]
public class CounterFlagTrigger(EntityData data, Vector2 offset) : Trigger(data, offset)
{

    private readonly string _prefix = data.Attr("prefix", "flag_");
    private readonly string _counter = data.Attr("counter", "counter");

    private int Counter => SceneAs<Level>().Session.GetCounter(_counter);

    public override void OnEnter(Player player)
    {
        base.OnEnter(player);

        SceneAs<Level>().Session.SetFlag(_prefix + Counter);
    }
}