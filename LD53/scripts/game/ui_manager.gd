extends CanvasLayer


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	pass
	
func update_score_ui(current_score: int):
	var score_label = get_node("ScorePanel/ScoreLabel")
	score_label.text = "Score: %s" % str(current_score)
	
func update_turns_ui(turns_left: int):
	var turns_label = get_node("TurnsPanel/TurnsLabel")
	turns_label.text = "Turns: %s" % str(turns_left)

func show_delivery_panel(bonus_turns: int):
	var delivery_panel = get_node("DeliveryPanel")
	delivery_panel.visible = true
	var delivery_label = get_node("DeliveryPanel/BonusTurnsLabel")
	delivery_label.text = "Bonus Turns: %s" % str(bonus_turns)
	
func update_delivery_panel(bonus_turns: int):
	var delivery_label = get_node("DeliveryPanel/BonusTurnsLabel")
	delivery_label.text = "Bonus Turns: %s" % str(bonus_turns)

func hide_delivery_panel():
	var delivery_panel = get_node("DeliveryPanel")
	delivery_panel.visible = false
