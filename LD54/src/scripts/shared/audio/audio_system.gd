extends Node
class_name AudioSystem

# Main Sounds
@export var bad_sound: AudioStreamWAV
@export var cursor_move_sound: AudioStreamWAV

# Placement Sounds
@export var wonder_place_sound: AudioStreamWAV
@export var settlement_place_sound: AudioStreamWAV

# Disaster Sounds
@export var earthquake_sound: AudioStreamWAV
@export var fire_sound: AudioStreamWAV
@export var flood_sound: AudioStreamWAV
@export var lightning_strike_sound: AudioStreamWAV

@export var sfx_player: AudioStreamPlayer
@export var move_sfx_player: AudioStreamPlayer
@export var bgm_player: AudioStreamPlayer

signal sfx_complete()
# Called when the node enters the scene tree for the first time.
func _ready():
	sfx_player.finished.connect(func(): sfx_complete.emit())


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func play_disaster_sound(disaster: DisasterEnums.Disaster):
	sfx_player.stop()
	if disaster == DisasterEnums.Disaster.Earthquake:
		sfx_player.stream = earthquake_sound
		sfx_player.play()
	elif disaster == DisasterEnums.Disaster.Flood:
		sfx_player.stream = flood_sound
		sfx_player.play()
	elif disaster == DisasterEnums.Disaster.Fire:
		sfx_player.stream = fire_sound
		sfx_player.play()
	elif disaster == DisasterEnums.Disaster.Lightning:
		sfx_player.stream = lightning_strike_sound
		sfx_player.play()

func play_error_sound():
	sfx_player.stop()
	sfx_player.stream = bad_sound
	sfx_player.play()

func play_cursor_move():
	move_sfx_player.stop()
	move_sfx_player.stream = cursor_move_sound
	move_sfx_player.play()

func play_placement(is_wonder: bool):
	sfx_player.stop()
	if is_wonder:
		sfx_player.stream = wonder_place_sound
	else:
		sfx_player.stream = settlement_place_sound
	sfx_player.play()
