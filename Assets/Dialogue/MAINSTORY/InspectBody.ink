INCLUDE globalsmainstory.INK
VAR reminisce = false
{ cluesFoundonBody >= 2: -> Result }
{ not talkToPoliceAgain: I should talk to the officer before investigating the body. -> END } #speaker: Player
<><i>The victim. He's not even in the early stages of decay, and he still smells awful.</i>
Now, where to start... -> Inspect

=== Inspect ===
    { cluesFoundonBody >= 2: -> Result } // Dirigerar till sista delen om man hittat alla ledtrådar på kroppen
    * [<b>Inspect his person</b>]
        Was this guy really only 27 years old? He looks a hell of a lot older... Alcohol will do that to you. Myself? I'm Straight Edge 4 Life.
        There are appears to be signs indicative of a struggle. He has pretty major bruising on his face, he's slouched over to the side... He's taken a pretty big beating.
        The officer did say that he died from alcohol poisoning, but how come he didn't mention these injuries?
        ~cluesFoundonBody++
        -> Inspect
    * [<b>Inspect his clothes</b>]
        There's nothing on him. #speaker: Player
        That's strange though. It looks like all of his pockets have been emptied. Was he robbed?
        This alone is reason enough for not ruling anything out. It could even have been a, dare I say it, <i>murder?</i>
        If he really was robbed, I need to find out what could have been on him.
        ~ gainknowledge(pocketsEmptied)
        ~cluesFoundonBody++
        -> Inspect
    + {!reminisce} [<b>Think back on your past.</b>]
        {Allt var bättre förr. -> Inspect | Jag skulle ha köpt BitCoin när jag hade chansen... -> Inspect | I'm a creep, I'm a weirdo. What the hell am I doing here? -> Inspect | -> StopThinking } #speaker: Player

= StopThinking
Enough of this, focus... #speaker: Player
~ reminisce = true
-> Inspect

=== Result ===
The officer seems confident that he just fell over and happened to die by himself, but everything here indicates something else entirely. #speaker: Player
Maybe I just didn't know the guy like the officer did... In fact, I didn't know him at all. But even at a cursory glance it doesn't add up.
    {foundAllClues(): // om man hittat alla andra ledtrådar redan
    * I have everything I need[.], I should talk to the officer.
        -> DONE
    - else: // om man forfarande har ledtrådar kvar att hitta runtomkring
    * I have everything I need[.] here, but I should inspect the scene again. #speaker: Player
        -> DONE
}

