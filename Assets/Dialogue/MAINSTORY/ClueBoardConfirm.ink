INCLUDE globalsmainstory.INK
~ temp randomResponse = RANDOM(1, 3)

{ randomResponse:
    - 1: 
        <i>Bam. Time to talk to the officer about who I chose as a suspect.</i>
        <i>If only I could solve Palme-mordet this easily...</i>
        ~ confirmedSolution = true
    - 2: 
        <i>What'ya hear, what'ya say?</i>
        <i>Alright. I've done everything I can. Now to confer with the officer about my final suspect.</i>
        ~ confirmedSolution = true
    - 3: 
        <i>All in a day's work. Looks like I was... Just in time.</i>
        <i>No one heard me say that... Anyway, time to tell the officer who I choose as my final suspect.</i>
        ~ confirmedSolution = true
}