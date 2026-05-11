INCLUDE globalsmainstory.INK
{ talkToPolice: <>-> Continued | -> Intro }
=== Intro ===
#speaker: 
The officer has been patiently waiting for you to arrive. He stands diligently watching over the crime scene, not moving an inch. 
Your sudden arrival seems to have thrown him off his balance, though...
Hello detective, quite an entrance you made. #speaker: Police
    * [I drive.]
        Not anymore, it would seem. You should probably take care of that later, but you have an extremely cool case to solve right now.
    * [What seems to be the problem?]
        I was expecting you to already be briefed about it, but I suppose I could give you the run-down.
    * [Cut to the chase.]
- Last night, the body of 37-year old Peter Grip was found lying here. The official cause of death is yet to be determined, but the victim has injuries indicitative of a struggle.
He was also a known drunk. I've dealt with him before on many occassions when he's had a few too many at the bar. 
No immediate family. Any family he has left wants nothing to do with him. Seems he burned his bridges since way back.
No witnesses have come forward to share any information yet, but we haven't had the chance to interview anyone yet either. We'll leave that to you. 
    -(opts)
    * { Suspects ? bossMan } [About Boss Man...]
        He couldn't tell us much. 
    * [Potential murder?]
        It's a possibility, but we have no reason to suspect or classify it as such.
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
- Before you go, you should take a moment to inspect the scene. Though our initial discovery gave no interesting results, maybe you'll have better luck.
I've heard that you have some kind of... sense. A vision. A Detective Vision, if you will.
~ talkToPolice = true
-> DetectiveVision

= DetectiveVision
#speaker:
Tills att vi implementerar detta i Tutorialscenen så får det vara kvar här:
<i>When surveying a location, you might find it helpful to hold down the right mouse button to activate your Detective vision. This will zoom in your FOV and highlight items of importance, helping you find clues to progress the case.</i>
<i>Your detective vision will be necessary to find clues in the game. Unless a clue has been highlighted through the detective vision, you are unable to obtain it.</i>
<i>Consider using it sparingly, however; every time you use your detective vision, it tracks how long you've used it. Use it too much and you will find yourself penalized by the end of the game.</i>
<i>This means that you shouldn't go around using it everywhere all the time! Some dialogue with characters will offer you directions on where you might need to go next to investigate.</i>
<i>Using context clues and paying attention to what people tell you will most likely minimize the amount of time that you need to use your detective vision.</i>
<i>You should try it out now, however. Get a good overview of the crime scene, and then activate it to see if anything is <color=\#FFFF00>highlighted!</color></i>
<i>Remember; as long as you <b>hold</b> the right mouse button, you will be using your detective vision.</i>
-> END

=== Continued ===
All done, detective? #speaker: Police
{ foundAllClues():
    -> Questions
- else:
    I think you still have a few things to check out. Inspect the body and its surroundings to make sure you've got everything.
    -> END
}

=Questions
* [The victim's pockets]
    We found nothing on him. 
* [Struggle?]





