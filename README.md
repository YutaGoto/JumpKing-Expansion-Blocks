# JumpKing Expansion Blocks Mod

![Jump King Mod Badge](https://img.shields.io/badge/Jump_King-Mod-61CE70?style=flat-square&labelColor=BA1313)

![thumbnail](https://steamuserimages-a.akamaihd.net/ugc/5838428307081072717/1CA52980181FF52EF1122EB3169F0AA583BB648B/)

[Steam Workshop Link](https://steamcommunity.com/sharedfiles/filedetails/?id=3214349391)

This mod adds custom blocks to Jump King.

## Ice & Friction Blocks

| Block Name | Hex | RGB | Description |
| :--- | :--- | :--- | :--- |
| **Slippery Ice** | `#00c0ff` | `0, 192, 255` | 3x more slippery than normal ice. |
| **Zero Friction** | `#00c1ff` | `0, 193, 255` | No friction at all. You will slide indefinitely. |
| **Cursed Ice** | `#909000` | `144, 144, 0` | No walk, ice platform, and reverse jump. |
| **Restrained Ice** | `#808000` | `128, 128, 0` | No walk and ice platform. Similar to wearing Giant Boots. |
| **OneWay Ice** | `#4182ff` | `65, 130, 255` | Semi-solid ice block that is solid only from the top. |
| **Heavy Ice** | `#00ffc8` | `0, 255, 200` | Ice platform where you cannot perform small jumps (like Snow block). |

## Sand & Sinking Blocks

| Block Name | Hex | RGB | Description |
| :--- | :--- | :--- | :--- |
| **Side Sand** | `#ff6d00` | `255, 109, 0` | Enter from any side. You sink but cannot slide. |
| **Quicksand** | `#ff6c00` | `255, 108, 0` | Sinks faster than normal sand. |
| **Up Sand** | `#ff6f00` | `255, 111, 0` | Enter from bottom, flows upwards. |
| **Magic Sand** | `#ff6e00` | `255, 110, 0` | Flows up if you go up, flows down if you go down. |

## Gravity Blocks

| Block Name | Hex | RGB | Description |
| :--- | :--- | :--- | :--- |
| **High Gravity** | `#800000` | `128, 0, 0` | Gravity is 1.5x stronger. Vertical speed is 0.9x. |
| **Rain Gravity** | `#ffc2c2` | `255, 194, 194` | Gravity is ~1.15x stronger. 28 frames for full jump. |
| **Super Low Gravity** | `#81ffff` | `129, 255, 255` | Gravity is lower than JumpKingPlus low gravity. |
| **Reversed Gravity** | `#21ffff` | `33, 255, 255` | Gravity is reversed when in air and X speed > 0.1. |

## Jump Modifiers

| Block Name | Hex | RGB | Description |
| :--- | :--- | :--- | :--- |
| **Double Jump** | `#e9e9eb` | `233, 233, 235` | Allows a second jump in mid-air. |
| **Cloud Jump** | `#e9e9ea` | `233, 233, 234` | Automatic mid-air jump (like Ghost of the Immortal Babe). |
| **Infinity Jump** | `#40ffff` | `64, 255, 255` | You can jump while standing on this block (Ice/Sand hybrid). |
| **Wall Jump** | `#101010` | `16, 16, 16` | Stand on the wall and jump. Requires 7+ frames charge. |
| **Air Jump** | `#e9e9e8` | `233, 233, 232` | Allows jumping in the air. |
| **Aerial Jump** | `#e9e9ed` | `233, 233, 237` | Another variation of air jump. |
| **Jump Step Hop** | `#e9e9ee` | `233, 233, 238` | Step hop jump behavior. |
| **Disabled Jump** | `#e9e9ec` | `233, 233, 236` | Limits jump to 1 frame only. |
| **Disabled Small Jump** | `#eaeaed` | `234, 234, 237` | Cannot jump with less than 7 frames charge. |
| **Force Neutral Jump** | `#eaeaef` | `234, 234, 239` | Forces the jump to be neutral (no X velocity). |
| **Reversed Charge** | `#212121` | `33, 33, 33` | 1 frame = full jump, Max frames = smallest jump. |
| **Revoke Jump Charge** | `#eaeaee` | `234, 234, 238` | Cancels any current jump charge. |
| **Super Charge** | Range | `50, 48, 0-255` | Blue channel controls charge power. >35 frames is stronger. |
| **Auto Jump Charge** | Range | `233, 233-235, 239` | Automatically charges jump. G controls direction/behavior. |
| **Force Frames Jump** | Range | `1-35, 233, 236` | Forces a specific jump frame amount based on Red channel. |

## Movement & Teleportation

| Block Name | Hex | RGB | Description |
| :--- | :--- | :--- | :--- |
| **Accelerate** | `#b4ff00` | `180, 255, 0` | Charge and move 2x faster. |
| **Speed Up** | `#b5ff00` | `181, 255, 0` | Increases movement speed. |
| **Deep Water** | `#00acac` | `0, 172, 172` | 4x slower than normal (2x slower than water). |
| **Reversed Walk** | `#202020` | `32, 32, 32` | Walking controls are reversed. |
| **Revoke Walking** | `#eaeaf0` | `234, 234, 240` | Disables walking. |
| **Ascend** | `#22ffff` | `34, 255, 255` | Moves the player upwards. |
| **Move Up** | `#23ffff` | `35, 255, 255` | Moves the player upwards (variation). |
| **Conveyor** | Range | `1-30, 30, 30-31` | **Left**: B=30, **Right**: B=31. Red channel = Speed. |
| **Multi Warp** | Range | `1-255, 0-2, 250` | Teleports player. R=Frames, G=Mode. |
| **Quick Move** | Range | `195, 1-90, 255` | Quickly moves the player. G determines speed/distance. |

## Platforms & Walls

| Block Name | Hex | RGB | Description |
| :--- | :--- | :--- | :--- |
| **Reflector Wall** | Range | `182-252, 0, 192` | Reflects the player at the same speed. |
| **Trampoline** | `#c100c1` | `193, 0, 193` | Bounces player back with 0.8x speed. |
| **Side Lock** | Range | `98-99, 6, 6` | Restricts movement to one direction. R=98 (Right), R=99 (Left). |
| **Soft Platform** | `#141414` | `20, 20, 20` | Platform you can jump through from below. |
| **Ceiling Shift** | `#161615` | `22, 22, 21` | Shifts the player when hitting the ceiling. |
| **Ceiling Shift Solid** | `#161616` | `22, 22, 22` | Solid version of Ceiling Shift. |
| **JKQ Platform** | Range | `20, 20, 21-25` | Various platform types (Platform, Walls, Ceil). |
| **Trap Hopping** | Range | `193, 1-4, 193` | Trap hopping mechanics. G controls direction/randomness. |
| **Anti Giant Boots** | `#698f6f` | `105, 143, 111` | Removes Giant Boots effect. |
