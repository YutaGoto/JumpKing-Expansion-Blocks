class ExpansionBlocks
  class << self
    attr_accessor :base_color_codes
    attr_accessor :conveyor_speed_color_codes
    attr_accessor :jkq_platform_color_codes
    attr_accessor :multi_warp_color_codes
    attr_accessor :quick_move_color_codes
    attr_accessor :reflector_wall_color_codes
    attr_accessor :side_lock_color_codes
    attr_accessor :super_charge_color_codes
    attr_accessor :trap_hopping_color_codes
    attr_accessor :auto_jump_charge_color_codes

    def base_colors
      self.base_color_codes = []
      File.open("JumpKing-Expansion-Blocks/Constants/ColorCodes.cs", "r") do |file|
        file.each_line do |line|
          if line.include?("public static readonly Color")
            # Extract the color name and RGB values
            if match = line.match(/public static readonly Color (\w+) = new Color\((\d+), (\d+), (\d+)\);/)
              # name = match[1]
              r = match[2].to_i
              g = match[3].to_i
              b = match[4].to_i
              self.base_color_codes << {r: r, g: g, b: b}
            end
          end
        end
      end
      self.base_color_codes
    end

    def conveyor_speed_colors
      self.conveyor_speed_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/ConveyorSpeedCodes.cs")

      if match = content.match(/CONVEYOR_R_MIN = (\d+).*CONVEYOR_R_MAX = (\d+).*CONVEYOR_G = (\d+).*CONVEYOR_B_RIGHT = (\d+).*CONVEYOR_B_LEFT = (\d+)/m)
        r_min, r_max, g, b_right, b_left = match.captures.map(&:to_i)

        (r_min..r_max).each do |r|
          self.conveyor_speed_color_codes << {r: r, g: g, b: b_right}
          self.conveyor_speed_color_codes << {r: r, g: g, b: b_left}
        end
      end

      self.conveyor_speed_color_codes
    end

    def jkq_platform_colors
      self.jkq_platform_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/JKQPlatformsCodes.cs")

      if match = content.match(/PLATFORM = (\d+);.*CEIL = (\d+);.*JKQ_R = (\d+);.*JKQ_G = (\d+);/m)
        b_min, b_max, r, g = match.captures.map(&:to_i)

        (b_min..b_max).each do |b|
          self.jkq_platform_color_codes << {r: r, g: g, b: b}
        end
      else
        puts "No match found"
      end

      self.jkq_platform_color_codes
    end

    def multi_warp_colors
      self.multi_warp_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/MultiWarpColorCodes.cs")

      if match = content.match(/MULTI_WARP_R_MIN = (\d+).*MULTI_WARP_R_MAX = (\d+).*MULTI_WARP_G = (\d+).*MULTI_WARP_B = (\d+)/m)
        r_min, r_max, g, b = match.captures.map(&:to_i)
      end

      (r_min..r_max).each do |r|
        self.multi_warp_color_codes << {r: r, g: g, b: b}
      end

      self.multi_warp_color_codes
    end

    def quick_move_colors
      self.quick_move_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/QuickMoveCodes.cs")

      if match = content.match(/QUICK_MOVE_R = (\d+).*QUICK_MOVE_G_MIN = (\d+).*QUICK_MOVE_G_MAX = (\d+).*QUICK_MOVE_B = (\d+)/m)
        r, g_min, g_max, b = match.captures.map(&:to_i)
      end

      (g_min..g_max).each do |g|
        self.quick_move_color_codes << {r: r, g: g, b: b}
      end

      self.quick_move_color_codes
    end

    def reflector_wall_colors
      self.reflector_wall_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/ReflectorWallCodes.cs")

      if match = content.match(/REFLECTOR_R_MIN = (\d+).*REFLECTOR_R_MAX = (\d+).*REFLECTOR_G = (\d+).*REFLECTOR_B = (\d+)/m)
        r_min, r_max, g, b = match.captures.map(&:to_i)
      end

      (r_min..r_max).each do |r|
        self.reflector_wall_color_codes << {r: r, g: g, b: b}
      end

      self.reflector_wall_color_codes
    end

    def side_lock_colors
      self.side_lock_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/SideLockColorCodes.cs")

      if match = content.match(/SIDE_LOCK_R_RIGHT = (\d+).*SIDE_LOCK_R_LEFT = (\d+).*SIDE_LOCK_G = (\d+).*SIDE_LOCK_B = (\d+)/m)
        r_right, r_left, g, b = match.captures.map(&:to_i)
      end

      (r_right..r_left).each do |r|
        self.side_lock_color_codes << {r: r, g: g, b: b}
      end

      self.side_lock_color_codes
    end

    def super_charge_colors
      self.super_charge_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/SuperChargeCodes.cs")

      if match = content.match(/SUPER_CHARGE_R = (\d+).*SUPER_CHARGE_G = (\d+).*SUPER_CHARGE_B_MIN = (\d+).*SUPER_CHARGE_B_MAX = (\d+)/m)
        r, g, b_min, b_max = match.captures.map(&:to_i)
      end

      (b_min..b_max).each do |b|
        self.super_charge_color_codes << {r: r, g: g, b: b}
      end

      self.super_charge_color_codes
    end

    def trap_hopping_colors
      self.trap_hopping_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/TrapHoppingCodes.cs")

      if match = content.match(/TRAP_HOPPING_R = (\d+).*TRAP_HOPPING_G_RIGHT = (\d+).*TRAP_HOPPING_G_RANDOM = (\d+).*TRAP_HOPPING_B = (\d+)/m)
        r, g_right, g_random, b = match.captures.map(&:to_i)
      end

      (g_right..g_random).each do |g|
        self.trap_hopping_color_codes << {r: r, g: g, b: b}
      end

      self.trap_hopping_color_codes
    end

    def auto_jump_charge_colors
      self.auto_jump_charge_color_codes = []
      content = File.read("JumpKing-Expansion-Blocks/Constants/AutoJumpChargeColorCodes.cs")

      if match = content.match(/AUTO_JUMP_CHARGE_R = (\d+);.*AUTO_JUMP_CHARGE_G_CONTROLLABLE = (\d+);.*AUTO_JUMP_CHARGE_G_RIGHT = (\d+);.*AUTO_JUMP_CHARGE_B = (\d+);/m)
        r, g_controllable, g_right, b = match.captures.map(&:to_i)
      end

      (g_controllable..g_right).each do |g|
        self.auto_jump_charge_color_codes << {r: r, g: g, b: b}
      end

      self.auto_jump_charge_color_codes
    end
  end
