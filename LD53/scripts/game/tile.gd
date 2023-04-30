extends Node3D

# Script for the individual tiles.

@export var tile_position: Vector2

@export var grassland_model: PackedScene
@export var forest_model: PackedScene
@export var village_model: PackedScene
@export var town_model: PackedScene

@export var delivery_start_model: PackedScene
@export var delivery_end_model: PackedScene

var tile_id = ""
var tile_type: TileEnums.Type = TileEnums.Type.GRASSLAND

var state_for: int
var state: TileEnums.State = TileEnums.State.NO_DELIVERY

# Called when the node enters the scene tree for the first time.
func _ready():
	randomize()
	state_for = range(5, 100)[randi()%range(5,100).size()]


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
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
		display.add_child(village_model.instantiate())
	elif tile_type == TileEnums.Type.TOWN:
		display.add_child(town_model.instantiate())

# It should be possible to cancel the delivery.
func delivery_invalidated():
	var alert_area = get_node("DisplayArea/AlertArea")
	for n in alert_area.get_children():
		alert_area.remove_child(n)
	state = TileEnums.State.NO_DELIVERY

# While the same as the above, we can expand this if needed.
func delivery_success():
	var alert_area = get_node("DisplayArea/AlertArea")
	for n in alert_area.get_children():
		alert_area.remove_child(n)
	state = TileEnums.State.NO_DELIVERY

func _on_turn_taken():
	# If we're on delivery end then we handle that elsewhere...
	if state == TileEnums.State.DELIVERY_END:
		pass
	# Only villages and towns can spawn deliveries.
	if tile_type == TileEnums.Type.VILLAGE || tile_type == TileEnums.Type.TOWN:
		state_for -= 1
		if state_for <= 0:
			randomize()
			state_for = range(1, 100)[randi()%range(1,100).size()]
			
			# Maybe change this to be more based on chance than just alternating.
			if state == TileEnums.State.NO_DELIVERY:
				state = TileEnums.State.DELIVERY_START
			elif state == TileEnums.State.DELIVERY_START:
				state = TileEnums.State.NO_DELIVERY
			var alert_area = get_node("DisplayArea/AlertArea")
			if state == TileEnums.State.DELIVERY_START:
				alert_area.add_child(delivery_start_model.instantiate())
			else:
				for n in alert_area.get_children():
					alert_area.remove_child(n)

func _on_delivery_set(expected_end_tile_id: String, expected_start_tile_id: String):
	if expected_start_tile_id == tile_id:
		var alert_area = get_node("DisplayArea/AlertArea")
		for n in alert_area.get_children():
			alert_area.remove_child(n)
		state_for = 15 #We should wait for 15 turns to revert the state so spamming cannot happen.
	
	if expected_end_tile_id == tile_id:
		var alert_area = get_node("DisplayArea/AlertArea")
		for n in alert_area.get_children():
			alert_area.remove_child(n)
		alert_area.add_child(delivery_end_model.instantiate())
		state = TileEnums.State.DELIVERY_END
