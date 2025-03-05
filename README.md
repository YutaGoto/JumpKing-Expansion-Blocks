# JumpKing Expansion Blocks Mod

![Jump King Mod Badge](https://img.shields.io/badge/Jump_King-Mod-61CE70?style=flat-square&labelColor=BA1313)

![thumbnail](https://steamuserimages-a.akamaihd.net/ugc/5838428307081072717/1CA52980181FF52EF1122EB3169F0AA583BB648B/)

https://steamcommunity.com/sharedfiles/filedetails/?id=3214349391

This mod adds custom blocks to Jump King.

## Ice

### SlipperyIce

`#00c0ff` `RGB(0, 192, 255)`

3 times slippery than normal ice.

- It is solid.
- You can walk on it.
- You will slide on it no matter what.

### ZeroFriction

`#00c1ff` `RGB(0, 193, 255)`

No friction at all.

- It is solid.
- You can walk on it.
- You will slide on it no matter what.

### CursedIce

`#909000` `RGB(144, 144, 0)`

No walk, ice platform and reverse jump.

- It is solid.
- You can't walk on it.
- You will slide on it no matter what.

### RestrainedIce

`#808000` `RGB(128, 128, 0)`

No walk and ice platform. Like wearing a Giant Boots.

- It is solid.
- You can't walk on it.
- You will slide on it no matter what.

### OneWay Ice

`#4182ff` `RGB(65, 130, 255)`

The semi-solid block is a Ice block that is solid only in one side.

- It is solid. (only one side)
- You can walk on it.
- You will slide on it no matter what.

### Heavy Ice

`#00ffc8` `RGB(0, 255, 200)`

Ice platform but you can't do small jump like Snow block.

- It is solid.
- You can walk on it.
- You will slide on it no matter what.

## Sand

### SideSand

`#ff6d00` `RGB(255, 109, 0)`

You are able to enter from any side (top, left, bottom, right).

- It is solid.
- You can walk on it.
- You can't slide on it.
- You will sink down.

### Quicksand

`#ff6c00` `RGB(255, 108, 0)`

Sinking speed is faster than normal sand.

- It is solid.
- You can walk on it.
- You can't slide on it.
- You will sink down.

### UpSand

`#ff6f00` `RGB(255, 111, 0)`

You are able to enter from the bottom. flow up.

- It is solid.
- You can't walk in it. You can walk on it.
- You can't slide in it. You can slide on it.
- You will flow up.

### MagicSand

`#ff6e00` `RGB(255, 110, 0)`

You are able to enter from any side. If it go up, it will flow up. If it go down, it will flow down.

- It is solid.
- If you sink down, you can walk on it. If you flow up, you can't walk on it.
- You can't slide in it.
- You will flow up or down.

## Jump, Moving

### Accelerate

`#b4ff00` `RGB(180, 255, 0)`

Charge speed and moving are 2 times faster than normal.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- It affects your direction (velocity, gravity and jump charge).

### DeepWater

`#00acac` `RGB(0, 172, 172)`

Charge speed and moving are 2 times slower than water block (4 times slower than normal).

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- It affects your direction (velocity, gravity and jump charge).

### ReversedCharge

`#212121` `RGB(33, 33, 33)`

1 frame charge then full jump. Maximum frame charge then the smallest jump.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- It affects your jump charge.

### SuperCharge

`#3230XX` `RGB(50, 48, XX)`

be able to charge than normal. Blue channel is charge power.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- 1 - 35 frames charge is normal. over 35 frames charge is stronger.
- Blue channel would be from 2 to 5.
- A higher value will allow you to pass through solid blocks.
- Setting it above 110 may cause the game to crash.

### Reversed Gravity

`#21ffff` `RGB(33, 255, 255)`

Gravity is reversed. It effects when you are in the air and X speed is more than `0.1` (Neutral Jumping is not affected).

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- Gravity is reversed.

### Disabled Jump

`#e9e9ec` `RGB(233, 233, 236)`

You can do only 1 frame jump.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- You can do only 1 frame jump.

### Disabled Small Jump

`#eaeaed` `RGB(234, 234, 237)`

You can't jump under 7 frames charging.

- It isn't solid.
- You can't walk on it.
- You can't slide on it.
- You can jump only 7 frames or more charging.

## Other

### Multi Side Teleport block

- `#0100fa` `RGB(1, 0, 250)` to `#a900fa` `RGB(169, 0, 250)`

The teleport block has a range of values that goes from 1 to 169. The value is the number of frames that the player will be teleported to the next block.

You must put the side. (the block checks that the player is on the side of the block)

- It is not solid.
- You can walk on it.
- You can't slide on it.

### Conveyor

- left: `#011e1e` `RGB(1, 30, 30)` to `#1e1e1e` `RGB(30, 30, 30)`
- right: `#011e1f` `RGB(1, 30, 31)` to `#1e1e1f` `RGB(30, 30, 31)`

It acts like a conveyor belt.

The blue channel is direction. The red channel is speed.

- It is solid.
- You can walk on it.
- You can't slide on it.

### DoubleJump

`#e9e9eb` `RGB(233, 233, 235)`

You can jump again while in the air.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.

### Cloud Jump

`#e9e9ea` `RGB(233, 233, 234)`

same effect from Ghost of the Immortal Babe.

You can jump again with automatic while in the air.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.

### HighGravity

`#800000` `RGB(128, 0, 0)`

The gravity is 1.5 times stronger than normal. Vertical speed is 0.9 times than normal.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- It affects your direction (velocity, gravity and jump curve).

### InfinityJump

`#40ffff` `RGB(64, 255, 255)`

You can jump while on the block.

note: this is ice and sand.

- It is solid.
- You can walk on it.
- You can slide on it.

### RainGravity

`#ffc2c2` `RGB(255, 194, 194)`

same high gravity from Ghost of the Immortal Babe.

The gravity is about 1.15 times stronger than normal. 28 frames charge is full jump. and it is adjusted horizontal speed.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- It affects your direction (velocity, gravity, jump charge and jump curve).

### Reflector Wall

`#c000c0` `RGB(192, 0, 192)`

You knock it, it will reflect you same speed.

- It is solid.
- You can walk on it.
- You can't slide on it.

### Trampoline

`#c100c1` `RGB(193, 0, 193)`

You bounce back with 0.8x speed. when your Y-velocity is very small, you can stand on it.

- It is solid.
- You can walk on it.
- You can't slide on it.

### ReversedWalk

`#202020` `RGB(32, 32, 32)`

You can walk backward.

problem: you can't hug the wall and slope. you through these.

- It is solid.
- You can walk on it.
- You can't slide on it.

### SpecialHighGravity

`#ffbebe` `RGB(255, 190, 190)`

28 frames charge is full jump. And you can jump almost same height between 22 frames charge and 28 frames charge.

- It isn't solid.
- You can’t walk on it.
- You can’t slide on it.
- It affects your direction (velocity, gravity, jump charge and jump curve).

### WallJump

`#101010` `RGB(16, 16, 16)`

You can stand on the wall and jump from it. You needs at least 7 frames to jump from this block.

note: this is snow and sand.

- It is solid.
- You can't walk on it.
- You can't slide on it no matter what.
