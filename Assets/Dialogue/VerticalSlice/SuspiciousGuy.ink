INCLUDE globalsmain.INK
EXTERNAL runAway()

-> Intro
=== Intro ===
How can I help? # speaker: Some guy
    * { foundReceipt } [I need to ask you a few questions...]
        -> GainEntry
    * [What are you doing here?]
        -> IntroContinued
    * [Det var inget.]
        -> END
    
= IntroContinued
Just hanging around, haha! #speaker: Some guy
Can you swisha mig några kronor för en öl? Snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla snälla 
    * [Jag måste gå nu...]
- -> DONE

=== GainEntry ===
- What? Why? #speaker: Some guy
~ runAway()
You'll never catch me din jävel!!! # anim: Running
->END