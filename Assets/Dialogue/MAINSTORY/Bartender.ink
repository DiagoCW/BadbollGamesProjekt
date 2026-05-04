INCLUDE globalsmainstory.INK

// Check if we've met him before
{ talkedToBartender: -> CanQuestion | -> Intro }

#speaker: Bartender
=== Intro ===
{ !What can I get you? | Back again? What do you want? } #anim: Talking
    * [A huaiufhiawuf.]
        Coming right up. That'll be fifty spänn.
        ~ getclue(beer)
        -> IntroCont
    * [I need information.]
        Information ain't free. Buy a drink or get out.
        -> IntroCont

= IntroCont
~ talkedToBartender = true
So, what is it?
    * [I've got some questions.]
        -> CanQuestion.Question
    * [I gots to go, baby.] 
        Yeah, don't let the door hit you.
        -> DONE

=== CanQuestion ===
Need another drink? Or just more talk?
-> Question

= Question
* [I've got some questions for you.]
    -> StartQuestion("Shoot.") 
* [I gots to go now, baby.]
    -> END

=== StartQuestion(msg) ===
~ talkedToBartender = true
{msg}

// These options only appear if the player has found the clues/suspects!
* { Clues ? snus } [Is this your snus?] -> Snus
* { Suspects ? bossMan } [Do you know the kebab guy out back?] -> BossMan
+ [Did you see anything last night?] -> LastNight
* [See you later.] -> END

// --- THE ANSWERS ---

= Snus
Never seen it before in my life.
-> StartQuestion("Anything else?")

= BossMan
The guy who runs the shop? He comes in sometimes. Keeps to himself.
-> StartQuestion("More questions?")

= LastNight
It was a busy night. Lots of people. I didn't see anything unusual, if that's what you're asking.
-> StartQuestion("Anything else?")