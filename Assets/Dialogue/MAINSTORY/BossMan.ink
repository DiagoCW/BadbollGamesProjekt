INCLUDE globalsmainstory.INK
VAR confused = false
VAR exhaustedOptions = false
{ Suspects ? bossMan: -> StartQuestion("Are you going to arrest me...?") }
{ talkedToBossMan: <>-> CanQuestion|-> Intro }
#speaker: Boss Man
=== Intro ===
{ !Nice driving, asshole! | Like I said... Nice driving, asshole. } #anim: Talking
    { talkToPolice: -> CanQuestion.Question } 
    * [Thanks.]
        Yeah, real nice... Business is already bad as it is, a totaled car right behind the shop is going to do wonders for the customers.
        -> IntroCont
    * [That wasn't me, it was the other guy.]
        Oh, the other guy. Of course. The one standing here talking to me right now about the other guy. THAT guy. Him.
        ** [That's him.]
            I know, that's what I just said. You should fuck off to go find him and have him fix this mess.
            *** [Me, as in...]
                You, as in...
                **** [Him is me?]
                **** [Who is him?]
                **** [Jak się masz?]
                    ---- 
                    ~ confused = true
                    ~ talkedToBossMan = true
                    I don't, I... What is this? What's going on?
                    Just get lost. You're cramping my style.
                    And get rid of that fucking car.
                    -> DONE
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
{confused:
You're weird. You're making this weird. I don't really feel like talking to you. #anim: Shake
    { talkToPolice: -> Question | Forget about this bozo. I think I have a body to inspect somewhere. -> END }
- else:
How's it going?
    { talkToPolice: -> Question | Don't I have something better to do? Like, inspect a body? -> END } 
}

= Question
* [I've got some questions for you.]
    -> StartQuestion("What about?") 
* [I gots to go now, baby.]
    -> END

// This is the main hub for interrogating the suspect - More options
// will become available as you talk to people and find more clues
=== StartQuestion(msg) ===
~ talkedToBossMan = true
{msg} #speaker: Boss Man
* { knowledge ? receiptsBelongToVictim and Clues !? victimWallet } [The victim came here last night.] -> InquireAboutReceipt
* { Clues ? victimWallet and knowledge !? stoleWallet } [About this wallet...] -> Wallet
* { Suspects !? bossMan } [Did you make the call?] -> Call // Endast om Boss Man inte är en suspect 
* { Suspects !? bossMan } [Did you know the victim?] -> Relation
* { Suspects ? bossMan } [More about last night] -> LastNight
* { Suspects ? bartender or Suspects ? storeClerk } [Ask about other suspects] -> AskAboutSuspects
* -> DeadEnd

= InquireAboutReceipt
Huh? #speaker: Boss Man
I have proof that he came here. I have a receipt from this shop that matches his card number. #speaker: Player
Oh. Well, that's nothing out of the ordinary. #speaker: Boss Man
Huh? #speaker: Player
Did you know he constantly lost his wallet? Someone must have found it and treated themself to one of my famous rolls. It's not the first time or the last. #speaker: Boss Man
Though, I guess this actually <b>would</b> be the last time...
I guess that makes sense... The bartender did tell me he often lost it. #speaker: Player
To weasel out of paying his tab, no doubt! #speaker: Boss Man
Look, he didn't come here, alright? But I guess I could keep an eye out in case his card shows up again.
-> StartQuestion("Anything else?")
-> END

= DeadEnd
{ Suspects ? bossMan:
    Suppose this leads nowhere, I'm bringing you in. I'll have the officer keep an eye on you. #speaker: Player
    -> END
- else:
    He knows more than he lets on. Call it a hunch, but I've seen this type of guy before... #speaker:
    Som på Bollen i Rullen 25 på nobeltorget.
    Maybe he really doesn't know anything. Or I could take a little look around his shop... But I need to distract him somehow. 
    Det har uppstått lite svårigheter med NavMeshen så det här blev inte bra! Men han kommer glida iväg nu hur som helst.
    Tanken är att han ska gå iväg så att du kan ta dig in och titta runt lite :)
    ~ runAway()
}
-> END

= AskAboutSuspects
* { Suspects ? bartender } [About the bartender...]
* { Suspects ? storeClerk } [About the store clerk...]
- -> StartQuestion("Här tar det stopp! Går tillbaka till hubben")
* [Ask about something else] -> StartQuestion("Hm.")

= Relation
Sure, if being pestered by an alcoholic in nearly every aspect of my life counts as knowing him. #speaker: Boss Man
I'd say that counts. 
-> StartQuestion("Anything else?")

= Wallet
Did you steal that from my shop? #speaker: Boss Man
It's not stealing if it's not yours, is it? #speaker: Player
I don't like what you're implying. Look, I know it belonged to Peter. He would lose his wallet <i>constantly</i>. I find it, hold on to it, he comes back the next day and asks if I found it. Rinse and repeat. #speaker: Boss Man
It was like my other full time job, except I never got anything in the form of gratitude. He would just expect me to have it, and that we would keep having this back and forth.
Only this time he never came back...
{ knowledge ? receiptsBelongToVictim: 
    -> WalletCont
- else:
    He's only telling half the truth. You can't press this issue further though, you have to find something more tangible and come back to this. #speaker:
    -> StartQuestion("Anything else?")
    }
-> END

= WalletCont
When did you find this wallet, exactly? #speaker: Player
Let's see... I must have found it sometime before 2 A.M. He probably dropped it after he left the bar. 
<i>The receipts you found in the trash earlier contradict this statement. They clearly show that a purchase was made with his card shortly after the time Boss Man supposedly found the wallet.</i> #portrait: 0
And you're sure that he never came here at any point last night? #speaker: Player #portrait:
Like I said. Are we done here? #speaker: Boss Man
That doesn't add up. Take a look at this receipt here.
You see Boss Man's eyes glance across the receipt, while he's trying to understand where you're going with this. #speaker:
I'd recognize a receipt from my shop, obviously. What's the angle here? #speaker: Boss Man
Well, guess who this receipt belonged to? #speaker: Player
His eyes stop and dart up from the receipt to look back at you. #speaker: 
That's right bitch boy... It's clear as day that Peter was here last night, after 2 A.M and shortly before his estimated time of death, buying his nightly falafel.
Ok, wait a minute here, hold on... I just <i>forgot</i> he came here. It was a busy night, and I was tired, yeah?
Bullshit artist. Even if that were true, that would mean he still had his wallet on him. And now it's here, with you instead... See where I'm going with this?
~ gainknowledge(stoleWallet)
He came here last night, bought a falafel, and as he walked away, you went after. Killed him, took his wallet. 
No, you're wrong! I didn't kill the poor guy! Let me explain...
What's there to explain? You have his wallet, the only way you could have it is if you took it from his body.
Actually, this logic is a bit contrived. It's not beyond a reasonable doubt that he could have dropped his wallet right after he bought a falafel. But this needs to be a bit contrived in order to work, so just play along please. #speaker:
I'm taking you in, buster.
No, wait! I <i>DID</i> take his wallet after I found his body, yeah? But I did not <i>KILL HIM!</i>
~ addsuspect(bossMan)
-> StartQuestion("Please let me explain...")

= LastNight
You'd better start making damn good sense. #speaker: Player
It's true, I found his body and decided to take his wallet. I figured that he owed me for all the times we had our dumb little back and forth. I was just gonna grab some cash and then give it back to him like normal the next day.
He died <i>shortly</i> after leaving your shop. You know how it looks, right? #speaker: Player
Yeah, but that's just why I <b>COULDN'T</b> have killed him! I heard you talking to the officer over there earlier, he said he died of alcohol poisoning or something, right? #speaker: Boss Man
Probably. He had injuries indicative of a struggle, though. You could have knocked him the fuck out, and it happened to kill him. Let's just say it was manslaughter and call it a day, huh? Heat of the moment! #speaker: Player
I didn't <b>DO IT.</b> As for his injuries, I can explain. He told me as much about them last night. #speaker: Boss Man
He had a falling out with the clerk by the gas station, said they fought over something.
Something? Like what? #speaker: Player
He didn't say. I could tell that he didn't want to talk about it, either.
Please, I'm telling you... If he really was murdered, I suppose I can't prove my innocense, but the store clerk sure as hell knows something about all this.
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
... you know, do anything else. I mean, I didn't know what to do really. They declared him dead instantly.
It's a weird feeling, going home, finding a body, coming back, and it's still lying there even know.
He didn't come by here before his death? I was told he was a regular here, to a fault almost. #speaker: Player
-> StartQuestion("No, he didn't. That's all I have to say about that.")
