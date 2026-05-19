INCLUDE globalsmainstory.INK
{ items ? backdoorkey: A door mat. -> END }
{ canEnterStorage:
    <i>You look under the door mat. There's a key. You pick it up. Your deduction tells you that key goes in door. Put key in this door.</i> #speaker: 
    ~ getitem(backdoorkey)
- else:
    A door mat.
    }
-> END