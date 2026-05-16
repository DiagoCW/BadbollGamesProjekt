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
<i>He darts across the road and immediately stops by the convience store to catch his breath. He seems suspicious...</i> #speaker:
->END

=== Question ===
I honestly thought I would get farther. Sorry bro... #speaker: Some Guy
* [Why did you run?]
    I will never stop loitering around, and you pigs can't fucking stop me.
    ** I'm not concerned about that.[] I want to ask you about something else entirely. #speaker: Player
            Then lay it on me! As long as I can keep on loitering I'll sing to your tune, officer. #speaker: Some Guy
            ~ talkedToSuspiciousGuy = true
                <>-> QuestionHub
                
=== QuestionHub ===
* [Did you know the victim?] -> AskVictim
* [About last night...] -> LastNight
+ { LIST_COUNT(Suspects) >= 1 } [Ask about current suspects] -> QuestionSuspects
* [I gots to go.]
    God speed, officer. I'll stay here and loiter.
    -> END

= AskVictim
Huh? #speaker: Some Guy
The guy that was found last night, Peter Grip. #speaker: Player
What the fuck are you saying? Something happened to Peter? #speaker: Some Guy
He's dead. #speaker: Player
Get the fuck out of here. What? #speaker: Some Guy
<i>He seems genuinely shocked. I don't think he had anything to do with this.</i> #speaker: Player
It's believed that he was alcohol poisoned. #speaker: Player
Nuh-Uh! <b>Poisoned</b>, maybe! Period! He knew how to hold his weight, no way he'd die from that shit. He had at least a good 15 years of heavy drinking left in him. #speaker: Some Guy
Interesting, so you're saying you believe he could've been murdered? #speaker: Player
Absolutely. This aggression will not stand, man. You gotta find out who did this to him! I'll tell you everything I know.
-> QuestionHub
= LastNight
I was at the bar for most of the night. Loitering. Peter and some other guy was there snoring his head off. We had a good time! #speaker: Some Guy
Peter did his usual karaoke routine, to the dismay of the bartender. He can't stand his singing.
After pounding a few cold ones he went to the gas station to get some snus.
I guess something must have happened when he went there, because when he came back he wasn't his usual self. We asked what had happened and he just waved his hand and sat down by himself by the counter.
After that, me and the other guy left an hour before closing and Peter was alone with the bartender.
{ bartenderToldHisAlibi: 
<i>Hold on a minute. The bartender told you that they all left during the same time last night...</i> #speaker:
Are you sure? The bartender told me that you all left at around the same time. #speaker: Player
That ain't right. After Peter came back from the shop, he was lowkey ruining the mood, so me and sleepyhead went home. #speaker: Some Guy
~ gainknowledge(bartenderAlibi)
<i>The bartender pulled the wool over your eyes, you fucking idiot. You got to go back there <b>right now</b> and confront him about this.</i> #speaker: 
-> QuestionHub
- else:
And they can corroborate your alibi? Where can I find him? #speaker: Player
For sure brother, don't worry about it. He's at the bar right now. #speaker: Some Guy
-> QuestionHub
}


// --- UNDER CONSTRUCTION
= QuestionSuspects
{ LIST_COUNT(Suspects) == 1: You don't have any suspects yet. Make a mental note to come back to this later, though. <> -> QuestionHub }
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