INCLUDE GlobalsMain2.ink

{ introCompleted: Your work here is done. Now get to it! -> END }
{not carHood:
Heat waves emanate from the hood of your car like a call for help, the paint job barely hides the damage beneath. This is a lovely car.
~carHood = true
}
Pop the hood?
    *[Pop the hood]
    The hood of the car slides open.
    -> Examine
    *[<b>Don't</b> pop the hood]
    You <b>don't</b> open the hood. For whatever reason.
-> END

=== Examine ===
{not carEngine:
The engine looks like a jumbled mess of wires and metal. Some people know what this all means and how to fix it. I'll just have to poke around and hope for the best.
~carEngine = true
}
{ What could the problem be? | {~That's not the problem here.| Nope. It's not that. | I can't imagine that would be the problem. } <>}
	* (oil) [Check oil]
		You pull the dipstick out of its tube, methodically wipe it on your coat sleave and dip it once more; It's a bit low.
        -> Examine
    *[Check radiator fan]
        You peer down at the radiator. The fan is spinning, it sounds strained while trying to keep up with the heat.
        -> Examine
	*[Check coolant]
		You lean over the car and look at a translucent reservoir, the level sits below the minimum line; it's usually a lot more blue.
		{ foundCoolantLeak: -> FoundProblem } 
        -> Examine
    * ->
        You can't make sense of any of this. You're more like a mechanic of justice and criminal proceedings, not cars. 
        Take a look around your car to see if there's anything irregular.
        -> DetectiveVisionTutorial
        
= DetectiveVisionTutorial
<i>One of your main mechanics for progressing through the story is the use of your Detective Vision.</i>
 <i>When you hold down the right mouse button, your senses become heightened and your vision becomes narrower. This is your Detective Vision, and it might help you identify things of interest through their <color=\#FFFF00>yellow highlight!</color></i>
<i>You might have noticed a strange blue liquid running from underneath your car. If you interact with it normally, nothing interesting will happen.</i>
<i>However, try using your Detective Vision now and interacting with it again!</i>

-> END
=== FoundProblem ===
Wait... You've gotta leak. As in, you've got \*A* leak. A coolant leak.
Time to fix 'er up and get going. Dead bodies don't hang around waiting for somebody to investigate them.
~ introCompleted = true
-> END