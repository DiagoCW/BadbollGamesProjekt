INCLUDE globalsmain.ink
{ startInvestigation: -> Receipt | -> Intro }

=== Intro ===
#speaker: Boss Man
You got here just in time... Jag har århundratets brott för dig att lösa. # anim: Talking
    * [Vad har hänt?]
    * [*Skippa denna skit, gå på sak!]
        -> StartInvestigation
        
- When I got my skattedeklaration last week, I was looking at a juicy 50.000kr back in skatteåterbäring. #speaker: Boss Man
Till mina stora överraskning så kom Ekobrottsmyndigheten till mitt stånd häromdagen och anklagade mig för att ha fejkat inkomster.
När vi gick igenom mina inkomster insåg jag snabbt att det verkar finnas en kund som inte betalat för sin mat, vilket innebär att jag har förlorat pengar på dem!
    * That makes no sense.[] I can see why they would investigate if your income was larger than your estimated sales, but if you're operating at a loss there's no crime being committed. # speaker: Player
            Tror du att de bryr sig? Jag har blivit rånad oavsett! # speaker: Boss Man # anim: Yelling
    * [Go on...]
- Hur som helst, jag behöver att du undersöker saken.  -> StartInvestigation

=== StartInvestigation ===
#speaker: Boss Man
/*
Så, sätt igång med undersökningen! # anim: Shake
{foundReceipt:
    * [Jag hittade det här i soptunnan.] -> ContinueInvestigation
  - else:
    *[How?] -> Introcont
    *[Alright...]<>-> FinishIntro
}
*/
Så, sätt igång med undersökningen! # anim: Shake
    * { foundReceipt } [Jag hittade det här i soptunnan.] -> ContinueInvestigation
    *[How?] -> Introcont
    *[Alright...]<>-> FinishIntro

= Introcont
#speaker: Boss Man
Det är du som är detektiven, jag är bara killen som riskerar näringsverksamhetsförbud. Du borde ha ett sorts sjätte sinne för den här typen av saker. 
<>-> FinishIntro

= FinishIntro
~ dvisionTutorialTrigger = true
~ startInvestigation = true
->END


=== Receipt ===
#speaker: Boss Man
{ foundReceipt: -> ContinueInvestigation | Vad står du och pillar i naveln för? Börja leta! Du kan lika gärna rota runt i min soptunna om du inte har nåt bättre för dig. } 
-> DONE

=== ContinueInvestigation ===
~ startInvestigation = true
{ foundCulprit: -> FinishInvestigation | -> GoToBar }

=== GoToBar ===
#speaker: Boss Man
Hm... Jag kommer inte ihåg detta kvitto. Om detta stämmer så var det nån som fick sin mat utan att betala!
Den som beställde denna rulle fick extra mycket av min väldigt äckliga sås. Jag vet inte varför jag erbjuder den eller varför folk ens köper den... #anim: Shake
... Men det var den enda personen från igår som jag minns beställde denna! 
Jag kommer också ihåg att det var några som gick till baren tvärs över parken. Med lite tur kommer du hitta gärningsmannen där...
Eller kvinnan!!! #anim: Yelling
-> END

=== FinishInvestigation ===
Så, det var han hela tiden... #anim: Talking #speaker: Boss Man
Tack för att du löste fallet åt mig!
Ekobrottsmyndigheten har dock valt att fälla mig, så jag ska sitta i fängelse i 1 år med 3 års näringsverksamhetsförbud. 
Spelet kommer nu att förstöras!
~ completedGame = true
->END