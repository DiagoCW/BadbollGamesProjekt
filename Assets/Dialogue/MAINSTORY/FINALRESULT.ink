INCLUDE globalsmainstory.INK
EXTERNAL fadeToBlack(alpha, duration)
-> Start
=== Start ===
~ finalDialogueTriggered = true
The officer still isn't here. #speaker: Player
That should give me a bit of time go over the facts once more. Who did I tell the officer to arrest, again? 
~ temp accusedID = getAccusedSuspectID()
//VAR accusedID = 1
{ accusedID == 1: -> BossMan }
{ accusedID == 2: -> Bartender }
{ accusedID == 0: -> StoreClerk }
{ accusedID == 5: -> NoSuspect }

= NoSuspect
Oh, right. I didn't arrest anyone. I couldn't crack the case.
The evidence that I linked to the suspect were either wrong, or they were actually innocent all along.
~ startMovementBlocking("Walking")
~ focusCamera("Officer")
Just in time? More like <i>Not in time</i>. #speaker: Police
Sorry, that was mean. But I told you, detective. This was a cut and dry case, not every little case is a murder to solve.
Just hand over the case file and that will be that.
So that's it? Just like that? It's like the writer just threw something together when he realised that he didn't have a proper ending if you fail to catch a suspect. #speaker: Player
-> Final

= BossMan
Boss Man. It has to be him. #speaker: Player
~ finalSuspect = "Boss Man"
First off, he had the victim's wallet.
Second, he claims to have found it <b>before</b> a purchase was made at his shop, which was made using the victim's card.
Third, and most important, he was the one who found the body and called it in. This could have been a ruse to make himself look less suspicious.
He claimed that he took the wallet as some form of 'payment' before realising that the victim was actually dead, at which point he called the police. However, this was a bluff. 
He <i>poisoned</i> the victim's falafel, which would have taken effect almost immediately. He could then approach the body, get his hands on his wallet, and dispose of any evidence; the half-eaten falafelrulle in the trash that matches the one that Peter orders.
Given the victim's prior history with Boss Man, no one who knew him would even give it a second thought. However, It looks like I was... <b>just in time.</b>
-> PoliceArrives

= Bartender
~ finalSuspect = "The bartender"
The bartender. It has to be him. #speaker: Player
First, he lied about his alibi. He claims that Peter and two other patreons were all together at his bar last night until closing. 
However, these other two patreons contradict this alibi. In reality, they both left together some hours before closing, leaving only the bartender alone with Peter.
Second, during this time, he had ample opportunity to kill him. That is, he <i>poisoned him</i>. #speaker: Player
The bar suffers from a rat infestation, and the bartender has rat poison strewn about all over the place; and it seems to have found its way into his beer <i>somehow</i>.
Third, there's the issue of the trisslott. I talked with two of Peter's aquaintances, and they both attest to him having won, big time. The bartender would also have known about it, but conviniently neglected to mention it.
His business was struggling and he was also risking foreclosure, he needed a rather big sum to avoid this, and he needed it fast. Once he heard talk about the trisslott, Peter's fate was already sealed.
As Peter was left alone with the bartender, he saw his opportunity and enacted his plan. The poison needed an hour or so to take effect, which would avoid the problem of Peter dying immediately at the bar, which would then cast bigger suspicion on the bartender. 
By the time Peter had stumbled away from the falafel shop, he had collapsed; at which point the trisslott was easy pickings.
The bartender almost got away with it too, but it looks like I was... <b>just in time.</b>
-> PoliceArrives

= StoreClerk
The store clerk. It has to be him. #speaker: Player
~ trueEnding = true
~ finalSuspect = "The Store Clerk"
First, him and the victim had been overheard arguing over something. This something was a trisslott. A winning one, at that. Peter had come in and started flaunting it right in his face. #speaker: Player
The store clerk felt slighted at this, insinuating that Peter didn't deserve to win such a huge sum. This quickly escalated into a brawl between the two and Peter would leave the store shortly after; but not without buying his snus.
Second, there is a witness that can attest to the store clerk not being at the store around the time of the victim's death, around 5 minutes or so. Though not definitive proof, there is reasonable doubt.
And considering the fact that the victim may have been poisoned brings me to my third and final point: He laced Peter's snus with coolant, meaning that the victim had been slowly poisoned throughout the night. #speaker: Player
After their brawl he went into the storage room, and it was there that he poisoned the snus, before bringing it back to the victim. Then he started playing the waiting game.
I believe that he would leave the store whenever there wasn't a customer to attend to in order to assess whether or not Peter had died yet.
And when he left the store for the final time, that's when he finally found the body and promptly looted his corpse.
It was the perfect crime. No one would question if he had been missing for only a few minutes, and there was certainly no time to murder a man under regular circumstances.
However, it looks like I was... <b>just in time...</b>
-> PoliceArrives

