using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.GrogsGarage.Entities;

[CustomEntity("GrogsGarage/ForceLoadSpinners")]
[Tracked]
public class ForceLoadSpinners(EntityData data, Vector2 offset) : Entity(data.Position + offset)
{
    public static void Load()
    {
        On.Celeste.CrystalStaticSpinner.InView += ModInView;
    }
    public static void Unload()
    {
        On.Celeste.CrystalStaticSpinner.InView -= ModInView;
    }
    
    private static bool ModInView(On.Celeste.CrystalStaticSpinner.orig_InView orig, CrystalStaticSpinner self)
    {
        if (self.Scene.Tracker.GetEntity<ForceLoadSpinners>() != null)
        {
            return true;
        }
        return orig(self);
    }
}