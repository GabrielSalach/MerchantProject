using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SavefileManager
{
    public const string pathToSavefile = @"c:/temp/savefile";
    public static FileStream fileStream;

    // Offsets in bytes for the main fields
    public static UInt16 itemSlotsOffset = 0;
    public static UInt16 moneyCountOffset = 400;
    public static UInt16 HeroOffSet = 402;


    // Returns an array of <count> bytes at the given offset
    public static Byte[] ReadSavefile(Int32 offset, Int32 count) {
        Byte[] bytes = new Byte[count];
        fileStream = new FileStream(pathToSavefile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        fileStream.Seek(offset, SeekOrigin.Begin);
        fileStream.Read(bytes, 0, count);
        fileStream.Close();
        return bytes;
    }

    // Writes the array of <count>bytes at the specified offset
    public static void WriteSavefile(Byte[] bytes, Int32 offset, Int32 count) {
        fileStream = new FileStream(pathToSavefile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        fileStream.Seek(offset, SeekOrigin.Begin);
        fileStream.Write(bytes, 0, count);
        fileStream.Close();
    }

}