= PoliceArrives
~ startMovementBlocking("Walking")
~ focusCamera("Officer")
Very impressive, detective. Very impressive indeed. #speaker: Police
No thanks to you. If I hadn't solved this case, you would just chalk his death up to alcohol poisoning. #speaker: Player
C'mon, don't be like that. We were a team you and I! We're just in time! #speaker: Police
That's MY catchphrase, you don't get to use it. I'M just in time! #speaker: Player
Alright, whatever. We have a few points of order to go though before I deliver the final verdict. #speaker: Police
What verdict? I finished the case. #speaker: Player
We just have to evaluate some of your stats during the game. You know, determine your final rank. #speaker: Police
That's right, I'm talking to <i>you</i>. Buckle up. You're about to find out if you fucked up big time.
-> DVisionEvaluation

= DVisionEvaluation
I looked into this <color=\#FFFF00>Detective Vision</color> of yours that you mentioned, rather interesting stuff... En sån skulle man ha haft, höhö! #speaker: Police
I understand that it puts quite the strain on the user, and excessive use might lead to unintended side effects. Let's see...
{ dvisionTotalTime <= 50: 
You used your Detective Vision quite sparingly; only {dvisionTotalTime} seconds recorded. Very impressive. If you've used it this efficiently during all your years of service, you may still have many good years still ahead of you.
This is news to me. What happens if you use it too much? #speaker: Player
Trust me, you don't want to know. #speaker: Police
- else: 
You certainly didn't skimp on using your Detective Vision... I have {dvisionTotalTime} seconds recorded here. That's not good.
Why? What do you mean? #speaker: Player
I mean, if you've used it this liberally during all your years in service considering the strain that it puts on you... #speaker: Police
I'd say you only have 5 months left to live.
Oh... #speaker: Player
Yeah, but don't worry about it. We still have to assess your deduction on the case, and subsequent arrest of the culprit! #speaker: Police
I kind of don't care about anything anymore... #speaker: Player
If I knew you'd be such a baby about it, I wouldn't have told you. Now, moving on... #speaker: Police
}
- -> FinalSuspect

= FinalSuspect
You decided to arrest {finalSuspect} based on the evidence that you went through earlier, when you were talking to yourself like some kind of freak. #speaker: Police
So...
{ finalSuspect == "The Store Clerk": 
    -> GoodEnding 
- else:
    -> BadEnding
}

=== GoodEnding ===
After you ordered his arrest, we decided to actually do our job and review the security footage from the gas station, as well as inspect the storage. #speaker: Police
And? What did you find? #speaker: Player
He definitely poisoned the victim. While reviewing the security footage, we could see him pouring a cannister of some blue liquid into the tin of snus that Peter would buy shortly after. #speaker: Police
After that it was pretty uneventful for a few hours, but around midnight he would suddenly start leaving the store and come back after a few minutes. He did this once every 10 minutes or so, up until the estimated time of the victim's death.
So it was just as I thought. He wanted to make sure that he would find the victim dead, at which point he could retrieve the trisslott. #speaker: Player
Exactly, and speaking of...
We found it. <i>Trisslotten</i>. #speaker: Police
To commerorate this massive achievement, we see it fit that you should be the one to have it. Go ahead, detective.
Don't you need this as evidence for the prosecution? #speaker: Player
Who cares? I hate this job. I do what I want. #speaker: Police
This... I don't know what to say. #speaker: Player
<i>You hold the trisslott with both hands. The final piece of the puzzle, and the entire reason for this whole horrible ordeal. You glance over it, and inspect the winning sum of...</i> #portrait: 5 #speaker: 
<i>...1000kr?</i> Really? That's it? #speaker: Player #portrait: 
Yeah, you would think that it would be a large enough sum to justify murdering someone. #speaker: Police
I guess it was a big deal for Peter though, this could cover his drinking tab for a few days at least.
This is very anti-climactic. I don't know how to feel about this. #speaker: Player
You solved the case, detective. What more do you need? A pat on the shoulder? Get real. Buy yourself something nice with it. Maybe a drink to honor Peter's memory... #speaker: Police
-> Final

