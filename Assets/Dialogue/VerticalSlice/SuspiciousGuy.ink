INCLUDE globalsmain.INK
EXTERNAL runAway()

-> Intro
=== Intro ===
How can I help? # speaker: Some guy
<PLAYERCHOICE>
{foundReceipt:
    * [I need to ask you a few questions...]
        -> GainEntry
  - else:
    * [What are you doing here?]
        -> IntroContinued
}
= IntroContinued
hehehehehehehehe
-> DONE

=== GainEntry ===
- What? Why?
~ runAway()
You'll never catch me fucker!!! # anim: Running
->END