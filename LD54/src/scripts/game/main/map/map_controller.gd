extends Node
class_name MapController

# The map controller handles everything to do with updating and displaying the actual map.
# Should NOT hold state on the actual gameplay itself.

@export var deity_system: DeitySystem

var model: MapModel

signal wonder_placed()
signal settlement_placed()
# Called when the node enters the scene tree for the first time.
func _ready():
	model = MapModel.new()
	
	model.current = get_node("./Island")
	model.select_position = Vector2i(0, 0)
	
	deity_system.select_new_tile.connect(move_cursor)
	deity_system.wonder_placed_map.connect(_place_wonder)
	deity_system.settlement_placed_map.connect(_place_settlement)
	deity_system.disaster_settlements_destroyed_map.connect(_destory_settlements)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func move_cursor(direction: MoveEnums.Tile):
	model.current.erase_cell(2, model.select_position)
	
	# update position in model here
	if direction == MoveEnums.Tile.Up:
		model.select_position.y = model.select_position.y - 1
	elif direction == MoveEnums.Tile.Right:
		model.select_position.x = model.select_position.x + 1
	elif direction == MoveEnums.Tile.Down:
		model.select_position.y = model.select_position.y + 1
	elif direction == MoveEnums.Tile.Left:
		model.select_position.x = model.select_position.x - 1
	
	model.current.set_cell(2, model.select_position, 1, Vector2i(0, 1))

func get_select_position() -> Vector2i:
	return model.select_position

func _place_wonder():
	model.current.set_cell(1, model.select_position, 1, Vector2i(1, 1))
	wonder_placed.emit()
	
func _place_settlement():
	model.current.set_cell(1, model.select_position, 1, Vector2i(0, 2))
	settlement_placed.emit()
	
func _destory_settlements(location: Vector2i):
	print("map controller - destroying settlements")
	model.current.erase_cell(1, location)
		# Probably should add some kind of mark to show something there?