end

expansion_blocks_base_colors = ExpansionBlocks.base_colors
expansion_blocks_conveyor_colors = ExpansionBlocks.conveyor_speed_colors
expansion_blocks_jkq_colors = ExpansionBlocks.jkq_platform_colors
expansion_blocks_multi_warp_colors = ExpansionBlocks.multi_warp_colors
expansion_blocks_quick_move_colors = ExpansionBlocks.quick_move_colors
expansion_blocks_reflector_wall_colors = ExpansionBlocks.reflector_wall_colors
expansion_blocks_side_lock_colors = ExpansionBlocks.side_lock_colors
expansion_blocks_super_charge_colors = ExpansionBlocks.super_charge_colors
expansion_blocks_trap_hopping_colors = ExpansionBlocks.trap_hopping_colors
expansion_blocks_auto_jump_charge_colors = ExpansionBlocks.auto_jump_charge_colors

jk_colors = [
  {r: 0, g: 0, b: 0},
  {r: 255, g: 255, b: 255},
  {r: 128, g: 128, b: 128},
  {r: 0, g: 255, b: 255},
  {r: 255, g: 255, b: 0},
  {r: 255, g: 106, b: 0},
  {r: 0, g: 255, b: 0},
  {r: 0, g: 170, b: 170},
  {r: 182, g: 255, b: 0},
]

# RGB(192,255,0) to RGB(239,255,0)
wind_gradient_block_colors = (192..239).map do |r|
  {r: r, g: 255, b: 0}
end

# RGB(1,0,255) to RGB(255,0,255)
teleport_block_colors = (1..255).map do |r|
  {r: r, g: 0, b: 255}
end

# jump king plus
# RGB(65,65,65) to RGB(68,65,65)
one_way_block_colors = (65..68).map do |r|
  {r: r, g: 65, b: 65}
end

