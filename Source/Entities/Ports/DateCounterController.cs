using System;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.GrogsGarage.Entities.Ports;

[CustomEntity("GrogsGarage/Ports/DateCounterController")]
public class DateCounter : Entity
{

    private readonly DateTime _targetDate;
    private readonly string _dayC, _hourC, _minuteC, _secondC;

    public DateCounter(EntityData data, Vector2 offset) : base(data.Position + offset)
    {

        DateTime currentDate = DateTime.Now;

        DateOnly date = new DateOnly(currentDate.Year, data.Int("month"), data.Int("day"));

        DateTime compare = new DateTime(date,TimeOnly.MinValue);

        if (compare.CompareTo(currentDate) < 0) date = date.AddYears(1);

        _targetDate = new DateTime(date, TimeOnly.MinValue);

        _dayC = data.Attr("dayCounter");
        _hourC = data.Attr("hourCounter");
        _minuteC = data.Attr("minuteCounter");
        _secondC = data.Attr("secondCounter");
    }

    public override void Update()
    {
        base.Update();

        TimeSpan timeLeft = _targetDate - DateTime.Now;

        Session session = SceneAs<Level>().Session;

        session.SetCounter(_dayC, timeLeft.Days);
        session.SetCounter(_hourC, timeLeft.Hours);
        session.SetCounter(_minuteC, timeLeft.Minutes);
        session.SetCounter(_secondC, timeLeft.Seconds);
    }
}