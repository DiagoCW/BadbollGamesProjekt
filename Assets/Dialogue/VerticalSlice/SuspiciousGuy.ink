INCLUDE globalsmain.INK
EXTERNAL runAway()

-> Intro
=== Intro ===
How can I help? # speaker: Some guy
{foundReceipt:
    * [I need to ask you a few questions...]
        -> GainEntry
  - else:
    * [What are you doing here?]
        -> IntroContinued
}
= IntroContinued
hehehehehehehehe #speaker: Some guy
-> DONE

=== GainEntry ===
- What? Why? #speaker: Some guy
~ runAway()
You'll never catch me fucker!!! # anim: Running
->END