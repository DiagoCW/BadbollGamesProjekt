INCLUDE globalsmainstory.INK
{ items ? backdoorkey: A door mat. -> END }
{ canEnterStorage:
    <i>You look under the door mat. There's a key. You pick it up.</i> #speaker: 
    ~ getitem(backdoorkey)
- else:
    A door mat.
    }
-> END