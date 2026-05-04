INCLUDE globalsmainstory.INK
VAR reminisce = false


Dödsoffret. Han stinker av allt möjligt, så pass att han nästan luktar behagligt.
{ talkToPolice: -> Inspect | I should talk to the officer before investigating the body. -> END }
=== Inspect ===
    { cluesFoundonBody >= 2: -> Result } // Dirigerar till sista delen om man hittat alla ledtrådar på kroppen
    * [*Inspektera fötterna*]
        Stora fötter... Stora skor.  
        ~cluesFoundonBody++
        -> Inspect
    * [Inspektera kroppen]
        Alla fickor har vänts ut och in... 
        Mördaren måste ha letat efter nåt. Osäkert om de hittade det de letade efter, eftersom alla fickor ser ut att vara tömda.
        Passade de på att plundra kroppen i efterhand, eller var det anledningen till mordet?
        ~cluesFoundonBody++
        -> Inspect
    + {!reminisce} [Tänk tillbaka på ditt förflutna.]
        {Allt var bättre förr. -> Inspect | Jag skulle köpt BitCoin när jag hade tillfället. -> Inspect | Jag är ett äckel, jag är konstig. Vad tusan gör jag här? -> Inspect | -> StopThinking }

= StopThinking
Nog om detta, jag tror jag har ett fall att lösa eller nåt...
~ reminisce = true
-> Inspect

=== Result ===
    {foundAllClues(): // om man hittat alla andra ledtrådar redan
    * Jag har allt jag behöver[.], jag borde prata med polisen.
        //~ talkToPolice = true
        -> DONE
    - else: // om man forfarande har ledtrådar kvar att hitta runtomkring
    * Jag har allt jag behöver[.] här, men jag borde fortsätta undersöka runtomkring. #speaker: Player
        -> DONE
}






