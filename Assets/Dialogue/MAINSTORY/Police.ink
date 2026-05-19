INCLUDE globalsmainstory.INK
{ talkToPolice: <>-> Brief | -> Intro }
=== Intro ===
#speaker: 
Jesus, are you alright?! What happened? #speaker: Police #anim: Talking
{ panicked: -> Panicked }
{ resignedToFate: -> Fate }
{ steppedOnGas: -> Gas }
- (Panicked)
The car started swerving, so I panicked. So what? Big deal, happens to the best of us. #speaker: Player
But you were barely going 20km an hour... #speaker: Police
<> -> introcont
- (Fate)
I left my fate in the hands of God, and he led me right where I needed to be. Praise his name. #speaker: Player
Yes, praise his name indeed... #speaker: Police
<> -> introcont 
- (Gas)
I started flooring it. Once I set my mind to something, I have to do it. #speaker: Player
You really don't have to do everything that comes to mind... #speaker: Police
<> -> introcont
- (introcont) Anyway, what took you so long? I've been waiting for quite a while... #speaker: Police
My car broke down on my way here, but it's alright. I fixed it using my <color=\#FFFF00>Detective Vision.</color> #speaker: Player
Uh... Ok. I don't know what that means. It sounds cool though, I'm happy for you. #speaker: Police #anim: Shake
Stop wasting my time, officer. Let's get on with the briefing. #speaker: Player 
Alright, settle down... Walk with me, it's just over here. #speaker: Police
~ startMovement("Walking")
~ talkToPolice = true
-> END

=== Brief ===
{ talkToPoliceAgain: -> Continued }
Last night, the body of 27-year old Peter Grip was found lying here. The official cause of death is yet to be determined, but knowing him I'd say it's clear as day. #speaker: Police
Why do you say that? Did you know the victim? #speaker: Player
He was a known drunk. I've dealt with him before on many occassions when he's had a few too many at the bar, I tended to run into him quite frequently. #speaker: Police
No witnesses have come forward to share any information either. We don't have much to go on, but I think it's pretty evident what happened. The drunk did drank too much. Case solved. 
    -(opts)
    * [<b>Potential murder?</b>]
        Absolutely not. We really have no reason to suspect or classify it as such. This is a cut and dry case. #speaker: Police
        We tested his blood alcohol level, and it was surprisingly high even for a guy like him. Seems like his liver gave up, kidneys shut down... And the rest is history.
        -> opts
    * [Who found the body?]
        As you said, somebody found the body. That someone must have phoned it in? #speaker: Player
        They did. <i>Boss Man.</i> #speaker: Police
        ** { talkedToBossMan } [I crashed my car behind his shop.]
            Then I take it that you've already introduced yourselves. I hope you apologized to him, I heard him shouting at you when you arrived... #speaker: Police
            Don't you worry, we squashed the beef. <>
        ** { not talkedToBossMan } [Boss Man?]
            The falafel guy, he has his shop right behind where you totaled your car. #speaker: Police
            It's a wonder that you didn't drive it straight into his shop... You should probably apologize to him later.
        -- Did you get an initial statement from him? #speaker: Player
        Only that he found the body after closing up shop. Though I figured that you could take care of the rest. #speaker: Police
        Anything suspicious about him? #speaker: Player
        Other than that he found a dead drunk lying here? Not a pretty sight, but someone had to stumble upon him sooner or later. #speaker: Police
        I know that Peter was at his shop basically every day, you should ask if he came by yesterday.
            -> opts
    * -> cont
- (cont) - Detective, you should take a moment to inspect the scene before you go talk with Boss Man. I doubt you'll find anything useful, but go ahead and knock yourself out.
//~ startMovement("Walking")
<color=\#FFFF00><i>If you ever need a refresher on how your Detective Vision works, open up your <b>Field Manual</b> in the main menu.</i></color> #speaker:
~ talkToPoliceAgain = true
-> END

=== Continued ===
{ finishedCrimeScene: -> Hub }
{ foundAllClues():
    -> Questions
- else:
    C'mon, you barely scoured the scene. Go back and make sure that you didn't miss anything. #speaker: Police
    Not that I care, but just in case.
    The name's Justin Time, actually. #speaker: Player
    -> END
}

=Questions
Did you find anything of note, detective? #speaker: Police
Did you beat this poor drunk man to death and then rob him, officer? #speaker: Player
Don't be silly. What do you mean? #speaker: Police
You say there's nothing that indicates a murder, or even a robbery gone wrong, but after a brief inspection I can safely say that you're either dumb or full of shit. #speaker: Player
Detective, if you have any questions about what you've found...
- (options)
{ <> | Anything else? | Anything else? | -> finish } #speaker: Police
* [<b>The victim's pockets</b>]
    His pockets were empty, or <i>emptied</i>, rather. Did you find anything on his body? #speaker: Player
    Nope. Nothing. #speaker: Police
    That's it? #speaker: Player
    That's it. #speaker: Police
    You don't find that odd? You usually carry <i>something</i> with you, like a wallet maybe. Could he have been robbed? #speaker: Player
    He dropped his wallet a lot, so that's nothing weird. I have found it myself quite a few times. Unless he got rich somehow, he would have nothing of value to rob him off. #speaker: Police
    -> options
* [Victim's injuries]
    He has some pretty serious injuries. From a struggle perhaps? Leading to his death? #speaker: Player
    Well, he was no stranger to brawling. They're nothing compared to the state I've seen him in before. #speaker: Police
    See that bruise on his cheek? I did that last week after he resisted an arrest. Right in the kisser!
    -> options
* [Snus]
    What do you make of this, officer? #speaker: Player
    <i>You hold the tin of snus up to the officer's face. He takes a long good whiff.</i> #portrait: 4 #speaker:
    Fyfan, bort med den där! Det luktar skit! #speaker: Police portrait:
    Doesn't it smell weird somehow? I recognize this smell from somewhere, or <i>something.</i> #speaker: Player
    It all smells the same to me. #speaker: Police
    -> options
* -> finish
- (finish) - Look, detective; I know that you think this is yet another murder-mystery that you have to solve, but there's really nothing to it. Just take Boss Man's statement and call it a day.
If you still feel the need to investigate more thoroughly, you can always come back to me and I'll help point you in the right direction.
~ finishedCrimeScene = true
-> END

=== Hub ===
Här ska spelaren i framtiden få information om vart de borde gå härnäst, baserat på vilken information och ledtrådar de har för tillfället. Just nu under construction #speaker:
* [Where should I go next?]
    { not talkedToBossMan: You can start with getting Boss Man's statement, as we've already discussed. -> END }
    { not talkedToBartender: Still not satisfied with Boss Man's statement, huh? You can check out the bar across the street. Maybe someone's heard or seen something. -> END }
    { not talkedToCashier: You're still here? Go home, detective. We have everything we need. If you still intend to stay, go to the gas station and get yourself a coffee at least. -> END }
* { Suspects ? storeClerk } [Gas Station Clerk...] -> AboutStoreClerk
* { Suspects ? bartender } [About the bartender...] -> AboutBartender
* -> END

= AboutBossMan
* { Suspects ? bossMan } [He stole the victim's wallet.]
    No kidding? #speaker: Police
    -> Hub
* -> END

= AboutStoreClerk
-> Hub

= AboutBartender
-> Hub
    
    
    





