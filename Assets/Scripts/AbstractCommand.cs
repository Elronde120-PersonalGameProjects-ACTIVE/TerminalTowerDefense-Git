using UnityEngine;
using System.Collections;

public abstract class Command : MonoBehaviour
{
    protected ManPage internalManPage;
     public const string NOT_INITIALIZED = "NOT_INITIALIZED";
    /// <summary>
    /// The string that represents how this command is supposed to be used
    /// </summary>
    /// <returns>The string that shows how this command is supposed to be used</returns>
    public abstract string GetUsage();

    /// <summary>
    /// The string that will cause this command to activate [I.E. "cd" or "man"]
    /// </summary>
    /// <returns>The string that will activate this command</returns>
    public abstract string GetCommandName();

    /// <summary>
    /// Executes the command
    /// </summary>
    /// <param name="args">arguments to pass into the command</param>
    /// /// <returns>Returns true if the command was excecuted successfully, false if any error occurs</returns>
    public abstract IEnumerator Excecute(params string[] args);

    /// <summary>
    /// Geta (or creates) a man page class for this command
    /// </summary>
    /// <returns>A man page for this command</returns>
    public abstract ManPage Man();
}

