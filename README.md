# SoundExtensions
Allows beatmaps to add hitsounds to bloqs!

# To Mappers (Guide)
If you're reading this, I'm assuming you're a mapper and know how to operate JSON files. Otherwise, turn back!

To load hitsounds into a beatmap, you must edit your `Info.dat` file where your beatmap difficulty is at and add `"_sounds": ["Example0.wav", "Example1.ogg"]` into `_customData`. Make sure it looks like [this](https://i.imgur.com/TUsVCiL.png), now let's move onto actually attaching our hitsounds to bloqs!

Open the beatmap file, look for a `_note` event you'd like to attach a custom hitsound to and then add `_customData` if it does not already exist.

To actually attach the hitsound to the `_note` event, simply add `"_soundID": 0` inside `_customData`. Where `_soundID` correlates to the files defined in `_sounds` in `Info.dat`, so for instance having a `_soundID` of `0` would be `Example0.wav` and having a `_soundID` of `1` would be `Example1.ogg`.

Your finalized `_note` event would look something like this: `{"_time":5.0,"_lineIndex":0,"_lineLayer":2,"_type":0,"_cutDirection":0,"_customData":{"_soundID":0}}`

NOTE: Please make sure you only add SoundExtensions as a suggestion and not a requirement, so players on other platforms (e.g. Quest) can play your beatmap. Also, your hitsounds should be in the `.wav` or `.ogg` format to prevent issues.

Congrats, you're ready to use SoundExtensions! Have fun.
