INCLUDE globalsmain2.INK

<i>A small blue streak of liquid has run down from under your car.</i>
{ foundCoolantLeak: I know it wasn't real piss, I was just joking... -> END}
Looks like someone took a piss here, right underneath my car. For shame.
{ not carEngine: 
    -> END
- else:
    ~ foundCoolantLeak = true
    <i>Wait,</i> this isn't just any piss... <color=\#FFA500>It's my car's piss!</color> The coolant must have leaked.
    Fixing this is no big deal. I'll just head back to the hood and fill 'er up.
    -> tutorial
}
    - (tutorial)
    <i><color=\#FFA500>If you return back to the hood of your car, you will see that a new option has become available. This means that some items that you find through Detective Vision will allow you to progress through the story by unlocking new dialogue with characters or other objects.</color></i>
    <i><color=\#FFA500>More often than not, you must meet a certain condition for the item to be highlighted through your Detective Vision.</color></i>
    <i><color=\#FFA500>If something catches your eye but it isn't highlighted while using your Detective Vision, try coming back to it later! It might just be because you're missing a piece of the puzzle.</color></i>
    <b>READ TUTORIAL AGAIN?</b>
        + [Ja tack!]
            -> tutorial
        * [Nej tack!]
    <i><color=\#FFA500>See you, space detective...</color></i>
    -> END
