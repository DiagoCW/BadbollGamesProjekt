INCLUDE globalsmainstory.ink
{ Clues ? receipts:
    Hey, what are you rummaging through my trash for? Stop that! #speaker: Boss Man
- else:
<i>You search the trash and grab some receipts from the top of the pile.</i> #speaker:
<i>Two receipts stick out; one receipt from the falafel shop, as expected, and another from the gas station.</i>
<i>Both of them are from last night around the same time the victim died. They also show that the same card was used for both purchases.</i>
<i>You feel like your grasping at straws. Most likely, these receipts mean nothing. You don't even know who they belong to or why it should matter.</i>
<i>But they <b>could</b> be important. Maybe...</i>
~ getclue(receipts)
{ LIST_COUNT(Clues) <= 2: -> ClueIntro | -> END }

}

=== ClueIntro ===
#portrait:
<i>You just found your first clue(s)! Clues are found by using your Detective Vision and are used to solve the crime.</i>
<i>Some clues unlock further dialogue options with characters, as you can inquire or confront people about them.</i>
<i>They can also be used at your clueboard to connect to potential suspects. When you get a chance, head back to the trunk of your car to view it.</i>
<i>You can open your inventory <b>(TAB)</b> to view all clues you have currently obtained. Hover over them to see a description of the clue in case you need to refresh your mind.</i>
-> END
