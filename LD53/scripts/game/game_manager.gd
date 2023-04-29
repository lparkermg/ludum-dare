extends Node

const tile_id_base = "tile_%s_%s"

@export var grid_size: int = 25
@export var grid_multiplier: int = 2

@export var tile_base: PackedScene
@export var player: Node3D

@export var turns_remaining: int

var current_tile_id = ""
var current_delivery_end_id = ""
var turns_until_delivery_invalid = 0

signal turn_taken
# Called when the node enters the scene tree for the first time.
func _ready():
	var tile = preload("res://resources/game/tile.tscn")
	for x in grid_size:
		for y in grid_size:
			var new_tile = tile.instantiate()
			new_tile.name = tile_id_base % [str(x * grid_multiplier), str(y * grid_multiplier)]
			new_tile.initialise(Vector2(x * grid_multiplier, y * grid_multiplier), tile_id_base % [str(x * grid_multiplier), str(y * grid_multiplier)])
			turn_taken.connect(new_tile._on_turn_taken)
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
		if current_tile.has_delivery_start:
			current_delivery_end_id = tile_id_base % ["0", "0"] # TODO: Update with valid tile.
			randomize()
			turns_until_delivery_invalid = range(1, 10)[randi()%range(1,10).size()]
			print("DEBUG: Tile ID - %s | Turns Remaining - %s" % [current_tile_id, str(turns_until_delivery_invalid)])
		# if it is, select a valid end point and set the id in this script.
		# TODO Later: flag on and off the display bits on the tile.
	else:
		if current_tile_id == current_delivery_end_id:
			print("We've made a delivery!!")
			# TODO: Reward stuff here.
			current_delivery_end_id = ""
			turns_until_delivery_invalid = 0
		elif turns_until_delivery_invalid > 0:
			turns_until_delivery_invalid -= 1
		else:
			print("Delivery invalidated")
			var end_tile = get_node("%s" % current_delivery_end_id)
			# TODO Clear anything on the tile. Plus punishments?
			current_delivery_end_id = ""
	turn_taken.emit()

func can_move_here(x:int, z:int):
	return x >= 0 && x < grid_size * grid_multiplier && z >= 0 && z < grid_size * grid_multiplier
