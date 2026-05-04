INCLUDE globalsmainstory.INK

// local variable to track if he is snoring
VAR isAsleep = true

// check if we met him before
{ talkedToArmchairGuy: -> Hub | -> Intro }

#speaker: Armchair Guy
=== Intro ===
The man is slumped deep in the armchair, snoring loudly. He hasn't even taken a single bite from his princesstårta before he fell asleep.
    * [Wake him up.]
        #speaker: Player
        Hey. Wake up.
        
        #speaker: Armchair Guy
        #anim: Shake
        #snore: stop
        Huh?! I'm awake! I wasn't sleeping, I was just... Okay, you got me I was sleeping.
        ~ talkedToArmchairGuy = true
        ~ isAsleep = false
        -> Hub
    * [Leave him be.]
        -> DONE

=== Hub ===
{ isAsleep: -> SleepingHub | -> AwakeHub }

=== SleepingHub ===
#speaker: Armchair Guy
<i>loud snoring</i>

    + [Wake him up.]
        #speaker: Player
        Hey!
        
        #speaker: Armchair Guy
        #anim: Shake
        #snore: stop
        Sorry.
        ~ isAsleep = false
        -> Hub
    + [Leave him be.]
        -> DONE


=== AwakeHub ===
#speaker: Armchair Guy
What do you want?

    * [Do you know Peter Grip?] -> KnowPeter
    * {KnowPeter} [I need that recording of Peter.] -> TheTape
    + [Did you see anything suspicious last night?] -> LastNight
    + [I'll leave you alone now.] -> AwakeExit

=== KnowPeter ===
#speaker: Player
Did you know a guy named Peter Grip?

#speaker: Armchair Guy
#anim: Idle
The karaoke guy? Yeah. Tragic what happened to him. Truly a loss for the community.
But between you and me? His singing was ass. 

#speaker: Player
The bartender mentioned he liked ABBA.

#speaker: Armchair Guy
Liked is an understatement. I actually recorded him on my phone last night to prove to my wife why I have to drink.
-> Hub

=== TheTape ===
#speaker: Player
You recorded him last night? I need that video. It could be evidence.

#speaker: Armchair Guy
Evidence of what?
Look, I already transferred it to a USB stick. You can have it. 
~ getitem(karaokeUSB)
-> Hub

=== LastNight ===
#speaker: Player
Did you see anything suspicious last night?

#speaker: Armchair Guy
#snore: start
<i>He immediately falls back to sleep </i> 
~ isAsleep = true
-> Hub

=== AwakeExit ===
#speaker: Player
I'll leave you alone now.

#speaker: Armchair Guy
Ha det bäst mannen.
-> END