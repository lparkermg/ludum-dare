extends Node3D

# Script for the individual tiles.

@export var tile_position: Vector2

@export var grassland_model: PackedScene
@export var forest_model: PackedScene
@export var village_model: PackedScene
@export var town_model: PackedScene

var tile_id = ""
var tile_type: TileEnums.Type = TileEnums.Type.GRASSLAND

var state_for: int
var has_delivery_start: bool = false

# Called when the node enters the scene tree for the first time.
func _ready():
	randomize()
	state_for = range(5, 100)[randi()%range(5,100).size()]


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
func initialise(pos: Vector2, id: String, type: TileEnums.Type):
	tile_id = id

	tile_position = pos
	position.x = tile_position.x
	position.z = tile_position.y
	
	tile_type = type
	var display = get_node("DisplayArea")
	if tile_type == TileEnums.Type.GRASSLAND:
		display.add_child(grassland_model.instantiate())
	elif tile_type == TileEnums.Type.FOREST:
		display.add_child(forest_model.instantiate())
	elif tile_type == TileEnums.Type.VILLAGE:
		display.add_child(grassland_model.instantiate())
	elif tile_type == TileEnums.Type.TOWN:
		display.add_child(forest_model.instantiate())
	
func _on_turn_taken():
	state_for -= 1
	if state_for <= 0:
		randomize()
		state_for = range(1, 100)[randi()%range(1,100).size()]
		has_delivery_start = !has_delivery_start
		print("delivery state changed for %s" % self.name)

func _on_delivery_set(expected_tile_id: String):
	if expected_tile_id == tile_id:
		print("Delivery has been set to this tile (%s) TODO: Update this code with display stuff etc.")
