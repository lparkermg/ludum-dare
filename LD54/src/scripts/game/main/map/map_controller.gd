extends Node
class_name MapController

# The map controller handles everything to do with updating and displaying the actual map.
# Should NOT hold state on the actual gameplay itself.

@export var deity_system: DeitySystem

@export var placements_layer: int = 2
@export var cursor_layer: int = 3

@export var tile_source_id: int = 1

@export var cursor_atlas_position: Vector2i = Vector2i(5, 4)

@export var settlement_atlas_position: Vector2i = Vector2i(2, 4)
@export var wonder_atlas_position: Vector2i = Vector2i(1, 4)

@export var destruction_atlas_position: Vector2i = Vector2i(0, 4)

@export var cursor_boundaries_min: int = 1
@export var cursor_boundaries_max: int = 18

var model: MapModel

signal wonder_placed()
signal settlement_placed()

signal move_complete()
# Called when the node enters the scene tree for the first time.
func _ready():
	model = MapModel.new()
	
	model.current = get_node("./Island")
	
	model.select_position = Vector2i(2, 2)
	model.current.set_cell(cursor_layer, model.select_position, tile_source_id, cursor_atlas_position)
	
	deity_system.select_new_tile.connect(move_cursor)
	deity_system.wonder_placed_map.connect(_place_wonder)
	deity_system.settlement_placed_map.connect(_place_settlement)
	deity_system.disaster_settlements_destroyed_map.connect(_destory_settlements)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func move_cursor(direction: MoveEnums.Tile):
	var old_position = model.select_position
	var changed = false
	
	# update position in model here
	if direction == MoveEnums.Tile.Up && model.select_position.y - 1 > cursor_boundaries_min:
		model.select_position.y = model.select_position.y - 1
		changed = true
	elif direction == MoveEnums.Tile.Right && model.select_position.x + 1 < cursor_boundaries_max:
		model.select_position.x = model.select_position.x + 1
		changed = true
	elif direction == MoveEnums.Tile.Down && model.select_position.y + 1 < cursor_boundaries_max:
		model.select_position.y = model.select_position.y + 1
		changed = true
	elif direction == MoveEnums.Tile.Left && model.select_position.x - 1 > cursor_boundaries_min:
		model.select_position.x = model.select_position.x - 1
		changed = true
	
	if changed:
		model.current.erase_cell(cursor_layer, old_position)
		model.current.set_cell(cursor_layer, model.select_position, tile_source_id, cursor_atlas_position)

		move_complete.emit()

func get_select_position() -> Vector2i:
	return model.select_position

func can_place_at_position() -> bool:
	var data = model.current.get_cell_tile_data(0, model.select_position)
	return data.get_custom_data("can_place")

func _place_wonder():	
	var data = model.current.get_cell_tile_data(0, model.select_position)
	
	model.current.set_cell(placements_layer, model.select_position, tile_source_id, wonder_atlas_position)
	wonder_placed.emit()
	
func _place_settlement():
	model.current.set_cell(placements_layer, model.select_position, tile_source_id, settlement_atlas_position)
	settlement_placed.emit()
	
func _destory_settlements(location: Vector2i):
	model.current.erase_cell(placements_layer, location)
	model.current.set_cell(placements_layer, location, tile_source_id, destruction_atlas_position)
