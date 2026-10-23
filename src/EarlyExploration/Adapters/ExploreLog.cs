using System;
using System.IO;
using BepInEx.Logging;

namespace EarlyExploration.Adapters;

/// <summary>
/// Writes to the BepInEx log and to an append-only file that survives restarts
/// (LogOutput.log and Player.log are both overwritten on the next launch).
/// Never throws: it is called from inside Load and the once-a-second check, where an escaping exception would reach the game.
/// </summary>
internal sealed class ExploreLog
{
    private const long TrimAboveBytes = 1024 * 1024;
    private const int KeepBytes = 200 * 1024;

    private readonly ManualLogSource _bepinex;
    private readonly string _path;
    private readonly object _gate = new();

    public ExploreLog(ManualLogSource bepinex, string path)
    {
        _bepinex = bepinex;
        _path = path;
    }

    public void Write(string message) => Write(message, isError: false);

    /// <summary>Logged as an error, so the Mod Manager can mark the mod as maybe needing an update after a game update.</summary>
    public void WriteError(string message) => Write(message, isError: true);

    private void Write(string message, bool isError)
    {
        try
        {
            if (isError) _bepinex.LogError(message);
            else _bepinex.LogInfo(message);
        }
        catch (Exception)
        {
            // A throwing log listener costs the BepInEx copy; the check logs from inside the game's frame.
        }

        lock (_gate)
        {
            try
            {
                File.AppendAllText(_path, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {message}{Environment.NewLine}");
            }
            catch (Exception)
            {
                // A locked or read-only file costs the file copy, never the plugin.
            }
        }
    }

    /// <summary>Lines add up over many sessions, so past 1 MB only the newest ~200 KB of whole lines is kept.</summary>
    public void TrimIfLarge()
    {
        lock (_gate)
        {
            try
            {
                var file = new FileInfo(_path);

                if (!file.Exists || file.Length <= TrimAboveBytes) return;

                var bytes = File.ReadAllBytes(_path);
                var cut = bytes.Length - KeepBytes;
                var nextLine = Array.IndexOf(bytes, (byte)'\n', cut) + 1;

                File.WriteAllBytes(_path, bytes[(nextLine > 0 ? nextLine : cut)..]);
            }
            catch (Exception)
            {
                // An oversized log is harmless; failing to trim it must not stop the plugin loading.
            }
        }
    }
}