# RGB(0,0,75) to RGB(60,45,75)
warp_block_colors = (0..60).map do |r|
  (0..45).map do |g|
    {r: r, g: g, b: 75}
  end
end.flatten

jkp_colors = [
  {r: 128, g: 255, b: 255}
]

switch_block_colors = [
  # Auto
  {r: 238, g: 124, b: 10},
  {r: 238, g: 124, b: 11},
  {r: 238, g: 124, b: 12},
  {r: 10, g: 124, b: 238},
  {r: 11, g: 124, b: 238},
  {r: 12, g: 124, b: 238},
  {r: 238, g: 11, b: 124},
  {r: 238, g: 12, b: 124},
  {r: 238, g: 17, b: 124},

  # Basic
  {r: 238, g: 124, b: 20},
  {r: 238, g: 124, b: 21},
  {r: 238, g: 124, b: 22},
  {r: 20, g: 124, b: 238},
  {r: 21, g: 124, b: 238},
  {r: 22, g: 124, b: 238},
  {r: 238, g: 21, b: 124},
  {r: 238, g: 22, b: 124},
  {r: 238, g: 23, b: 124},
  {r: 238, g: 24, b: 124},
  {r: 238, g: 25, b: 124},
  {r: 238, g: 26, b: 124},
  {r: 238, g: 27, b: 124},

  # Countdown
  {r: 238, g: 124, b: 30},
  {r: 238, g: 124, b: 31},
  {r: 238, g: 124, b: 32},
  {r: 30, g: 124, b: 238},
  {r: 31, g: 124, b: 238},
  {r: 32, g: 124, b: 238},
  {r: 238, g: 31, b: 124},
  {r: 238, g: 34, b: 124},
  {r: 238, g: 37, b: 124},

  # Group
  {r: 238, g: 124, b: 50},
  {r: 238, g: 124, b: 51},
  {r: 238, g: 124, b: 52},
  {r: 50, g: 124, b: 238},
  {r: 51, g: 124, b: 238},
  {r: 52, g: 124, b: 238},
  {r: 124, g: 238, b: 50},
  {r: 124, g: 238, b: 51},
  {r: 124, g: 238, b: 52},
  {r: 50, g: 238, b: 124},
  {r: 51, g: 238, b: 124},
  {r: 52, g: 238, b: 124},
  {r: 238, g: 51, b: 124},
  {r: 238, g: 54, b: 124},

  # Jump
  {r: 31, g: 31, b: 31},
  {r: 31, g: 32, b: 31},
  {r: 31, g: 33, b: 31},
  {r: 95, g: 95, b: 95},
  {r: 95, g: 96, b: 95},
  {r: 95, g: 97, b: 95},
  {r: 95, g: 95, b: 96},

  # Sand
  {r: 238, g: 124, b: 40},
  {r: 40, g: 124, b: 238},
  {r: 238, g: 41, b: 124},
  {r: 238, g: 42, b: 124},
  {r: 238, g: 43, b: 124},
  {r: 238, g: 44, b: 124},
  {r: 238, g: 45, b: 124},
  {r: 238, g: 46, b: 124},

  # Sequence
  {r: 238, g: 124, b: 60},
  {r: 238, g: 124, b: 61},
  {r: 238, g: 124, b: 62},
  {r: 60, g: 124, b: 238},
  {r: 61, g: 124, b: 238},
  {r: 62, g: 124, b: 238},
  {r: 124, g: 238, b: 60},
  {r: 124, g: 238, b: 61},
  {r: 124, g: 238, b: 62},
  {r: 60, g: 238, b: 124},
  {r: 61, g: 238, b: 124},
  {r: 62, g: 238, b: 124},
  {r: 238, g: 61, b: 124},
  {r: 238, g: 64, b: 124},
]


# TrapSand
trap_sand_colors = [
  {r: 255, g: 68, b: 68},
  {r: 255, g: 69, b: 69},
]

# AntiSnakeRing
anti_snake_ring_colors = [
  {r: 105, g: 111, b: 143}
]

# Momentum Stop Blocks
momentum_stop_colors = [
  {r: 111, g: 24, b: 101},
  {r: 111, g: 24, b: 102},
  {r: 111, g: 25, b: 101},
  {r: 111, g: 25, b: 102},
]