=== BadEnding ===
{ finalSuspect == "Boss Man": 
    -> BossManEnding
- else:
-> BartenderEnding
}

= BossManEnding
We took a closer look at the falafel that was supposedly poisoned. #speaker: Police
And? Was I correct? #speaker: Player
Not really. It wasn't poisoned, his falafels are just really shit. #speaker: Police
Not that it matters. Once we took him in and started pressing him a bit, he spilled the beans. If you catch my drift. #speaker: Player
Yeah, uh... That usually means that they confessed? #speaker: Player
I meant that he shit himself, like not figuratively. Is that not how you use that phrase? Because he did also confess to it, yeah. #speaker: Police
I don't understand. If he didn't deliberately poison the falafel, then how did he do it? #speaker: Player
The victim choked to death, which was all obviously part of Boss Man' plan. He put extra everything into the falafelrulle, which was too much for poor little Peter to digest properly. #speaker: Police
That's extremely dumb and makes no sense. It almost feels like a half-baked ending to a story line that the writer didn't know how to finish properly. #speaker: Player
No one cares what you think, it's what happened. #speaker: Police
-> Final 

=== BartenderEnding ===
We analyzed Peter's beer that you collected as evidence, and it sure as hell tested positive for rat poison. #speaker: Police
It also tested positive for Hanta-virus, which... has certain implications. None of which concerns the case, however.
It didn't take long for the bartender to confess. Just like you said, he saw an opportunity to save his business; no matter the cost. He needed that trisslott.
It's a shame... Peter had at least a good 15 years of heavy drinking left in him. I should have known that he couldn't have died just from that.
-> MissingTrissLott

=== MissingTrissLott ===
One thing still bothers me, though... We never did find the trisslott. #speaker: Police
Maybe you just didn't look hard enough. {finalSuspect} must have it stashed away somewhere. It has to show up eventually. #speaker: Player
I hope so. Without the trisslott as actual evidence, it will be harder to prosecute {finalSuspect}. #speaker: Police
But you said that they already confessed to everything, why does it matter? #speaker: Player
It just does, deal with it. #speaker: Police
-> Final

=== Final ===
That'll be all. Until next time, detective. #speaker: Police
{ dvisionTotalTime <= 50 and trueEnding:
    * [<color=\#FFFF00>Hold on a minute...]
        -> TrueEnding
- else:
    ~ rollCredits()
    -> END
    }  


= TrueEnding
You can leave, now.
I... I don't want to. #speaker: Player
You have to. It's over. #speaker: Police
It can't be. This feels wrong for some reason... Something is off. #speaker: Player
You did it, detective. You solved the big case. You're Justin Time, and you're just in time. #speaker: Police
And now you have to leave. 
~ fadeToBlack(1, 3)
Officer...? #speaker: Player
~ stopAllAmbience()
//~ changeTypingSpeed(0.12)
//You're <b>done. This is it.</b> #speaker: ???
//~ changeTypingSpeed(0.03)
~ playAmbience("PREMONITION-IN-THE-PARK")
<i>You are still in your car on your way to the crime scene, and your car is swerving. In just a few seconds, you are going to crash into that tree in the park. Only difference is you won't make it this time.</i> #speaker: 
The human mind really is fascinating... I managed to solve an entire case in my head mere moments before dying. I might be the greatest detective alive. #speaker: Player
Well, I won't be alive for much longer I reckon.
{ panicked: -> Panicked }
{ resignedToFate: -> Fate }
{ steppedOnGas: -> Gas }
- (Panicked)
Maybe this is because I started panicking when my car started swerving. If only I had kept my cool... #speaker: 
-> introcont
- (Fate)
Like I said, this is fine. I will be with Him soon. #speaker: 
-> introcont 
- (Gas)
Why the fuck did I even start flooring it? #speaker: 
-> introcont
- (introcont) 
<> Well, I solved the case and that's all that matters. Even if it was all in my head.
~ lowerPitch("PREMONITION-IN-THE-PARK")
<i>As the car draws closer and closer to the town, time slows down exponentially, until time stands completely still.</i> #speaker: 
Looks like I'm... <i>just in...</i> #speaker: Player
~ playAudio("car-crash")
</i>Until it doesn't. You reach an abrupt stop.</i> #speaker: 
~ rollCredits()
-> END