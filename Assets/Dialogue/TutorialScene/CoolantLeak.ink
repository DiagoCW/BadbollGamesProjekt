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
    -> END
    }