# Conveyor
conveyor_colors = (100..101).map do |g|
  (1..16).map do |b|
    {r: 255, g: g, b: b}
  end
end.flatten

# Forced Slope Blocks
forced_slope_colors = [
  {r: 255, g: 1, b: 0},
  {r: 255, g: 2, b: 0},
  {r: 255, g: 0, b: 1},
  {r: 255, g: 0, b: 2},
]

# Upside-Down Block
upside_down_block_colors = [
  {r: 80, g: 80, b: 80}
]

# Checkpoint Block
check_point_block_colors = [
  {r: 1, g: 238, b: 124},
  {r: 5, g: 238, b: 124},
  {r: 2, g: 238, b: 124},
  {r: 3, g: 238, b: 124},
  {r: 6, g: 238, b: 124},
  {r: 4, g: 238, b: 124},
]

all_colors = [
  *jk_colors,
  *wind_gradient_block_colors,
  *teleport_block_colors,
  *one_way_block_colors,
  *warp_block_colors,
  *jkp_colors,
  *switch_block_colors,
  *trap_sand_colors,
  *anti_snake_ring_colors,
  *momentum_stop_colors,
  *conveyor_colors,
  *forced_slope_colors,
  *upside_down_block_colors,
  *check_point_block_colors,
  # Expansion Blocks
  *expansion_blocks_base_colors,
  *expansion_blocks_conveyor_colors,
  *expansion_blocks_jkq_colors,
  *expansion_blocks_multi_warp_colors,
  *expansion_blocks_quick_move_colors,
  *expansion_blocks_reflector_wall_colors,
  *expansion_blocks_side_lock_colors,
  *expansion_blocks_super_charge_colors,
  *expansion_blocks_trap_hopping_colors,
  *expansion_blocks_auto_jump_charge_colors,
]

unless all_colors.uniq.size == all_colors.size
  puts "Error: Duplicate colors in all_colors"
  dups = all_colors.select { |color| all_colors.count(color) > 1 }.uniq

  puts "Duplicate colors are..."
  puts dups.inspect

  puts "\nChecking which arrays contain these colors:"
  dups.each do |dup_color|
    puts "\nColor #{dup_color.inspect} is found in:"
    puts "jk_colors" if jk_colors.include?(dup_color)
    puts "wind_gradient_block_colors" if wind_gradient_block_colors.include?(dup_color)
    puts "teleport_block_colors" if teleport_block_colors.include?(dup_color)
    puts "one_way_block_colors" if one_way_block_colors.include?(dup_color)
    puts "warp_block_colors" if warp_block_colors.include?(dup_color)
    puts "jkp_colors" if jkp_colors.include?(dup_color)
    puts "switch_block_colors" if switch_block_colors.include?(dup_color)
    puts "trap_sand_colors" if trap_sand_colors.include?(dup_color)
    puts "anti_snake_ring_colors" if anti_snake_ring_colors.include?(dup_color)
    puts "momentum_stop_colors" if momentum_stop_colors.include?(dup_color)
    puts "conveyor_colors" if conveyor_colors.include?(dup_color)
    puts "forced_slope_colors" if forced_slope_colors.include?(dup_color)
    puts "upside_down_block_colors" if upside_down_block_colors.include?(dup_color)
    puts "expansion_blocks_base_colors" if expansion_blocks_base_colors.include?(dup_color)
    puts "expansion_blocks_conveyor_colors" if expansion_blocks_conveyor_colors.include?(dup_color)
    puts "expansion_blocks_jkq_colors" if expansion_blocks_jkq_colors.include?(dup_color)
    puts "expansion_blocks_multi_warp_colors" if expansion_blocks_multi_warp_colors.include?(dup_color)
    puts "expansion_blocks_quick_move_colors" if expansion_blocks_quick_move_colors.include?(dup_color)
    puts "expansion_blocks_reflector_wall_colors" if expansion_blocks_reflector_wall_colors.include?(dup_color)
    puts "expansion_blocks_side_lock_colors" if expansion_blocks_side_lock_colors.include?(dup_color)
    puts "expansion_blocks_super_charge_colors" if expansion_blocks_super_charge_colors.include?(dup_color)
    puts "expansion_blocks_trap_hopping_colors" if expansion_blocks_trap_hopping_colors.include?(dup_color)
  end
  exit 1
end
