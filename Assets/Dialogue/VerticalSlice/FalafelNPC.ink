INCLUDE globalsmain.ink
temp startInvestigation = false

# speaker: Boss Man 
{ dvisionTutorialTrigger: -> IsCulpritFound | -> Intro }
=== Intro ===
{ startInvestigation: -> EndGame | -> Idle }
You got here just in time... Jag har århundratets brott för dig att lösa. # anim: Talking
<PLAYERCHOICE> 
    * [Vad har hänt?]
    * [*Skippa denna skit, gå på sak!]
        -> StartInvestigation
        
- When I got my skattedeklaration last week, I was looking at a juicy 50.000kr back in skatteåterbäring. #speaker: Boss Man
Till mina stora överraskning så kom Ekobrottsmyndigheten till mitt stånd häromdagen och anklagade mig för att ha fejkat inkomster.
När vi gick igenom mina inkomster insåg jag snabbt att det verkar finnas en kund som inte betalat för sin mat, vilket innebär att jag har förlorat pengar på dem!
<PLAYERCHOICE>
    * That makes no sense.[] I can see why they would investigate if your income was larger than your estimated sales, but if you're operating at a loss there's no crime being committed. # speaker: Player
            Tror du att de bryr sig? # speaker: Boss Man # anim: Yelling
    * [Go on...]
- Hur som helst, jag behöver att du undersöker saken.  -> StartInvestigation 

=== StartInvestigation ===
Så, sätt igång med undersökningen! # anim: Shake
<PLAYERCHOICE>
    *[How?]
        Det är du som är detektiven, jag är bara killen som riskerar näringsverksamhetsförbud. Du borde ha ett sorts sjätte sinne för den här typen av saker. 
    *[Alright...]
- ~ dvisionTutorialTrigger = true
~ startInvestigation = true
<> -> END

=== IsCulpritFound ===
{ startInvestigation: -> EndGame | -> Idle }

=== Idle ===
Vad står du och pillar i naveln för? Hitta boven!
-> DONE

=== EndGame ===
Du gjorde det... Jag är skyldig dig mitt fucking liv bror
-> END