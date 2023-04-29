extends Node

@export var grid_size: int = 25
@export var grid_multiplier: int = 2

@export var tile_base: PackedScene

# Called when the node enters the scene tree for the first time.
func _ready():
	var tile = preload("res://resources/game/tile.tscn")
	for x in grid_size:
		for y in grid_size:
			var new_tile = tile.instantiate()
			new_tile.initialise(Vector2(x * grid_multiplier, y * grid_multiplier))
			add_child(new_tile)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
