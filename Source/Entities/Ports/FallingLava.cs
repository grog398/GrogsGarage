

using Microsoft.Xna.Framework;
using Monocle;
using Celeste.Mod.Entities;

namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[Tracked]
[CustomEntity("GrogsGarage/Ports/FallingLava")]
public class FallingLava : RisingLava
{

    // ReSharper disable once InconsistentNaming
    private new float Speed;

    private readonly float _rubberbandSpeed, _rubberbandAccel, _rubberbandDist, _baseSpeed;
    private readonly Color _surfaceColor, _edgeColor,  _centerColor;

    private readonly bool _rubberbanding;


    private readonly string _flag;
    private bool Flag => SceneAs<Level>().Session.GetFlag(_flag);

    public FallingLava(EntityData data, Vector2 offset) : base(data, offset)
    {
        Remove(Components.Get<LavaRect>());

        Add(bottomRect = new LavaRect(400f, 2000f, 4));
        bottomRect.Position = new Vector2(-40f, -2032f);
        bottomRect.OnlyMode = LavaRect.OnlyModes.OnlyBottom;
        bottomRect.SmallWaveAmplitude = 2f;

        Collider = new Hitbox(340f, 120f, 0, -152);

        Speed = _baseSpeed = data.Float("speed", 30);
        Depth = data.Int("depth", Depth);

        _rubberbandSpeed = data.Float("rubberbandSpeed", 500);
        _rubberbandAccel = data.Float("rubberbandAccel", 100);
        _rubberbandDist = data.Float("rubberbandDist", 92);
        _rubberbanding = data.Bool("enableRubberbanding", true);
        

        _flag = data.Attr("pauseFlag");

        bottomRect.BigWaveAmplitude = 0;
        bottomRect.CurveAmplitude = 0;

        _surfaceColor = data.HexColor("surface_color", Hot[0]);
        _edgeColor = data.HexColor("edge_color", Hot[1]);
        _centerColor = data.HexColor("center_color", Hot[2]);

        bottomRect.SurfaceColor = _surfaceColor;
        bottomRect.EdgeColor = _edgeColor;
        bottomRect.CenterColor = _centerColor;
    }

    public override void Added(Scene scene)
    {
        base.Added(scene);
        X = SceneAs<Level>().Bounds.Left - 10;
        Y = SceneAs<Level>().Bounds.Top + 16;
        iceMode = SceneAs<Level>().Session.CoreMode == Session.CoreModes.Cold;
        loopSfx.Play("event:/game/09_core/rising_threat", "room_state", iceMode ? 1 : 0);
        loopSfx.Position = new Vector2(Width / 2f, 0f);
    }

    public override void Update()
    {
        delay -= Engine.DeltaTime;
        X = SceneAs<Level>().Camera.X;
        Player entity = Scene.Tracker.GetEntity<Player>();
        if (entity == null) return;
        Visible = true;

        Speed = Calc.Approach(Speed, (entity.Y - Bottom > _rubberbandDist && _rubberbanding) ? _rubberbandSpeed : _baseSpeed, _rubberbandAccel);

        if (Flag)
        {
            bottomRect.Wave(1, 1);

            loopSfx.Param("rising", 0f);
        }
        else
        {
            float num2 = SceneAs<Level>().Camera.Bottom - 12f;
            if (Top > num2 + 96f)
            {
                Top = num2 + 96f;
            }
            var num = ((!(Top > num2)) ? Calc.ClampedMap(num2 - Top, 0f, 32f, 1f, 0.5f) : Calc.ClampedMap(Top - num2, 0f, 96f, 1f, 2f));
            if (delay <= 0f)
            {
                loopSfx.Param("rising", 1f);
                Y += Speed * num * Engine.DeltaTime;
            }
        }

        if (_surfaceColor == Hot[0] && _edgeColor == Hot[1] && _centerColor == Hot[2])
        {
            this.lerp = Calc.Approach(this.lerp, this.iceMode ? 1f : 0.0f, Engine.DeltaTime * 4f);
            this.bottomRect.SurfaceColor = Color.Lerp(RisingLava.Hot[0], RisingLava.Cold[0], this.lerp);
            this.bottomRect.EdgeColor = Color.Lerp(RisingLava.Hot[1], RisingLava.Cold[1], this.lerp);
            this.bottomRect.CenterColor = Color.Lerp(RisingLava.Hot[2], RisingLava.Cold[2], this.lerp);
            this.bottomRect.Spikey = this.lerp * 5f;
            this.bottomRect.UpdateMultiplier = (float)((1.0 - (double)this.lerp) * 2.0);
            this.bottomRect.Fade = this.iceMode ? 128f : 32f;
        }
        else
        {
            this.lerp = Calc.Approach(this.lerp, this.iceMode ? 1f : 0.0f, Engine.DeltaTime * 4f);
            this.bottomRect.Spikey = this.lerp * 5f;
            this.bottomRect.UpdateMultiplier = (float)((1.0 - (double)this.lerp) * 2.0);
            this.bottomRect.Fade = this.iceMode ? 128f : 32f;
        }
    }
}