INCLUDE globalsmainstory.INK
VAR exhaustedOptions = false
{ Suspects ? bossMan: <> Boss Man is currently under suspicion. -> END } #speaker: Player
{ talkedToBossMan: <>-> CanQuestion|-> Intro }
=== Intro ===
Nice driving, asshole! #anim: Yelling #speaker: Boss Man
    //{ talkToPolice: -> CanQuestion.Question } 
    * {debug} [LÅS UPP SOM SUSPECT]
        ~ addsuspect(bossMan)
        ~ unlockSuspect(bossManID)
        -> Intro
    * [Thanks.]
        Yeah, real nice... Business is already bad as it is, a totaled car right behind the shop is going to do wonders for the customers. #anim: Angry #speaker: Boss Man
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
        That wasn't so hard, now was it? Now get lost, I don't wanna see your face again.
        ~ talkedToBossMan = true
        { finishedCrimeScene: 
            Actually... I kinda need to take your statement. I'm here on behalf of Peter. #speaker: Player
            -> StartQuestion("For fuck's sake.")
        - else:
          -> DONE  
            } 

= Apologize // loopar igenom tills spelaren ber om ursäkt
{ &You still haven't. | Still waiting. | What was that? }
* [Ok, I'm sorry.]
    -> IntroCont
+ [{&Cool. | Right on. | Yeah. | You know...}]
    -> Apologize

=== CanQuestion
{ isTalkingToPolice: -> StartQuestion.TalkingToPolice }
What's going on, big guy? Crash any cars lately? #speaker: Boss Man #anim: Talking
{ finishedCrimeScene: -> Question | I don't have time for this bozo, I've got a crime scene to investigate. -> END }

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
{ Call and Relation: -> DeadEnd }
* { canDistract and Clues !? victimWallet } [<b>Distract him</b>] -> Police 
* { knowledge ? receiptsBelongToVictim and Clues !? victimWallet } [<color=\#FFFF00>The victim came here last night.</color>] -> InquireAboutReceipt
* { Clues ? victimWallet and knowledge !? stoleWallet } [<color=\#FFFF00>About this wallet...</color>] -> Wallet
* { Suspects !? bossMan } [Did you make the call?] -> Call // Endast om Boss Man inte är en suspect 
* { not canDistract & Suspects !? bossMan } [Did you know the victim?] -> Relation
* { not canDistract & Suspects ? bossMan } [What happened last night?] -> LastNight
// * { Suspects ? bartender or Suspects ? storeClerk } [Ask about other suspects] -> AskAboutSuspects
* [Bye loser.] -> END

= Police
The officer needs to take your statement. Again... #speaker: Player
Are you kidding me? What more can I say that I haven't already told you people? How incompetent are you!? #speaker: Boss Man
You don't really have much of a choice. He's expecting you by the scene, so if I were you I would get going. #speaker: Player
~ startMovement("Walking")
Unbelievable... This is what my skattepengar goes to. #speaker: Boss Man
<i>Beautiful, he's left the shop unattended. Now I can have a little look around...</i> #speaker: Player
~ isTalkingToPolice = true
-> END

= TalkingToPolice
What is this? You told me that the officer had to take my statement. #speaker: Boss Man #anim: Talking
Yeah. What's the problem? #speaker: Player
Detective, I said that <b>you</b> were supposed to take his statement. #speaker: Police
Oh. Sorry! Let me do that now real quick. #speaker: Player
~ startMovement("Walking")
You already took my statement! Fuck this. Don't waste my time again. #speaker: Boss Man
~ isTalkingToPolice = false
-> END

= InquireAboutReceipt
Huh? #speaker: Boss Man
I have proof that he came here. I have a receipt from this shop that matches his card number. #speaker: Player #portrait: 0
Oh. Well, that's nothing. #speaker: Boss Man #portrait: 
Huh? #speaker: Player 
Did you know he constantly lost his wallet? Someone must have found it and treated themself to one of my famous rolls. It's not the first time it's happened, or the last. #speaker: Boss Man
Though, I guess this actually <b>would</b> be the last time... #anim: Shake
I guess that makes sense... The officer told me he would lose it a lot. Even the bartender said so. #speaker: Player
To weasel out of paying his tab, no doubt! #speaker: Boss Man
Look, he didn't come here, alright? But I guess I could keep an eye out in case it shows up again.
-> StartQuestion("Anything else?")
-> END

= DeadEnd
<i>He knows more than he lets on. Call it a hunch, but I've seen this type of guy before...</i> #speaker:
<i>Som på Bollen i Rullen 25 på Nobeltorget.</i>
<i>Maybe he really doesn't know anything. Or I could take a little look around his shop... But I need to find a way to distract him somehow.</i>
~ canDistract = true
-> END

= AskAboutSuspects
* { Suspects ? bartender } [About the bartender...]
* { Suspects ? storeClerk } [About the store clerk...]
- -> StartQuestion("Här tar det stopp! Går tillbaka till hubben")
* [Ask about something else] -> StartQuestion("Hm.")

= Relation
Sure, if being pestered by an alcoholic in nearly every aspect of my life counts as knowing him. #speaker: Boss Man
I'd say that counts. #speaker: Player
Alright, he wasn't all bad. He was a paying customer, which isn't something that most people would agree with me on. #speaker: Boss Man
What do you mean? #speaker: Player
He goes to the bar pretty much every day, drinking his life away. He's racked up a pretty sizeable tab I've heard, which won't ever get paid off at this point. #speaker: Boss Man
The bartender is too good on him. Peter knew that shit wouldn't fly with me though, but the bartender needs all the business he can get. Even if that business is putting him deeper in debt.
I see. I'll make sure to talk to him as well, but right now I'm more interested in your relationship to the victim. #speaker: Player
Sorry, I can't tell you anything that I haven't already said. <>
-> StartQuestion("Anything else?")

= Wallet
Did you steal that from my shop? #speaker: Boss Man #anim: Angry
It's not stealing if it's not yours, is it? #speaker: Player
I don't like what you're implying. Look, I know it belonged to Peter. He would lose his wallet <i>constantly</i>. I find it, hold on to it, he comes back the next day and asks if I found it. Rinse and repeat. #speaker: Boss Man #anim: Angry
It was like my other full time job, except I never got anything in the form of gratitude. He would just expect me to have it, and we would keep having this little back and forth.
Only this time he never came back... #anim: Shake
When did you find this wallet, exactly? #speaker: Player
Let's see... I must have found it sometime before 2 A.M. He probably dropped it after he left the bar. #speaker: Boss Man
And you're sure that he never came here at any point last night? #speaker: Player
Like I said. Are we done here? #speaker: Boss Man
{ knowledge ? receiptsBelongToVictim: 
    -> WalletCont
- else:
    <i>He's only telling half the truth. You can't press this issue further though, you have to find something more tangible and come back to this.</i> #speaker:
    -> StartQuestion("Anything else?")
    }
-> END

= WalletCont
<i>The receipts you found in the trash earlier contradict this statement. They clearly show that a purchase was made with his card after the time Boss Man supposedly found the wallet.</i> #portrait: 0
That doesn't add up. Take a look at this receipt here. #portrait:
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
-> LastNight

= LastNight
It's true, I found his body and decided to take his wallet. I was just gonna grab some cash and then give it back to him like normal the next day. #speaker: Boss Man
Just like I told you before, I soon realized that he was actually dead and so I called the cops.
Then I realized that I still had his wallet, which wasn't a good look for me. I couldn't admit something like that, that would make me look guilty! So I never mentioned it. 
He died <i>shortly</i> after leaving your shop. You know how it looks, right? #speaker: Player
I didn't do it, please believe me! #speaker: Boss Man
Why should I? You lied to me before, you might still be lying. He had injuries indicative of a struggle. You could have knocked him the fuck out, and it happened to kill him. Let's just say it was manslaughter and call it a day, huh? Heat of the moment! #speaker: Player
I overheard you speaking with the Officer earlier, and he clearly stated that the victim couldn't have died from that! That means I couldn't have done it, yeah? #speaker: Boss Man
{ knowledge ? victimPoisoned:
    It's very likely that the victim could have been poisoned, and this falafelrulle right here could be the culprit. #portrait: 7 #speaker: Player
    That's bullshit, that just means that you don't have any real proof that it's been poisoned! #speaker: Boss Man
    Eat it. #speaker: Player
    Beat it. #speaker: Boss Man
    No, I mean <b>eat it</b>. This falafelrulle. If it's not poisoned, it should be fine to eat, right? #speaker: Player #portrait: 7
    I'm... I'm not eating that. I don't know how old that thing is, and you could have put something in it yourself. I have no reason to trust you! #speaker: Boss Man
    And I no longer have any reason to trust you. I have everything I need here. #speaker: Player
    Wait! I have information! I can tell you about something Peter told me last night. #speaker: Boss Man
    * [<b>Go on...<b>]
        -> StoreClerkFight
    * [<b>Not interested.</b>]
        Not interested. Like I said, I have everything I need here.
        -> UnlockSuspect
- else:
    <i>I guess that makes sense. I don't think I have anything to prove an alternative theory.</i> #speaker: Player
    <i>Still, I know I'm onto something here... I'm just missing a small piece. I could ask around and see if I find out something interesting.</i>
-> END
}

= StoreClerkFight
Alright, let's hear it. #speaker: Player
When he came here last night and bought his nightly falafelrulle, he told me that he and the cashier down by the gas station had an argument about something. #speaker: Boss Man
So what? That doesn't mean anything. I can imagine that a lot of people had a bone to pick with Peter. #speaker: Player
Yeah, yeah; but this was something else. He would always tell me whenever he got into trouble with the police, or if he was kicked out of the bar because of his shitty karaoke, and so on. #speaker: Boss Man
But last night... He seemed off. When I asked him about what he and the cashier were arguing about, he got pissed and started yelling at me. So I didn't press him any further, and then he left. And you know the rest.
<i>I could choose to follow up on this lead, and even if it leads nowhere; I still have my prime suspect right here.</i> #speaker: Player
Alright. I'll see where this goes, but don't expect to be in the clear because of this.
-> UnlockSuspect

= UnlockSuspect
~ addsuspect(bossMan)
~ unlockSuspect(bossManID)
<i>You have unlocked Boss Man as a suspect. You can speak with The Officer regarding this or consult your Field Manual for further information.</i>
-> END

= Call
Didn't you already take my statement about this? #speaker: Boss Man
The officer did, yes. I would like to hear it as well. Spare me no details, capische? #speaker: Player
Fine, the quicker the better. #speaker: Boss Man
Last night, at maybe around 2 A.M, I closed up shop and headed home.
That's when I stumbled upon him. I though for sure that he had just passed out from another night of drinking.
But... he wasn't breathing. I tried to wake him up, but I got no response.
I realized he wasn't just black out drunk, and quickly called the cops. They arrived almost immediately, I didn't even have time to...
...you know, do anything else. I mean, I didn't know what to do really.
He didn't come by here last night then? I was told he was a regular here, to a fault almost. #speaker: Player
-> StartQuestion("No, he didn't. That's all I have to say about that.")
