INCLUDE globalsmainstory.INK
-> Brief
//{ talkToPolice: <>-> Brief | -> Intro }
/*
=== Intro ===
Jesus, are you alright?! What happened? #speaker: Police #anim: Talking
Looks like I'm... <i>Jus imhime...</i> time to... Justin... #speaker: Player
What? What are you talking about, did you have a concussion or something? #speaker: Police
{ panicked: -> Panicked }
{ resignedToFate: -> Fate }
{ steppedOnGas: -> Gas }
- (Panicked)
The car started swerving, so I panicked. So what? Big deal, happens to the best of us. I didn't cry like a baby or nothing. #speaker: Player
But you were barely going 20km an hour... #speaker: Police
<> -> introcont
- (Fate)
I left my fate in the hands of God, and he led me right where I needed to be. Praise his name. #speaker: Player
Yes, praise his name indeed... #speaker: Police
<> -> introcont 
- (Gas)
I started flooring it. Sadly, my foot got stuck on the pedal. Not much to be done about that. #speaker: Player
You shouldn't have floored it to begin with, but I guess you're right. #speaker: Police
<> -> introcont
- (introcont) Anyway, what took you so long? I've been waiting for quite a while. #speaker: Police
My car broke down on my way here, but it's alright. I fixed it using my <color=\#FFFF00>Detective Vision.</color> #speaker: Player
Uh... Ok. I don't know what that means. It sounds cool though, I'm happy for you. #speaker: Police #anim: Shake
Stop wasting my time, officer. Let's get on with the briefing. #speaker: Player 
Alright... Walk with me, it's just over here. #speaker: Police
~ startMovement("Walking")
~ talkToPolice = true
-> END
*/
=== Brief ===
{ talkToPoliceAgain: -> Continued }
What's the MO, chief? What's the beef, who's the perp? 10-17, over. #speaker: Player
Cut it out. #speaker: Police
Last night, the body of 27-year old Peter Grip was found lying here. The official cause of death is yet to be determined, but knowing him I'd say it came as no surprise. #speaker: Police
Why do you say that? Was he <i>murdered,</i> perhaps? #speaker: Player
No, no. Nothing like that. He was the town drunk! I've dealt with him before on many occassions when he's had a few too many at the bar. #speaker: Police
He likely collapsed after yet another night of drinking, but this time for good.
So, that's pretty much it! Any questions?
    -(opts)
    * [Who found the body?]
        As you said, somebody found the body. That someone must have phoned it in? #speaker: Player
        Yeah. <i>Boss Man.</i> #speaker: Police
        { talkedToBossMan:
            I crashed my car behind his shop. #speaker: Player
            Then I take it that you've already introduced yourselves. I hope you apologized to him, I heard him shouting at you when you arrived... #speaker: Police
            Don't you worry, we squashed the beef. <>
        - else:
            That's a made up name. You are lying to me. #speaker: Player
            He's the falafel guy, and it's what everyone calls him. He has his shop right behind where you crashed your car. #speaker: Police
            It's a wonder that you didn't drive it straight into his shop... You should probably apologize to him later.
        }
        -- Did you get an initial statement from him? #speaker: Player
        I didn't feel like it, to be honest. Though I figured that you could take care of that since you're here now! #speaker: Police
        Peter was also a regular there, I'm pretty sure. You could ask him if he came by yesterday.
            -> opts
    * -> cont
- (cont) - Before you talk to Boss Man, you should take a moment to inspect the scene. I doubt you'll find anything useful, but go ahead. Knock yourself out.
<color=\#FFFF00><i>If you ever need a refresher on how your Detective Vision works, open up your <b>Field Manual</b> in the pause menu.</i></color> #speaker:
~ talkToPoliceAgain = true
-> END

=== Continued ===
<>
{ finishedCrimeScene: -> Hub }
{ foundAllClues():
    -> Questions
- else:
    C'mon, you barely scoured the scene. Go back and make sure that you didn't miss anything. #speaker: Police
    Not that I care, but just in case.
    The name's Justin Time, actually. #speaker: Player
    No way that's your actual name. #speaker: Police
    -> END
}

