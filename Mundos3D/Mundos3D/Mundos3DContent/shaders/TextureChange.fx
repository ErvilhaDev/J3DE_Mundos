float4x4 World;
float4x4 View;
float4x4 Projection;

Texture2D texture1;
Texture2D texture2;

float BlendAmount;

sampler2D textureSampler1 = sampler_state
{
    Texture = <texture1>;
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
};

sampler2D textureSampler2 = sampler_state
{
    Texture = <texture2>;
    MinFilter = Linear;
    MagFilter = Linear;
    MipFilter = Linear;
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

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.TexCoord = input.TexCoord;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float4 c1 = tex2D(textureSampler1, input.TexCoord);
    float4 c2 = tex2D(textureSampler2, input.TexCoord);

    return lerp(c1, c2, BlendAmount);
}

technique Technique1
{
    pass P0
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader  = compile ps_2_0 PixelShaderFunction();
    }
}
