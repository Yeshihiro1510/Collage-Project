using System;
using UnityEngine;

public static class TextureUtil
{
    private static Sprite[] _cachedSprites;
    private static string _cachedTexturePath;

    public static Sprite GetSpriteFromTexture(this string texturePath, string spriteName)
    {
        if (_cachedTexturePath != texturePath)
        {
            _cachedTexturePath = texturePath;
            _cachedSprites = Resources.LoadAll<Sprite>(texturePath);
        }

        foreach (var s in _cachedSprites)
            if (s.name == spriteName)
                return s;

        throw new Exception($"Invalid sprite name: {spriteName} or texture path: {texturePath}.");
    }
}