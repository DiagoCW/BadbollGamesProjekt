INCLUDE globalsmainstory.ink
{ Clues ? receipts:
    Hey, what are you rummaging through my trash for? Stop that! #speaker: Boss Man
- else:
<i>You search the trash and grab some receipts from the top of the pile.</i> #portrait: 0 #speaker: 
<i>Two receipts stick out; one receipt from the falafel shop, as expected, and another from the gas station.</i>
<i>Both of them are from last night around the same time the victim died. They also show that the same card was used for both purchases.</i>
<i>You feel like your grasping at straws. Most likely, these receipts mean nothing. You don't even know who they belong to or why it should matter.</i>
<i>But they <b>could</b> be important. Maybe...</i>
<i>You also find a half eaten falafel underneath the receipts.</i> #portrait: 2
Probably also not important, but I'll hang on to it just in case I get hungry or it turns out to be important. #speaker: Player
~ getclue(receipts)
}
-> END
