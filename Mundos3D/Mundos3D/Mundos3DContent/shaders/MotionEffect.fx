float4x4 World;
float4x4 View;
float4x4 Projection;
Texture flagTexture;
float time;


float WaveAmplitude;
float WaveFrequency;
float WaveSpeed;
float2 WaveDirection;

sampler flagTextureSampler = sampler_state
{
	texture = <flagTexture>;
	magfilter = LINEAR;
	minfilter = LINEAR;
	mipfilter = LINEAR;
};


struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};


VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float wave =
        sin(
            dot(input.Position.yz, WaveDirection) * WaveFrequency
            + time * WaveSpeed
        );

    input.Position.x += wave * WaveAmplitude;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;

    return output;
}


float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 output = tex2D(flagTextureSampler, input.TexCoord);

    return output;
}


technique Technique1
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
