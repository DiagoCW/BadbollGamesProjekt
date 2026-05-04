INCLUDE globalsmainstory.INK
EXTERNAL runAway()
VAR ranaway = false
-> Intro
=== Intro ===
{ ranaway: -> Question }
What'd ya want from me? # speaker: Some guy
The man before you is clearly intoxicated. Inebriated, even. Absolutely piss drunk. #speaker: Player
    * { Clues ? Clues.none } [I need to ask you a few questions.]
        -> GainEntry
    * [What are you doing?]
        I am LOITERING. And I will NEVER stop loitering. So don't bother asking me anything. # speaker: Some guy
    * [Nothing...]
- You don't even want to bother with this guy. Until you find something you could reasonably question him with you should just go. #speaker: Player
        -> END

= GainEntry
- What? Why? #speaker: Some guy
~ runAway()
~ ranaway = true
You'll never catch me pig!!! # anim: Running
->END

=== Question ===
I honestly thought I would get farther. Sorry bro. #speaker: Some Guy
* [Why did you run?]
    I will never stop loitering around, and you pigs can't fucking stop me.
    ** I'm not concerned about that.[] I want to ask you about something else entirely. #speaker: Player
            Then lay it on me! As long as I can keep on loitering I'll sing to your tune, officer. #speaker: Some Guy
                <>-> QuestionCont
                
= QuestionCont
+ [Did you know the victim?]
    Under construction 
    -> QuestionCont
+ { LIST_COUNT(Suspects) >= 1 } [Ask about current suspects] -> QuestionSuspects
* [I gots to go.]
    God speed, officer. I'll stay here and loiter.
    -> END

// --- UNDER CONSTRUCTION
= QuestionSuspects
{ LIST_COUNT(Suspects) == 1: You don't have any suspects yet. Make a mental note to come back to this later, though. <> -> QuestionCont }
* { Suspects ? bossMan } [Boss Man?]
* { Suspects ? bartender } [Bartender?]
* { Suspects ? storeClerk } [Store Clerk?]
* -> END // fallback
--> END