INCLUDE globalsmainstory.INK

{ knowledge ? foundCoolantBartender:
    I don't need that.
- else:
An empty cannister of coolant. #speaker:
This smells... familiar.
Wait... of course, I'm so stupid. The victim's snus smells <b>exactly</b> like this coolant! It could have been used to <i>poison the victim.</i>
~ getclue(kylarVätska)
~ gainknowledge(foundCoolantBartender)
~ gainknowledge(victimPoisoned)
}
-> END

An empty can of rat poison. Feels like a health hazard to keep it in the bathroom... #speaker: Player