=Questions
So, find anything Detective? #speaker: Police
Did you beat this poor drunk man to death and then rob him, officer? #speaker: Player
Don't be silly. What do you mean? #speaker: Police
You say there's nothing that indicates a murder, but after a brief inspection I can safely say that you're either dumb or full of shit. #speaker: Player
Detective, if you have any questions about what you've found...
- (options)
{ <> | Anything else? | Anything else? | -> finish } #speaker: Police
* [<b>The victim's pockets</b>]
    His pockets were empty, or <i>emptied</i>, rather. Did you find anything on him? #speaker: Player
    Nope. Nothing. #speaker: Police
    You don't find that odd? You usually carry <i>something</i> with you like a wallet, or something at least. Could he have been robbed? #speaker: Player
    He lost his wallet a lot, so that's nothing unusual. Someone would find it lying somewhere, and get it right back to him.
    And unless he won big at the <i>lottery</i> or something, I doubt he had something worth robbing him for. #speaker: Police
    -> options
* [Victim's injuries]
    He has some pretty serious injuries. From a struggle perhaps? Leading to his death? #speaker: Player
    Well, he was no stranger to brawling. Thats' nothing compared to the state I've seen him in before. #speaker: Police
    See that bruise on his cheek? I did that last week after he resisted an arrest. Right in the kisser!
    -> options
* [Snus]
    What do you make of this, officer? #speaker: Player
    <i>You hold the tin of snus up to the officer's face. He takes a long good whiff.</i> #portrait: 4 #speaker:
    Fyfan, bort med den där! Det luktar skit! #speaker: Police #portrait: 
    Doesn't it smell weird, though? I recognize this smell from somewhere, or <i>something.</i> #speaker: Player
    It all smells the same to me. #speaker: Police
    -> options
* -> finish
- (finish) - Look, detective; I know that you think this is some murder-mystery to solve, but there's really nothing to it; just take Boss Man's statement and call it a day.
If you still feel the need to investigate more thoroughly, you can always come back to me and I'll help point you in the right direction.
~ finishedCrimeScene = true
-> END

=== Hub ===
#speaker: Police
{ confirmedSolution: -> FinalScene }
{ isTalkingToPolice: <>Detective, what did you tell Boss Man exactly? Please straighten this out. -> END }
What's up, doc? #speaker: Police
* [Where should I go next?]
    { not talkedToBossMan: You can start with getting Boss Man's statement, as we've already discussed. -> END }
    { not talkedToBartender: Still not satisfied with Boss Man's statement, huh? You could check out the bar across the street. Maybe someone's heard or seen something. -> END }
    { not talkedToCashier: You're still here? Go home, detective. We have everything we need. If you still intend to stay, go to the gas station and get yourself a coffee or something at least. Maybe you could call a tow truck... -> END }
    -> GetLead
+ {LIST_COUNT(Suspects) > 1 } [I have a suspect.]
    Oh? #speaker: Police
    ** { Suspects ? bossMan } [Boss Man.] -> AboutBossMan
    ** { Suspects ? bartender } [Bartender.] -> AboutBartender
    ** { Suspects ? storeClerk } [Gas Station Clerk.] -> AboutStoreClerk
    ** -> END


= GetLead
I need to solve this case immediately. And I need suspects to solve the case. I can't solve a case if I don't have a suspect. Please, officer! I need help! Anything! #speaker: Player
Jesus, alright, relax man... I'll humour you, ok? #speaker: Police #anim: Shake
{ not findWallet and not trisslottLead and knowledge !? victimPoisoned: -> poisonLead }
{ not poisonLead and not trisslottLead and Clues !? victimWallet: -> findWallet } 
{ not poisonLead and not findWallet and Clues !? trisslott: -> trisslottLead }
* -> noMoreLeads 
-(poisonLead)
I'm sure he died because he was an alcoholic. But, if I were to put myself into your shoes, that's not a satisfying deduction. Maybe there's an alternative explanation to his death? #speaker: Police #anim: Talking
    -> end 
-(findWallet)
You could try to track down his wallet somehow. <i>Assuming</i> he was robbed, then whoever robbed him should have his wallet. You just gotta find out who. Easy. #speaker: Police #anim: Talking
    -> end 
-(trisslottLead)
I see you found his wallet, but it's not exactly worth robbing him for. He could have had something else entirely that was the reason for robbing him... Someone has to know something about it. #speaker: Police #anim: Talking 
    -> end 
-(noMoreLeads)
I'd say that you have everything to find your imagined suspect at this point. If you still don't, then that just means you have some more leg work to do. #speaker: Police #anim: Talking
    -> end
-(end)
Retrace your steps, detective. Maybe something has showed up that you missed. Talk to people, look around. -> END

= SearchTrash
This case proved harder than I thought. I haven't found any more clues. #speaker: Player
That's a shame, detective. Maybe you'll find some <i>important</i> clues if you go around rummaging through any trash you find, Ha-Ha! #speaker: Police
<i>That's... not a bad idea. I love trash.</i> #speaker: Player
-> END

= AboutBossMan
First off, he has the victim's wallet. #speaker: Player
No kidding? #speaker: Police
Second, he claims to have found it <b>before</b> a purchase was made at his shop, made using the victim's card. #speaker: Police
Interesting... #speaker: Player
Third, and most important, he was the one who found the body and called it in. It could have been a ruse to make himself look less suspicious. #speaker: Player
As a last stand, he claimed to have only found the body of Peter which he believed to be unconscious, and took the wallet as some sort of "payback", before realizing he was actually dead and subsequently calling the police. #speaker: Player
Hm, I don't quite buy it. Boss Man knew that Peter was dirt poor. Killing him for some pocket change is idiotic, to say the least. #speaker: Police
{ knowledge ? knowAboutTrisslott:
    However, if Boss Man also had knowledge of Peter's trisslott, then that changes things quite a lot. He would absolutely have a reason for killing him, and an even bigger reason for digging himself deeper into these lies.
- else:
{ LIST_COUNT(Suspects) > 2: Him having his wallet is good enough for me. I do have other suspects in mind, though... -> Hub } #speaker: Player
{ LIST_COUNT(Suspects) > 1: Him having his wallet is good enough for me, and I don't have any other suspects... Can't we just take him away, and the case is closed? } #speaker: Player
}
Well, if that's what you think... You should have all the clues you need to connect them to him as a suspect. If you decide to go for him, come back to me after you've connected his clues on your dainty little clueboard. #speaker: Detective
-> Hub

= AboutStoreClerk
First, him and the victim had been overheard arguing over something. This something was a trisslott. A winning one, at that. #speaker: Player
The store clerk felt slighted at this, insinuating that Peter didn't deserve to win. That escalated into a brawl between the two, and Peter left the store shortly after.
Second, there is a witness that can attest to the store clerk not being at the store around the time of the victim's death, around 5 minutes or so. Though not definitive proof, there is reasonable doubt.
Hardly enough time to kill a person with only 5 minutes to spare. #speaker: Police
Not at all. However, considering that he may have been poisoned brings me to my third and final point: He laced Peter's snus with kylvätska, and he had been slowly poisoned throughout the night. #speaker: Player
When he surmised that enough time had passed that Peter should have died, he left his post and went to look for him.
Normally he would have had an airtight alibi, since no one would question that he left the store. Sadly for him, there was a customer there that noticed that he was missing for those few minutes.
To summarize, in order to get Peter's trisslott, he poisoned his snus using the coolant in his storage room.
Well, if that's what you think... You should have all the clues you need to connect them to him as a suspect. If you decide to go for him, come back to me after you've connected his clues on your dainty little clueboard. #speaker: Detective
-> Hub

= AboutBartender
First, he lied about his alibi. He claims that Peter and two other patreons were all together at his bar last night until closing. #speaker: Player
However, these other two patreons contradict this alibi. In reality, they both left together some hours before closing, leaving only the bartender alone with Peter.
You don't say... #speaker: Police
Second, during this time, he had ample opportunity to kill him. #speaker: Player
How so? He was found dead right here. He would have killed him at the bar if that was the case. #speaker: Police
He poisoned him. The bar suffers from a rat infestation, and the bartender has rat poison strewn about all over the place. And it seems to have found its way into his beer somehow... #speaker: Player
Third, there's the issue of the trisslott. I talked with two of Peter's aquaintances, and they both attest to him having won, big time. The bartender would also have known about it, but conviniently neglected to mention it.
To sum up: When the bartender found out about the winning trisslott and the other patreons left him alone with Peter, he saw his opportunity by poisoning his beer with rat poison.
Well, if that's what you think... You should have all the clues you need to connect them to him as a suspect. If you decide to go for him, come back to me after you've connected his clues on your dainty little clueboard. #speaker: Detective
-> Hub
    
=== FinalScene ===
So, you found yourself a suspect huh? I still think you're onto nothing here, but I'll go ahead and talk to them and see whether it leads anywhere. #speaker: Police
Go ahead and wait for me at the station. I'll come back and brief you about what I found out.
~ goToPoliceHouse = true
-> END
    





