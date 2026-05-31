INCLUDE globalsmainstory.INK
{ Clues ? kylarVätska: -> END }
Just a bunch of coolant stacked on a shelf. Would've been nice to have this when my car broke down earlier, instead of MacGyvering it. #speaker: Player
There's a bunch of snus stacked right next to them. This is a health hazard just waiting to happen. 
{ knowledge ? victimPoisoned: 
    <color=\#FFFF00>Hang on.</color> The health hazard might already have happened. <b>Deliberately.</b>
    There's a lot of snus here, but it's all of the same brand; the one that Peter had. And I seem to recall that his snus had a peculiar smell... #portrait: 4
    That's it. This snus has been absolutely soaked in coolant. Ingesting this in such small doses would not  be noticeable, but it will still lead to certain death. #portrait: 
    Let's see what the store clerk has to say about this.
- else:
    I have to confront the store clerk about this. This simply will not do.
}
~ getclue(kylarVätska)
-> END

/* Store clerk ska till sitt försvar säga: 
'Det är därför de ligger inne på lagret, för att jag tog ut dom ur försäljning! Jag hade en incident när jag packade upp varorna. Jag skulle självklart inte sälja dessa.'
För att pressa vidare måste man ha knowledge om store clerk alibi samt trisslotten - detta är det som gör honom till suspect.
*/
