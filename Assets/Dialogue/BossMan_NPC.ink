INCLUDE globals.ink

{talkedToBossMan: -> FamilyGuy | -> Intro}


=== Intro ===
{visitedCrimeScene: -> FoundRoll | -> Intro1}
=== Intro1 ===
Hey brother, thanks for coming by so fast. Let me fill you in on the 'deets'. # speaker: Boss man # anim: Talking
Yesterday, I "sold" a kebabrulle but the problem is that the customer who ordered it ended up not paying for it.
What I need from you is to find out who it was or I'll be locked up for tax fraud. 
<PLAYERCHOICE>
    * [How is this my problem?]
        Because if you don't solve this for me you are not getting paid, simple as. -> Continued
    * [Alright, where should I start looking?]
        -> Continued
    * [Who are you...?]
        I'm the Boss, Man! You can call me Boss man. -> Continued
=== Continued ===
Go check out the nearby trash bins, maybe you'll find something.
~ talkedToBossMan = true
-> DONE

=== FamilyGuy ===
{visitedCrimeScene: -> FoundRoll | Peter Grip är så fucking rolig!!! Älskar den killen }
->DONE

=== FoundRoll ===
You did it... You found my famous roll
-> END