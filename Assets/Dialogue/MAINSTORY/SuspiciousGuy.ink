INCLUDE globalsmainstory.INK
//EXTERNAL runAway(x) // sätt namnet på animation trigger här
VAR ranaway = false
{ talkedToSuspiciousGuy: -> QuestionHub | -> Intro }
=== Intro ===
{ ranaway: -> Question }
<>What'd ya want from me? # speaker: Some guy
The man before you is clearly intoxicated. Inebriated, even. Absolutely piss drunk. #speaker: Player
    * { finishedCrimeScene } [I need to ask you a few questions.]
        -> GainEntry
    * [What are you doing?]
        I am LOITERING. And I will NEVER stop loitering. So don't bother asking me anything. # speaker: Some guy
    * [Nothing...]
- You don't even want to bother with this guy. Until you find something you could reasonably question him with you should just go. #speaker: Player
        -> END

= GainEntry
- What? Why? #speaker: Some guy
~ startMovement("Running")
~ ranaway = true
You'll never catch me pig!!! //# anim: Running
<i>This guy seems suspicious... I have to track him down.</i> #speaker: Player
->END

=== Question ===
<i>The suspicious man is desperately trying to catch his breath. He only managed to run a few meters.</i> #speaker:
Sorry bro, just... give me a sec to... Phew... Jesus, I need to lose weight fucking immediately, this is really bad... #speaker: Some Guy
Why did you run? You just earned yourself the top spot in my suspect list. #speaker: Player
I will never stop loitering, and you pigs can't fucking stop me. I don't care if you sentence me to death. I'd rather die loitering than live on my knees NOT loitering. #speaker: Some Guy
That's very poignant, but I'm not concerned about that. I want to ask you about something else entirely. #speaker: Player
Then lay it on me! As long as I can keep on loitering I'll sing to your tune, detective. #speaker: Some Guy
    ~ talkedToSuspiciousGuy = true
<>-> QuestionHub
                
=== QuestionHub ===
* { knowledge !? victimPoisoned } [Did you know the victim?] -> AskVictim
* { knowledge !? bartenderAlibi } [About last night...] -> LastNight
+ { LIST_COUNT(Suspects) > 1 } [Ask about current suspects] -> QuestionSuspects
* [I gots to go.]
    God speed, officer. I'll stay here and loiter.
    -> END

= AskVictim
Huh? #speaker: Some Guy
The guy that was found last night, Peter Grip. Ring a bell? #speaker: Player
What the fuck are you saying? Something happened to Peter? #speaker: Some Guy
He's... dead? #speaker: Player
Get the fuck out of here. What? #speaker: Some Guy
<i>He seems genuinely shocked. I don't know how he managed to miss this.</i> #speaker: Player
It's believed that he succumbed to alcohol poisoning. #speaker: Player
Hell no! <b>Poisoning</b>, maybe! Period! He knew how to hold his weight, no way he'd die from that shit. He had at least a good 15 years of heavy drinking left in him. #speaker: Some Guy
Interesting, so you suspect foul play? #speaker: Player
Absolutely. This aggression will not stand, man. You gotta find out who did this! I'll tell you anything I know.
<i>Poisoned, huh...? That's an interesting theory. It's possible.</i> #speaker: Player
{ Suspects ? bossMan: Could Boss Man have done it somehow? }
~ gainknowledge(victimPoisoned)
-> QuestionHub
= LastNight
I was at the bar for most of the night. Peter and Sleepy Guy was there as well. We had a good time! #speaker: Some Guy
{ talkedToArmchairGuy: 
    I talked to him earlier. He wouldn't tell me anything, he just keeps falling asleep when I try to ask him any questions. #speaker: Player
    He's narcoleptic. If you have something important to ask him about, he'll be more inclined to answer you. #speaker: Some Guy
    However, if you have nothing interesting to say he'll probably just fall asleep again...
    Anyway, <>
- else:
    Sleepy guy...? #speaker: Player
    He sits in the armchair close to the bar disk, you can't miss him. He's narcoleptic and tends to fall asleep randomly. Talk to him when you get the chance. #speaker: Some Guy
    Anyway, <>
}
Peter did his usual karaoke routine, to the dismay of the bartender. He can't stand his singing.
After knocking back a few cold ones he went to the gas station to get some snus.
I guess something must have happened when he went there, because when he came back he wasn't his usual self. We asked what had happened and he just waved his hand and sat down by himself by the counter.
After that, me and the other guy left an hour or so before closing, and Peter was alone with the bartender.
{ bartenderToldHisAlibi: 
<i>Hold on a minute. The bartender told me they all left during the same time last night...</i> #speaker: Player
Are you sure? The bartender told me that you all left at around the same time.
That ain't right. After Peter came back from the shop, he was lowkey ruining the vibe, so me and sleepyhead went home. #speaker: Some Guy
~ gainknowledge(bartenderAlibi)
<i>The bartender pulled the wool over your eyes, you fucking idiot. There's something he's not telling you.</i> #speaker: 
-> QuestionHub
- else:
And they can corroborate your alibi? Where can I find him? #speaker: Player
For sure brother, don't worry about it. He's probably at the bar right now, and drifted off into a peaceful sleep. #speaker: Some Guy
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