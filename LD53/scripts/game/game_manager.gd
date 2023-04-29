extends Node

var tile_id_base = "tile_%s_%s"

@export var grid_size: int = 25
@export var grid_multiplier: int = 2

@export var tile_base: PackedScene
@export var player: Node3D

@export var turns_remaining: int

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


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
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
		take_a_turn()
		
func take_a_turn():
	turns_remaining -= 1
	turn_taken.emit()

func can_move_here(x:int, z:int):
	return x >= 0 && x < grid_size * grid_multiplier && z >= 0 && z < grid_size * grid_multiplier
