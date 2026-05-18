INCLUDE globalsmainstory.INK
VAR exhaustedOptions = false
{ Suspects ? bossMan: -> StartQuestion("Are you going to arrest me...?") }
{ talkedToBossMan: <>-> CanQuestion|-> Intro }
#speaker: Boss Man
=== Intro ===
Nice driving, asshole! #anim: Yelling
    { talkToPolice: -> CanQuestion.Question } 
    * {debug} [LÅS UPP SOM SUSPECT]
        ~ addsuspect(bossMan)
        ~ unlockSuspect(bossManID)
        -> Intro
    * [Thanks.]
        Yeah, real nice... Business is already bad as it is, a totaled car right behind the shop is going to do wonders for the customers. #anim: Angry
        -> IntroCont
    * [That wasn't me, it was the other guy.]
        Oh, the other guy. Of course. The one standing here talking to me right now about the other guy. THAT guy. Him. #anim: Shake
        ** [That's him.]
            Alright. Enough of this bit, it was <b>obviously</b> you. <>
                    -> IntroCont
        ** [Alright, I did it...]
            Really? I never would've guessed. I could've sworn you said it was the other guy! 
            -> IntroCont
    * [I gots to go, baby.]
        Go fucking where? Your car is impaled through that tree!
        -> DONE
    

= IntroCont
{!So, what are we gonna do about it? | You're sorry for what? }
    * [I said I was sorry.]
        You didn't, actually. 
        ** [Yeah.]
            -> Apologize
    * [{I'm sorry. | For crashing my car behind your shop epic style.}]
        That's all I wanted. I guess the car is kind of a selling point, now that I think about it. 
        People will be intrigued by the commotion, and flock towards my shop... Yeah...
        ~ talkedToBossMan = true
        -> DONE

= Apologize // loopar igenom tills spelaren ber om ursäkt
{ &You still haven't. | Still waiting. | What was that? }
* [Ok, I'm sorry.]
    -> IntroCont
+ [{&Cool. | Right on. | Yeah. | You know...}]
    -> Apologize

=== CanQuestion
How's it going?
    { talkToPolice: -> Question | Don't I have something better to do? Like, inspect a body? -> END } 


= Question
* [I've got some questions for you.]
    -> StartQuestion("What about?") 
* [I gots to go now, baby.]
    -> END

// This is the main hub for interrogating the suspect - More options
// will become available as you talk to people and find more clues
=== StartQuestion(msg) ===
~ talkedToBossMan = true
{msg} #speaker: Boss Man #anim: Talking
* [Start walking, buddy.]
    Ok :)
    ~ startMovement("Walking")
    -> END
* { knowledge ? receiptsBelongToVictim and Clues !? victimWallet } [The victim came here last night.] -> InquireAboutReceipt
* { Clues ? victimWallet and knowledge !? stoleWallet } [About this wallet...] -> Wallet
* { Suspects !? bossMan } [Did you make the call?] -> Call // Endast om Boss Man inte är en suspect 
* { Suspects !? bossMan } [Did you know the victim?] -> Relation
* { Suspects ? bossMan } [More about last night] -> LastNight
* { Suspects ? bartender or Suspects ? storeClerk } [Ask about other suspects] -> AskAboutSuspects
* -> DeadEnd

= InquireAboutReceipt
Huh? #speaker: Boss Man
I have proof that he came here. I have a receipt from this shop that matches his card number. #speaker: Player #portrait: 0
Oh. Well, that's nothing. #speaker: Boss Man
Huh? #speaker: Player portrait: 
Did you know he constantly lost his wallet? Someone must have found it and treated themself to one of my famous rolls. It's not the first time it's happened, or the last. #speaker: Boss Man
Though, I guess this actually <b>would</b> be the last time... #anim: Shake
I guess that makes sense... The bartender did tell me he often lost it. #speaker: Player
To weasel out of paying his tab, no doubt! #speaker: Boss Man
Look, he didn't come here, alright? But I guess I could keep an eye out in case it shows up again.
-> StartQuestion("Anything else?")
-> END

= DeadEnd
{ Suspects ? bossMan:
    Suppose this leads nowhere, I'm bringing you in. I'll have the officer keep an eye on you. #speaker: Player
    -> END
- else:
    He knows more than he lets on. Call it a hunch, but I've seen this type of guy before... #speaker:
    Som på Bollen i Rullen 25 på Nobeltorget.
    Maybe he really doesn't know anything. Or I could take a little look around his shop... But I need to distract him somehow. 
}
-> END

= AskAboutSuspects
* { Suspects ? bartender } [About the bartender...]
* { Suspects ? storeClerk } [About the store clerk...]
- -> StartQuestion("Här tar det stopp! Går tillbaka till hubben")
* [Ask about something else] -> StartQuestion("Hm.")

= Relation
Sure, if being pestered by an alcoholic in nearly every aspect of my life counts as knowing him. #speaker: Boss Man
I'd say that counts. #speaker: Player
Alright, he wasn't all bad. He was a paying customer, which not all people would say about him. #speaker: Boss Man
What do you mean? #speaker: Player
He goes to the bar pretty much every day, drinking his life away. He's racked up a pretty sizeable tab I've heard, which won't ever get paid off at this point. #speaker: Boss Man
The bartender is too good on him. Peter knew that shit wouldn't fly with me though, but the bartender needs all the business he can get. Even if that business is putting him deeper in debt.
I see. I'll make sure to talk to him as well, but right now I'm interested in your relationship to the victim. #speaker: Player
Can't tell you much else. He came, he paid, he died. <>
-> StartQuestion("Anything else?")

= Wallet
Did you steal that from my shop? #speaker: Boss Man #anim: Angry
It's not stealing if it's not yours, is it? #speaker: Player
I don't like what you're implying. Look, I know it belonged to Peter. He would lose his wallet <i>constantly</i>. I find it, hold on to it, he comes back the next day and asks if I found it. Rinse and repeat. #speaker: Boss Man #anim: Angry
It was like my other full time job, except I never got anything in the form of gratitude. He would just expect me to have it, and that we would keep having this back and forth.
Only this time he never came back...
When did you find this wallet, exactly? #speaker: Player
    Let's see... I must have found it sometime before 2 A.M. He probably dropped it after he left the bar. #speaker: Boss Man
{ knowledge ? receiptsBelongToVictim: 
    -> WalletCont
- else:
    He's only telling half the truth. You can't press this issue further though, you have to find something more tangible and come back to this. #speaker:
    -> StartQuestion("Anything else?")
    }
-> END

= WalletCont
<i>The receipts you found in the trash earlier contradict this statement. They clearly show that a purchase was made with his card after the time Boss Man supposedly found the wallet.</i> #portrait: 0
And you're sure that he never came here at any point last night? #speaker: Player #portrait:
Like I said. Are we done here? #speaker: Boss Man
That doesn't add up. Take a look at this receipt here.
You see Boss Man's eyes glance across the receipt, while he's trying to understand where you're going with this. #speaker:
I'd recognize a receipt from my shop, obviously. What's the angle here? #speaker: Boss Man
Well, guess who this receipt belonged to? #speaker: Player
His eyes stop and dart up from the receipt to look back at you. #speaker: 
That's right bitch boy... It's clear as day that Peter was here last night, after 2 A.M and shortly before his estimated time of death, buying his nightly falafel. #speaker: Player
Ok, wait a minute here, hold on... I just <i>forgot</i> he came here. It was a busy night, and I was tired, yeah? #speaker: Boss Man
Bullshit artist. Even if that were true, that would mean he still had his wallet on him. And now it's here, with you instead... See where I'm going with this? #speaker: Player
~ gainknowledge(stoleWallet)
He came here last night, bought a falafel, and as he walked away, you went after. Killed him, took his wallet. Bam.
No, you're wrong! I didn't kill the poor guy! Let me explain...
What's there to explain? You have his wallet, the only way you could have it is if you took it from his body.
Like you took his wallet, I'm taking you... in, for murder.
No, wait! I <i>DID</i> take his wallet after I found his body, yeah? But I didn't <i>KILL HIM!</i>
~ addsuspect(bossMan)
~ unlockSuspect(bossManID)
-> StartQuestion("Please, let me explain...")

= LastNight
You'd better start making damn good sense. #speaker: Player
It's true, I found his body and decided to take his wallet. I figured that he owed me for all the times we had our dumb little back and forth. I was just gonna grab some cash and then give it back to him like normal the next day.
Only then did I notice that he wasn't breathing. I tried to shake him awake, slap him around a bit you know?
And, nothing. I immediately called the police, and told them that it's about Peter, but there was something <b>wrong</b> this time and that they really had to hurry.
Then I realized that I still had his wallet, which wasn't a good look for me. I couldn't admit something like that, that would make me look guilty! So I never mentioned it. 
I wasn't lying when I said that I would often find his wallet. It just wasn't the truth this time. I figured if anyone would confront me about it, I could just use it as a fallback.
He died <i>shortly</i> after leaving your shop. You know how it looks, right? #speaker: Player
Yeah, but that's just why I <b>COULDN'T</b> have killed him! I heard you talking to the officer over there earlier, he said he died of alcohol poisoning or something, right? #speaker: Boss Man
Probably. He had injuries indicative of a struggle, though. You could have knocked him the fuck out, and it happened to kill him. Let's just say it was manslaughter and call it a day, huh? Heat of the moment! #speaker: Player
I didn't <b>do it!</b> As for his injuries, I can explain. He told me as much about them last night. #speaker: Boss Man
He had a falling out with the clerk by the gas station, said they fought over something.
Something? Like what? #speaker: Player
He didn't say. I could tell that he didn't want to talk about it, either.
Please, I'm telling you... If he really was murdered, I suppose I can't prove my innocence, but the store clerk sure as hell knows something about all this.
// ~ addsuspect(storeClerk)
-> DeadEnd

= Call
Didn't you already take my statement about this? #speaker: Boss Man
The officer did, yes. I would like to hear it as well. Spare me no details, capische? #speaker: Player
Fine, the quicker the better. #speaker: Boss Man
Last night, at maybe around 2 A.M, I closed up shop and was ready to head home. I was walking along my usual path.
And that's when I stumbled upon him. I though for sure that he had just passed out from another night of drinking, but..
He wasn't breathing. I tried to wake him up, but his body was absolutely limp, it was scary.
I had a hunch that it might not just be another blackout, and I quickly called the cops. They arrived almost immediately, I didn't even have time to...
...you know, do anything else. I mean, I didn't know what to do really. They declared him dead instantly.
He didn't come by here last night then? I was told he was a regular here, to a fault almost. #speaker: Player
-> StartQuestion("No, he didn't. That's all I have to say about that.")
