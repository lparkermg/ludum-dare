extends Node
class_name MapController

@export var deity_system: DeitySystem

var model: MapModel

# Called when the node enters the scene tree for the first time.
func _ready():
	model = MapModel.new()
	
	model.current = get_node("./Island")
	model.select_position = Vector2i(0, 0)
	
	deity_system.select_new_tile.connect(move_cursor)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func move_cursor(direction: MoveEnums.Tile):
	model.current.erase_cell(1, model.select_position)
	
	# update position in model here
	if direction == MoveEnums.Tile.Up:
		model.select_position.y = model.select_position.y - 1
	elif direction == MoveEnums.Tile.Right:
		model.select_position.x = model.select_position.x + 1
	elif direction == MoveEnums.Tile.Down:
		model.select_position.y = model.select_position.y + 1
	elif direction == MoveEnums.Tile.Left:
		model.select_position.x = model.select_position.x - 1
	
	model.current.set_cell(1, model.select_position, 1, Vector2i(0, 1))
