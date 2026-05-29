INCLUDE globalsmain2.INK

{ seenPuddle:
<i>I know it isn't real piss, I was just joking...</i>
 -> END
 - else:
 <i>Looks like someone took a piss here. Right underneath my car. For shame.</i>
 ~ seenPuddle = true
 -> END
}