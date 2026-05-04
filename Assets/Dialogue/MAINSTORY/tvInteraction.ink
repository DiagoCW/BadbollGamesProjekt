INCLUDE globalsmainstory.INK

{ items ? karaokeUSB: -> PlayVideo | -> NoUSB }

=== NoUSB ===
#speaker: Player
Nothing is playing on the TV.
-> END

=== PlayVideo ===
#speaker: Player
Let's see what this is about.

#video: play_karaoke
Oh...
-> END