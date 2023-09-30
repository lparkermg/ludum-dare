extends Node
class_name UiController

# The UI Controller handles everything to do with updating and displaying the UI.

@export var deity_system: DeitySystem

@export var place_wonder_button: BaseButton
@export var place_settlement_button: BaseButton

var model: UiModel

signal place_wonder_clicked()
signal place_settlement_clicked()
# Called when the node enters the scene tree for the first time.
func _ready():
	
	deity_system.wonder_placed_ui.connect(_wonder_placed)
	deity_system.settlement_placed_ui.connect(_settlement_placed)
	
	model = UiModel.new()
	
	model.worship_amount = 0
	model.worship_points = 5
	model.settlements = 0
	
	model.max_population = 0
	model.current_population = 0
	
	model.current_deity_points = 0
	
	model.can_place_settlement = false
	model.can_place_wonder = true
	
	#UI Signals
	place_wonder_button.pressed.connect(func(): place_wonder_clicked.emit())
	place_settlement_button.pressed.connect(func(): place_settlement_clicked.emit())
	
	_update_view(model)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func update_action_states(can_place_wonder: bool, can_place_settlement: bool):
	model.can_place_settlement = can_place_settlement
	model.can_place_wonder = can_place_wonder
	
	_update_view(model)

# Applies the provided ui model to the UI (our view in this case
func _update_view(ui_model: UiModel):
	place_wonder_button.disabled = !ui_model.can_place_wonder
	place_settlement_button.disabled = !ui_model.can_place_settlement
	
func _wonder_placed():
	print("wonder placed LETSGO")
	update_action_states(false, true)
	
func _settlement_placed(settlement_amount: int, worship_points_amount: int):
	model.settlements = settlement_amount
	model.worship_points = worship_points_amount
	_update_view(model)
