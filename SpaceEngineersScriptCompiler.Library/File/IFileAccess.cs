﻿namespace SpaceEngineersScriptCompiler.Library.File
{
    public interface IFileAccess
    {
        bool Exists(string filePath);

        string ReadAllText(string filePath);
    }
}