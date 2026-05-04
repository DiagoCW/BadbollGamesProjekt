INCLUDE globalsmain.INK
EXTERNAL runAway()
VAR ranaway = false
-> Intro
=== Intro ===
{ ranaway: -> Question }
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

= GainEntry
- What? Why? #speaker: Some guy
~ runAway()
~ ranaway = true
You'll never catch me din jävel!!! # anim: Running
->END

=== Question ===
I honestly thought I would get farther. Sorry bro.
* [Why did you run?]
    I will never stop loitering around, and you pigs can't fucking stop me.
    ** I'm not concerned about that.[] I want to ask you about something else entirely.
            Then lay it on me! As long as I can keep on loitering I'll sing to your tune.
                <>-> QuestionCont
                
= QuestionCont

-> END