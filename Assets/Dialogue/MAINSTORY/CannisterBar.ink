INCLUDE globalsmainstory.INK

{ knowledge ? foundCoolantBartender:
    I don't need that.
- else:
An empty cannister of coolant. #speaker:
This smells... familiar.
Wait... of course, I'm so stupid. The snus smells <b>exactly</b> like coolant! It could have been used to <i>poison the victim.</i>
~ getclue(kylarVätska)
~ gainknowledge(foundCoolantBartender)
~ gainknowledge(victimPoisoned)
}
-> END
