INCLUDE globalsmainstory.INK
VAR reminisce = false
{ cluesFoundonBody >= 2: -> Result }
<i>The (murder?) victim. He's not even in the early stages of decay, and he still smells awful.</i>
{ talkToPoliceAgain: Now, where to start... -> Inspect | I should talk to the officer before investigating the body. -> END }
=== Inspect ===
    { cluesFoundonBody >= 2: -> Result } // Dirigerar till sista delen om man hittat alla ledtrådar på kroppen
    * [-Inspect his person]
        Was this guy really only 27 years old? He looks a hell of a lot older... Alcohol will do that to you. I'm staying straight edge.
        There are appears to be signs indicative of a struggle. He has pretty major bruising on his face, he's slouched over to the side... Did the officer not think this was noteworthy?
        ~cluesFoundonBody++
        -> Inspect
    * [-Inspect his clothes]
        His pockets appears emptied. All of them... Was he robbed?
        Someone could have been after something that he was carrying, and that was the reason for...
        ...wait, best not to get ahead of myself. But this really makes no sense.
        He has some pretty major injuries, and his pockets have clearly been emptied after the fact. There's obviously some foul play going on here.
        I need to find out what could have been on him, it might help me make sense of this...
        ~ gainknowledge(pocketsEmptied)
        ~cluesFoundonBody++
        -> Inspect
    + {!reminisce} [Think back on your past.]
        {Everything was better in the '50s. -> Inspect | I should've bought BitCoin when I had the chance... -> Inspect | I'm a creep, I'm a weirdo. What the hell am I doing here? -> Inspect | -> StopThinking }

= StopThinking
Enough of this, focus... #speaker: Player
~ reminisce = true
-> Inspect

=== Result ===
The officer seems confident that he just fell over and happened to die by himself, but everything here indicates something else entirely. #speaker: Player
Maybe I just didn't now the guy like the officer did... In fact, I didn't know him at all.
    {foundAllClues(): // om man hittat alla andra ledtrådar redan
    * I have everything I need[.], I should talk to the officer.
        //~ talkToPolice = true
        -> DONE
    - else: // om man forfarande har ledtrådar kvar att hitta runtomkring
    * I have everything I need[.] here, but I should inspect the scene again. #speaker: Player
        -> DONE
}






