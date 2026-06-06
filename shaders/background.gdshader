shader_type canvas_item;

uniform float speed = 0.5;
uniform float scale = 5.0;
uniform vec3 color1 : source_color = vec3(0.05, 0.1, 0.25);
uniform vec3 color2 : source_color = vec3(0.15, 0.05, 0.3);
uniform vec3 color3 : source_color = vec3(0.02, 0.05, 0.15);

float hash(vec2 p) {
    return fract(sin(dot(p, vec2(127.1, 311.7))) * 43758.5453);
}

float noise(vec2 p) {
    vec2 i = floor(p);
    vec2 f = fract(p);
    vec2 u = f * f * (3.0 - 2.0 * f);
    return mix(mix(hash(i + vec2(0.0, 0.0)), hash(i + vec2(1.0, 0.0)), u.x),
               mix(hash(i + vec2(0.0, 1.0)), hash(i + vec2(1.0, 1.0)), u.x), u.y);
}

void fragment() {
    vec2 uv = UV * scale;
    uv.x += TIME * speed * 0.5;
    uv.y += TIME * speed * 0.3;
    float n = noise(uv);
    n += noise(uv * 2.0) * 0.5;
    n += noise(uv * 4.0) * 0.25;
    n = clamp(n, 0.0, 1.0);
    vec3 bgColor = mix(color1, color2, n);
    bgColor = mix(bgColor, color3, smoothstep(0.6, 0.9, n));
    
    COLOR = vec4(bgColor, 1.0);
}
