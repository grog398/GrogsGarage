using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.GrogsGarage.Triggers.Ports;

[CustomEntity("GrogsGarage/Ports/BoostTrigger")]
public class BoostTrigger(EntityData data, Vector2 offset) : Trigger(data, offset)
{

    private readonly float _boost = data.Float("boostAmount");
    private readonly bool _mult = data.Bool("multiplicative");

    private readonly bool _horizontal = data.Bool("horizontal");

    public override void OnEnter(Player player)
    {
        base.OnEnter(player);

        if (_mult)
        {
            if (_horizontal)
                player.Speed.X *= _boost;
            else
                player.Speed.Y *= _boost;
        }
        else
        {
            if (_horizontal)
                player.Speed.X += _boost;
            else
                player.Speed.Y += _boost;
        }
    }
}