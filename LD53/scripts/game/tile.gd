extends Node3D

# Script for the individual tiles.

@export var tile_position: Vector2

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func initialise(pos: Vector2):
	print("Initialising Tile")
	print(pos)
	tile_position = pos
	position.x = tile_position.x
	position.z = tile_position.y
