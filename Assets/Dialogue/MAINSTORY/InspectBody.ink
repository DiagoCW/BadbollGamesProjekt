INCLUDE globalsmainstory.INK
VAR reminisce = false

The victim. It's not even in the early stages of decay, and he still smells awful.
{ talkToPolice: Now, where to start... -> Inspect | I should talk to the officer before investigating the body. -> END }
=== Inspect ===
    { cluesFoundonBody >= 2: -> Result } // Dirigerar till sista delen om man hittat alla ledtrådar på kroppen
    * [-Inspect his person]
        As the officer said, he was a known drunk. That would explain this rancid smell. Seems like all these years of drinking finally caught up to him last night.
        However, there are signs indicative of a struggle. He has minor lacerations on his neck, but nothing fatal.
        If he was murdered, it's not apparent how. I should put a pin in this.
        ~cluesFoundonBody++
        -> Inspect
    * [-Inspect his clothes]
        His pockets are emptied. All of them... Someone must have been looking for something, but what? This could have been the motive for a murder. 
        Wait, best not to get ahead of myself, it could have been opportunistic. Whoever found him could have decided to loot his body after the fact.
        I need to find out what could have been on him, it could crack this whole case.
        ~ gainknowledge(pocketsEmptied)
        ~cluesFoundonBody++
        -> Inspect
    + {!reminisce} [Think back on your past.]
        {Everything was better way back when. -> Inspect | I should've bought BitCoin when I had the chance... -> Inspect | I'm a creep, I'm a weirdo. What the hell am I doing here? -> Inspect | -> StopThinking }

= StopThinking
Enough of this, focus...
~ reminisce = true
-> Inspect

=== Result ===
    {foundAllClues(): // om man hittat alla andra ledtrådar redan
    * I have everything I need[.], I should talk to the officer.
        //~ talkToPolice = true
        -> DONE
    - else: // om man forfarande har ledtrådar kvar att hitta runtomkring
    * I have everything I need[.] here, but I should inspect the scene again. #speaker: Player
        -> DONE
}






