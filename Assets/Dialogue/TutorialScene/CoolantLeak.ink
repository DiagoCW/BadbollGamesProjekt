INCLUDE globalsmain2.INK

A small blue streak of liquid has run down from under your car.
{ foundCoolantLeak: I know it wasn't real piss, I was just joking... -> END}
Looks like someone took a piss here, right underneath my car. For shame.
{ not carEngine: 
    -> END
- else:
    Wait, this isn't just any piss... It's my car's piss! The coolant must have leaked.
    Fixing this is no big deal. Go back and take another look under the hood again.
    ~ foundCoolantLeak = true
    <i>This is the power of Detective Vision...</i>
    <i>Something important to keep in mind is that not all items will be highlighted through your Detective Vision at all times. More often than not, you must unlock a certain requirement until it can become highlighted.</i>
    <i>This can be anything from having to gain knowledge about something through dialogue with characters, or even finding other clues.</i>
    <i>If something catches your eye but it isn't highlighted while using your Detective Vision, try coming back to it later! It might just be because you're missing a piece of the puzzle.</i>
    <i>See you, space detective...</i>
    -> END
    }
