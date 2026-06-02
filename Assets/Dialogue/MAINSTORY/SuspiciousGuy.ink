INCLUDE globalsmainstory.INK
VAR ranaway = false
{ talkedToSuspiciousGuy: My man! What's up? -> QuestionHub | -> Intro } #speaker: Leif
=== Intro ===
{ ranaway: -> Question }
<>What'd ya want from me? # speaker: Leif
The man before you is clearly intoxicated. Inebriated, even. Absolutely piss drunk. #speaker: Player
    * { finishedCrimeScene } [I need to ask you a few questions.]
        -> GainEntry
    * { not finishedCrimeScene } [What are you doing?]
        I am LOITERING. And I will NEVER stop loitering. So don't bother asking me anything. # speaker: Leif
    * [Nothing...]
- <i>I don't even want to bother with this guy, I should just avoid him. For now...</i> #speaker: Player
        -> END

= GainEntry
What? Why? #speaker: Leif
~ startMovement("Running")
~ focusCamera("Leif")
~ ranaway = true
You'll never catch me pig!!! //# anim: Running
<i>This guy seems suspicious... I have to track him down.</i> #speaker: Player
~ resetCamera()
->END

=== Question ===
<i>The suspicious man is desperately trying to catch his breath. He only managed to run a few meters.</i> #speaker:
Sorry bro, just... give me a sec to... Phew... Jesus, I need to lose weight fucking immediately, this is really bad... #speaker: Leif
Why did you run? You just earned yourself the top spot in my suspect list. #speaker: Player
I will never stop loitering, and you pigs can't fucking stop me. I don't care if you sentence me to death. I'd rather die loitering than live on my knees NOT loitering. #speaker: Leif
That's very poignant, but I'm not concerned about that. I want to ask you about something else entirely. #speaker: Player
Then lay it on me! As long as I can keep on loitering I'll sing to your tune, detective. #speaker: Leif
    ~ talkedToSuspiciousGuy = true
<>-> QuestionHub
                
=== QuestionHub ===
* { knowledge !? victimPoisoned } [Did you know the victim?] -> AskVictim
* { knowledge !? bartenderAlibi } [About last night...] -> LastNight
//+ { LIST_COUNT(Suspects) > 1 } [Ask about current suspects] -> QuestionSuspects
* [I gots to go.]
    God speed, officer. I'll stay here and loiter.
    -> END

= AskVictim
Huh? #speaker: Leif
The guy that was found last night, Peter Grip. Ring a bell? #speaker: Player
What the fuck are you saying? Something happened to Peter? #speaker: Leif
He's... dead? #speaker: Player
Get the fuck out of here. What? #speaker: Leif
<i>He seems genuinely shocked. I don't know how he managed to miss this.</i> #speaker: Player
It's believed that he collapsed last night after having a few too many. #speaker: Player
I don't buy that! He knew how to hold his weight, no way he'd die from that shit. He had at least a good 15 years of heavy drinking left in him. #speaker: Leif
Interesting, so you suspect foul play? #speaker: Player
Absolutely! This aggression will not stand, man. You gotta find out what actually happened. He could have been poisoned for all I know!
<i>Poisoned, huh...? That's an interesting theory. It's possible.</i> #speaker: Player
{ Clues ? falafel or beer or ratPoison or kylarVätska: I might have some idea on how he could have been poisoned... Better look through my inventory. }
~ gainknowledge(victimPoisoned)
-> QuestionHub
= LastNight
I was at the bar for most of the night. Peter was there, and Torsten as well. We had a good time! #speaker: Leif
{ talkedToArmchairGuy: 
    I talked to him earlier. He wouldn't tell me anything, he just keeps falling asleep when I try to ask him any questions. #speaker: Player
    He's narcoleptic. If you have something important to ask him about, he'll be more inclined to answer you. #speaker: Leif
    However, if you have nothing interesting to say he'll probably just fall asleep again...
    Anyway, <>
- else:
    Torsten...? #speaker: Player
    Some people call him <i>sleepyhead</i>. He sits in the armchair close to the bar disk, you can't miss him. He's narcoleptic and tends to fall asleep randomly. Talk to him when you get the chance. #speaker: Leif
    Anyway, <>
}
Peter did his usual karaoke routine, much to the dismay of the bartender. He can't stand his singing.
After knocking back a few cold ones he went to the gas station to procure some more snus.
I guess something must have happened when he went there, because when he came back he wasn't his usual self. I him asked what had happened and he just waved me away and sat down by himself in the corner.
After that, me and Torsten left an hour or so before closing, and Peter was alone with the bartender for all I know.
{ bartenderToldHisAlibi: 
<i><color=\#FFFF00>Hold on a minute...</color></i> #speaker: 
Are you sure? The bartender told me that you all left at around the same time. #speaker: Player
That ain't right. After Peter came back from the shop, he was lowkey ruining the vibe so me and Torsten went home. #speaker: Leif
~ gainknowledge(bartenderAlibi)
Can anyone verify this? #speaker: Player
Yeah, that would be Torsten. Even if he falls asleep, just push through and keep asking him! He'll tell you the same thing that I did. #speaker: Leif
<i>I have to remember to confront the bartender about this. It could be important.</i> #speaker: 
-> QuestionHub
- else:
And he'll tell me the same thing you just did? #speaker: Player
For sure brother, don't worry about it. He's probably at the bar right now, drifting off into a peaceful sleep. #speaker: Leif
-> QuestionHub
}

// --- UNDER CONSTRUCTION
= QuestionSuspects
* { Suspects ? bossMan } [Boss Man?]
* { Suspects ? bartender } [Bartender?]
* { Suspects ? storeClerk } [Store Clerk?]
* -> END // fallback
--> END

= AskBartender
-> END
= AskBossMan
-> END
= AskStoreClerk
-> END