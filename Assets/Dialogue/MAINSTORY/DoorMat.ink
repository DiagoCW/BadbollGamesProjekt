INCLUDE globalsmainstory.INK
{ canEnterStorage:
    <i>You look under the door mat. There's a key. You pick it up. Your deduction tells you that key goes in door. Put key in this door.</i> #speaker: 
    ~ getitem(backdoorkey)
- else:
    I could have saved myself some trouble if I just looked under it to begin with... #speaker: Player
    }
-> END