INCLUDE globalsmainstory.INK
{ talkToPolice: <>-> Continued | -> Intro }
=== Intro ===
#speaker: 
The officer has been patiently waiting for you to arrive. He stands diligently watching over the crime scene, not moving an inch. 
Your sudden arrival seems to have thrown him off his balance, though.
Hello detective, quite an entrance you made there. There was really no need to rush. #speaker: Police
    * [I drive.]
        Not anymore, it would seem. You should probably take care of that later...
    * [What seems to be the problem?]
        I was expecting you to already be briefed about it, but I suppose I could give you the run-down. There's not much to it.
    * [Cut to the chase.]
- Last night, the body of 57-year old Peter Grip was found lying here. The official cause of death is yet to be determined, but knowing him I'd say it was inevitable.
Why do you say that? Did you know the victim? #speaker: Player
He was a known drunk. I've dealt with him before on many occassions when he's had a few too many at the bar. I tended to run into him frequently. If a call is made, it's likely he was involved somehow.
Anyway, he has no immediate family. Any family he has left wants nothing to do with him. Seems he burned his bridges since way back.
No witnesses have come forward to share any information yet, but we haven't had the chance to interview anyone yet either. We'll leave that to you, though I can't imagine anything will come of it.
    -(opts)
    * { Suspects ? bossMan } [About Boss Man...]
        Did you get an initial statement from him? #speaker: Player
        Only that he found the body after closing up shop. Though I figured that you could do the rest, seeing as you're here. #speaker: Police
        Anything suspiscious about him?
        Other than that he found a dead drunk lying here? Not a pretty sight, but someone had to stumble upon him sooner or later.
        -> opts
    * [Potential murder?]
        I knew you would start going off on the murder aspect... I suppose, but we really have no reason to suspect or classify it as such. As I said, this came a surprise to no one. 
        We tested his blood alcohol level, and it was surprisingly high even for a guy like him. Seems like his liver gave up, kidneys shut down... And the rest is history.
        -> opts
    * [Who found the body?]
        As you said, somebody found the body. That someone must have phoned it in? #speaker: Justin Time
        They did. <i>Boss Man.</i>
        ** { talkedToBossMan } [I crashed my car behind his shop.]
            Then I take it that you've already introduced yourselves.
        ** { not talkedToBossMan } [Boss Man?]
            The falafel guy across the street. 
        -- <> He found the body on his way home after closing up shop last night.
        ~ addsuspect(bossMan)
            -> opts
    * -> cont
- (cont) - Before you go, you should take a moment to inspect the scene. Though our initial discovery gave no interesting results, maybe you'll have better luck.
I've heard that you have some kind of... sense. A vision. A Detective Vision, if you will.
~ talkToPolice = true
-> DetectiveVision

= DetectiveVision
#speaker:
Tills att vi implementerar detta i Tutorialscenen så får det vara kvar här:
<i>When surveying a location, you might find it helpful to hold down the right mouse button to activate your Detective vision. This will zoom in your FOV and highlight items of importance, helping you find clues to progress the case.</i>
<i>Your detective vision will be necessary to find clues in the game. Unless a clue has been highlighted through the detective vision, you are unable to obtain it.</i>
<i>Consider using it sparingly, however; every time you use your detective vision, it puts unneccessary strain on your mind and it tracks how long you've used it. Use it too much and you will find yourself penalized by the end of the game.</i>
<i>Using context clues and paying attention to what people tell you will most likely minimize the amount of time that you need to use your detective vision.</i>
<i>You should try it out now, however. Get a good overview of the crime scene, and then activate it to see if anything is <color=\#FFFF00>highlighted!</color></i>
-> END
<i>This means that you shouldn't go around using it everywhere all the time! Some dialogue with characters will offer you directions on where you might need to go next to investigate.</i>

=== Continued ===
{ finishedCrimeScene: -> Hub }
{ foundAllClues():
    -> Questions
- else:
    I think you still have a few things to check out. Inspect the body and its surroundings to make sure you've got everything.
    -> END
}

=Questions
{ !All done, detective? | Anything else? } #speaker: Police
* [The victim's pockets]
    We found nothing on him. #speaker: Police
    That's it? #speaker: Player
    That's it. #speaker: Police
    You don't find that odd? You usually carry <i>something</i> with you, like a wallet maybe. Could he have been robbed? #speaker: Player
    Excuse my french, men han var pissfattig. Everyone knew that. He could have nothing of value on him that anyone would want. #speaker: Police
    <>-> Questions
* [Victim's injuries]
    The victim has some injuries, from a struggle perhaps? Leading to his death? #speaker: Player
    Well, he was no stranger to brawling. These injuries are nothing compared to the state I've seen him in before. #speaker: Police
    I can tell you one thing for certain; <i>these injuries are not what did him in</i>. I'm telling you that he succumbed to the bottle. It was inevitable.
<>-> Questions
* Snus[What do you make of this?] #speaker: Player
    You hold the tin of snus up to the officer's face. He takes a long good whiff. #speaker:
    Fyfan, bort med den där! Det luktar skit! #speaker: Police
    Doesn't it smell off somehow? I recognize this smell from somewhere, or <i>something.</i> #speaker: Player
    It all smells the same. I don't touch the stuff.
    <> -> Questions
* -> finish
- (finish)
Look, detective; I know that you think this is yet another murder-mystery that you have to solve, but there's really nothing to it. Just take Boss Man's statement and call it a day.
If you still feel the need to investigate more thoroughly, you can come back to me and I'll help point you in the right direction.
~ finishedCrimeScene = true
-> END

=== Hub ===
Här ska spelaren i framtiden få information om vart de borde gå härnäst, baserat på vilken information och ledtrådar de har för tillfället. Just nu under construction
* [About Boss Man...] -> AboutBossMan
* { Suspects ? storeClerk } [Gas Station Clerk...] -> AboutStoreClerk
* { Suspects ? bartender } [About the bartender...] -> AboutBartender
* -> END

= AboutBossMan
* { Suspects ? bossMan } [He stole the victim's wallet.]
    No kidding? 
    -> Hub
* -> END

= AboutStoreClerk
-> Hub

= AboutBartender
-> Hub
    
    
    





