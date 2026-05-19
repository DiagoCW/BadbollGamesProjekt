INCLUDE globalsmainstory.INK
{ Clues ? snus: Don't need it. -> END }
A snusdosa, thrown off to the side by the victim. It still has a few pouches left. #portrait: 4
Smells worse than it should... Something about it smells familiar, but I can't quite sätta fingret på det... 
Could be important though.
~cluesFoundbyBody++
~getclue(snus)
// --- Dirigera till tutorial om detta är första ledtråden spelaren hittar
{ LIST_COUNT(Clues) <= 2: -> ClueIntro | -> END }

=== ClueIntro ===
#portrait:
You just found your first clue! Clues are found by using your Detective Vision and are used to solve the crime.
Some clues unlock further dialogue options with characters, as you can inquire or confront people about them. 
They are also used at your corkboard to connect to potential suspects. If you haven't already, head back to the trunk of your car to view the corkboard.
You can open your inventory <b>(TAB)</b> to view all clues you have currently obtained. Hover over them to see a description of the clue in case you need to refresh your mind.
-> END
