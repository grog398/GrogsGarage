using System.Linq;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[Tracked]
[CustomEntity("GrogsGarage/Ports/DeathRedirectController")]
public class DeathRedirectController(EntityData data, Vector2 offset) : Entity(data.Position + offset)
{

    private readonly string _flag = data.Attr("flag");
    private readonly string _room = data.Attr("room");

    private readonly int _priority = data.Int("priority");
    private bool Flag => SceneAs<Level>().Session.GetFlag(_flag);


    public static void Load()
    {
        On.Celeste.Player.Die += hook_Die;
    }

    public static void Unload()
    {
        On.Celeste.Player.Die -= hook_Die;
    }

    private static PlayerDeadBody hook_Die(On.Celeste.Player.orig_Die orig, Player player, Vector2 dir, bool evenIfInvincible, bool registerDeathInStats)
    {

        Level level = player.SceneAs<Level>();

        if (level.Tracker.GetEntity<DeathRedirectController>() != null)
        {
            try
            {
                DeathRedirectController controller = level.Tracker
                    .GetEntities<DeathRedirectController>()?
                    .Cast<DeathRedirectController>()
                    .Where(d => d.Flag)
                    .OrderBy(d => d._priority)
                    .First();
                if (controller is { Flag: true })
                {
                    level.OnEndOfFrame += () =>
                    {
                        level.TeleportTo(player, controller._room, Player.IntroTypes.Transition, Vector2.Zero);
                    };
                    return null;
                }
            } catch
            {
                return orig(player, dir, evenIfInvincible, registerDeathInStats);
            }            
        }

        return orig(player, dir, evenIfInvincible, registerDeathInStats);
    }
}