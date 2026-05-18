INCLUDE GlobalsMain2.ink

{ introCompleted: <i>Your work here is done. Now jump back behind the wheel and get going!</i> -> END }
{not carHood:
<i>Heat waves emanate from the hood of your car like a call for help.</i>
~carHood = true
}
<i><b>Pop the hood?</b></i>
    *[Pop the hood]
    The hood of the car slides open.
    -> Examine
    *[<b>Don't</b> pop the hood]
    <i>You <b>don't</b> open the hood. For whatever reason. You can always come back whenever you feel like taking this seriously.</i>
-> END

=== Examine ===
{not carEngine:
<i>The engine looks like a jumbled mess of wires and metal. Some people know what this all means and how to fix it.</i> <>
~carEngine = true
}
{ <i>I'll just have to poke around and hope for the best....</i> | {~<i>That's not the problem here.</i>| <i>Nope. It's not that.</i> | <i>I can't imagine that would be the problem.</i> } <>}
	* (oil) [<b>Check oil</b>]
		<i>You pull the dipstick out of its tube, methodically wipe it on your coat sleave and dip it once more; It's a bit low.</i>
        -> Examine
    *[<b>Check radiator fan</b>]
        <i>You peer down at the radiator. The fan is spinning, it sounds strained while trying to keep up with the heat.</i>
        -> Examine
	* { foundCoolantLeak} [<color=\#FFFF00><b>Check coolant</color></b>]
		<i>You lean over the car and look at a translucent reservoir, the level sits below the minimum line; it's usually a lot more blue.</i>
		-> FoundProblem
    * ->
        <i>You can't make sense of any of this. You're more like a mechanic of justice and criminal proceedings, not cars.</i>
        <i>Take a look around your car to see if there's anything irregular. Make sure to use that <color=\#FFA500>special vision</color> of yours...</i>
        -> DetectiveVisionTutorial
        
= DetectiveVisionTutorial
<i><color=\#FFA500>One of your main mechanics for progressing through the story is the use of your Detective Vision.</color></i> #speaker:
 <i><color=\#FFA500>When you hold down the right mouse button, your senses become heightened and your vision becomes narrower. This is your Detective Vision, and it will help you identify things of interest through their <color=\#FFFF00>yellow highlight!</color></i>
<i><color=\#FFA500>You might have noticed a strange blue liquid running from underneath your car. If you interact with it normally, nothing interesting will happen.</color></i>
<i><color=\#FFA500>However, try viewing it using your Detective Vision and interact with it again!</color></i>

-> END
=== FoundProblem ===
Wait... I've gotta leak. As in, I've got \*A* leak. A coolant leak.
Time to fix 'er up and get going. Dead bodies don't hang around waiting for somebody to investigate them.
~ introCompleted = true
-> END