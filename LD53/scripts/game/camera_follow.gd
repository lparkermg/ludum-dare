extends Camera3D

@export var position_node: Node3D
@export var player_node: Node3D

@export var lerp_speed = 5.0

# Called when the node enters the scene tree for the first time.
func _ready():
	self.position = position_node.global_position
	look_at(player_node.position, player_node.transform.basis.y)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	self.position = lerp(self.position, position_node.global_position, lerp_speed * delta)
	
