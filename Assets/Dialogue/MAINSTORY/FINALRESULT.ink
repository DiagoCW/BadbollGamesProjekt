INCLUDE globalsmainstory.INK
-> Intro
=== Intro ===
<i>You find yourself at the local precinct awaiting a final brief from the officer, after managing to apprehend a suspect.</i> #speaker: 
<i>Who was it you arrested, again?</i>
{finalSuspect}. It must have been him. #speaker: Player
{ finalSuspect == "Boss Man": -> BossMan }
{ finalSuspect == "Bartender": -> Bartender }
{ finalSuspect == "Store Clerk": -> StoreClerk }
{ finalSuspect == "Wrong": -> NoSuspect }

= NoSuspect
-> END

= BossMan
First off, he had the victim's wallet. #speaker: Player
Second, he claims to have found it <b>before</b> a purchase was made at his shop, which was made using the victim's card. #speaker: Police
Third, and most important, he was the one who found the body and called it in. This could have been a ruse to make himself look less suspicious. #speaker: Player
He claimed that he took the wallet as some form of 'payment' for the menial task of always having to return his wallet to him, before realising that the victim was actually dead, at which point he called the police. However, this was a bluff. 
If Boss Man had knowledge of Peter's trisslott, then that changes things quite a lot. He would absolutely have a reason for killing him, and an even bigger reason for digging himself deeper into these lies.
He <i>poisoned</i> the victim's falafel, which would have taken effect almost immediately. He could then approach the body, get his hands on the lottery ticket, and dispose of any evidence.
Given the victim's prior history with Boss Man, no one who knew them would even give it a second though. However, It looks like I was... just in time...
-> END

= Bartender
First, he lied about his alibi. He claims that Peter and two other patreons were all together at his bar last night until closing. #speaker: Player
However, these other two patreons contradict this alibi. In reality, they both left together some hours before closing, leaving only the bartender alone with Peter.
Second, during this time, he had ample opportunity to kill him. That is, he <i>poisoned him</i>. #speaker: Player
The bar suffers from a rat infestation, and the bartender has rat poison strewn about all over the place; and it seems to have found its way into his beer <i>somehow</i>.
Third, there's the issue of the trisslott. I talked with two of Peter's aquaintances, and they both attest to him having won, big time. The bartender would also have known about it, but conviniently neglected to mention it.
As Peter was left alone with the bartender, he saw his opportunity and enacted his plan. The poison took an hour or so to take its effect, and by the time Peter stumbled away from the falafel shop, he collapsed.
This is why
-> END

= StoreClerk
First, him and the victim had been overheard arguing over something. This something was a trisslott. A winning one, at that. #speaker: Player
The store clerk felt slighted at this, insinuating that Peter didn't deserve to win such a huge sum. This quickly escalated into a brawl between the two, and Peter left the store shortly, but not before buying his snus.
Second, there is a witness that can attest to the store clerk not being at the store around the time of the victim's death, around 5 minutes or so. Though not definitive proof, there is reasonable doubt.
Hardly enough time to kill a person with only 5 minutes to spare. #speaker: Police
Not at all. However, considering that he may have been poisoned brings me to my third and final point: He laced Peter's snus with kylvätska, and he had been slowly poisoned throughout the night. #speaker: Player
When he surmised that enough time had passed that Peter should have died, he left his post and went to look for him.
Normally he would have had an airtight alibi, since no one would question that he left the store. Sadly for him, there was a customer there that noticed that he was missing for those few minutes.
-> END

= PoliceArrives
~ startMovement("Walking")
Very impressive, detective. Thanks to you, we might get justice for Peter. #speaker: Police
We have a few points of order to go though before I give you the final verdict, however. #speaker: Police
What verdict? I finished the case. #speaker: Player
We just have to evaluate some of your stats while playing the game. You know, determine your final rank. #speaker: Police
That's right, I'm talking to <i>you</i>. Buckle up.
-> DVisionEvaluation

= DVisionEvaluation
{ dvisionTotalTime <= 50: You used your Detective Vision quite sparingly; only {dvisionTotalTime} seconds. Very impressive. }
{ dvisionTotalTime > 50: You didn't skimp on using your Detective Vision... I have {dvisionTotalTime} seconds recorded here. This reflects rather poorly on you. }
-> FinalSuspect

= FinalSuspect
You decided to arrest {finalSuspect} based on the evidence that you went through earlier, when you were talking to yourself like some kind of freak. #speaker: Police
That arrest, however...
{ finalSuspect == "Store Clerk": 
    -> GoodEnding 
- else:
    -> BadEnding
}

=== GoodEnding ===
... was absolutely correct! After you ordered the arrest of him, we decided to actually do our job and review the security footage from the gas station, as well as inspect the storage. #speaker: Police
And? What did you find? #speaker: Player
We found it. <i>Trisslotten</i>. #speaker: Police
To commerorate this massive achievement, we see it fit that you should be the one to have it. Go ahead, detective.
This... I don't know what to say. #speaker: Player
<i>You hold the trisslott with both hands, inspecting the winning sum of...</i> #portrait: 0 #speaker: 
1000kr... Really? That's it? #speaker: Player #portrait: 
Oh. That's a bummer. We didn't actually look at it. We just assumed it had to have been a big enough sum to justify poisoning a man. #speaker: Police
For ol' Peter, it sure must have been a lot! It could have covered his drinking tab for a few days. Maybe.
This is very anti-climactic. I don't know how to feel about this. #speaker: Player
SLUT PÅ SPELET HEJDÅ!!!!!!!!!
-> END

=== BadEnding ===
... was not even close to right. You arrested an innocent man.
Huh? #speaker: Player
{ finalSuspect == "Boss Man": 
    -> BossManEnding
- else:
-> BartenderEnding
}

/*
= BossManEnding
Your reasoning is that Boss Man poisoned Peter in order to get a hold of his wallet, and therefore his trisslott. #speaker: Police
We examined the falafel. It wasn't poisoned, his falafels are just really shit. On further examination, some weird chemical imbalance within Peter caused his body to shut down because of this.
Furthermore, no evidence of a supposed trisslott was found. Nothing indicates that Boss Man has it, let alone cashed it in.
But... the victim's wallet. Boss Man had it. It could have contained the trisslott. Just him having the wallet is evidence is enough. #speaker: Player
It's all circumstantial. It doesn't matter when or how he found the wallet. #speaker: Police

-> END


= BartenderEnding
Your reasoning is that the Bartender, during the time he was alone with Peter, poisoned his beer with rat poison in an attempt to acquire the trisslott for himself. #speaker: Police
However, no such trisslott was ever found. 
-> END

*/