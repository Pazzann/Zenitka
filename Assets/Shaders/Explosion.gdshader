shader_type spatial;

render_mode cull_disabled, depth_draw_always, diffuse_toon, specular_toon, blend_mix;

uniform sampler2D displacement_map;
uniform sampler2D alpha_map;
uniform sampler2D emission_map;

uniform float max_displacement : hint_range(0.0, 1.0, 0.05);

uniform vec3 emission_color : source_color;
uniform vec3 albedo_color : source_color;

void vertex() {
	VERTEX += NORMAL * texture(displacement_map, UV + vec2(TIME) * 0.1).r * max_displacement;
}

void fragment() {
	ALPHA = min(round(texture(alpha_map, UV).r * COLOR.a * 3.0), 1.0);
	EMISSION = emission_color * max(0.0, COLOR.a - 0.5) * 5.0 * (texture(emission_map, UV).r + 0.5);
	ALBEDO = albedo_color;
}
