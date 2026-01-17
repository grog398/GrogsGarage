using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.GrogsGarage.Entities;

[CustomEntity("GrogsGarage/ForceDisableRefillFlash")]
public class ForceDisableRefillFlash(EntityData data, Vector2 offset) : Entity(data.Position + offset)
{
    public override void Update()
    {
        base.Update();
        Player.FlashHairColor = Player.UsedHairColor;
        Scene.OnEndOfFrame += () => {Player.FlashHairColor = Color.White;
        };
    }
}