INCLUDE globalsmainstory.INK
#speaker: Store Clerk #anim: Talking
{ Suspects ? storeClerk: We're done here. I will answer no further questions. }
{ askedQuestion: Welcome back! Hope you didn't have any more questions, because I won't answer them. | Welcome, welcome! What can I do for you today? }
-> Intro
=== Intro ===
* { debug } [LÅS UPP SOM SUSPECT]
    ~ addsuspect(storeClerk)
    ~ unlockSuspect(storeClerkID)
    -> Intro
* {finishedCrimeScene and not askedQuestion } [Did you know the victim?] -> Victim
* {finishedCrimeScene and not askedQuestion} [About last night...] -> LastNight
* {askedQuestion} [One more question] -> OneMoreQuestion
// * {askedQuestion and Clues ? kylarVätska } [About last night...] -> LastNight
* [I gots to go, baby.] -> END

= Victim
I heard from some customers that they found someone, <i>dead</i>. Scary times we're living in. #anim: Shake
You don't know who it was, then? #speaker: Player
Sir, I'm going on the 18th hour of my 30 hour shift. I don't have time to worry about stuff like that. Wait until after my shift if you want to ask me about anything non-work related. #speaker: Store Clerk
~ talkedToCashier = true
-> Intro

= LastNight
{ Victim: As I said, sir, } I don't have time to answer any questions you might have for me. I'm on the clock here, I got work to do. #speaker: Store Clerk
C'mon. One question, please? #speaker: Player
Alright, <i>one question.</i> #speaker: Store Clerk
    * { Suspects ? bossMan or knowledge ? receiptsBelongToVictim } [Did Peter Grip come here last night?] -> LastNightVictim
    * [Where were you last night?]
        Here, as usual. I started my shift 18 hours ago, and still have some hours to go. #speaker: Store Clerk
        ~ cashierToldHisAlibi = true
        And you've never left even once? #speaker: Player
        -> LastNightEnd
    * [How's it going?]
        Fine, thank you. #speaker: Store Clerk
        Busy day? #speaker: Player
        -> LastNightEnd

= LastNightVictim
Ah, Peter... He did, yes. #speaker: Store Clerk
You don't sound too thrilled to hear his name. Did something happen? #speaker: Store Clerk
-> LastNightEnd

= OneMoreQuestion
Could I just ask one more question? #speaker: Player
No. #speaker: Store Clerk
Please? #speaker: Player
Alright, alright. Make it quick, I have work to do you know. #speaker: Store Clerk
~ askedQuestion = false
-> Intro


= LastNightEnd
That's your second question, I'm afraid. As I said, I'm happy to answer any questions you might have once I get off my shift. #speaker: Store Clerk
{ (Suspects ? bossMan): 
    * [<color=\#FFFF00><b>Keep pressing him</b></color>] -> AltercationVictim
- else:
    ~ askedQuestion = true
    <i>Damn, I didn't think he meant literally one question... Should I have asked something else?</i>
    -> END
}

= AltercationVictim
No, I have more questions actually. I have overheard that you got into some altercation with the victim last night, and I would like to know what it was about. #speaker: Player
That's... it was nothing, he just flew off into some drunken stupor like he always did. I had to rough him up a bit, you know? #speaker: Store Clerk #Shake
<i>Did?</i> I never told you who the victim was, and you implied that you didn't know who it was, either. But you speak about him in the past tense. #speaker: Player
Oh... oh. #speaker: Store Clerk
Oh is right, buddy. So you knew who the victim was, then. What are you hiding? #speaker: Player
Alright look; he just came in, mouthed off about some shit and started rubbing it in my face. And I wasn't having none of it, so I gave him a piece of my mind. I admit it, OK? It was a rough start to my shift. #speaker: Store Clerk
What could he possibly have said to make you fly off the handle like that? #speaker: Player
* { Clues ? trisslott } [<color=\#FFFF00>Ask about trisslott</color>] -> AskTrisslott
I don't see how that's any of your business. He pissed me off like he always <b>did</b>. No more. That's all I have to say about that. #speaker: Store Clerk
<i>I need to find out what their argument was about. It could be the first puzzle piece in a very, very long series of intricate steps that eventually culminated in the victim's death.</i> #speaker: Player
<i>Or not, but I should find out more about this...</i>
<> -> Intro

= AskTrisslott
Win big, lately? #speaker: Player
I stay winning, loser. #speaker: Store Clerk
Says the guy working at a gas station. Anyway, Peter must have mentioned it, yeah? How he won that huge sum off of his trisslott. #speaker: Player
I know that's what caused your little schism last night. Look, I get it. I would be furious if some low-life drunk that pesters me constantly in the middle of my 30-hour shift suddenly came barging in, announcing that he won the lottery of all things.
Yeah, you're right. I <b>was</b> furious. That's why I tried to knock him the fuck out. #speaker: Store Clerk
That's not the whole reason, is it?. I think you tried to take the trisslott from him as well somehow. Since brute force didn't work, you tried a more... methodical approach.
What are you saying? #speaker: Store Clerk
{ knowledge ? cashierAlibi:
    -> AskAlibi
- else:
    Uh... I don't really know where I'm going with this. Did you kill him, yes or no? #speaker: Player
    No. #speaker: Store Clerk
    Are you sure? #speaker: Player
    Yes. #speaker: Store Clerk
    <i>Damn, I almost had him. I have nothing to go on. Peter died while this guy was still on the same shift as he is right now.</i>
    <i>That is assuming of course he was here all the time... If he left at any point during his shift, say around the time of Peter's death, then that would change things.</i>
    <i>Maybe I could ask around. Someone must have seen something.</i>
    -> END
}

= AskAlibi
You were gone for a few minutes last night during your shift. Where did you go? #speaker: Player
You expect me to just stay put here for 30 hours? I have to take out the trash, go to the bathroom, grab a smoke, whatever... I take many little breaks here and there, no one notices it. #speaker: Store Clerk
Someone did happen to notice that you were gone around the time the victim died last night, however; making you M.I.A during his death. #speaker: Player
Yeah? That's what we call a coincidence. As you said, I was gone for only a few minutes. Do you believe it to be possible to leave, find a person, kill them, and then come back in just a few minutes? #speaker: Store Clerk
He wasn't the victim of a stabbing or a gun shot or anything like that. How do you propose I killed him then in these few minutes?
{ Clues ? kylarVätska:
    -> AskCoolant
- else:
    You punched him, or something? #speaker: Player
    Are you joking, I killed him just by punching him? Look at my arms, they look like little cigarettes. Why, you could smoke them little arms if you wanted to! #speaker: Store Clerk
    I'm fine, thank you. #speaker: Player
    <i>If he did kill Peter, there's still one final piece of the puzzle that I'm missing. I just don't know what yet...</i> 
    -> END
}

= AskCoolant
I had a little look around your storage. I found the key under the door mat, anyone could get in there really. #speaker: Player
I trust in the goodness of strangers that they wouldn't do that. But nothing is beneath you, it seems... #speaker: Store Clerk
Don't spin this on me, buddy! The issue is what I found inside the storage. And that is... #speaker: Player
An empty cant of coolister.
What? #speaker: Store Clerk
I meant an empty cannister of coolant, sorry. I might have suffered a concussion earlier... #speaker: Player
Still, what? That's your big reveal? Do you know where we are? We are at <b>the gas station</b> buddy, where you might go to get coolant for your car. #speaker: Store Clerk
I saw that you keep it next to a bunch of snus. It just so happens that's the very same snus that Peter has, and which I have proof that he also bought here last night. #speaker: Player #portrait: 0
Look, that snus is in the storage because I took them out of sale. You know why I did that? <b>Because a bunch of coolant has seeped into them!</b> I obviously know that it's a health hazard, and I wouldn't ever sell it to my customers. #speaker: Store Clerk #portrait: 
Except that you did sell it to him. I found this on Peter's body. And it sure as hell smells like it has been soaking in coolant. #portrait: 4 #speaker: Player
The fact that you two had an argument and that you were not here during the time of the victim's death, coupled with the fact that Peter likely succumbed to that coolant-soaked snus; what say you?
Uh... As I said earlier, sir, I don't have time to answer any questions right now... #speaker: Store Clerk #anim: Shake #portrait: 
That's fine. I have everything I need. #speaker: Player
~ addsuspect(storeClerk)
~ unlockSuspect(storeClerkID)
<i>You have unlocked The Store Clerk as a suspect. You can speak with The Officer regarding this or consult your Field Manual for further information.</i>
-> END
