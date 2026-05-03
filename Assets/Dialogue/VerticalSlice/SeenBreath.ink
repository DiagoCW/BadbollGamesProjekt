INCLUDE globalsmain.INK
{ seenBreath:
    Don't feel like standing around all day smelling this guy's breath. #speaker: Player
- else:
    This guy stinks... #speaker: Player
    Added Garlic Breath Clue! #portrait: 1
    ~ seenBreath = true
}