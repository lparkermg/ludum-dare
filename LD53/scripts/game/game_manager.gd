extends Node

const tile_id_base = "tile_%s_%s"

# Grid details.
@export var grid_size: int = 25
@export var grid_multiplier: int = 2

# Tile and player stuff.
@export var tile_base: PackedScene
@export var player: Node3D

# Remaining turns.
@export var turns_remaining: int

# Internal tile_id states.
var current_tile_id = ""
var current_delivery_end_id = ""

# Delivery bonus stuff.
@export var max_bonus_turns: int = 10
var bonus_turns: int = 0

var valid_end_tiles = []

# Spawn thresholds.
@export var grasslandThreshold: int = 40
@export var forestThreshold: int = 60
@export var villageThreshold: int = 80
@export var townThreshold:int = 100

# Scoreing
var current_score: int = 0
@export var base_score_per_delivery: int = 100

signal turn_taken
signal delivery_start
# Called when the node enters the scene tree for the first time.
func _ready():
	var tile = preload("res://resources/game/tile.tscn")
	for x in grid_size:
		for y in grid_size:
			var new_tile = tile.instantiate()
			new_tile.name = tile_id_base % [str(x * grid_multiplier), str(y * grid_multiplier)]
			var tile_type = get_tile_type()
			new_tile.initialise(Vector2(x * grid_multiplier, y * grid_multiplier), tile_id_base % [str(x * grid_multiplier), str(y * grid_multiplier)], tile_type)
			if tile_type == TileEnums.Type.TOWN || tile_type == TileEnums.Type.VILLAGE:
				valid_end_tiles.append(new_tile.name)
			turn_taken.connect(new_tile._on_turn_taken)
			delivery_start.connect(new_tile._on_delivery_set)
			add_child(new_tile)
			
	# Player spawn location stuff
	var playerX = range(0, grid_size)[randi()%range(0, grid_size).size()] * grid_multiplier
	var playerZ = range(0, grid_size)[randi()%range(0, grid_size).size()] * grid_multiplier
	player.position.x = playerX
	player.position.z = playerZ
	current_tile_id = tile_id_base % [str(player.position.x), str(player.position.z)]


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if Input.is_action_just_pressed("move_north"):
		try_move(player.position.x, player.position.z + 2)
	elif Input.is_action_just_pressed("move_east"):
		try_move(player.position.x + 2, player.position.z)
	elif Input.is_action_just_pressed("move_south"):
		try_move(player.position.x, player.position.z - 2)
	elif Input.is_action_just_pressed("move_west"):
		try_move(player.position.x - 2, player.position.z)

func try_move(x: int, z: int):
	if turns_remaining <= 0:
		print("Game End: kick off finish process here.")
	elif can_move_here(x, z):
		player.position.x = x
		player.position.z = z
		current_tile_id = tile_id_base % [str(player.position.x), str(player.position.z)]
		take_a_turn()
		
func take_a_turn():
	turns_remaining -= 1
	
	if current_delivery_end_id == "":
		var current_tile = get_node("%s" % current_tile_id)
		if current_tile.state == TileEnums.State.DELIVERY_START:
			randomize()
			var tile_id = valid_end_tiles[randi() % valid_end_tiles.size()]
			current_delivery_end_id = tile_id
			randomize()
			bonus_turns = range(0, max_bonus_turns)[randi()%range(0, max_bonus_turns).size()]
			# ui_manager.show_delivery_ui(bonus_turns)
			delivery_start.emit(current_delivery_end_id, current_tile_id)
	else:
		if current_tile_id == current_delivery_end_id:
			print("We've made a delivery!!")
			# TODO: Reward stuff here.
			if bonus_turns >= 1:
				current_score += base_score_per_delivery * bonus_turns
				turns_remaining += bonus_turns
			else:
				current_score += base_score_per_delivery
			var end_tile = get_node("%s" % current_delivery_end_id)
			end_tile.delivery_success()
			# ui_manager.update_score(current_score)
			current_delivery_end_id = ""
			print("Delivered - New Score: %s - Turns Remaining: %s" % [str(current_score), str(turns_remaining)])
		if bonus_turns > 0:
			bonus_turns -= 1
			#ui_manager.update_delivery_ui(bonus_turns)
	turn_taken.emit()

func can_move_here(x:int, z:int):
	return x >= 0 && x < grid_size * grid_multiplier && z >= 0 && z < grid_size * grid_multiplier
	
func get_tile_type():
	var rn = range(0, 100)[randi()%range(0,100).size()]
	
	if rn < grasslandThreshold:
		return TileEnums.Type.GRASSLAND
	elif rn < forestThreshold:
		return TileEnums.Type.FOREST
	elif rn < villageThreshold:
		return TileEnums.Type.VILLAGE
	else:
		return TileEnums.Type.TOWN
