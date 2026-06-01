INCLUDE globalsmainstory.INK

// local variable to track if he is snoring
VAR isAsleep = true

// check if we met him before
{ talkedToArmchairGuy: -> Hub | -> Intro }
=== Intro ===
The man is slumped deep in the armchair, snoring loudly. He hasn't even taken a single bite from his princesstårta before he fell asleep.
    * { finishedCrimeScene } [Wake him up.]
        Hey. Wake up. #speaker: Player
        Huh?! I'm awake! I wasn't sleeping, I was just... Okay, you got me I was sleeping. #speaker: Torsten #anim: Shake #snore: stop
        ~ talkedToArmchairGuy = true
        ~ isAsleep = false
        -> Hub
    * [Leave him be.]
        -> DONE

=== Hub ===
{ isAsleep: -> SleepingHub | -> AwakeHub }
=== SleepingHub ===
    + [Wake him up.]
        Hey! #speaker: Player
        Sorry. #speaker: Torsten #anim: Shake #snore: stop
        ~ isAsleep = false
        { LastNight > 1: <i>I'm never getting anything out of the guy like this... Maybe down the line he will provide me with some information to blow this whole case wide open. But that time is not now.</i> } #speaker:
        <>-> Hub
    + [Leave him be.]
        -> DONE

=== AwakeHub ===
{ talkedToBartender and talkedToCashier and Clues ? victimWallet and Clues !? trisslott: -> TrissLott }
    * { Clues ? trisslott } [About that trisslott...] -> TrissLottAgain
    * { items !? karaokeUSB } [Do you know Peter Grip?] -> KnowPeter
    * { KnowPeter } [I need that recording of Peter.] -> TheTape
    + [See anything suspicious last night?] -> LastNight
    + [I gots to go, baby.] -> AwakeExit
    * { knowledge ? bartenderAlibi } [Bartender's alibi] -> BartenderAlibi

= TrissLottAgain
Torsten, I need to know more about that trisslott. What exactly did Peter tell you? #speaker: Player
~ isAsleep = true
<i>You guessed it, he fell back asleep again.</i> #snore: start speaker: 
I figured... #speaker: Player
-> Hub

= BartenderAlibi
Leif told me that you and he left around the same time last night, is this true? #speaker: Player
<i>He falls back asleep.</i> #snore: start #speaker:
HEY! <i>You snap your fingers in his face.</i> #speaker: Player
Yeah! Yeah, sorry, that's right. I decided to go home and catch up on some Z's, and left with loiter guy. #speaker: Torsten #snore: stop
The only person left here was Peter. I wonder where he is now? He's usually here by this hour. 
<i>That confirms it. The bartender lied about his alibi.</i>
-> END

= TrissLott
Hang on... Is that <i>Peter's wallet?</i> Did you rob him or something?! #speaker: Torsten #portrait: 2
No, no, it's nothing like that. I just found it... somewhere. #speaker: Player #portrait: 
Likely story, pal! He'd never drop his wallet. Do you mistake him for a common drunk? You probably heard about his immense wealth and decided to rob him blind! #speaker: Torsten
Peter was rich? I certainly didn't get that impression. You're not pulling my leg here, right? #speaker: Player
He will be, once he cashes in his trisslott! You probably assumed that he would keep it in his wallet, you filthy robber. #speaker: Torsten
Slow down, I'm not following you. Are you saying Peter had a winning trisslott on him? #speaker: Player
Oh yeah, buddy. It's pretty much all he talked about last night. He was declaring how he'll soon be the richest man in town. I told him that it's probably a bad idea to go around telling everyone that. #speaker: Torsten
But I don't have to tell you that since you already knew all about it, you filthy robber... What did you do to him!? I have half a mind to call the cops on you.
<i>No one I've talked to has mentioned this at all... Where's this coming from?</i>  #speaker: Player
Please, start from the beginning. What's going on here? What trisslott?
~ isAsleep = true
<i>He falls back to sleep again.</i> #snore: start #speaker: 
Hey, you have to tell me more about this! #speaker: Player
<I>...No good, he's fast asleep. I think I've gotten all the information I can about this.</i>
<i>I should keep this in mind though. If this is true, then it casts a bit of doubt on many of the people I've talked to...</i>
~ getclue(trisslott)
~ addClueThroughDialogue()
-> Hub

=== KnowPeter ===
Do you know a guy named Peter Grip? #speaker: Player
Do I ever! He's here every night, singing his heart out on the karaoke machine. #speaker: Torsten
But between you and me? His singing is ass. #speaker: Torsten
{ talkedToBartender:
The bartender mentioned he liked ABBA. #speaker: Player
Liked is an understatement. <>
- else:
Oh yeah? What would he sing? #speaker: Player
<i>ABBA's Greatest Hits.</i> No exceptions. <>
}
I actually recorded him on my phone last night to prove to my wife why I have to drink. It would be funny if it wasn't so tragic. #speaker: Torsten
-> Hub

=== TheTape ===
You recorded him last night? I need that video. It could be evidence. #speaker: Player
Evidence of what? You're talking like he's been involved in something. Look, I already transferred it to a USB stick. You can have it. #speaker: Torsten
<i> Where is he, anyway? He's usually here at this hour...</i>
~ getitem(karaokeUSB)
-> Hub

=== LastNight ===
Did you see anything suspicious last night? #speaker: Player
<i>He immediately goes back to sleep.</i> #speaker: #snore: start
~ isAsleep = true
{ cashierToldHisAlibi: -> StoreClerkAlibi }
<>-> Hub

= StoreClerkAlibi
<b>WAKE THE FUCK UP!</b> #speaker: Player
~ isAsleep = false
Why are you shouting? I'm awake. #snore: stop #speaker: Torsten
<i>I have his attention. I need to ask him something before he falls asleep again.<i>
I wanted to ask you about the store clerk by the gas station. Do you know where he was last night? #speaker: Player
Uh, he's still on the same shift now as he was last night I think. Poor guy probably still has a few more hours to go. He looked beat when I went there last night at around 2AM. #speaker: Torsten
You were there last night? Do you know if he ever left his post?
<i>He falls back asleep.</i> #speaker: #snore: start
<i><b>NOT!</b> Got you. Alright, back to the story.</i> #snore: stop
Well, I don't know if he left... but he came out of the storage after I'd been waiting at the counter for a few minutes. #speaker: Torsten
~ gainknowledge(cashierAlibi)
~ askedQuestion = false
<i>So there's at least a few minutes where he can't be placed at his post... I should confront him about this.</i>
-> Hub

=== AwakeExit ===
I'll leave you to it, then. #speaker: Player
Ha det bäst mannen. #speaker: Torsten
-> END