INCLUDE globalsmainstory.INK
{ talkToPolice: -> Continued | -> Intro }
=== Intro ===
The officer has been patiently waiting for you to arrive. He stands diligently watching over the crime scene, not moving an inch. 
Your sudden arrival seems to have thrown him off his balance, though...
Hello detective, quite an entrance you made there.
    * [I drive.]
        Not anymore, it would seem. You should probably take care of that later, but you have an extremely cool case to solve right now.
    * [What seems to be the problem?]
        I was expecting you to already be briefed about the case, but I suppose I could give you the run-down.
    * [Cut to the chase.]
- Last night, the body of 27-year old Peter Grip was found lying here. The official cause of death is yet to be determined, but the victim has injuries indicitative of a struggle.
We are also awaiting a toxocology report. You should check back here later so I can share the results.
There appears to be no witnesses at this point, but 
    -(opts)
    * [The victim's identity]
        hm jag venne
        -> END
    * [Who found the body?]
        Someone phoned it in. Boss Man, I think.
        ** { talkedToBossMan } [I crashed my car behind his shop.]
            Then I take it that you've already introduced yourselves.
        ** { not talkedToBossMan } [Boss Man?]
            The falafel guy. He sells falafel for a living. Can you imagine?
        ** [Where can I find him?]
    -- You'll find him right across the street.
Before you do that though, you should take a moment to inspect the scene. Though our initial discovery gave no interesting results, maybe you'll have better luck.
I've heard that you have some kind of... sense. A vision. A Detective Vision, if you will. It should help discovering clues around the scene.
~ talkToPolice = true
-> DetectiveVision

= DetectiveVision
When surveying a location, you might find it helpful to press 'V' to activate your vision. This will highlight items of importance, helping you find clues to progress the case.
Consider using it sparingly, however; each time you use your detective vision you will find yourself penalized by the end of the game. 
You can try it out now, however. Get a good overview of the crime scene, and then activate it to see if anything is highlighted!
-> END

=== Continued ===
{ cluesFoundonBody == 2 and cluesFoundbyBody == 2:
DETTA VISAS OM DU HAR HITTAT ALLA LEDTRÅDAR
- else:
DETTA VISAS OM DU HAR LEDTRÅDAR KVAR ATT HITTA
}
->END





