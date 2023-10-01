extends Node
class_name UiController

# The UI Controller handles everything to do with updating and displaying the UI.

@export var deity_system: DeitySystem

# Help stuff
@export var help_display: Panel
@export var start_game_button: BaseButton

# Action buttons
@export var place_wonder_button: BaseButton
@export var place_settlement_button: BaseButton
@export var activate_lightning_disaster: BaseButton
@export var activate_flood_disaster: BaseButton
@export var activate_earthquake_disaster: BaseButton
@export var activate_fire_disaster: BaseButton

# Timers
@export var disaster_disable_timer: Timer

# Stats display UI
@export var ascension_progress: ProgressBar

@export var settlements_label: Label
@export var settlers_label: Label
@export var worship_label: Label
@export var deity_points_label: Label

var model: UiModel

signal place_wonder_clicked()
signal place_settlement_clicked()

signal activate_lightning_disaster_clicked()
signal activate_flood_disaster_clicked()
signal activate_earthquake_disaster_clicked()
signal activate_fire_disaster_clicked()
# Called when the node enters the scene tree for the first time.
func _ready():
	
	deity_system.wonder_placed_ui.connect(_wonder_placed)
	deity_system.settlement_placed_ui.connect(_settlement_placed)
	deity_system.settlers_arrived_ui.connect(_settlers_arrived)
	deity_system.worship_level_changed_ui.connect(_worship_level_changed)
	deity_system.deity_points_changed_ui.connect(_deity_points_changed)
	deity_system.disaster_completed_ui.connect(_disable_disaster_buttons)
	deity_system.settlement_destroyed_ui.connect(_settlement_destroyed)
	deity_system.ascension_level_increased_ui.connect(_ascension_changed)
	
	model = UiModel.new()
	
	model.worship_amount = 0
	model.deity_points = 10
	model.settlements = 0
	
	model.max_settlers = 0
	model.current_settlers = 0
	
	model.can_place_settlement = false
	model.can_place_wonder = false
	model.showing_help = true
	
	#UI Signals
	place_wonder_button.pressed.connect(func(): place_wonder_clicked.emit())
	place_settlement_button.pressed.connect(func(): place_settlement_clicked.emit())
	
	activate_lightning_disaster.pressed.connect(func(): activate_lightning_disaster_clicked.emit())
	activate_flood_disaster.pressed.connect(func(): activate_flood_disaster_clicked.emit())
	activate_earthquake_disaster.pressed.connect(func(): activate_earthquake_disaster_clicked.emit())
	activate_fire_disaster.pressed.connect(func(): activate_fire_disaster_clicked.emit())
	
	disaster_disable_timer.timeout.connect(_enable_disaster_buttons)
	
	start_game_button.pressed.connect(_start_game)
	
	_update_view(model)

# Applies the provided ui model to the UI (our view in this case
func _update_view(ui_model: UiModel):
	help_display.visible = ui_model.showing_help
	
	place_wonder_button.disabled = !ui_model.can_place_wonder
	place_settlement_button.disabled = !ui_model.can_place_settlement
	
	activate_lightning_disaster.disabled = !ui_model.can_perform_actions
	activate_earthquake_disaster.disabled = !ui_model.can_perform_actions
	activate_flood_disaster.disabled = !ui_model.can_perform_actions
	activate_fire_disaster.disabled = !ui_model.can_perform_actions
	
	settlements_label.text = "%s" % str(ui_model.settlements)
	settlers_label.text = "%s/%s" % [str(ui_model.current_settlers), str(ui_model.max_settlers)]
	worship_label.text = "%s" % str(ui_model.worship_amount)
	deity_points_label.text = "%s" % str(ui_model.deity_points)
	
	ascension_progress.value = ui_model.ascension_level

func update_action_states(can_place_wonder: bool, can_place_settlement: bool, can_perform_disasters: bool):
	model.can_place_settlement = can_place_settlement
	model.can_place_wonder = can_place_wonder
	model.can_perform_actions = can_perform_disasters
	_update_view(model)
	
func _wonder_placed():
	update_action_states(false, true, false)
	
func _settlement_placed(settlement_amount: int, new_max_settlers: int):
	model.settlements = settlement_amount
	model.max_settlers = new_max_settlers
	
	if settlement_amount == 1:
		model.can_perform_actions = true
	_update_view(model)
	
func _settlement_destroyed(settlement_amount: int, new_max_settlers: int):
	model.settlements = settlement_amount
	model.max_settlers = new_max_settlers
	_update_view(model)
	
func _settlers_arrived(new_settler_amount: int):
	model.current_settlers = new_settler_amount
	
	_update_view(model)
	
func _worship_level_changed(new_level: int):
	model.worship_amount = new_level
	
	_update_view(model)
	
func _deity_points_changed(new_amount: int):
	model.deity_points = new_amount

	_update_view(model)
	
func _ascension_changed(new_amount: int):
	model.ascension_level = new_amount
	
	_update_view(model)
	
func _disable_disaster_buttons():
	print("Disaster buttons disabled.")
	model.can_perform_actions = false
	
	_update_view(model)
	disaster_disable_timer.start(2.5)

func _enable_disaster_buttons():
	print("Enabling disaster buttons")
	model.can_perform_actions = true
	
	_update_view(model)
	disaster_disable_timer.stop()

func _start_game():
	model.showing_help = false
	model.can_place_wonder = true
	
	_update_view(model)
